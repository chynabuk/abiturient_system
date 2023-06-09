﻿using Abiturient_System.Model;
using Abiturient_System.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

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

                using (var command = new NpgsqlCommand("insert into abiturients (phone, first_name, last_name, diploma_img, ort_certificate_img, passport_img, registration_certificate_img, ort_score) " +
                    "values (@v1, @v2, @v3, @v4, @v5, @v6, @v7, @v8)", Connection.getInstance().getConnection()))
                {
                    command.Parameters.AddWithValue("v1", abiturient.Phone);
                    command.Parameters.AddWithValue("v2", abiturient.FirstName);
                    command.Parameters.AddWithValue("v3", abiturient.LastName);
                    command.Parameters.AddWithValue("v4", abiturient.DiplomaImage);
                    command.Parameters.AddWithValue("v5", abiturient.OrtCertificateImage);
                    command.Parameters.AddWithValue("v6", abiturient.PassportImage);
                    command.Parameters.AddWithValue("v7", abiturient.RegistrationCertificateImage);
                    command.Parameters.AddWithValue("v8", abiturient.OrtScore);

                    command.ExecuteNonQuery();
                }
            }
            else
            {
                Admission admission = (Admission)user;

                using (var command = new NpgsqlCommand("insert into admissions (phone, first_name, last_name, faculty_id, email) " +
                    "values (@v1, @v2, @v3, @v4, @v5)", Connection.getInstance().getConnection()))
                {
                    command.Parameters.AddWithValue("v1", admission.Phone);
                    command.Parameters.AddWithValue("v2", admission.FirstName);
                    command.Parameters.AddWithValue("v3", admission.LastName);
                    command.Parameters.AddWithValue("v4", admission.FacultyId);
                    command.Parameters.AddWithValue("v5", admission.Email);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Abiturient GetAbiturient(String phone)
        {
            using (var abiturientCommand = new NpgsqlCommand("select * from abiturients where phone=@phone", Connection.getInstance().getConnection()))
            {
                abiturientCommand.Parameters.AddWithValue("phone", phone);

                using (var abiturientReader = abiturientCommand.ExecuteReader())
                {
                    if (abiturientReader.Read())
                    {
                        return new Abiturient()
                        {
                            Phone = phone,
                            FirstName = abiturientReader[0].ToString(),
                            LastName = abiturientReader["last_name"].ToString(),
                            ApplicationAvailable = (int)abiturientReader["application_available"],
                            Role = "Абитуриент"
                        };
                    }
                }
            }
            throw new Exception("Пользователь не найден");
        }

        public List<Abiturient> TopFiveAbiturients()
        {
            List<Abiturient> abiturients = new List<Abiturient>();

            using (var applicationCommand = new NpgsqlCommand("select * from abiturients order by ort_score desc limit 5", Connection.getInstance().getConnection()))
            {

                using (var reader = applicationCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Abiturient abiturient = new Abiturient()
                        {
                            Phone = reader["phone"].ToString(),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            OrtScore = (int) reader["ort_score"]
                        };
                        abiturients.Add(abiturient);
                    }
                }
            }

            return abiturients;
        }

        public int MinOrtScore()
        {
            int minScore = 0;
            using (var applicationCommand = new NpgsqlCommand("select min(ort_score) from abiturients", Connection.getInstance().getConnection()))
            {
                minScore = (int) applicationCommand.ExecuteScalar();
                
            }

            return minScore;
        }

        public decimal AverageOrtScore()
        {
            decimal averageScore = 0;
            using (var applicationCommand = new NpgsqlCommand("select avg(ort_score) from abiturients", Connection.getInstance().getConnection()))
            {
                averageScore = (decimal) applicationCommand.ExecuteScalar();
            }

            return averageScore;
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
                        if (userReader["password"].ToString().Equals(password))
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
                                                LastName = abiturientReader[6].ToString(),
                                                ApplicationAvailable = (int) abiturientReader["application_available"],
                                                Role = "Абитуриент"
                                            };
                                        }
                                    }
                                }
                            }
                            else if (userReader["role"].ToString().Equals("Приемная комиссия"))
                            {
                                Connection.getInstance().getConnection().Close();
                                Connection.getInstance().getConnection().Open();

                                using (var admissionCommand = new NpgsqlCommand("select * from admissions where phone=@phone", Connection.getInstance().getConnection()))
                                {
                                    admissionCommand.Parameters.AddWithValue("phone", phone);

                                    using (var admissionReader = admissionCommand.ExecuteReader())
                                    {
                                        if (admissionReader.Read())
                                        {
                                            return new Admission()
                                            {
                                                Phone = phone,
                                                FirstName = admissionReader["first_name"].ToString(),
                                                LastName = admissionReader["last_name"].ToString(),
                                                FacultyId = (long) admissionReader["faculty_id"],
                                                Email = admissionReader["email"].ToString(),
                                                Role = "Приемная комиссия"
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
