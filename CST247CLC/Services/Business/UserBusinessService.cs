using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CST247CLC.Models;
using CST247CLC.Services.Data;

namespace CST247CLC.Services.Business
{
    public class UserBusinessService
    {
        UserDataService uds = new UserDataService();

        //login method
        public bool loginUser(UserModel user)
        {
            return uds.authenticate(user);
        }

        //register method
        public bool createUser(UserModel user)
        {
            return uds.createUser(user);
        }
    }
}
