﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FSiMed_x2._1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class fsimed_configdbEntities : DbContext
    {
        connection c = new connection();
        public fsimed_configdbEntities()
            : base("name=fsimed_configdbEntities")
        {
            this.Database.Connection.ConnectionString = c.con.ConnectionString;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<user_master> user_master { get; set; }
    }
}
