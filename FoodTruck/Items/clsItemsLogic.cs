using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;



namespace FoodTruck.Items
{
    class clsItemsLogic
    {
        ////Used to access the database
        private static DataAccess db = new DataAccess();

        ////holds info in the databse
        //DataSet ds;

        ////Tells if user is deleting a row
        //bool IsDeleting = false;

        //public static List<Items> GetAllItems()
        //  { 
        //  try
        //      {
        //          //Create a DataSet to hold the data
        //          DataSet ds;

        //          //Number of return values
        //          int iRet = 0;

        //          //Execute the statement and get the data
        //          ds = db.ExecuteSQLStatement(txtQuery.Text, ref iRet);

        //          //Show the data
        //          dataGridView1.DataSource = ds;
        //          dataGridView1.DataMember = ds.Tables[0].TableName;
        //      }
        //      catch (Exception ex)
        //      {
        //          MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        //      }


        public static void UpdateItem(ItemModel itemModel)
        {
            try
            {
                string sql = clsItemsSQL.UPDATE_ITEM_DESC
                    .Replace("@ItemCode", itemModel.ItemCode)
                    .Replace("@ItemDesc", itemModel.Desc)
                    .Replace("@Cost", itemModel.Cost.ToString());
                int response = db.ExecuteUpdate(sql);
                //int debug = 0;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }

        }
        public static void DeleteItem(ItemModel itemModel)
        {
            try
            {
                string sql = clsItemsSQL.DELETE_FROM_ITEM
                    .Replace("@ItemCode", itemModel.ItemCode)
                    .Replace("@ItemDesc", itemModel.Desc)
                    .Replace("@Cost", itemModel.Cost.ToString());
                int response = db.ExecuteDelete(sql);
                //int debug = 0;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("You cannot delete an Item that still has references.");
            }

        }

        public static void InsertItems(ItemModel itemModel)
        {
            try
            {
                string sql = clsItemsSQL.INSERT_ITEM
                    .Replace("@ItemCode", itemModel.ItemCode)
                    .Replace("@Desc", itemModel.Desc)
                    .Replace("@Cost", itemModel.Cost.ToString());
                int response = db.ExecuteInsert(sql);
                //int debug = 0;
            }
            catch (Exception ex)
            {
                //System.Windows.MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        public static List<ItemModel> GetAllItems()
        {
            try
            {
                //Number of return values
                int iRet = 0;

                //Execute the statement and get the data
                DataSet ds = db.ExecuteSQLStatement(clsItemsSQL.All_Items, ref iRet);

                List<ItemModel> itemModels = new List<ItemModel>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ItemModel itemModel = new ItemModel();
                    itemModel.ItemCode = dr[0].ToString();
                    itemModel.Desc = dr[1].ToString();
                    itemModel.Cost = (decimal)dr[2];
                    itemModels.Add(itemModel);
                }
                return itemModels;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return new List<ItemModel>();
            }

        }
    }
}
