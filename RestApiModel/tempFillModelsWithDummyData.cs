using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiModel.Models;

namespace RestApiModel
{
    public class tempFillModelsWithDummyData
    {
        public static tempFillModelsWithDummyData Current { get; } = new tempFillModelsWithDummyData();
        public List<Company> Company { get; set; }

        public tempFillModelsWithDummyData()
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
