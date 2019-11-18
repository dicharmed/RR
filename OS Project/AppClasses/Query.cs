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
        public string getStudentByGroupString(int id)
        {
            return $"SELECT * FROM students, univ_groups WHERE group_id = id_group and group_id = '{id}'";
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

        //PROGRESS
        public string getStudentProgressString(int id)
        {
            return $"SELECT * FROM progress WHERE student_id = '{id}'";
        }

        //SPECIALTIES
        public string getSpecialtiesString()
        {
            return "SELECT * FROM specialities";
        }

        //GROUPS
        public string getGroupsString(int id_spec)
        {
            return $"SELECT * FROM univ_groups WHERE specialty_id = '{id_spec}'";
        }

        //UPDATE
        public string updateStudentAccess(int id)
        {
            return $"UPDATE students SET admittedto = NOT admittedto WHERE id_student = '{id}'";
        }

        public string updateStudentResults(int id)
        {
            return $"UPDATE progress SET test_1 = null WHERE student_id = '{id}'";
        }

        public string updateStudentTest(int id, int testResult)
        {
            return $"UPDATE progress SET test_1 = '{testResult}' WHERE student_id = '{id}'";
        }

    }
}
