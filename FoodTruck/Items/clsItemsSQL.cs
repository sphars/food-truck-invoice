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

        /// <summary>
        /// SELECT statement for retrieving all LineItems for a specific InvoiceNum.
        /// </summary>
        public static readonly string S_LI_P_NUM =
            "SELECT InvoiceNum, LineItemNum, ItemCode FROM LineItems WHERE InvoiceNum = @NUM;";

        /// <summary>
        /// SELECT statement for retrieving a specific Item from ItemDesc by specified ItemCode.
        /// </summary>
        public static readonly string S_IDESC_P_CODE =
            "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc WHERE ItemCode = @CODE;";

        /// <summary>
        /// INSERT statement for a new Invoice with parameters InvoiceDate and TotalCharge.
        /// </summary>
        public static readonly string I_INV_P_DATE_TOTAL =
            "INSERT INTO Invoices (InvoiceDate, TotalCharge) VALUES(@DATE, @TOTAL);";

        /// <summary>
        /// INSERT statement for adding line items to an Invoice with parameters InvoiceNum, LineItemNum, ItemCode.
        /// </summary>
        public static readonly string I_LI_P_INUM_LNUM_CODE =
            "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES(@INUM, @LNUM, @CODE);";

        /// <summary>
        /// UPDATE statement for an existing Invoice with parameters InvoiceDate, TotalCharge, and InvoiceNum.
        /// </summary>
        public static readonly string U_INV_P_DATE_TOTAL_NUM =
            "UPDATE Invoices SET InvoiceDate = @DATE, TotalCharge = @TOTAL WHERE InvoiceNum = @NUM;";

        /// <summary>
        /// DELETE statement for deleting all line items for a specific InvoiceNum
        /// </summary>
        public static readonly string D_LI_P_NUM =
            "DELETE FROM LineItems WHERE InvoiceNum = @NUM;";

        /// <summary>
        /// DELETE statement for deleting an Invoice by specified InvoiceNum
        /// </summary>
        public static readonly string D_INV_P_NUM =
            "DELETE FROM Invoices WHERE InvoiceNum = @NUM;";



    }

}

