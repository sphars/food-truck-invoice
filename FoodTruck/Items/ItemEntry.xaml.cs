using FoodTruck.Items;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Reflection;


namespace FoodTruck
{
    /// <summary>
    /// Interaction logic for ItemEntry.xaml
    /// </summary>
    public partial class ItemEntry : Window
    {
        /// <summary>
        /// Used to access the database
        /// </summary>
        DataAccess db = new DataAccess();

        /// <summary>
        /// Object to hold a datasource
        /// </summary>
        public object DataSource { get; set; }

        /// <summary>
        /// This invoice was being displayed by the MainWindow.  When recreating MainWindow, pass it back.
        /// </summary>
        private Invoice storedInvoice;

        /// <summary>
        /// Constructor for ItemEntry window
        /// </summary>
        /// <param name="invoice">The currently selected invoice</param>
        public ItemEntry(Invoice invoice)
        {
            InitializeComponent();
            DataGridItemEntry.ItemsSource = clsItemsLogic.GetAllItems();
            storedInvoice = invoice;
        }

        /// <summary>
        /// Add button gives an option to add new items 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Edit item button edits the edits in the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ItemModel itemModel = (ItemModel)DataGridItemEntry.CurrentItem;
                ItemModel itemModel = new ItemModel();
                itemModel.ItemCode = ItemCodeBox.Text;
                itemModel.Desc = ItemDescBox.Text;
                itemModel.Cost = Decimal.Parse(CostBox.Text);

                clsItemsLogic.UpdateItem(itemModel);
                DataGridItemEntry.ItemsSource = clsItemsLogic.GetAllItems();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// delete button deletes the items in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// ID read only pulls a list that is read only not able to make edits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDreadOnlyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridItemEntry.ItemsSource = clsItemsLogic.GetAllItems();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles the changing of items from the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectionchanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dataGrid = (DataGrid)sender;
                if (dataGrid.SelectedItem == null)
                    return;

                ItemModel itemModel = (ItemModel)dataGrid.SelectedItem;
                ItemCodeBox.Text = itemModel.ItemCode;
                ItemDescBox.Text = itemModel.Desc;
                CostBox.Text = itemModel.Cost.ToString();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

            // int debug = 0;
        }

        /// <summary>
        /// Handles closing the item entry window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Item_Entry(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                var window = new MainWindow(storedInvoice);
                window.Show();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles clicking the back arrow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgArrowLeft_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// exception handler that shows the error
        /// </summary>
        /// <param name="sClass">the class</param>
        /// <param name="sMethod">the method</param>
        /// <param name="sMessage">the error message</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

    }
}
