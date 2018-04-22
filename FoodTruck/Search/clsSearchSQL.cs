using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Search
{
    /// <summary>
    /// This class contains the SQL statements used by clsSearchLogic
    /// </summary>
    public static class clsSearchSQL
    {
        /* The format for the string names are as follows:
         * The first group says what kind of statement it is: S=Select, I=Insert, U=Update, D=Delete
         * The second group is an abbreviated name of the table.
         * If the statement has parameters, the third group will just be the letter P.
         * The remaining groups are the parameters themselves.
         * 
         * Don't forget to replace these parameters before executing the statement.
         */

        /// <summary>
        /// SELECT statement for retrieving all invoices from Invoices
        /// </summary>
        public static readonly string S_INVOICES =
            "SELECT InvoiceNum, InvoiceDate, TotalCharge FROM Invoices;";

        /// <summary>
        /// SELECT statement for retrieving all invoices that match the filters
        /// </summary>
        //public static readonly string S_INV_P_INUM_IDATE_TC =
        //    "SELECT InvoiceNum, InvoiceDate, TotalCharge FROM Invoices WHERE InvoiceNum = @INUM AND InvoiceDate = @DATE AND TotalCharge = @CHARGE;";

        /// <summary>
        /// SELECT statement for retrieving all invoices that match the given invoice number
        /// </summary>
        //public static readonly string S_INV_P_INUM =
        //    "SELECT InvoiceNum, InvoiceDate, TotalCharge FROM Invoices WHERE InvoiceNum = @INUM;";

        /// <summary>
        /// SELECT statement for retrieving all invoices that match the given invoice date
        /// </summary>
        //public static readonly string S_INV_P_DATE =
        //    "SELECT InvoiceNum, InvoiceDate, TotalCharge FROM Invoices WHERE InvoiceDate = @DATE;";

        /// <summary>
        /// SELECT statement for retrieving all invoices that match the given total charge
        /// </summary>
        //public static readonly string S_INV_P_TC =
        //    "SELECT InvoiceNum, InvoiceDate, TotalCharge FROM Invoices WHERE TotalCharge = @CHARGE;";

        /// <summary>
        /// SELECT statement for retrieving all invoice numbers in the Invoices table
        /// </summary>
        public static readonly string S_INVNUM =
            "SELECT InvoiceNum FROM Invoices;";

        /// <summary>
        /// SELECT statement for retrieving all unique invoice dates in the Invoices table
        /// </summary>
        public static readonly string S_INVDATE =
            "SELECT DISTINCT InvoiceDate FROM Invoices;";

        /// <summary>
        /// SELECT statement for retrieving all unique total charges in the Invoices table
        /// </summary>
        public static readonly string S_TOTCHA =
            "SELECT DISTINCT TotalCharge FROM Invoices;";

    }
}
