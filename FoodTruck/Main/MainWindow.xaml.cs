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
        /// Use this constructor for the launch, or for when an invoice IS NOT being provided.
        /// The Search window should use the parameterized constructor.
        /// </summary>
        public MainWindow() {
            InitializeComponent();

            ResetForm();
        }

        public MainWindow(Invoice invoice) : this() {
            this.invoice = invoice;

            //LoadInvoice();
        }

        private void ResetForm() {
            btnClear.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDeleteInvoice.IsEnabled = false;
            btnEditInvoice.IsEnabled = false;

            btnCreateInvoice.IsEnabled = true;
            dtgLineItems.ItemsSource = null;

            spEditPanel.IsEnabled = false;
            spEditPanel.Visibility = Visibility.Hidden;
            tbInvoiceNum.Text = "TBD";
        }

        /// <summary>
        /// This event handler for the menu item opens an ItemEntry window in dialog mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInventoryOnClick(object sender, RoutedEventArgs e) {
            var window = new ItemEntry();
            window.ShowDialog();
        }

        /// <summary>
        /// This event handler for the menu item displays the Invoice Search window in dialog mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchInvoicesOnClick(object sender, RoutedEventArgs e) {
            // Passing delegate method to setInvoice as part of the constructor.
            var window = new InvoiceSearch();
            window.Show();
            this.Close();
        }
    }
}
