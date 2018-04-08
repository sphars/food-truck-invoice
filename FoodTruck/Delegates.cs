using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This file contains delegate signatures used throughout the project.

namespace FoodTruck
{
    /// <summary>
    /// This delegate is primary used to pass an invoice from InvoiceSearch to MainWindow.
    /// </summary>
    /// <param name="toSave">Invoice object to return to MainWindow</param>
    public delegate void ReturnInvoice(object toSave);
}
