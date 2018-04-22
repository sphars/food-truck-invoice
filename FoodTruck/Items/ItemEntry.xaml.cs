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

        

        public ItemEntry()
        {

            InitializeComponent();
            DataGridItemEntry.ItemsSource = clsItemsLogic.GetAllItems();

        }

        /// <summary>
        /// Add button gives an option to add new items 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {

            //Use the object reference to add new items
            // clsItemsLogic.InsertItems(ItemCodeBox.Text, ItemDescBox.Text, Convert.ToInt32(CostBox.Text));
            ItemModel itemModel = new ItemModel();
            itemModel.ItemCode = ItemCodeBox.Text;
            itemModel.Desc = ItemDescBox.Text;
            itemModel.Cost = Decimal.Parse(CostBox.Text);
            clsItemsLogic.InsertItems(itemModel);

            DataGridItemEntry.ItemsSource = clsItemsLogic.GetAllItems();
        }

        /// <summary>
        /// Edit item button edits the edits in the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItemButton_Click(object sender, RoutedEventArgs e)
        {
            //ItemModel itemModel = (ItemModel)DataGridItemEntry.CurrentItem;
            ItemModel itemModel = new ItemModel();
            itemModel.ItemCode = ItemCodeBox.Text;
            itemModel.Desc = ItemDescBox.Text;
            itemModel.Cost = Decimal.Parse(CostBox.Text);

            clsItemsLogic.UpdateItem(itemModel);
            DataGridItemEntry.ItemsSource = clsItemsLogic.GetAllItems();
        }

        /// <summary>
        /// delete button deletes the items in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            //ItemModel itemModel = (ItemModel)DataGridItemEntry.CurrentItem;
            ItemModel itemModel = new ItemModel();
            itemModel.ItemCode = ItemCodeBox.Text;
            itemModel.Desc = ItemDescBox.Text;
            itemModel.Cost = Decimal.Parse(CostBox.Text);

            clsItemsLogic.DeleteItem(itemModel);


            DataGridItemEntry.SelectionChanged -= selectionchanged;
            DataGridItemEntry.ItemsSource = null;
            DataGridItemEntry.ItemsSource = clsItemsLogic.GetAllItems();
            DataGridItemEntry.SelectionChanged += selectionchanged;
            
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


        private void selectionchanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            ItemModel itemModel = (ItemModel)dataGrid.SelectedItem;
            ItemCodeBox.Text = itemModel.ItemCode;
            ItemDescBox.Text = itemModel.Desc;
            CostBox.Text = itemModel.Cost.ToString();

           // int debug = 0;
        }

        private void Close_Item_Entry(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
        }
    }


}
