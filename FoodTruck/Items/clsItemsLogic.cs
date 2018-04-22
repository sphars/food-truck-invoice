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

        public static void UpdateItem(ItemModel itemModel)
        {
            //asks user whether or not they wanting to edit 
            var result = System.Windows.MessageBox.Show("Are you sure you want to edit " + itemModel.Desc + " from the item entry", "Item Entry", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.Yes);

            //the user clicks yes
            if (result == System.Windows.MessageBoxResult.Yes)
            {

                try
                {
                    //grabs the sql from clsItemsSQL class
                    string sql = clsItemsSQL.UPDATE_ITEM_DESC
                        .Replace("@ItemCode", itemModel.ItemCode)
                        .Replace("@ItemDesc", itemModel.Desc)
                        .Replace("@Cost", itemModel.Cost.ToString());
                    int response = db.ExecuteUpdate(sql);

                }
                catch (Exception ex)
                {
                    //throws error message
                    System.Windows.MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

                }
            }

        }
        public static void DeleteItem(ItemModel itemModel)
        {
            //asks user whether or not they wanting to delete
            System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to delete " + itemModel.Desc + " from the item entry", "Item Entry", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.Yes);

            //the user clicks yes
            if (result == System.Windows.MessageBoxResult.Yes)
            {

                try
                {
                    //grabs the sql from clsItemsSQL class
                    string sql = clsItemsSQL.DELETE_FROM_ITEM
                        .Replace("@ItemCode", itemModel.ItemCode)
                        .Replace("@ItemDesc", itemModel.Desc)
                        .Replace("@Cost", itemModel.Cost.ToString());
                    int response = db.ExecuteDelete(sql);

                }
                catch (Exception ex)
                {
                    //throw error message if they are trying to delete a primary key referenced to a table
                    System.Windows.MessageBox.Show("You cannot delete an Item that still has references.");
                }
            }

        }

        public static void InsertItems(ItemModel itemModel)
        {
            //asks user whether or not they wanting to insert
            System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to add this item to the item entry?", "Item Entry", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.Yes);

            //the user clicks yes
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                try
                {
                    //grabs the sql from clsItemsSQL class
                    string sql = clsItemsSQL.INSERT_ITEM
                        .Replace("@ItemCode", itemModel.ItemCode)
                        .Replace("@Desc", itemModel.Desc)
                        .Replace("@Cost", itemModel.Cost.ToString());
                    int response = db.ExecuteInsert(sql);

                }
                catch (Exception ex)
                {
                    //throw an error message
                    System.Windows.MessageBox.Show("You cannot add item code " + itemModel.ItemCode + " because it already exists!");

                }
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
                //throw error message
                System.Windows.MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return new List<ItemModel>();
            }

        }
    }
}
