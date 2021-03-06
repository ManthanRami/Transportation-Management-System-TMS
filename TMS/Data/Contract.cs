﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    /// <summary>
    /// This enum represents the supported cities in the TMS
    /// </summary>
    public enum City
    {
        Windsor = 0,
        London = 1,
        Hamilton = 2,
        Toronto = 3,
        Oshawa = 4,
        Belleville = 5,
        Kingston = 6,
        Ottawa = 7
    }

    /// <summary>
    /// This enum provides a more readable reference to FTL and LTL jobs
    /// </summary>
    public enum JobType
    {
        FTL = 0,
        LTL = 1
    }

    /// <summary>
    /// This enum provides a more readable reference to van type used.
    /// </summary>
    public enum VanType
    {
        Dry = 0,
        Reefer = 1
    }

    public enum Status
    {
        PENDING   = 0,      // Order has been accepted, but no carriers or trips have been assigned
        STARTED   = 1,      // Order has trips and carriers assigned, would be "on the road"
        DELIVERED = 2,      // Order has reached destination, and has been delivered
        FINISHED  = 3,      // Planner has marked order as complete
        INVOICED  = 4       // Buyer has generated an invoice for the order
    }

    /// <summary>
    /// The Contract class models the contract table from the contract marketplace database.
    /// </summary>
    public class Contract
    {
        public uint ContractID { get; set; }
        public Status Status { get; set; }
        public Customer Customer { get; set; }

        public JobType JobType { get; set; }
        public VanType VanType { get; set; }

        public int Quantity { get; set; }

        public City Origin { get; set; }

        public City Destination { get; set; }

        /// <summary>
        /// carrier should be set as a contract is being accepted
        /// </summary>
        public Carrier Carrier { get; set; }
    }
}