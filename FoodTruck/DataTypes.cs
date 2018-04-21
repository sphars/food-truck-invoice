using System;

namespace FoodTruck {
    /// <summary>
    /// This class represents an Invoice as stored in the database.
    /// </summary>
    public class Invoice {
        public int InvoiceNum { get; set; } = -1;

        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public Decimal TotalCharge { get; set; } = 0m;

        // Copy constructor
        public Invoice(Invoice old) {
            InvoiceNum = old.InvoiceNum;
            InvoiceDate = old.InvoiceDate;
            TotalCharge = old.TotalCharge;
        }
    }

    /// <summary>
    /// This class represents a LineItem as stored in the database.
    /// </summary>
    public class LineItem {
        public int InvoiceNum { get; set; } = -1;
        public int LineItemNum { get; set; } = -1;
        public string ItemCode { get; set; } = "";
    }

    /// <summary>
    /// This class represents an Item as stored in the database.
    /// </summary>
    public class ItemDesc {
        public string ItemCode { get; set; } = "";
        public string Desc { get; set; } = "";
        public Decimal Cost { get; set; } = 0m;

        public override string ToString() {
            return $"({ItemCode}, {Cost:C}): {Desc}";
        }
    }
}
