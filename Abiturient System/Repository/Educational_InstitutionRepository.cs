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
        
    }
}
