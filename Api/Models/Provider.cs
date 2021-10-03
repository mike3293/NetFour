using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }

        public double LogoUrl { get; set; }

        public List<Car> Cars { get; set; }
    }
}
