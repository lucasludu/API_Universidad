using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models._career.Request
{
    public class CareerInsertRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int DurationInYears { get; set; }
    }
}
