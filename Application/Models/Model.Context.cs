﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbEntities : DbContext
    {
        public dbEntities()
            : base("name=dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Appointments_Service> Appointments_Service { get; set; }
        public virtual DbSet<Bil> Bils { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Ticket_Response> Ticket_Response { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
