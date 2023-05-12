using Abiturient_System.Model;
using Abiturient_System.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abiturient_System.Repository
{
    public class UserRepository 
    {
        public void Register(User user)
        {
            using (var command = new NpgsqlCommand("insert into users (phone, password, role) values (@v1, @v2, @v3)", Connection.getInstance().getConnection()))
            {
                command.Parameters.AddWithValue("v1", user.Phone);
                command.Parameters.AddWithValue("v2", user.Password);
                command.Parameters.AddWithValue("v3", user.Role);

                command.ExecuteNonQuery();
            }

            if (user.Role.Equals("Абитуриент"))
            {
                Abiturient abiturient = (Abiturient) user;

                using (var command = new NpgsqlCommand("insert into abiturients (phone, first_name, last_name, diploma_img, ort_certificate_img, passport_img, registration_certificate_img) " +
                    "values (@v1, @v2, @v3, @v4, @v5, @v6, @v7)", Connection.getInstance().getConnection()))
                {
                    command.Parameters.AddWithValue("v1", abiturient.Phone);
                    command.Parameters.AddWithValue("v2", abiturient.FirstName);
                    command.Parameters.AddWithValue("v3", abiturient.LastName);
                    command.Parameters.AddWithValue("v4", abiturient.DiplomaImage);
                    command.Parameters.AddWithValue("v5", abiturient.OrtCertificateImage);
                    command.Parameters.AddWithValue("v6", abiturient.PassportImage);
                    command.Parameters.AddWithValue("v7", abiturient.RegistrationCertificateImage);

                    command.ExecuteNonQuery();
                }
            }
            else
            {
                Admission admission = (Admission)user;

                using (var command = new NpgsqlCommand("insert into abiturients (phone, first_name, last_name, diploma_img, ort_certificate_img, passport_img, registration_certificate_img) " +
                    "value (@v1, @v2, @v3, @v4, @v5, @v6, @v7)", Connection.getInstance().getConnection()))
                {
                    command.Parameters.AddWithValue("v1", admission.Phone);
                    command.Parameters.AddWithValue("v2", admission.FirstName);
                    command.Parameters.AddWithValue("v3", admission.LastName);
                    command.Parameters.AddWithValue("v3", admission.LastName);
                }
            }
        }

        public User Login(String phone, String password)
        {
            using (var userCommand = new NpgsqlCommand("select * from users where phone=@phone", Connection.getInstance().getConnection()))
            {
                userCommand.Parameters.AddWithValue("phone", phone);

                using(var userReader = userCommand.ExecuteReader())
                {
                    if (userReader.Read())
                    {
                        if (userReader[1].ToString().Equals(password))
                        {
                            if (userReader[2].ToString().Equals("Абитуриент"))
                            {
                                Connection.getInstance().getConnection().Close();
                                Connection.getInstance().getConnection().Open();

                                using(var abiturientCommand = new NpgsqlCommand("select * from abiturients where phone=@phone", Connection.getInstance().getConnection()))
                                {
                                    abiturientCommand.Parameters.AddWithValue("phone", phone);

                                    using(var abiturientReader = abiturientCommand.ExecuteReader())
                                    {
                                        if (abiturientReader.Read())
                                        {
                                            return new Abiturient()
                                            {
                                                Phone = phone,
                                                FirstName = abiturientReader[0].ToString(),
                                                DiplomaImage = abiturientReader[1].ToString(),
                                                OrtCertificateImage = abiturientReader[2].ToString(),
                                                PassportImage = abiturientReader[3].ToString(),
                                                RegistrationCertificateImage = abiturientReader[4].ToString(),
                                                LastName = abiturientReader[6].ToString()
                                            };
                                        }
                                    }
                                }
                            }
                        }
                        throw new Exception("Неверный пароль");
                    }
                }
            }
            throw new Exception("Пользователь не найден");
        }
    }
}
