using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abiturient_System.Model
{
    public class ApplicationForm
    {
        public long Id { get; set; }
        public string AbiturientId { get; set; }
        public Abiturient abiturient { get; set; }
        public long FacultyId { get; set; }
        public string Status { get; set; }
        public Faculty faculty { get; set; }
    }
}
