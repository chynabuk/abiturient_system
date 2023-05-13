using Abiturient_System.Model;
using Abiturient_System.Repository;
using Abiturient_System.Util;
using Npgsql;
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

        public List<Abiturient> TopFiveAbiturients()
        {
            return userRepository.TopFiveAbiturients();
        }

        public int MinOrtScore()
        {
            return userRepository.MinOrtScore();
        }

        public decimal AverageOrtScore()
        {
            return userRepository.AverageOrtScore();
        }
    }
}
