using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck {
    public class DataAccess {

        /// <summary>
        /// This SQL statement gets all the invoice numbers 
        /// for the drop down on the search invoice window 
        /// for invoice numbers  
        /// </summary>
        string SQLGetInvoicesNum = "SELECT InvoiceNum FROM Invoices;";

        /// <summary>
        /// This SQL statement gets all the invoice dates
        /// for the drop down on the search window 
        /// for the invoce dates
        /// </summary>
        string SQLGetInvoicesDate = "SELECT InvoiceDate FROM Invoices;";

        /// <summary>
        /// This SQL statement gets all the total charges
        /// for the drop down on the search window
        /// for the invoce charges
        /// </summary>
        string SQLGetInvoicesTotalCharge = "SELECT TotalCharge FROM Invoices;";

        /// <summary>
        /// This SQL statement selects all the rows from line items
        /// </summary>
        string SQLGetLineItemsForInvoice = "SELECT * FROM LineItems WHERE InvoiceNum =";

        /// <summary>
        /// This SQL statement selects all of the invoices from the invoice table 
        /// </summary>
        string SQLGetInvoices = "SELECT * FROM Invoices;";

        //string sSQL = "INSERT INTO INVOICES (InvoiceNum, InvoiceDate, TotalCharge)
       // VALUES(')
    }
}
