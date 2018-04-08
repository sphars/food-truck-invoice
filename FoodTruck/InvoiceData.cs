using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace FoodTruck
{
    /// <summary>
    /// A class for the business logic of the invoice program. 
    /// SQL statements will reside in this class. Calls DataAccess.cs to get the data.
    /// </summary>
    class InvoiceData
    {
        #region SQL Strings
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


        /// <summary>
        /// This SQL statement inserts data back into the invoce table
        /// from the item entry window
        /// </summary>
        string SQLinsertInvoice = @"INSERT INTO INVOICES (InvoiceDate, TotalCharge) 
        VALUES (%s, %s) ";

        #endregion

        /// <summary>
        /// Object for accessing the database
        /// </summary>
        DataAccess da;

        #region Methods

        /// <summary>
        /// EXAMPLE METHOD:
        /// This SQL gets all data on an invoice for a given InvoiceID.
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceData(string sInvoiceID)
        {
            DataSet ds = new DataSet();

            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;
            int iRetRows = 0;

            ds = da.ExecuteSQLStatement(sSQL, ref iRetRows); //call the data access layer to execute a SQL statement. 

            string sInvoice = ds.Tables[0].Rows.ToString(); //get the first row of the dataset

            return sInvoice; //return the invoice
        }

        /// <summary>
        /// This SQL inserts the data for InvoiceNum, InvoiceDate, TotalCharge
        /// into the database based on whats entered in from the item entry window
        /// </summary>
        public void insert()
        {
           //inserted date 
            string date = "tododate";

            //inserted charge 
            string charge = "todoCharge";
  
            //formats the sql with the insert statement 
            string formattedQuery = String.Format(SQLinsertInvoice, date, charge);
        }
        #endregion
    }
}
