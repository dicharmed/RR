using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace rr_program
{
    class Teacher: IDisposable
    {
        public int id;
        public string fio;
        public string login;
        public string password;
        public string positionName;
        public Connection con;
        public Query queryString = new Query();


        public Teacher() { }
        public Teacher(int id, string fio, string login, string password, string positionName)
        {
            this.id = id;
            this.fio = fio;
            this.login = login;
            this.password = password;
            this.positionName = positionName;
        }

        public List<Teacher> getTeacherById(int Id)
        {
            string str = queryString.getTeacherByIdString(Id);
            return teacherData(str);
        }
        public List<Teacher> getTeacherByLogin(string login, string pswd)
        {
            string str = queryString.getTeacherAuthQueryString(login, pswd);
            return teacherData(str);
        }
        public List<Teacher> teacherData(string str)
        {
            con = new Connection(str);
            try
            {
                NpgsqlDataReader reader = con.getReader();
                List<Teacher> teacher = new List<Teacher>();

                if (reader.HasRows)
                {
                    foreach (IDataRecord data in reader)
                    {
                        id = Convert.ToInt32(data["id_teacher"]);
                        fio = (data["fio"]).ToString();
                        login = (data["login"]).ToString();
                        password = (data["pswd"]).ToString();
                        positionName = (data["name"]).ToString();

                        teacher.Add(new Teacher(id, fio, login, password, positionName));
                    }

                    return teacher;
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
