using FoodTruck.Items;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;


namespace FoodTruck
{


    /// <summary>
    /// Interaction logic for ItemEntry.xaml
    /// </summary>
    public partial class ItemEntry : Window
    {
        //Used to access the database
        DataAccess db = new DataAccess();

        public object DataSource { get; set; }


    //    private DataGridView dataGridView1 = new DataGridView();
    //private BindingSource bindingSource1 = new BindingSource();

        //holds info in the databse
        DataSet ds;

        //private ItemDesc selectedItem;

        public ItemEntry()
        {

            InitializeComponent();
        }

        /// <summary>
        /// Add button gives an option to add new items 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Edit item button edits the edits in the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItemButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// delete button deletes the items in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// ID read only pulls a list that is read only not able to make edits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDreadOnlyButton_Click(object sender, RoutedEventArgs e)
        {

            // selectedItem = (ItemDesc)DataGridItemEntry.SelectedItem;

            //DataGridItemEntry.ItemsSource = ds.DefaultViewManager;

            DataTable ds = new DataTable("ItemDesc");

           // DataGridItemEntry.DataSource = ds;
           // DataGridItemEntry.DataBind();

            // DataAccess db = new DataAccess("ItemDesc");


            //try
            //{
            //    //Create a DataSet to hold the data
            //    DataSet ds;

            //    //Number of return values
            //    int iRet = 0;

            //    //Execute the statement and get the data
            //    ds = db.ExecuteSQLStatement(clsItemsSQL.All_Items, ref iRet);

            //    //Show the data
            //    DataGridItemEntry.DataSource = ds.Tables[0];
            //    DataGridItemEntry.DataMember = ds.Tables[0].TableName;
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            //}

        }

        /// <summary>
        /// The description button pulls up the description 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescriptionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Cost button pulls up the cost 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CostButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Save button saves the chages that were made 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
