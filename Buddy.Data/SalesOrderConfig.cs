using Buddy.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buddy.Data
{
	class SalesOrderConfig : IEntityTypeConfiguration<SalesOrder>
	{
		public void Configure(EntityTypeBuilder<SalesOrder> builder)
		{
			builder
				.ToView("SalesOrderHeader", "SalesLT")
				.HasKey(x => x.SalesOrderID);

			builder
				.HasMany(x => x.Details)
				.WithOne(x => x.SalesOrder)
				.HasForeignKey(x => x.SalesOrderID);
		}
	}
}
