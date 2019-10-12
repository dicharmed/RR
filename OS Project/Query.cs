using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_program
{
    class Query
    {   
        public string allStudents, studentById, studentAuth;


        public Query() { }

        //STUDENT QUERIES
        public string getAllStudentsString()
        {
            return allStudents = "SELECT * FROM students, univ_groups WHERE group_id = id_group";             
        }
        public string getStudentByIdString(int id)
        {
            return studentById = $"SELECT * FROM students, univ_groups WHERE group_id = id_group and id_student = '{id}'";
        }
        public string getAuthQueryString(string login, string pswd)
        {
            return studentAuth = $"SELECT * FROM students, univ_groups WHERE group_id = id_group and login = '{login}' and pswd = '{pswd}'";
        }
    }
}
