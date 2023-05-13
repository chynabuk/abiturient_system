using Abiturient_System.Model;
using Abiturient_System.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Abiturient_System.Repository
{
    public class Educational_InstitutionRepository
    {

        public List<Educational_Institution> ReadAllEducational_Institutions()
        {
            List<Educational_Institution> educational_Institutions = new List<Educational_Institution>();

            using (var educational_InstitutionCommand = new NpgsqlCommand("select * from educational_institutions", Connection.getInstance().getConnection()))
            {
                using (var reader = educational_InstitutionCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Educational_Institution education = new Educational_Institution()
                        {
                            Id = (long) reader["id"],
                            Name = reader["name"].ToString(),
                            Link = reader["link"].ToString()
                        };

                        educational_Institutions.Add(education);
                    }
                }
            }

            return educational_Institutions;
        }

        public List<Faculty> getAllFacultiesOfEducation(long id)
        {
            List<Faculty> faculties = new List<Faculty>();

            using (var facultyCommand = new NpgsqlCommand("select * from faculties where educational_institution_id = @educationId", Connection.getInstance().getConnection()))
            {
                facultyCommand.Parameters.AddWithValue("educationId", id);

                using (var reader = facultyCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Faculty faculty = new Faculty()
                        {
                            Id = (long)reader["id"],
                            EducationId = (long)reader["educational_institution_id"],
                            Name = (string) reader["name"],
                            Link = (string)reader["link"],
                            PlaceAmount = (int)reader["place_amount"],
                            FreePlaceAmount = (int)reader["free_place_amount"]
                        };
                        faculties.Add(faculty);
                    }
                }
            }

            return faculties;
        }

        public List<ApplicationForm> getAllApplications()
        {
            List<ApplicationForm> applicationForm = new List<ApplicationForm>();

            using (var applicationCommand = new NpgsqlCommand("select * from applications", Connection.getInstance().getConnection()))
            {

                using (var reader = applicationCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ApplicationForm place = new ApplicationForm()
                        {
                            Id = (long)reader["id"],
                        };
                        applicationForm.Add(place);
                    }
                }
            }

            return applicationForm;
        }

        public List<ApplicationForm> getAllApplicationsByFaculty(long id)
        {
            List<ApplicationForm> applicationForm = new List<ApplicationForm>();

            using (var applicationCommand = new NpgsqlCommand("select * from applications where faculty_id = @id", Connection.getInstance().getConnection()))
            {
                applicationCommand.Parameters.AddWithValue("id", id);

                using (var reader = applicationCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ApplicationForm place = new ApplicationForm()
                        {
                            Id = (long)reader["id"],
                            AbiturientId = reader["abiturient_phone"].ToString(),
                            FacultyId = (long)reader["faculty_id"],
                            Status = reader["status"].ToString()
                        };
                        applicationForm.Add(place);
                    }
                }
            }

            return applicationForm;
        }

        public List<ApplicationForm> getAllApplicationsByFaculty(Faculty faculty)
        {
            List<ApplicationForm> applicationForm = new List<ApplicationForm>();

            using (var applicationCommand = new NpgsqlCommand("select * from applications where faculty_id = @id and status = 'в ожидании'", Connection.getInstance().getConnection()))
            {
                applicationCommand.Parameters.AddWithValue("id", faculty.Id);

                using (var reader = applicationCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ApplicationForm place = new ApplicationForm()
                        {
                            Id = (long)reader["id"],
                            AbiturientId = reader["abiturient_phone"].ToString(),
                            FacultyId = (long)reader["faculty_id"],
                            faculty = faculty,
                            Status = reader["status"].ToString()
                        };
                        applicationForm.Add(place);
                    }
                }


            }

            return applicationForm;
        }

        public List<ApplicationForm> getAllApplicationsByFacultySortedByOrtScore(Faculty faculty)
        {
            List<ApplicationForm> applicationForm = new List<ApplicationForm>();

            using (var applicationCommand = new NpgsqlCommand("select id, abiturient_phone, faculty_id, abiturients.first_name, abiturients.last_name, abiturients.ort_score, status from applications join abiturients on abiturient_phone = abiturients.phone where faculty_id = @id order by abiturients.ort_score desc", Connection.getInstance().getConnection()))
            {
                applicationCommand.Parameters.AddWithValue("id", faculty.Id);

                using (var reader = applicationCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ApplicationForm application = new ApplicationForm()
                        {
                            Id = (long)reader["id"],
                            AbiturientId = reader["abiturient_phone"].ToString(),
                            FacultyId = (long)reader["faculty_id"],
                            faculty = faculty,
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            OrtScore = (int)reader["ort_score"],
                            Status = reader["status"].ToString()
                        };
                        applicationForm.Add(application);
                    }
                }
            }

            return applicationForm;
        }
        public void sentApplicationForm(ApplicationForm applicationForm)
        {
            if (((Abiturient)Authentication.User).ApplicationAvailable > 0)
            {
                string abiturientPhone = null;

                using (var applicationCommand = new NpgsqlCommand("select abiturient_phone from applications where faculty_id = @facultyId", Connection.getInstance().getConnection()))
                {
                    applicationCommand.Parameters.AddWithValue("facultyId", applicationForm.FacultyId);

                    using(var reader = applicationCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            abiturientPhone = reader["abiturient_phone"].ToString();
                            if (abiturientPhone.Equals(applicationForm.AbiturientId))
                            {
                                throw new Exception("Вы уже оставляли заявку");
                            }
                        }
                    }
                }
                using (var command = new NpgsqlCommand("insert into applications (id, faculty_id, abiturient_phone) values (@v1, @v2, @v3)", Connection.getInstance().getConnection()))
                {
                    command.Parameters.AddWithValue("v1", applicationForm.Id);
                    command.Parameters.AddWithValue("v2", applicationForm.FacultyId);
                    command.Parameters.AddWithValue("v3", applicationForm.AbiturientId);

                    command.ExecuteNonQuery();
                }

                updateFacultyAndAbiturent(applicationForm);
            }
            
        }

        public void confirmApplicationForm(ApplicationForm applicationForm)
        {
            Connection.getInstance().getConnection().Close();
            Connection.getInstance().getConnection().Open();
            using (var command = new NpgsqlCommand("update applications set status = 'принят' where id = @applicationId", Connection.getInstance().getConnection()))
            {
                command.Parameters.AddWithValue("applicationId", applicationForm.Id);
                command.ExecuteNonQuery();
            }
        }

        private void updateFacultyAndAbiturent(ApplicationForm applicationForm) 
        {
            using (var command2 = new NpgsqlCommand("update faculties set free_place_amount = free_place_amount - 1 where id = @facultyId", Connection.getInstance().getConnection()))
            {
                command2.Parameters.AddWithValue("facultyId", applicationForm.FacultyId);
                command2.ExecuteNonQuery();

                

                using (var command3 = new NpgsqlCommand("update abiturients set application_available = application_available - 1 where phone = @phone2", Connection.getInstance().getConnection()))
                {
                    command3.Parameters.AddWithValue("phone2", applicationForm.AbiturientId);
                    command3.ExecuteNonQuery();
                }
            }
        }
        

        public void rejectApplicationForm(ApplicationForm applicationForm)
        {
            using (var command = new NpgsqlCommand("delete from applications where id = @applicationId", Connection.getInstance().getConnection()))
            {
                command.Parameters.AddWithValue("applicationId", applicationForm.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
