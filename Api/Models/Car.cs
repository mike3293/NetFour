using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }

        public double Fueld { get; set; }

        public string RegNumber { get; set; }

        public string ImageUrl { get; set; }
    }
}
