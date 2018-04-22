using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FoodTruck.Search
{
    /// <summary>
    /// This class is the business logic for the Invoice Search window
    /// </summary>
    public static class clsSearchLogic
    {
        /// <summary>
        /// Fetches invoices from the database
        /// </summary>
        /// <returns>List of invoices</returns>
        public static List<Invoice> GetAllInvoices()
        {
            var dataAccess = new DataAccess();
            int iRows = 0;
            var dataSet = dataAccess.ExecuteSQLStatement(clsSearchSQL.S_INVOICES, ref iRows);
            var list = new List<Invoice>();
            if(iRows > 0)
            {
                foreach(DataRow row in dataSet.Tables[0].Rows)
                {
                    list.Add(new Invoice()
                    {
                        InvoiceNum = (int)row[0],
                        InvoiceDate = (DateTime)row[1],
                        TotalCharge = (decimal)row[2]
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// Gets a list of all invoice numbers
        /// </summary>
        /// <returns>List of invoice numbers</returns>
        public static List<int> GetInvoiceNumbers()
        {
            var dataAccess = new DataAccess();
            int iRows = 0;
            var dataSet = dataAccess.ExecuteSQLStatement(clsSearchSQL.S_INVNUM, ref iRows);
            var list = new List<int>();
            
            if(iRows > 0)
                foreach(DataRow row in dataSet.Tables[0].Rows)
                    list.Add((int)row[0]);

            return list;
        }

        /// <summary>
        /// Gets a list of all distinct invoice dates
        /// </summary>
        /// <returns>List of invoice dates</returns>
        public static List<DateTime> GetInvoiceDates()
        {
            var dataAccess = new DataAccess();
            int iRows = 0;
            var dataSet = dataAccess.ExecuteSQLStatement(clsSearchSQL.S_INVDATE, ref iRows);
            var list = new List<DateTime>();

            if (iRows > 0)
                foreach (DataRow row in dataSet.Tables[0].Rows)
                    list.Add((DateTime)row[0]);

            return list;
        }

        /// <summary>
        /// Gets a list of all distinct invoice total charges
        /// </summary>
        /// <returns>List of invoice total charges</returns>
        public static List<decimal> GetTotalCharges()
        {
            var dataAccess = new DataAccess();
            int iRows = 0;
            var dataSet = dataAccess.ExecuteSQLStatement(clsSearchSQL.S_TOTCHA, ref iRows);
            var list = new List<decimal>();

            if (iRows > 0)
                foreach (DataRow row in dataSet.Tables[0].Rows)
                    list.Add((decimal)row[0]);

            return list;
        }
    }
}
