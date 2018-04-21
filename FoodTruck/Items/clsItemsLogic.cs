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
        //DataAccess db = new DataAccess();

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


        public static List<ItemDesc> GetAllItems()
        {
            var dataAccess = new DataAccess();
            int iRows = 0;
            var dataSet = dataAccess.ExecuteSQLStatement(clsItemsSQL.All_Items, ref iRows);
            var list = new List<ItemDesc>();
            if (iRows > 0)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    list.Add(new ItemDesc()
                    {
                        ItemCode = (string)row[0],
                        Desc = (string)row[1],
                        //Cost = (decimal)row[2]
                    });
                }
            }

            return list;
        }
    }
}
