using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abiturient_System.Model
{
    public class Admission : User
    {
        public long FacultyId { get; set; }
        public string Email { get; set; }
    }
}
