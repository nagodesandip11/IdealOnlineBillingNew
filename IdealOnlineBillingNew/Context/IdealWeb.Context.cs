﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IdealOnlineBillingNew.Context
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class IdealWebDB : DbContext
    {
        public IdealWebDB()
            : base("name=IdealWebDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<tblCategoryMaster> tblCategoryMasters { get; set; }
    
        public virtual ObjectResult<SelectEmployee_Result> SelectEmployee()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SelectEmployee_Result>("SelectEmployee");
        }
    }
}
