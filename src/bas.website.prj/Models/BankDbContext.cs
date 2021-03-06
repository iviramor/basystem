﻿using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.website.Models.Data
{
    public class BankDbContext : DbContext
    {
        private string _connectionString;

        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {

        }
        public BankDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Bank_client> Bank_client { get; set; }
    }
}