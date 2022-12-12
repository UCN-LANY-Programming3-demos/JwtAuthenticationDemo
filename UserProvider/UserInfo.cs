using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserProvider
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public UserInfo(string username, string password, string fullname, string email)
        {
            Id = Guid.NewGuid();
            Username = username;
            Fullname = fullname;
            Email = email;
            PasswordHash = Encoding.ASCII.GetString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(password)));
        }
    }
}
