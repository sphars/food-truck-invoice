using FoodTruck.Main;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace FoodTruck {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        #region Fields
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
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for a humble Invoice program.
        /// Use this constructor for the launch, or for when an invoice IS NOT being provided.
        /// The Search window should use the parameterized constructor.
        /// </summary>
        public MainWindow() {
            try {
                InitializeComponent();
                this.Closing += OnClosing;

                ResetWindow();
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This constructor loads the MainWindow with a specific invoice.
        /// Such as when the Search window wants to display or allow the user to edit the invoice.
        /// </summary>
        /// <param name="invoice">Invoice object with a proper InvoiceNum pointing to the database.</param>
        public MainWindow(Invoice invoice) : this() {
            try {
                initialInvoice = invoice;
                LoadInvoice();
            } catch(Exception) {
                throw;
            }
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// This event handler for the menu item opens an ItemEntry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInventoryOnClick(object sender, RoutedEventArgs e) {
            try {
                bool closeNow = EnsureClose();
                if(closeNow) {
                    var window = new ItemEntry();
                    window.Show();
                    this.Close();
                }
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This event handler for the menu item displays the Invoice Search window in dialog mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchInvoicesOnClick(object sender, RoutedEventArgs e) {
            try {
                bool closeNow = EnsureClose();
                if(closeNow) {
                    var window = new InvoiceSearch();
                    window.Show();
                    this.Close();
                }
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Event handler for the Create Invoice button's Click event.
        /// Creates a new invoice and enters the Window into Edit Mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e) {
            try {
                CreateInvoice();
                EditMode();
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This event handler runs when the user is trying to close the window.  If the window is in edit mode, they're prompted before closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosing(object sender, CancelEventArgs e) {
            try {
                bool closeNow = EnsureClose();
                if(!closeNow) {
                    e.Cancel = true;
                }
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This event handler adds the selected item in cbItemList to the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToInvoice_Click(object sender, RoutedEventArgs e) {
            try {
                if(cbItemList.SelectedItem is ItemDesc item) {
                    invoiceManager.AddLineItem(item);
                    UpdateLineItems();
                }
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This event handler is used to enable or disable btnAddToInvoice, depending on if the item is valid or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbItemList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                // Button is enabled when the selected item is an ItemDesc object, disabled if false.
                btnAddToInvoice.IsEnabled = cbItemList.SelectedItem is ItemDesc;
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This event handler prompts the user about deleting the displayed invoice.  If the user clicks ok, it deletes the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e) {
            try {
                var result = MessageBox.Show("Are you sure you want to delete this invoice?",
                        "Permanently delete invoice?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if(result == MessageBoxResult.OK) {
                    DeleteInvoice();
                    ResetWindow();
                }
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This event handler allows the user to edit the display invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e) {
            try {
                EditMode();
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        
        /// <summary>
        /// This event handler saves to the database the changes to the current invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e) {
            try {
                invoiceManager.SetInvoiceDate(dpInvoiceDate.SelectedDate ?? initialInvoice.InvoiceDate);
                invoiceManager.SaveInvoice();
                initialInvoice = invoiceManager.CurrentInvoice;
                ResetWindow();
                LoadInvoice();
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This event handler undoes any changes the user has done (but not saved) to the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevert_Click(object sender, RoutedEventArgs e) {
            try {
                ResetWindow();
                if(initialInvoice != null) {
                    LoadInvoice();
                }
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This event handler is called by the Remove buttons in the DataGrid.  It removes the associated LineItem from the Invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveLineItem_Click(object sender, RoutedEventArgs e) {
            try {
                var button = (Button)sender;
                ItemDesc lineItem = (ItemDesc)button.DataContext;

                RemoveLineItem(lineItem);
            } catch(Exception ex) {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        
        #endregion

        /// <summary>
        /// This method updates the displayed list of LineItems and the Running Total.
        /// </summary>
        private void UpdateLineItems() {
            try {
                dgLineItems.ItemsSource = null;
                dgLineItems.ItemsSource = invoiceManager.GetLineItems();
                UpdateTotal();
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This method does the actual deleting of the displayed invoice.
        /// </summary>
        private void DeleteInvoice() {
            try {
                if(invoiceManager != null) {
                    invoiceManager.DeleteInvoice();
                }
                initialInvoice = null;
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This method resets the appearance of the window to an initial state.
        /// </summary>
        private void ResetWindow() {
            try {
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
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This method enables Edit Mode.
        /// It disables the Create, Edit, Delete buttons.
        /// It enables the Revert and Save buttons.
        /// </summary>
        private void EditMode() {
            try {
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
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// If the window is in edit mode, prompt the user before losing their work.
        /// </summary>
        /// <returns>Returns true if the user wants to close the window, false otherwise.</returns>
        private bool EnsureClose() {

            // In case this method is called again by the Closing event handler, short circuit.
            if(CloseNow)
                return true;

            try {
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
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// Creates a new invoice and allows the user to edit it.
        /// </summary>
        private void CreateInvoice() {
            try {
                initialInvoice = null;
                invoiceManager = new InvoiceManager(new Invoice());
                LoadInvoice();
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This enables and shows the controls for the user to edit the Invoice.
        /// </summary>
        private void LoadInvoice() {
            try {
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

                // If the invoice is completely new, display TBD instead of -1.
                if(invoiceManager.CurrentInvoice.InvoiceNum == -1)
                    tbInvoiceNum.Text = "TBD";
                else
                    tbInvoiceNum.Text = invoiceManager.CurrentInvoice.InvoiceNum.ToString();
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This method fills the DataGrid with the LineItems from the InvoiceManager.
        /// </summary>
        private void ShowLineItems() {
            try {
                var lineItems = invoiceManager.GetLineItems();
                dgLineItems.ItemsSource = lineItems;
                dgLineItems.IsEnabled = true;
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This method updates the running total of the displayed Invoice.
        /// </summary>
        private void UpdateTotal() {
            try {
                if(initialInvoice == null && invoiceManager == null) {
                    tbTotal.Text = "$0.00";
                } else if(initialInvoice == null && invoiceManager != null) {
                    tbTotal.Text = $"{invoiceManager.CurrentInvoice.TotalCharge:C}";
                } else if(initialInvoice != null && invoiceManager == null) {
                    tbTotal.Text = $"{initialInvoice.TotalCharge:C}";
                } else {
                    tbTotal.Text = $"{invoiceManager.CurrentInvoice.TotalCharge:C}";
                }
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This method displays the edit panels for the Invoice Date and adding line items.
        /// It only shows the panels, but doesn't enable them.
        /// </summary>
        private void ShowEditPanels() {
            try {
                spAddItems.Visibility = Visibility.Visible;
                spInvoiceNumDate.Visibility = Visibility.Visible;
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This method hides AND disables the edit panels.
        /// </summary>
        private void HideEditPanels() {
            try {
                spAddItems.IsEnabled = false;
                spAddItems.Visibility = Visibility.Hidden;
                spInvoiceNumDate.IsEnabled = false;
                spInvoiceNumDate.Visibility = Visibility.Hidden;
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This method fills the ComboBox to allow the user to add LineItems to the invoice.
        /// </summary>
        private void LoadItems() {
            try {
                var items = InvoiceManager.GetAllItemDescs();
                cbItemList.ItemsSource = items;
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// This method removes the specified lineItem from the Invoice and calls the method to update the display of LineItems.
        /// </summary>
        /// <param name="lineItem">LineItem to remove by InvoiceManager</param>
        private void RemoveLineItem(ItemDesc lineItem) {
            try {
                invoiceManager.RemoveLineItem(lineItem);
                UpdateLineItems();
            } catch(Exception) {
                throw;
            }
        }

        /// <summary>
        /// From Assignment 6P2, this method displays an exception in a Messagebox.  If there's an exception with that, it writes it to a file.
        /// </summary>
        /// <param name="sClass">The class where the exception occurred</param>
        /// <param name="sMethod">The method where the exception occurred</param>
        /// <param name="sMessage">The exception's message</param>
        private void HandleError(string sClass, string sMethod, string sMessage) {
            try {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            } catch(Exception ex) {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
