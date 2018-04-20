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
        /// The object for the business logic
        /// </summary>
        private InvoiceData id = new InvoiceData();

        /// <summary>
        /// Constructor for the Search Window.
        /// </summary>
        public InvoiceSearch() {
            InitializeComponent();

            //Populate the dropdown boxes
            GetInvoiceNumbers();
            GetInvoiceDates();
            GetInvoiceAmounts();
        }

        #region Methods
        
        /// <summary>
        /// Resets the window to default state. Clears filters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Selects the chosen invoice from the datagrid and sends it back to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e) {

            // <Set or get selectedInvoice here>
            selectedInvoice = null; // null for now
            var window = new MainWindow(selectedInvoice);
            window.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the changing of the selected invoice number combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        /// <summary>
        /// Handles the changing of the selected invoice date combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        /// <summary>
        /// Handles the changing of the selected invoice date combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceTotal_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        /// <summary>
        /// Gets a list of invoice numbers and populates combobox
        /// </summary>
        private void GetInvoiceNumbers()
        {
            List<string> lInvoiceNums = new List<string>();
            lInvoiceNums = id.GetInvoiceNums();

            cboInvoiceNumber.ItemsSource = lInvoiceNums;
        }

        /// <summary>
        /// Gets a list of invoice dates and populates combobox
        /// </summary>
        private void GetInvoiceDates()
        {
            List<string> lInvoiceDates = new List<string>();
            lInvoiceDates = id.GetInvoiceDates();

            cboInvoiceDate.ItemsSource = lInvoiceDates;
        }

        /// <summary>
        /// Gets a list of invoice amounts and populates combobox
        /// </summary>
        private void GetInvoiceAmounts()
        {
            List<string> lInvoiceAmounts = new List<string>();
            lInvoiceAmounts = id.GetInvoiceAmounts();

            cboInvoiceTotal.ItemsSource = lInvoiceAmounts;
        }

        #endregion
    }
}
