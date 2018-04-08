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
        /// This SQL statement inserts data back into the invoice table
        /// from the item entry window
        /// </summary>
        string SQLinsertInvoice = @"INSERT INTO INVOICES (InvoiceDate, TotalCharge) 
        VALUES (%s, %s) ";

        /// <summary>
        /// This SQL statement deletes data back from the invoice table
        /// from the invoice
        /// </summary>
        string SQLdeleteInvoice = @"Delete FROM INVOICES WHERE InvoiceNum = %d";

     

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
        /// Method to get invoice numbers. Used for Invoice Search dropdown.
        /// </summary>
        /// <returns>A list of invoice numbers</returns>
        public List<string> GetInvoiceNums()
        {
            List<string> lInvoiceNums = new List<string>();

            DataSet dsInvoiceNums = new DataSet();
            int iRetRows = 0;

            dsInvoiceNums = da.ExecuteSQLStatement(SQLGetInvoicesNum, ref iRetRows);

            foreach (DataRow dr in dsInvoiceNums.Tables[0].Rows)
            {
                string sInvoiceNum = dr[0].ToString();
                lInvoiceNums.Add(sInvoiceNum);
            }

            return lInvoiceNums;
        }


        /// <summary>
        /// Method to get invoice dates. Used for Invoice Search date dropdown
        /// </summary>
        /// <returns>List of invoice dates</returns>
        public List<string> GetInvoiceDates()
        {
            List<string> lInvoiceDates = new List<string>();

            DataSet dsInvoiceDates = new DataSet();
            int iRetRows = 0;

            dsInvoiceDates = da.ExecuteSQLStatement(SQLGetInvoicesDate, ref iRetRows);

            foreach (DataRow dr in dsInvoiceDates.Tables[0].Rows)
            {
                string sInvoiceDate = dr[0].ToString();
                lInvoiceDates.Add(sInvoiceDate);
            }

            return lInvoiceDates;
        }


        /// <summary>
        /// Method to get invoice amounts. Used for Invoice Search dropdown
        /// </summary>
        /// <returns>List of invoice amounts</returns>
        public List<string> GetInvoiceAmounts()
        {
            List<string> lInvoiceAmounts = new List<string>();

            DataSet dsInvoiceAmounts = new DataSet();
            int iRetRows = 0;

            dsInvoiceAmounts = da.ExecuteSQLStatement(SQLGetInvoicesTotalCharge, ref iRetRows);

            foreach (DataRow dr in dsInvoiceAmounts.Tables[0].Rows)
            {
                string sInvoiceAmount = dr[0].ToString();
                lInvoiceAmounts.Add(sInvoiceAmount);
            }

            return lInvoiceAmounts;
        }

        /// <summary>
        /// Gets a list of line items for a given invoice number
        /// </summary>
        /// <param name="sInvoiceNum">The invoice number</param>
        /// <returns>List of line items</returns>
        public List<string> GetInvoiceLineItems(string sInvoiceNum)
        {
            List<string> lInvoiceLineItems = new List<string>(); //This should be a list of Item objects, eventually

            DataSet dsInvoiceLineItems = new DataSet();
            int iRetRows = 0;

            SQLGetLineItemsForInvoice += sInvoiceNum; //add the invoice num to the query string

            dsInvoiceLineItems = da.ExecuteSQLStatement(SQLGetLineItemsForInvoice, ref iRetRows);

            foreach (DataRow dr in dsInvoiceLineItems.Tables[0].Rows)
            {
                string sInvoiceNumber = dr[0].ToString();
                string sLineItemNumber = dr[1].ToString();
                string sItemCode = dr[2].ToString();

                lInvoiceLineItems.Add(sInvoiceNumber + " " + sLineItemNumber + " " + sItemCode);
            }

            return lInvoiceLineItems;
        }


        /// <summary>
        /// Gets a list of all invoices
        /// </summary>
        /// <returns>List of invoices</returns>
        public List<string> GetInvoices()
        {
            List<string> lInvoices = new List<string>(); //This should be a list of Invoice objects, eventually

            DataSet dsInvoices = new DataSet();
            int iRetRows = 0;

            dsInvoices = da.ExecuteSQLStatement(SQLGetInvoices, ref iRetRows);

            foreach (DataRow dr in dsInvoices.Tables[0].Rows)
            {
                string sInvoiceNum = dr[0].ToString();
                string sInvoiceDate = dr[1].ToString();
                string sTotalCharge = dr[2].ToString();

                lInvoices.Add(sInvoiceNum + " " + sInvoiceDate + " " + sTotalCharge);
            }

            return lInvoices;
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

        /// <summary>
        /// This SQL deletes the data for InvoiceNum, InvoiceDate, TotalCharge
        /// from the database from the item entry window
        /// </summary>
        public void delete (int invoiceNum)
        {
            //formats the sql with the delete statement 
            string formattedQuery = String.Format(SQLdeleteInvoice, invoiceNum);

            // TODO EXECUTE QUERY

        }

 


        #endregion
    }
}
