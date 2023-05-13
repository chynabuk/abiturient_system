using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abiturient_System.Model
{
    public class Faculty
    {
        public long Id { get; set; }
        public long EducationId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int PlaceAmount { get; set; }
        public int FreePlaceAmount { get; set; }
    }
}