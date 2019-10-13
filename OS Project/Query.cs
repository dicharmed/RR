using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_program
{
    class Query
    {   

        public Query() { }

        //STUDENT QUERIES
        public string getAllStudentsString()
        {
            return "SELECT * FROM students, univ_groups WHERE group_id = id_group";             
        }
        public string getStudentByIdString(int id)
        {
            return $"SELECT * FROM students, univ_groups WHERE group_id = id_group and id_student = '{id}'";
        }
        public string getAuthQueryString(string login, string pswd)
        {
            return $"SELECT * FROM students, univ_groups WHERE group_id = id_group and login = '{login}' and pswd = '{pswd}'";
        }

        //TEACHER QUERIES
        public string getTeacherAuthQueryString(string login, string pswd)
        {
            return $"SELECT * FROM teachers, positions WHERE position_id = id_pos and login = '{login}' and pswd = '{pswd}'";
        }
        public string getTeacherByIdString(int id)
        {
            return $"SELECT * FROM teachers, positions WHERE position_id = id_pos and id_teacher = '{id}'";
        }
    }
}
