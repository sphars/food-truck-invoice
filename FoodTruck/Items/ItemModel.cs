using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Items
{
    class ItemModel
    {
        public string ItemCode { get; set; } = "";
        public string Desc { get; set; } = "";
        public Decimal Cost { get; set; } = 0m;
        public override string ToString()
        {
            return $"({ItemCode}, {Cost:C}): {Desc}";
        }

    }
}
