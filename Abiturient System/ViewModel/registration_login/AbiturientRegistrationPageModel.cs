using Abiturient_System.Model;
using Abiturient_System.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Abiturient_System.ViewModel.registration_login
{
    internal class AbiturientRegistrationPageModel
    {
        private UserRepository userRepository;

        public AbiturientRegistrationPageModel()
        {
            userRepository = new UserRepository();
        }

        public void register(Abiturient abiturient)
        {
            userRepository.Register(abiturient);
            MessageBox.Show("Успешно зарегистрированы");
        }
    }
}
