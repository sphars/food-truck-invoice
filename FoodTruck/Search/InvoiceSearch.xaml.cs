using FoodTruck.Search;
using System;
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

namespace FoodTruck {
    /// <summary>
    /// Interaction logic for InvoiceSearch.xaml
    /// </summary>
    public partial class InvoiceSearch : Window {
        /// <summary>
        /// This field is the invoice selected in the UI that will be returned to MainWindow.
        /// </summary>
        private Invoice selectedInvoice;

        /// <summary>
        /// Constructor for the Search Window.
        /// </summary>
        public InvoiceSearch() {
            InitializeComponent();

            ResetForm();            
        }

        #region Methods

        /// <summary>
        /// Resets the window to the initial state, populating the datagrid with all invoices
        /// </summary>
        private void ResetForm()
        {
            selectedInvoice = null;
            btnSelectInvoice.IsEnabled = false;
            lblMessage.Content = "";

            dtgInvoices.ItemsSource = null;
            dtgInvoices.Items.Clear();

            //Fill the grid with every invoice
            dtgInvoices.ItemsSource = clsSearchLogic.GetAllInvoices();

            //Populate the comboboxes
            GetInvoiceNumbers();
            GetInvoiceDates();
            GetInvoiceAmounts();

            UpdateInvoiceGrid();
        }

        private void UpdateInvoiceGrid()
        {
            if (dtgInvoices.Items.IsEmpty)
                lblMessage.Content = "There are no invoices to be displayed. Please adjust your filters.";
            else
            {
                dtgInvoices.Items.Refresh();
                lblMessage.Content = "";
            }
        }
        
        /// <summary>
        /// Resets the window to default state. Clears filters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e) {
            ResetForm();
        }

        /// <summary>
        /// Selects the chosen invoice from the datagrid and sends it back to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            // Set the selected invoice
            selectedInvoice = (Invoice)dtgInvoices.SelectedItem;
            if(selectedInvoice != null)
            {
                var window = new MainWindow(selectedInvoice);
                window.Show();
                this.Close();
            }            
        }

        /// <summary>
        /// Handles the changing of the selected invoice number combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateInvoiceGrid();
        }

        /// <summary>
        /// Handles the changing of the selected invoice date combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateInvoiceGrid();
        }

        /// <summary>
        /// Handles the changing of the selected invoice date combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceTotal_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateInvoiceGrid();
        }

        /// <summary>
        /// Gets a list of invoice numbers and populates combobox
        /// </summary>
        private void GetInvoiceNumbers()
        {
            cboInvoiceNumber.ItemsSource = null;
            cboInvoiceNumber.Items.Clear();
            cboInvoiceNumber.ItemsSource = clsSearchLogic.GetInvoiceNumbers();
        }

        /// <summary>
        /// Gets a list of invoice dates and populates combobox
        /// </summary>
        private void GetInvoiceDates()
        {
            cboInvoiceDate.ItemsSource = null;
            cboInvoiceDate.Items.Clear();
            cboInvoiceDate.ItemsSource = clsSearchLogic.GetInvoiceDates();
        }

        /// <summary>
        /// Gets a list of invoice total charge amounts and populates combobox
        /// </summary>
        private void GetInvoiceAmounts()
        {
            cboInvoiceTotal.ItemsSource = null;
            cboInvoiceTotal.Items.Clear();
            cboInvoiceTotal.ItemsSource = clsSearchLogic.GetTotalCharges();
        }

        /// <summary>
        /// Handles the selection of the item in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSelectInvoice.IsEnabled = true;
        }

        #endregion

    }
}
