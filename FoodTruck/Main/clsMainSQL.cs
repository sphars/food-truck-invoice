namespace FoodTruck.Main {
    /// <summary>
    /// This class contains the SQL statements used by clsMainLogic.
    /// </summary>
    public static class clsMainSQL {

        /* The format for the string names are as follows:
         * The first group says what kind of statement it is: S=Select, I=Insert, U=Update, D=Delete
         * The second group is an abbreviated name of the table.
         * If the statement has parameters, the third group will just be the letter P.
         * The remaining groups are the parameters themselves.
         * 
         * Don't forget to replace these parameters before executing the statement.
         */

        /// <summary>
        /// SELECT statement for retrieving all items from ItemDesc.
        /// </summary>
        public static readonly string S_IDESC =
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
