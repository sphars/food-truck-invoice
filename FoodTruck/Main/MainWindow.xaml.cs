using FoodTruck.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodTruck {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        /// <summary>
        /// Invoice this window has created or Invoice passed by the search Window.
        /// </summary>
        private Invoice initialInvoice;

        /// <summary>
        /// This is the class that manages creating and editing an Invoice.
        /// </summary>
        private InvoiceManager invoiceManager;

        /// <summary>
        /// This private field indicates whether the window is creating or modifying an Invoice.
        /// </summary>
        private bool IsEditMode = false;

        /// <summary>
        /// This is so EnsureClose() doesn't prompt again.
        /// </summary>
        private bool CloseNow;

        /// <summary>
        /// Use this constructor for the launch, or for when an invoice IS NOT being provided.
        /// The Search window should use the parameterized constructor.
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            this.Closing += OnClosing;

            ResetWindow();
        }

        /// <summary>
        /// This constructor loads the MainWindow with a specific invoice.
        /// Such as when the Search window wants to display or allow the user to edit the invoice.
        /// </summary>
        /// <param name="invoice">Invoice object with a proper InvoiceNum pointing to the database.</param>
        public MainWindow(Invoice invoice) : this() {
            initialInvoice = invoice;
            invoiceManager = new InvoiceManager(invoice);
            LoadInvoice();
        }

        /// <summary>
        /// This method resets the appearance of the window to an initial state.
        /// </summary>
        private void ResetWindow() {
            IsEditMode = false;

            btnClear.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDeleteInvoice.IsEnabled = false;
            btnEditInvoice.IsEnabled = false;
            btnAddToInvoice.IsEnabled = false;
            cbItemList.ItemsSource = null;
            cbItemList.IsEnabled = false;

            btnCreateInvoice.IsEnabled = true;
            lvLineItems.ItemsSource = null;

            HideEditPanels();
            spTotalAmount.Visibility = Visibility.Hidden;

            tbInvoiceNum.Text = "TBD";
            dpInvoiceDate.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// This event handler for the menu item opens an ItemEntry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInventoryOnClick(object sender, RoutedEventArgs e) {
            bool closeNow = EnsureClose();
            if(closeNow) {
                var window = new ItemEntry();
                window.Show();
                this.Close();
            }
        }

        /// <summary>
        /// This event handler for the menu item displays the Invoice Search window in dialog mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchInvoicesOnClick(object sender, RoutedEventArgs e) {
            bool closeNow = EnsureClose();
            if(closeNow) {
                var window = new InvoiceSearch();
                window.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Event handler for the Create button's Click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e) {
            CreateInvoice();
            EditMode();
        }

        /// <summary>
        /// This method enables Edit Mode.
        /// It disables the Create, Edit, Delete buttons.
        /// </summary>
        private void EditMode() {
            IsEditMode = true;
            btnCreateInvoice.IsEnabled = false;
            btnDeleteInvoice.IsEnabled = false;
            btnEditInvoice.IsEnabled = false;
            btnClear.IsEnabled = true;
            btnSave.IsEnabled = true;
            cbItemList.IsEnabled = true;

            spInvoiceNumDate.IsEnabled = true;
            spAddItems.IsEnabled = true;

            LoadItems();
        }

        /// <summary>
        /// This event handler runs when the user is trying to close the window.  If the window is in edit mode, they're prompted before closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosing(object sender, CancelEventArgs e) {
            bool closeNow = EnsureClose();
            if(!closeNow) {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// If the window is in edit mode, prompt the user before losing their work.
        /// </summary>
        /// <returns>Returns true if the user wants to close the window, false otherwise.</returns>
        private bool EnsureClose() {
            if(CloseNow)
                return true;

            if(IsEditMode) {
                var result = MessageBox.Show("There are unsaved changes.  Are you sure you want to close this window?",
                    "Discard changes and close?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if(result == MessageBoxResult.OK) {
                    CloseNow = true;
                    return true;
                } else return false;
            }

            // If not in edit mode, close away.
            CloseNow = true;
            return true;
        }

        /// <summary>
        /// Creates a new invoice and allows the user to edit it.
        /// </summary>
        private void CreateInvoice() {
            initialInvoice = new Invoice();
            invoiceManager = new InvoiceManager(initialInvoice);
            LoadInvoice();
        }

        /// <summary>
        /// This enables and shows the controls for the user to edit the Invoice.
        /// </summary>
        private void LoadInvoice() {
            if(initialInvoice == null) {
                return;
            }

            ShowEditPanels();
            spTotalAmount.Visibility = Visibility.Visible;

            UpdateTotal();

            tbInvoiceNum.Text = initialInvoice.InvoiceNum == -1 ? "TBD" : initialInvoice.InvoiceNum.ToString();
            dpInvoiceDate.SelectedDate = initialInvoice.InvoiceDate;
        }

        private void UpdateTotal() {
            if(initialInvoice == null)
                tbTotal.Text = "$0.00";
            else tbTotal.Text = $"{initialInvoice.TotalCharge:C}";
        }

        /// <summary>
        /// This method only shows the panels, it doesn't enable them.
        /// </summary>
        private void ShowEditPanels() {
            spAddItems.Visibility = Visibility.Visible;
            spInvoiceNumDate.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Hides AND disables the panels.
        /// </summary>
        private void HideEditPanels() {
            spAddItems.IsEnabled = false;
            spAddItems.Visibility = Visibility.Hidden;
            spInvoiceNumDate.IsEnabled = false;
            spInvoiceNumDate.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// This method fills the ComboBox to allow the user to add LineItems to the invoice.
        /// </summary>
        private void LoadItems() {
            var items = invoiceManager.GetAllItemDescs();
            cbItemList.ItemsSource = items;
        }

        /// <summary>
        /// This event handler adds the selected item in cbItemList to the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToInvoice_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// This event handler is used to enable or disable btnAddToInvoice, depending on if the item is valid or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbItemList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // Button is enabled when the selected item is an ItemDesc object, disabled if false.
            btnAddToInvoice.IsEnabled = cbItemList.SelectedItem is ItemDesc;
        }
    }
}
