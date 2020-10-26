using Buddy.Abstractions;
using Buddy.Data;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Buddy.Api.Controllers
{
	// Uncomment to use Authentication/Authorization.  Correctly implement Authentication\UserService.cs to load users from Database or wherever

	//[Authorize]
	public class SalesOrdersController : ODataController
	{
		public SalesOrdersController(BuddyDbContext db)
		{
			this.db = db;
		}

		BuddyDbContext db;

		[EnableQuery]
		public IQueryable<SalesOrder> Get()
		{
			return db.SalesOrders;
		}

		[EnableQuery]
		public SingleResult<SalesOrder> Get([FromODataUri] string key)
		{
			IQueryable<SalesOrder> result = db.SalesOrders.Where(x => x.SalesOrderNumber == key);
			return SingleResult.Create(result);
		}

		[HttpPost]
		public async Task<IActionResult> DoSomething([FromODataUri] string key, ODataActionParameters parameters)
		{
			var intParam = (int)parameters["intParam"];
			var dateParam = (DateTimeOffset?)parameters["dateParam"];

			var order = await db.SalesOrders.FirstOrDefaultAsync(x => x.SalesOrderNumber == key);

			//TODO: Do Something
			if (Debugger.IsAttached) Debugger.Break();

			return Ok(order);
		}
	}
}
