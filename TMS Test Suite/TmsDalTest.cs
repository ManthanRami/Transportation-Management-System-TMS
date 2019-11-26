﻿using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using TMS.Data;
using TMS.Exceptions;

namespace TMS_Test_Suite
{
    [TestClass]
    public class TmsDalTest
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TMSConnectionString"].ConnectionString;

        public void TruncateTable(string tableName)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                
                const string queryString = "TRUNCATE TABLE @table;";

                MySqlCommand query = new MySqlCommand(queryString, conn);
                query.Parameters.AddWithValue("@table", tableName);
                query.ExecuteNonQuery();

                conn.Close();
            }
        }

        [TestMethod]
        public void TestCreateUser()
        {
            TruncateTable("User");

            TmsDal dal = new TmsDal();

            User user = new User();
            user.Username = "admin";
            user.Password = "password";
            user.Email = "admin@test.com";
            user.FirstName = "Testing";
            user.LastName = "Testerson";
            user.Type = UserType.Admin;

            bool excepted = false;

            try
            {
                dal.CreateUser(user);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            Assert.IsFalse(excepted);
        }


        [TestMethod]
        public void TestGetUserID()
        {
            TmsDal dal = new TmsDal();

            bool excepted = false;

            try
            {
                dal.GetUserID("admin");
            }
            catch (UserNotExistsException)
            {
                excepted = true;
            }

            Assert.IsFalse(excepted);
        }

        [TestMethod]
        public void TestCreateCarrier()
        {
            Carrier carrier = new Carrier();
            carrier.DepotCity = City.Windsor;
            carrier.FtlAvailability = 100;
            carrier.LtlAvailability = 50;

            TmsDal dal = new TmsDal();

            bool excepted = false;

            try
            {
                carrier = dal.CreateCarrier(carrier);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            Assert.IsFalse(excepted);
            Assert.IsTrue(carrier.CarrierID > 0);
        }

        [TestMethod]
        public void TestGetCarrier()
        {
            Carrier carrier = new Carrier();
            carrier.DepotCity = City.Hamilton;
            carrier.FtlAvailability = 20;
            carrier.LtlAvailability = 13;

            TmsDal dal = new TmsDal();

            bool excepted = false;

            try
            {
                carrier = dal.CreateCarrier(carrier);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            try
            {
                carrier = dal.GetCarrier(carrier.CarrierID);
            }
            catch (CarrierNotExistsException)
            {
                excepted = true;
            }

            Assert.IsFalse(excepted);
        }

        [TestMethod]
        public void TestGetCarriers()
        {
            TmsDal dal = new TmsDal();

            List<Carrier> carriers = dal.GetCarriers();

            Assert.IsFalse(carriers.Count == 0);
        }

        [TestMethod]
        public void TestUpdateCarrier()
        {
            Carrier carrier = new Carrier();
            carrier.DepotCity = City.Windsor;
            carrier.LtlAvailability = 10;
            carrier.FtlAvailability = 22;

            TmsDal dal = new TmsDal();

            bool excepted = false;

            // Insert carrier
            try
            {
                carrier = dal.CreateCarrier(carrier);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            // Update values
            carrier.DepotCity = City.London;

            try
            {
                carrier = dal.UpdateCarrier(carrier.CarrierID, carrier);
            }
            catch (CouldNotUpdateException)
            {
                excepted = true;
            }

            Assert.IsFalse(excepted);
        }

        [TestMethod]
        public void TestSetFtlRate()
        {
            Carrier carrier = new Carrier();
            carrier.DepotCity = City.Oshawa;
            carrier.LtlAvailability = 8;
            carrier.FtlAvailability = 18;

            TmsDal dal = new TmsDal();

            bool excepted = false;

            // Insert carrier
            try
            {
                carrier = dal.CreateCarrier(carrier);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            // Try inserting a new ftl rate
            try
            {
                dal.SetFtlRate(carrier.CarrierID, (float) 250.89);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            // Then try updating the existing rate
            try
            {
                dal.SetFtlRate(carrier.CarrierID, (float) 180.20);
            }
            catch (CouldNotUpdateException)
            {
                excepted = true;
            }

            Assert.IsFalse(excepted);
        }

        [TestMethod]
        public void TestSetLtlRate()
        {
            Carrier carrier = new Carrier();
            carrier.DepotCity = City.Ottawa;
            carrier.LtlAvailability = 9;
            carrier.FtlAvailability = 19;

            TmsDal dal = new TmsDal();

            bool excepted = false;

            // Insert carrier
            try
            {
                carrier = dal.CreateCarrier(carrier);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            // Try inserting a new ltl rate
            try
            {
                dal.SetLtlRate(carrier.CarrierID, (float) 250.89);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            // Then try updating the existing rate
            try
            {
                dal.SetLtlRate(carrier.CarrierID, (float) 180.20);
            }
            catch (CouldNotUpdateException)
            {
                excepted = true;
            }

            Assert.IsFalse(excepted);
        }

        [TestMethod]
        public void TestSetReeferCharge()
        {
            Carrier carrier = new Carrier();
            carrier.DepotCity = City.Ottawa;
            carrier.LtlAvailability = 9;
            carrier.FtlAvailability = 19;

            TmsDal dal = new TmsDal();

            bool excepted = false;

            // Insert carrier
            try
            {
                carrier = dal.CreateCarrier(carrier);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            // Try inserting a new ltl rate
            try
            {
                dal.SetReeferCharge(carrier.CarrierID, (float) 49.99);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            // Then try updating the existing rate
            try
            {
                dal.SetReeferCharge(carrier.CarrierID, (float) 44.99);
            }
            catch (CouldNotUpdateException)
            {
                excepted = true;
            }

            Assert.IsFalse(excepted);
        }

        [TestMethod]
        public void TestCreateCustomer()
        {
            Customer customer = new Customer();
            customer.Name = "Test";

            TmsDal dal = new TmsDal();

            bool excepted = false;

            try
            {
                customer = dal.CreateCustomer(customer);
            }
            catch (CouldNotInsertException)
            {
                excepted = true;
            }

            Assert.IsFalse(excepted);
        }
    }
}
