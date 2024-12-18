using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopJobsAPI.Entities;

namespace TopJobsAPI.Repositories
{
    internal interface IEmployerRepository
    {
        Employers EditDetails(Employers employers);
        Employers GetDetailsByEID(int employerId);
        Employers GetDetailsBYUID(int userID);
        Employers Register(Employers employer);
        Employers DeleteEmployer(int employerId);
    }
}
