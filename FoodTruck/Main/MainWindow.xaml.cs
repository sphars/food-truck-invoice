﻿using FoodTruck.Main;
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
        private Invoice invoice;

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
            this.invoice = invoice;

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

            spEditPanel.IsEnabled = false;
            spEditPanel.Visibility = Visibility.Hidden;
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
            invoice = new Invoice();
            LoadInvoice();
        }

        /// <summary>
        /// This enables and shows the controls for the user to edit the Invoice.
        /// </summary>
        private void LoadInvoice() {
            if(invoice == null) {
                return;
            }

            spEditPanel.IsEnabled = true;
            spEditPanel.Visibility = Visibility.Visible;

            tbInvoiceNum.Text = invoice.InvoiceNum == -1 ? "TBD" : invoice.InvoiceNum.ToString();
            dpInvoiceDate.SelectedDate = invoice.InvoiceDate;

            LoadItems();
        }

        /// <summary>
        /// This method fills the ComboBox to allow the user to add LineItems to the invoice.
        /// </summary>
        private void LoadItems() {
            var items = clsMainLogic.GetAllItemDescs();
            cbItemList.ItemsSource = items;
            btnAddToInvoice.IsEnabled = true;
        }

        /// <summary>
        /// This event handler adds 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToInvoice_Click(object sender, RoutedEventArgs e) {

        }
    }
}
