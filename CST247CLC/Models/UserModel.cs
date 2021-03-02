using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST247CLC.Models
{
    public class UserModel
    {
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }
        public int sex { get; set; }

        public string state { get; set; }
        public int age { get; set; }

        public string email { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

    }
}
