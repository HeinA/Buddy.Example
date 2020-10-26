using Buddy.Data;
using Buddy.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buddy.Api.Authentication
{
	public class UserService : IUserService
	{
		public UserService(BuddyDbContext db)
		{
			this.db = db;
		}

		BuddyDbContext db;

		public async Task<User> Authenticate(string username, string password)
		{
			if (username != "TestUser" || password != "TestUser") return null;
			return await Task.FromResult(new User() { id = 1, Username = "TestUser" });

			//	User user = null;

			//	user = await Task.Run(() => db.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault());
			//	if (user?.Password != password) user = null;

			//	if (user == null) return null;
			//	return user;
			//}
		}
	}
}
