using Buddy.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buddy.Data
{
	public class BuddyDbContext : DbContext
	{
		public BuddyDbContext(DbContextOptions options) : base(options)
		{
		}


		//public DbSet<User> Users { get; set; }

		public DbSet<SalesOrder> SalesOrders { get; set; }
		public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuddyDbContext).Assembly);
		}
	}
}
