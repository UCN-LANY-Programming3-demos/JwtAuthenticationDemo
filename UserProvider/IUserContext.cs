using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProvider
{
    public interface IUserContext
    {
        UserInfo? GetUserInfo(string username);

        bool ValidateLogin(string username, string password, out UserInfo? userInfo);
    }
}
