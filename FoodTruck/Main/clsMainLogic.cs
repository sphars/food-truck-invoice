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
    public class InvoiceManager {

        public Invoice CurrentInvoice { get; private set; }

        public InvoiceManager(Invoice invoice) {
            if(invoice != null) {
                CurrentInvoice = new Invoice(invoice);
            } else {
                throw new ArgumentNullException("Invoice cannot be null");
            }
        }

        /// <summary>
        /// Gets all the ItemDesc rows from the table.
        /// </summary>
        /// <returns>Returns a List of ItemDesc objects</returns>
        public List<ItemDesc> GetAllItemDescs() {
            var dataAccess = new DataAccess();
            int rows = -1;
            var dataSet = dataAccess.ExecuteSQLStatement(clsMainSQL.S_IDESC, ref rows);
            var list = new List<ItemDesc>();
            if(rows > 0) {
                foreach(DataRow row in dataSet.Tables[0].Rows) {
                    list.Add(new ItemDesc() {
                        ItemCode = (string)row[0],
                        Desc = (string)row[1],
                        Cost = (decimal)row[2]
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// This method returns a list of ItemDesc that represents the LineItems of the current invoice.
        /// </summary>
        /// <returns>Returns a list of ItemDesc objects, not a list of LineItems</returns>
        public List<ItemDesc> GetLineItems() {
            // First get a list of the LineItems in the DataBase.
            var sql = clsMainSQL.S_LI_P_NUM.Replace("@NUM", CurrentInvoice.InvoiceNum.ToString());
            var dataAccess = new DataAccess();
            int rows = -1;
            var dataSet = dataAccess.ExecuteSQLStatement(sql, ref rows);
            var LineItemList = new List<LineItem>();
            if(rows > 0) {
                foreach(DataRow row in dataSet.Tables[0].Rows) {
                    LineItemList.Add(new LineItem() {
                        InvoiceNum = (int)row[0],
                        LineItemNum = (int)row[1],
                        ItemCode = (string)row[2]
                    });
                }
            }

            // Get all the ItemDesc from the table
            var AllItemDescs = GetAllItemDescs();

            // This is the list that will be returned.
            var ItemDescList = new List<ItemDesc>();

            foreach(var lineItem in LineItemList) {
                ItemDescList.Add(AllItemDescs.First(i => i.ItemCode == lineItem.ItemCode));
            }

            return ItemDescList;
        }

        public void SetInvoiceDate(DateTime date) {
            if(date != null) {
                CurrentInvoice.InvoiceDate = date.Date;
            }
        }
    }
}
