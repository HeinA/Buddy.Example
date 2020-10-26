using Buddy.Abstractions;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buddy.Api
{
	public static class ApplicationConfiguration
	{
		#region IoC Container

		/*
		 * Dependency Injection Pattern
		 * https://en.wikipedia.org/wiki/Dependency_injection
		 * 
		 * SimpleInjector Overview
		 * https://simpleinjector.readthedocs.io/en/latest/quickstart.html
		 * 
		 */

		public static void Initialize(this Container container)
		{
		}

		#endregion

		#region OData Edm Model

		public static IEdmModel GetEdmModel()
		{
			var builder = new ODataConventionModelBuilder();

			builder.EntitySet<SalesOrder>("SalesOrders").EntityType
				.HasKey(x => x.SalesOrderNumber)
				.HasMany(x => x.Details);

			builder.EntitySet<SalesOrderDetail>("SalesOrderDetails").EntityType
				.HasKey(x => x.SalesOrderDetailID);

			var doSomethingConfig = builder.EntityType<SalesOrder>()
				.Action("DoSomething")
				.ReturnsFromEntitySet<SalesOrder>("SalesOrders");
			doSomethingConfig.Parameter<int>("intParam");
			doSomethingConfig.Parameter<DateTime?>("dateParam");


			return builder.GetEdmModel();
		}

		#endregion
	}
}
