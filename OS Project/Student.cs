using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace rr_program
{
    class Student:IDisposable
    {        
        public int id;
        public string name;
        public string login;
        public string password;
        public string groupName;
        public bool admitted;
        public Connection con;

        public Student() { }
        public Student(int id, string name, string login, string password, string groupName, bool admitted)
        {
            this.id = id;
            this.name = name;
            this.login = login;
            this.password = password;
            this.groupName = groupName;
            this.admitted = admitted;
        }

        public List<Student> getStudents()
        {
            con = new Connection("SELECT * FROM students, univ_groups WHERE group_id = id_group");
            NpgsqlDataReader reader =  con.getReader();
            
            List<Student> students = new List<Student>();
            if (reader.HasRows)
            {
                foreach (IDataRecord data in reader)
                {
                    int id = Convert.ToInt32(data["id_student"]);
                    string name = (data["fio"]).ToString();
                    string login = (data["login"]).ToString();
                    string pswd = (data["login"]).ToString();
                    string groupName = (data["name"]).ToString();
                    bool admitted = Convert.ToBoolean(data["admittedto"]);


                    students.Add(new Student(id, name, login, pswd, groupName, admitted));
                }               
            }

            return students;
        }

      public void Dispose()
        {
            con.Dispose();
        }
    }
}
