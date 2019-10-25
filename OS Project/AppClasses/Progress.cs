using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace rr_program
{
    class Progress: IDisposable
    {
        public bool theory;
        public int test1, test2, test3, idStudent;
        public Connection con;
        public Query queryString = new Query();

        

        public Progress() { }

        public Progress(int idStudent,int test1, int test2, int test3, bool theory)
        {
            this.idStudent = idStudent;
            this.test1 = test1;
            this.test2 = test2;
            this.test3 = test3;
            this.theory = theory;
        }
        
        public List<Progress> getStudentProgress(int id)
        {
            string str = queryString.getStudentProgressString(id);
            return progressData(str);
        }

        public List<Progress> progressData(string str)
        {
            con = new Connection(str);
            try
            {
                NpgsqlDataReader reader = con.getReader();
                List<Progress> progress = new List<Progress>();

                if (reader.HasRows)
                {
                    foreach (IDataRecord data in reader)
                    {
                        idStudent = Convert.ToInt32(data["student_id"]);
                        
                        if(Convert.IsDBNull(data["test_1"]))
                        {
                            test1 = -1;
                        }else
                        {
                            test1 = Convert.ToInt32(data["test_1"]);
                        }

                        if (Convert.IsDBNull(data["test_2"]))
                        {
                            test2 = -1;
                        }
                        else
                        {
                            test2 = Convert.ToInt32(data["test_2"]);
                        }

                        if (Convert.IsDBNull(data["test_3"]))
                        {
                            test3 = -1;
                        }
                        else
                        {
                            test3 = Convert.ToInt32(data["test_3"]);
                        }
                        theory = Convert.ToBoolean(data["theory"]);

                        progress.Add(new Progress(idStudent, test1, test2, test3, theory));
                    }

                    return progress;
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public void Dispose()
        {
            con.Dispose();
        }
    }
}
