﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace TMS
{

    //=======================================================================================================================
    /// <summary>
    /// This is the Logging Screen for the user to by which TMS system can identify the user and navigate the user throught 
    /// the Application according to their roles 
    /// </summary>
    //=======================================================================================================================
    public partial class MainWindow : Window
    {

        protected string username;
        protected string password;
        protected bool newPageLoaded;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        //=======================================================================================================================
        /// <summary>
        /// Description :   btnLogin_Click function will call two different function called ValidateFields & DisplayScreen
        ///                 which allows user to navugate through the TMS application
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button.</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        public void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string whichScreen = ""; 
            whichScreen=ValidateFields(UserNameBox, PasswordBox);
            if(whichScreen!="")
            {
              DisplayScreen(whichScreen);
            }         
        }
        //=======================================================================================================================
        /// <summary>
        /// ValidateFields  : This function will takes value from the user login screen and validate all the fields checks 
        ///                   for if the field is empty or not also check for appropraite user name.
        /// </summary>
        /// <param name="name"> TextBox on the Login screen which is used to get username input from the current user which 
        ///                     helps to identify the user role</param>
        /// <param name="pass"> PasswordBox on the Login screen which is used to get password for the current user account 
        ///                     to login in</param>
        /// <returns></returns>
        //========================================================================================================================
        public string ValidateFields(TextBox name, PasswordBox pass)
        {
            username = name.Text;
            password = pass.Password;

            if (username == "" && password == "")
            {
                username = "";
                name.Text="";
                pass.Password = "";
                Error.Content = "Please Enter Usename and Password !!";
            }
            else if (username == "")
            {
                username = "";
                name.Text = "";
                pass.Password = "";
                Error.Content = "Please Enter the Username !!";
            }
            else if (password == "")
            {
                username = "";
                name.Text = "";
                pass.Password = "";
                Error.Content = "Please Enter the Password !!";
            }            
            else if(username!="admin"&&username!="buyer"&&username!="planner")
            {
                username = "";
                name.Text = "";
                pass.Password = "";
                Error.Content = "Please Enter Appropriate User Name !!";              
            }
            
            return username;
            
        }
        //=====================================================================================================================
        /// <summary>
        /// DisplayScreen : This funtion will identify the current user and Navigate user according to his/her role in TMS.
        /// </summary>
        /// <param name="Screen"> Name of the Screen need to be display after the login Screen.</param>
        //======================================================================================================================
        public int DisplayScreen(string Screen)
        {
            int userType = 0;

            if (Screen == "admin")
            {
                AdminWindow admin = new AdminWindow();
                admin.Show();
                newPageLoaded = true;
                userType = 1;
            }
            else if (Screen == "buyer")
            {
                BuyerWindow buyer = new BuyerWindow();
                buyer.Show();
                newPageLoaded = true;
                userType = 2;
            }
            else if (Screen == "planner")
            {
                PlannerWindow planner = new PlannerWindow();
                planner.Show();
                newPageLoaded = true;
                userType = 3;
            }
            if (newPageLoaded == true)
            {
                this.Close();
            }
            return userType;
        }
    }
}
