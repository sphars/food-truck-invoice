using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Items
{
    class clsItemsSQL
    {
        ////Used to access the database
        //DataAccess db = new DataAccess();

        ////holds info in the databse
        //DataSet ds;

        ////Tells if user is deleting a row
        //bool IsDeleting = false;


        /// <summary>
        /// SELECT statement for retrieving all read only for the item desc db
        /// </summary>
        public static readonly string All_Items =
            "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc;";


    }
}
