﻿using System;
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
using TMS.Data;
namespace TMS.Pages_UI.Pages_Buyer
{
    /// <summary>
    /// Interaction logic for CustomerManagement.xaml
    /// </summary>
    public partial class CustomerManagement : Page
    {

        // Get a list of current customers
        List<Customer> ccl = new List<Customer>();

        // List for temporary storage of newly fetched customers
        List<Customer> ncl = new List<Customer>();

        TmsDal td = new TmsDal();
        public CustomerManagement()
        {
            InitializeComponent();
            btnEdit_Customer.IsEnabled = false;
        }

        private void LoadCurrentCustomerData()
        {
            ccl = td.GetCustomers();
            CustomerData.ItemsSource = ccl;
        }

        private void btnCurrent_Customers_Click(object sender, RoutedEventArgs e)
        {
            LoadCurrentCustomerData();
        }

        private void CurrentCustomerData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEdit_Customer.IsEnabled = true;
        }
    }
}