using System;

namespace FoodTruck.Items
{
    /// <summary>
    /// This class represents an Item as stored in the database.
    /// </summary>
    class ItemModel
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
