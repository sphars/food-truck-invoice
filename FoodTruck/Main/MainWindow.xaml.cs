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
            LoadInvoice();
        }

        #region EventHandlers

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
        /// This event handler adds the selected item in cbItemList to the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToInvoice_Click(object sender, RoutedEventArgs e) {
            if(cbItemList.SelectedItem is ItemDesc item) {
                invoiceManager.AddLineItem(item);
                UpdateLineItems();
            }
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

        /// <summary>
        /// This event handler prompts the user about deleting the invoice.  If the user clicks ok, it deletes the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e) {
            var result = MessageBox.Show("Are you sure you want to delete this invoice?",
                "Permanently delete invoice?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if(result == MessageBoxResult.OK) {
                DeleteInvoice();
                ResetWindow();
            }
        }

        private void btnEditInvoice_Click(object sender, RoutedEventArgs e) {
            EditMode();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            invoiceManager.SetInvoiceDate(dpInvoiceDate.SelectedDate ?? initialInvoice.InvoiceDate);
            invoiceManager.SaveInvoice();
            initialInvoice = invoiceManager.CurrentInvoice;
            ResetWindow();
            LoadInvoice();
        }

        private void btnRevert_Click(object sender, RoutedEventArgs e) {
            ResetWindow();
            if(initialInvoice != null) {
                LoadInvoice();
            }
        }

        private void RemoveLineItem_Click(object sender, RoutedEventArgs e) {
            Console.WriteLine("Click of remove button: " + sender.ToString() + " Events: " + e.ToString());
        }

        #endregion

        private void UpdateLineItems() {
            dgLineItems.ItemsSource = null;
            dgLineItems.ItemsSource = invoiceManager.GetLineItems();
            UpdateTotal();
        }

        private void DeleteInvoice() {
            if(invoiceManager != null) {
                invoiceManager.DeleteInvoice();
            }
            initialInvoice = null;
        }

        /// <summary>
        /// This method resets the appearance of the window to an initial state.
        /// </summary>
        private void ResetWindow() {
            IsEditMode = false;
            invoiceManager = null;

            btnRevert.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDeleteInvoice.IsEnabled = false;
            btnEditInvoice.IsEnabled = false;
            btnAddToInvoice.IsEnabled = false;
            cbItemList.ItemsSource = null;
            cbItemList.IsEnabled = false;

            btnCreateInvoice.IsEnabled = true;
            dgLineItems.ItemsSource = null;
            dgLineItems.IsEnabled = false;

            HideEditPanels();
            spTotalAmount.Visibility = Visibility.Hidden;

            tbInvoiceNum.Text = "TBD";
            dpInvoiceDate.SelectedDate = DateTime.Now;
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
            btnRevert.IsEnabled = true;
            btnSave.IsEnabled = true;
            cbItemList.IsEnabled = true;

            spInvoiceNumDate.IsEnabled = true;
            spAddItems.IsEnabled = true;

            LoadItems();
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
            initialInvoice = null;
            invoiceManager = new InvoiceManager(new Invoice());
            LoadInvoice();
        }

        /// <summary>
        /// This enables and shows the controls for the user to edit the Invoice.
        /// </summary>
        private void LoadInvoice() {

            invoiceManager = new InvoiceManager(initialInvoice);

            if(initialInvoice != null) {
                btnDeleteInvoice.IsEnabled = true;
                btnEditInvoice.IsEnabled = true;
                dpInvoiceDate.SelectedDate = initialInvoice.InvoiceDate;
            } else {
                dpInvoiceDate.SelectedDate = DateTime.Now;
            }

            ShowEditPanels();
            spTotalAmount.Visibility = Visibility.Visible;

            ShowLineItems();
            UpdateTotal();

            if(invoiceManager.CurrentInvoice.InvoiceNum == -1)
                tbInvoiceNum.Text = "TBD";
            else
                tbInvoiceNum.Text = invoiceManager.CurrentInvoice.InvoiceNum.ToString();
        }

        private void ShowLineItems() {
            var lineItems = invoiceManager.GetLineItems();
            dgLineItems.ItemsSource = lineItems;
            dgLineItems.IsEnabled = true;
        }

        private void UpdateTotal() {
            if(initialInvoice == null && invoiceManager == null) {
                tbTotal.Text = "$0.00";
            } else if(initialInvoice == null && invoiceManager != null) {
                tbTotal.Text = $"{invoiceManager.CurrentInvoice.TotalCharge:C}";
            } else if(initialInvoice != null && invoiceManager == null) {
                tbTotal.Text = $"{initialInvoice.TotalCharge:C}";
            } else {
                tbTotal.Text = $"{invoiceManager.CurrentInvoice.TotalCharge:C}";
            }
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
            var items = InvoiceManager.GetAllItemDescs();
            cbItemList.ItemsSource = items;
        }
    }
}
