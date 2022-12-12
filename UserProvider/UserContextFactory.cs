using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProvider
{
    public static class UserContextFactory
    {
        public static IUserContext Create()
        {
            return new UserContext();
        }
    }
}
