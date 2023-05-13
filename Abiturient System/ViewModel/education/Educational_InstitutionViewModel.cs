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

        public List<Faculty> GetFaculties(long id)
        {
            return institutionRepository.getAllFacultiesOfEducation(id);
        }

        public List<ApplicationForm> GetApplicationFormsByFaculty(long id)
        {
            return institutionRepository.getAllApplicationsByFaculty(id);
        }

        public List<ApplicationForm> GetApplicationFormsByFaculty(Faculty faculty)
        {
            return institutionRepository.getAllApplicationsByFaculty(faculty);
        }

        public List<ApplicationForm> GetApplicationForms()
        {
            return institutionRepository.getAllApplications();
        }

        public void sentApplicationForm(ApplicationForm form)
        {
            institutionRepository.sentApplicationForm(form);
        }

        public void ConfirmApplicationForm(ApplicationForm form)
        {
            institutionRepository.confirmApplicationForm(form);
        }
        public void RejectApplicationForm(ApplicationForm form)
        {
            institutionRepository.rejectApplicationForm(form);
        }
    }
}
