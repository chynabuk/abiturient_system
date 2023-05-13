using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abiturient_System.Model
{
    public class Abiturient : User
    {
        public String PassportImage { get; set; }
        public String DiplomaImage { get; set; }
        public String OrtCertificateImage { get; set; }
        public String RegistrationCertificateImage { get; set; }
        public int ApplicationAvailable { get; set; }

    }
}
