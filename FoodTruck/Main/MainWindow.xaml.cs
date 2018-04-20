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

        private bool IsEditMode = false;

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

            LoadInvoice();
        }

        private void ResetForm() {
            IsEditMode = false;
            this.Closing -= EnsureClose;

            btnClear.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDeleteInvoice.IsEnabled = false;
            btnEditInvoice.IsEnabled = false;

            btnCreateInvoice.IsEnabled = true;
            lvLineItems.ItemsSource = null;

            spEditPanel.IsEnabled = false;
            spEditPanel.Visibility = Visibility.Hidden;
            tbInvoiceNum.Text = "TBD";
            dpInvoiceDate.SelectedDate = DateTime.Now;
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
            // If the window is in edit mode, the event handler would prevent a close.
            this.Closed += delegate (object s, EventArgs ev) {
                var window = new InvoiceSearch();
                window.Show();
            };
            this.Close();
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

            this.Closing += EnsureClose;
        }

        private void EnsureClose(object sender, CancelEventArgs e) {
            if(IsEditMode) {
                var result = MessageBox.Show("There are unsaved changes.  Are you sure you want to close this window?",
                    "Discard changes and close?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Cancel || result == MessageBoxResult.None) {
                    e.Cancel = true;
                }
            }
        }

        private void CreateInvoice() {
            invoice = new Invoice();
            LoadInvoice();
        }

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

        private void LoadItems() {

        }
    }
}
