using Buddy.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buddy.Api.Authentication
{
	public interface IUserService
	{
		Task<User> Authenticate(string username, string password);
	}
}
