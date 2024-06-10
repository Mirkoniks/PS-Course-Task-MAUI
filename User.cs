using System;
using Welcome.Others;

namespace Welcome.Models 
{
	public class User
	{
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRolesEnum Role { get; set; }
    }
}
