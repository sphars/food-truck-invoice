using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck {
    public class DataAccess {

        string SQLGetInvoices = "SELECT * FROM Invoices;";
        string SQLGetLineItemsForInvoice = "SELECT * FROM LineItems WHERE InvoiceNum =";
    }
}
