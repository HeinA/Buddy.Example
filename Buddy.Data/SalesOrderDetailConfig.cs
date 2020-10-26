using Buddy.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buddy.Data
{
	class SalesOrderDetailConfig : IEntityTypeConfiguration<SalesOrderDetail>
	{
		public void Configure(EntityTypeBuilder<SalesOrderDetail> builder)
		{
			builder
				.ToTable("SalesOrderDetail", "SalesLT")
				.HasKey(x => x.SalesOrderDetailID);
		}
	}
}
