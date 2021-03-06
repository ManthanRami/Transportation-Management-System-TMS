﻿//File          : TripsTest.cs
//Project       : TMS Software Quality Project
//Course        : SENG2020 Software Quality
//Programmer    : David Obeda
//ID            : 8031148
//Summary       : Test File for backend trip logic classes

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMS;
using TMS.Data;

namespace TMS_Test_Suite
{
    [TestClass]
    public class TripsTest
    {
        [TestMethod]
        public void CityListValid()
        {
            TmsDal CityData = new TmsDal();
            List<Trip> CityInfo = CityData.GetTrips();
            CitiesData[] CityList = new CitiesData[CityInfo.Count];
            int indexNum = 0;
            foreach (Trip City in CityInfo)
            {
                CityList[indexNum] = new CitiesData(indexNum, City.Distance, City.TravelTime);
                indexNum++;
            }
            //Validate KMs
            Assert.AreEqual(CityList[0].EastKM, 191);
            Assert.AreEqual(CityList[1].EastKM, 128);
            Assert.AreEqual(CityList[2].EastKM, 68);
            Assert.AreEqual(CityList[3].EastKM, 60);
            Assert.AreEqual(CityList[4].EastKM, 134);
            Assert.AreEqual(CityList[5].EastKM, 82);
            Assert.AreEqual(CityList[6].EastKM, 196);

            //Validate Time
            Assert.AreEqual(CityList[0].EastMinutes, 150);
            Assert.AreEqual(CityList[1].EastMinutes, 105);
            Assert.AreEqual(CityList[2].EastMinutes, 75);
            Assert.AreEqual(CityList[3].EastMinutes, 78);
            Assert.AreEqual(CityList[4].EastMinutes, 99);
            Assert.AreEqual(CityList[5].EastMinutes, 72);
            Assert.AreEqual(CityList[6].EastMinutes, 150);
        }
        [TestMethod]
        public void TripValid()
        {
            TmsDal CityData = new TmsDal();
            List<Trip> CityInfo = CityData.GetTrips();
            CitiesData[] CityList = new CitiesData[CityInfo.Count];
            int indexNum = 0;
            foreach (Trip City in CityInfo)
            {
                CityList[indexNum] = new CitiesData(indexNum, City.Distance, City.TravelTime);
                indexNum++;
            }

            TripLogic TripTest = new TripLogic(0, 0, 3, CityList);
            Assert.AreEqual(TripTest.BillDays, 0);
            Assert.AreEqual(TripTest.distance, 387);

            TripTest = new TripLogic(0, 0, 7, CityList);
            Assert.AreEqual(TripTest.BillDays, 1);
            Assert.AreEqual(TripTest.currentCity, 1);
            Assert.AreEqual(TripTest.distance, 859);
            TripTest.AddTime(CityList);//day 0, in transit
            Assert.AreEqual(TripTest.direction, 1);
            TripTest.AddTime(CityList);//day 1, arrived.
            Assert.AreEqual(TripTest.direction, 0);

            TripTest = new TripLogic(10, 1, 5, CityList);
            Assert.AreEqual(TripTest.BillDays, 1);
            Assert.AreEqual(TripTest.distance, 390);

            TripTest = new TripLogic(10, 5, 1, CityList);
            Assert.AreEqual(TripTest.BillDays, 1);
            Assert.AreEqual(TripTest.distance, 390);
        }
        [TestMethod]
        public void PlannerComboTest()
        {
            PlannerCombine ComboTest = new PlannerCombine(5.21, 0.3621, 60);
            Assert.AreEqual(ComboTest.ftlTrucks, 2);
            Assert.AreEqual(ComboTest.ltlPallets, 8);

            ComboTest = new PlannerCombine(5.21, 0.3621, 40);
            Assert.AreEqual(ComboTest.ftlTrucks, 2);
            Assert.AreEqual(ComboTest.ltlPallets, 0);
        }
    }
}
