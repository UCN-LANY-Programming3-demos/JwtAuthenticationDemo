using System.Security.Cryptography;
using System.Text;

namespace UserProvider
{
    internal class UserContext : IUserContext
    {
        private static IList<UserInfo> _users = new List<UserInfo>()
        {
            new("donald", "password", "Donald Duck", "donald@duckburg.com"),
            new("money", "beagleboyssuck", "Scrooge McDuck", "rich@moneybin.com"),
            new("gyro", "4F9435849EB34EF5B0E5DAFFA3C6A5EB", "Gyro Gearloose", "doctorg@dia.com"),
        };

        public UserInfo? GetUserInfo(string username)
        {
            return _users.SingleOrDefault(u => u.Username == username);
        }

        public bool ValidateLogin(string username, string password, out UserInfo? userInfo)
        {
            userInfo = GetUserInfo(username);
            if (userInfo == null)
                return false;
            return username == userInfo.Username && Encoding.ASCII.GetString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(password))) == userInfo.PasswordHash;
        }
    }
}
