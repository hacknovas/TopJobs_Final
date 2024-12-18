using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopJobsAPI.Entities;

namespace TopJobsAPI.Repositories
{
    public class EmployerRepository:IEmployerRepository
    {
        private TopJobContext _context;

        public EmployerRepository()
        {
            _context = new TopJobContext();
        }

        public Employers DeleteEmployer(int employerId)
        {
            var data=_context.Employers.SingleOrDefault(x=>x.EmployerId== employerId);
            _context.Employers.Remove(data);
            return data;
        }

        public Employers EditDetails(Employers employer)
        {
            var employerData = _context.Employers.SingleOrDefault(x => x.EmployerId == employer.EmployerId);
            if (employerData != null)
            {
                employerData.Name = employer.Name;
                employerData.ContactNumber = employer.ContactNumber;
                employerData.Organisation = employer.Organisation;
                employerData.OrganisationDetails=employer.OrganisationDetails;
                employerData.Email = employer.Email;
                
                _context.SaveChanges();

                return employerData;
            }

            return null;
        }

        public Employers GetDetailsByEID(int employerId)
        {
            var employerData = _context.Employers.SingleOrDefault(x => x.EmployerId == employerId);
            if (employerData != null)
            {
                return employerData;
            }

            return null;
        }

        public Employers GetDetailsBYUID(int userID)
        {
            var employerData = _context.Employers.SingleOrDefault(x => x.UserId == userID);
            if (employerData != null)
            {
                return employerData;
            }

            return null;
        }

        public Employers Register(Employers employer)
        {
            var employerData=_context.Employers.Add(employer);
            _context.SaveChanges();
            return employerData;
        }
    }
}
