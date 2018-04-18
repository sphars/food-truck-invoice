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

namespace FoodTruck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object invoice;

        //Matches delegate declaration for ReturnInvoice
        public void setInvoice(object invoice)
        {
            this.invoice = invoice;
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This event handler for the menu item opens an ItemEntry window in dialog mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInventoryOnClick(object sender, RoutedEventArgs e)
        {
            var window = new ItemEntry();
            window.ShowDialog();
        }

        /// <summary>
        /// This event handler for the menu item displays the Invoice Search window in dialog mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchInvoicesOnClick(object sender, RoutedEventArgs e)
        {
            // Passing delegate method to setInvoice as part of the constructor.
            var window = new InvoiceSearch(setInvoice);
            window.ShowDialog();
            // When it returns, the invoice object should be set now, thanks to the delegate passed into the constructor.
        }
    }
}
