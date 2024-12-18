using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopJobsAPI.Entities;

namespace TopJobsAPI.Repositories
{
    internal interface IUsersRepository
    {
        Users Login(string username, string password);
        Users Register(Users user);
    }
}
