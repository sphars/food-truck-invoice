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
        /// This is how we will return the invoice to the MainWindow.  Call callback() with the invoice.
        /// </summary>
        private ReturnInvoice callback;

        /// <summary>
        /// This field is the invoice selected in the UI that will be returned to MainWindow.
        /// </summary>
        private object selectedInvoice;

        /// <summary>
        /// This constructor accepts a callback delegate that saves the invoice in MainWindow.
        /// </summary>
        /// <param name="callback"></param>
        public InvoiceSearch(ReturnInvoice callback) {
            InitializeComponent();
            this.callback = callback;
        }

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
            selectedInvoice = null;

            // Using the delegate callback, send it back to the MainWindow:
            callback(selectedInvoice);
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
    }
}
