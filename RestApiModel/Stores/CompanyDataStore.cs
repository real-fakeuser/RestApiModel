using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiModel.Model;

namespace RestApiModel.Stores
{
    public class CompanyDataStore
    {
        public static CompanyDataStore Current { get; } = new CompanyDataStore();
        public List<Company> Company { get; set; }

        public CompanyDataStore()
        {
            Company = new List<Company>()
            {
                new Company()
                {
                    Id = 1,
                    Name = "Tobit.Software"
                },
                new Company()
                {
                    Id = 2,
                    Name = "Tobit.Labs"
                },
                new Company()
                {
                    Id = 3,
                    Name = "Tobit.EDV"
                },
                new Company()
                {
                    Id = 4,
                    Name = "Tobit.Test"
                }

            };
        }







    }
}
