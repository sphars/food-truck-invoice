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

        bool isAdd = false;
        private BindingListCollectionView ItemView;
        //CustomerDataContext dc = new CustomerDataContext();

        //holds info in the databse
        DataSet ds;

        //private ItemDesc selectedItem;

        public ItemEntry()
        {

            InitializeComponent();
            //var items = ds.GetChanges();
            //this.DataContext = items;
            //this.ItemView = (BindingListCollectionView)(CollectionViewSource.GetDefaultView(items));
        }

        /// <summary>
        /// Add button gives an option to add new items 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            ItemModel itemModel = (ItemModel)DataGridItemEntry.SelectedItem;
            clsItemsLogic.InsertItems(itemModel);

            DataGridItemEntry.ItemsSource = clsItemsLogic.GetAllItems();
           // int debug = 0;
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
            //{

            //    if (e.Command == DataGridItemEntry.DeleteCommand)

            //    {
            //        if (!(MessageBox.Show("Are you sure you want to delete?", "Please confirm.", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
            //        {
            //            // Cancel Delete
            //            e.Handled = true;

            //        }

            //    }

            //}
        }

        /// <summary>
        /// ID read only pulls a list that is read only not able to make edits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDreadOnlyButton_Click(object sender, RoutedEventArgs e)
        {
            DataGridItemEntry.ItemsSource = clsItemsLogic.GetAllItems();
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
            //this.ds.SubmitChanges();
            isAdd = false;
        }

        public void Save()

        {

            // Ask the Model or the DAL to persist me...

        }



        /// <summary>

        /// Deletes a Formula 1 Driver.

        /// </summary>

        public void Delete()

        {

            // Ask the Model or the DAL to delete me...

        }

        
    }
}
