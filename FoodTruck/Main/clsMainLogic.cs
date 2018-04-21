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


        private List<ItemDesc> LineItems;

        public InvoiceManager(Invoice invoice) {
            if(invoice != null) {
                // Copy of the old invoice so it doesn't overwrite the original in case the user cancels their edits.
                CurrentInvoice = new Invoice(invoice);
            } else {
                throw new ArgumentNullException("Invoice cannot be null");
            }
        }

        /// <summary>
        /// Gets all the ItemDesc rows from the table.
        /// </summary>
        /// <returns>Returns a List of ItemDesc objects</returns>
        public static List<ItemDesc> GetAllItemDescs() {
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
            // If LineItems has already been populated, return it.
            if(LineItems != null) {
                return LineItems;
            }

            // Otherwise, query the database to set it up for the first time:
            LineItems = new List<ItemDesc>();

            if(CurrentInvoice.InvoiceNum != -1) {
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

                foreach(var lineItem in LineItemList) {
                    LineItems.Add(AllItemDescs.First(i => i.ItemCode == lineItem.ItemCode));
                }
            }
            return LineItems;
        }

        public void SetInvoiceDate(DateTime date) {
            if(date != null) {
                CurrentInvoice.InvoiceDate = date.Date;
            }
        }

        /// <summary>
        /// This method deletes the CurrentInvoice from the Database.
        /// If the InvoiceNum is -1, then it hasn't been saved, and nothing needs to be done in the DB.
        /// </summary>
        public void DeleteInvoice() {
            if(CurrentInvoice != null) {
                if(CurrentInvoice.InvoiceNum != -1) {
                    // First delete its LineItems:
                    var sql = clsMainSQL.D_LI_P_NUM.Replace("@NUM", CurrentInvoice.InvoiceNum.ToString());
                    var dataAccess = new DataAccess();
                    int rowsAffected = dataAccess.ExecuteNonQuery(sql);
                    Console.WriteLine($"Deleted {rowsAffected} rows for InvoiceNum {CurrentInvoice.InvoiceNum}.");

                    // Now delete the actual Invoice:
                    sql = clsMainSQL.D_INV_P_NUM.Replace("@NUM", CurrentInvoice.InvoiceNum.ToString());
                    rowsAffected = dataAccess.ExecuteNonQuery(sql);

                    Console.WriteLine($"Deleted Invoice with InvoiceNum {CurrentInvoice.InvoiceNum}");
                }

                CurrentInvoice = null;
            }
        }

        public void SaveInvoice() {
            if(CurrentInvoice == null)
                return;

            if(CurrentInvoice.InvoiceNum == -1) {
                // The invoice hasn't yet been written to the database.

                // First insert 
            }
        }
    }
}
