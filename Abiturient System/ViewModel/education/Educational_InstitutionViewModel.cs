using Abiturient_System.Model;
using Abiturient_System.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abiturient_System.ViewModel.education
{
    public class Educational_InstitutionViewModel
    {
        private Educational_InstitutionRepository institutionRepository;

        public Educational_InstitutionViewModel()
        {
            institutionRepository = new Educational_InstitutionRepository();
        }

        public List<Educational_Institution> GetEducational_Institutions()
        {
            return institutionRepository.ReadAllEducational_Institutions();
        }
    }
}
