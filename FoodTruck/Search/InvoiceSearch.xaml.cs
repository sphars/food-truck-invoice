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
using System.Reflection;

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
        /// If the MainWindow was displaying an invoice, this field allows the MainWindow to display it again on recreate.
        /// </summary>
        private Invoice previousInvoice;

        /// <summary>
        /// This field is used by the Select Invoice button to tell the Closing event to use selectedInvoice.
        /// </summary>
        private bool loadSelectedInvoice;

        /// <summary>
        /// Constructor for the Search Window.
        /// </summary>
        /// <param name="previousInvoice">Invoice that MainWindow was displaying before the open of this window.</param>
        public InvoiceSearch(Invoice previousInvoice) {
            InitializeComponent();
            this.previousInvoice = previousInvoice;
            ResetForm();
        }

        #region Methods

        /// <summary>
        /// Resets the window to the initial state, populating the datagrid with all invoices
        /// </summary>
        private void ResetForm()
        {
            try
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
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Update the datagrid of invoices
        /// </summary>
        private void UpdateInvoiceGrid(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Resets the window to default state. Clears filters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e) {
            try
            {
                ResetForm();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Selects the chosen invoice from the datagrid and sends it back to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the selected invoice
                //selectedInvoice = (Invoice)dtgInvoices.SelectedItem;
                if (selectedInvoice != null)
                {
                    loadSelectedInvoice = true;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Handles the selection of the item in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnSelectInvoice.IsEnabled = true;
                selectedInvoice = (Invoice)dtgInvoices.SelectedItem;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Clears the comboboxes and fills them with data from the database
        /// </summary>
        private void FillComboboxes()
        {
            try
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
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles when the user chooses to close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (loadSelectedInvoice)
                {
                    var window = new MainWindow(selectedInvoice);
                    window.Show();
                }
                else
                {
                    var window = new MainWindow(previousInvoice);
                    window.Show();
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles the left arrow to close the window
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

        #endregion

    }
}
