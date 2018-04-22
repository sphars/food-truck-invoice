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
        /// The list of all the invoices from the database
        /// </summary>
        private List<Invoice> lInvoices;

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
            lInvoices = clsSearchLogic.GetAllInvoices();

            //UpdateInvoiceGrid();
            dtgInvoices.ItemsSource = lInvoices;

            if (dtgInvoices.Items.IsEmpty)
                lblMessage.Content = "There are no invoices to display. Check the database is correct.";

            //Populate the comboboxes
            FillComboboxes();
        }

        /// <summary>
        /// Update the datagrid of invoices
        /// </summary>
        private void UpdateInvoiceGrid(object sender, SelectionChangedEventArgs e)
        {
            lblMessage.Content = "";

            //an enumerable list of all invoices
            IEnumerable<Invoice> filtered = lInvoices;

            //filter the list of invoices based on the selected dropdown box
            if (cboInvoiceNumber.SelectedItem != null)
                filtered = filtered.Where(a => a.InvoiceNum == (int)cboInvoiceNumber.SelectedItem);
            if (cboInvoiceDate.SelectedItem != null)
                filtered = filtered.Where(a => a.InvoiceDate.Date == (DateTime)cboInvoiceDate.SelectedItem);
            if (cboInvoiceTotal.SelectedItem != null)
                filtered = filtered.Where(a => a.TotalCharge == (decimal)cboInvoiceTotal.SelectedItem);

            //set the datagrid to the filtered list
            dtgInvoices.ItemsSource = filtered.ToList();

            //refresh the datagrid
            dtgInvoices.Items.Refresh();

            //check if there was any invoice returned
            if (dtgInvoices.Items.IsEmpty)
                lblMessage.Content = "There are no invoices to be displayed. Please adjust your filters.";
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
            //selectedInvoice = (Invoice)dtgInvoices.SelectedItem;
            if(selectedInvoice != null)
            {
                var window = new MainWindow(selectedInvoice);
                window.Show();
                this.Close();
            }            
        }

        /// <summary>
        /// Handles the selection of the item in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSelectInvoice.IsEnabled = true;
            selectedInvoice = (Invoice)dtgInvoices.SelectedItem;
        }

        /// <summary>
        /// Clears the comboboxes and fills them with data from the database
        /// </summary>
        private void FillComboboxes()
        {
            cboInvoiceNumber.ItemsSource = null;
            cboInvoiceNumber.Items.Clear();
            cboInvoiceNumber.ItemsSource = clsSearchLogic.GetInvoiceNumbers();

            cboInvoiceDate.ItemsSource = null;
            cboInvoiceDate.Items.Clear();
            cboInvoiceDate.ItemsSource = clsSearchLogic.GetInvoiceDates();

            cboInvoiceTotal.ItemsSource = null;
            cboInvoiceTotal.Items.Clear();
            cboInvoiceTotal.ItemsSource = clsSearchLogic.GetTotalCharges();
        }

        /// <summary>
        /// Handles when the user chooses to close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (selectedInvoice == null)
            {
                var window = new MainWindow();
                window.Show();
            }
            else return;
        }

        #endregion

    }
}
