using System;

namespace FoodTruck
{
    /// <summary>
    /// This class represents an Invoice as stored in the database.
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// This is the primary-key ID column of the invoice in the database.  A special value of -1 indicates the invoice is not in the database.
        /// </summary>
        public int InvoiceNum { get; set; } = -1;

        /// <summary>
        /// This is the date of the invoice.  By default, it has today's date.
        /// </summary>
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        /// <summary>
        /// This is the total charge of the invoice.  It may be out of date, if the price of each line item was changed after the invoice was saved.
        /// </summary>
        public Decimal TotalCharge { get; set; } = 0m;

        /// <summary>
        /// This copy constructor accepts an invoice whose values will be copied.
        /// </summary>
        /// <param name="old">Pre-existing invoice</param>
        public Invoice(Invoice old)
        {
            if (old == null)
                return;

            InvoiceNum = old.InvoiceNum;
            InvoiceDate = old.InvoiceDate;
            TotalCharge = old.TotalCharge;
        }

        /// <summary>
        /// This default constructor creates an invoice object using default values for the properties.
        /// </summary>
        public Invoice()
        {
            // The default values above remove the need to have anything here
        }
    }

    /// <summary>
    /// This class represents a LineItem as stored in the database.
    /// </summary>
    public class LineItem
    {
        /// <summary>
        /// This is the foreign key integer identifying which invoice owns the line item.
        /// </summary>
        public int InvoiceNum { get; set; } = -1;

        /// <summary>
        /// This integer represents which position in the invoice the line item appears.
        /// </summary>
        public int LineItemNum { get; set; } = -1;

        /// <summary>
        /// This is the foreign key string identifying the item in the ItemDesc table.
        /// </summary>
        public string ItemCode { get; set; } = "";
    }

    /// <summary>
    /// This class represents an Item as stored in the database.
    /// </summary>
    public class ItemDesc
    {
        /// <summary>
        /// This is the user-facing primary key string identifying the item.
        /// </summary>
        public string ItemCode { get; set; } = "";

        /// <summary>
        /// This is a human-readable string description of the item.
        /// </summary>
        public string Desc { get; set; } = "";

        /// <summary>
        /// This is the cost of the item.  Negative values can represent discounts.
        /// </summary>
        public Decimal Cost { get; set; } = 0m;

        /// <summary>
        /// This overridden method returns a string representation of the Item.
        /// </summary>
        /// <returns>Returns a string of the following format: (ItemCode, Cost): Description</returns>
        public override string ToString()
        {
            try
            {
                return $"({ItemCode}, {Cost:C}): {Desc}";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
