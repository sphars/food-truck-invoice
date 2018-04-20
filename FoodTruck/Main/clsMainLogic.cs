using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Main {
    /// <summary>
    /// This class is the business logic for the MainWindow
    /// </summary>
    public static class clsMainLogic {

        public static List<ItemDesc> GetAllItemDescs() {
            var dataAccess = new DataAccess();
            int rows = -1;
            var dataSet = dataAccess.ExecuteSQLStatement(clsMainSQL.S_IDESC, ref rows);
            var list = new List<ItemDesc>();
            if(rows > 0) {
                foreach(DataRow row in dataSet.Tables[0].Rows) {
                    list.Add(new ItemDesc() {
                        ItemCode = row[0].ToString(),
                        Desc = row[1].ToString(),
                        Cost = (decimal)row[2]
                    });
                }
            }
            return list;
        }
    }
}
