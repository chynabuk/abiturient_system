using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abiturient_System.Model
{
    public class Place
    {
        public long Id { get; set; }
        public long FacultyId { get; set; }
        public bool IsFree { get; set; }
    }
}
