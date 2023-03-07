﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StripeAndTwilio.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=DbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<AllUser> AllUsers { get; set; }
        public virtual DbSet<ConfirmOrder> ConfirmOrders { get; set; }
        public virtual DbSet<Orderinfo> Orderinfoes { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    
        public virtual ObjectResult<getOrderinfo_Result> getOrderinfo(string oStatus)
        {
            var oStatusParameter = oStatus != null ?
                new ObjectParameter("OStatus", oStatus) :
                new ObjectParameter("OStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getOrderinfo_Result>("getOrderinfo", oStatusParameter);
        }
    }
}
