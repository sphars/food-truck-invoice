﻿using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

/// <summary>
/// Class used to access the database.
/// No SQL statements here. They need to be passed in.
/// </summary>
public class DataAccess
{



    ///// <summary>
    ///// This SQL statement gets all the invoice numbers 
    ///// for the drop down on the search invoice window 
    ///// for invoice numbers  
    ///// </summary>
    //string SQLGetInvoicesNum = "SELECT InvoiceNum FROM Invoices;";

    ///// <summary>
    ///// This SQL statement gets all the invoice dates
    ///// for the drop down on the search window 
    ///// for the invoce dates
    ///// </summary>
    //string SQLGetInvoiceDate = "SELECT InvoiceDate FROM Invoices;";

    ///// <summary>
    ///// This SQL statement gets all the total charges
    ///// for the drop down on the search window
    ///// for the invoce charges
    ///// </summary>
    //string SQLGetInvoiceTotalCharge = "SELECT TotalCharge FROM Invoices;";

    ///// <summary>
    ///// This SQL statement selects all the rows from line items
    ///// </summary>
    //string SQLGetLineItemsForInvoice = "SELECT * FROM LineItems WHERE InvoiceNum =";

    ///// <summary>
    ///// This SQL statement selects all of the invoices from the invoice table 
    ///// </summary>
    //string SQLGetInvoices = "SELECT * FROM Invoices;";


  
    



    /// <summary>
    /// Connection string to the database.
    /// </summary>
    private string sConnectionString;

    /// <summary>
    /// Constructor that sets the connection string to the database
    /// </summary>
    public DataAccess()
    {
        sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\FoodTruckInvoices.mdb";
    }

    /// <summary>
    /// This method takes an SQL statment that is passed in and executes it.  The resulting values
    /// are returned in a DataSet.  The number of rows returned from the query will be put into
    /// the reference parameter iRetVal.
    /// </summary>
    /// <param name="sSQL">The SQL statement to be executed.</param>
    /// <param name="iRetVal">Reference parameter that returns the number of selected rows.</param>
    /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
    public DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal)
    {
        try
        {
            //Create a new DataSet
            DataSet ds = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(sConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {

                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                    adapter.SelectCommand.CommandTimeout = 0;

                    //Fill up the DataSet with data
                    adapter.Fill(ds);
                }
            }

            //Set the number of values returned
            iRetVal = ds.Tables[0].Rows.Count;

            //return the DataSet
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// This method takes an SQL statment that is passed in and executes it.  The resulting single 
    /// value is returned.
    /// </summary>
    /// <param name="sSQL">The SQL statement to be executed.</param>
    /// <returns>Returns a string from the scalar SQL statement.</returns>
    public string ExecuteScalarSQL(string sSQL)
    {
        try
        {
            //Holds the return value
            object obj;

            using (OleDbConnection conn = new OleDbConnection(sConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {

                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                    adapter.SelectCommand.CommandTimeout = 0;

                    //Execute the scalar SQL statement
                    obj = adapter.SelectCommand.ExecuteScalar();
                }
            }

            //See if the object is null
            if (obj == null)
            {
                //Return a blank
                return "";
            }
            else
            {
                //Return the value
                return obj.ToString();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// This method takes an SQL statment that is a non query and executes it.
    /// </summary>
    /// <param name="sSQL">The SQL statement to be executed.</param>
    /// <returns>Returns the number of rows affected by the SQL statement.</returns>
    public int ExecuteNonQuery(string sSQL)
    {
        try
        {
            //Number of rows affected
            int iNumRows;

            using (OleDbConnection conn = new OleDbConnection(sConnectionString))
            {
                //Open the connection to the database
                conn.Open();

                //Add the information for the SelectCommand using the SQL statement and the connection object
                OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                cmd.CommandTimeout = 0;

                //Execute the non query SQL statement
                iNumRows = cmd.ExecuteNonQuery();
            }

            //return the number of rows affected
            return iNumRows;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }
}