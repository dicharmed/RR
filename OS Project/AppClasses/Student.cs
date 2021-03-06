﻿using System;
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
        public Query queryString = new Query();

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
            string str = queryString.getAllStudentsString();
            return studentsData(str);            
        }
        public List<Student> getStudentByLogin(string login, string pswd)
        {
            string str = queryString.getAuthQueryString(login, pswd);
            return studentsData(str);
        }
        public List<Student> getStudentById(int Id)
        {
            string str = queryString.getStudentByIdString(Id);
            return studentsData(str);
        }
        public List<Student> getStudentByGroup(int Id)
        {
            string str = queryString.getStudentByGroupString(Id);
            return studentsData(str);
        }
        public void updateStudentsAccess(int Id)
        {   
            string str = queryString.updateStudentAccess(Id);
            update(str);
        }
        public void updateStudentsResults(int Id)
        {
            string str = queryString.updateStudentResults(Id);
            update(str);
        }
        public void update(string str)
        {
            con = new Connection(str);
            con.UpdateConnection();
        }
        public List<Student> studentsData(string str)
        {            
            con = new Connection(str);
            try
            {
                NpgsqlDataReader reader = con.getReader();
                List<Student> student = new List<Student>();

                if (reader.HasRows)
                {
                    foreach (IDataRecord data in reader)
                    {
                        id = Convert.ToInt32(data["id_student"]);
                        name = (data["fio"]).ToString();
                        login = (data["login"]).ToString();
                        password = (data["pswd"]).ToString();
                        groupName = (data["name"]).ToString();
                        admitted = Convert.ToBoolean(data["admittedto"]);


                        student.Add(new Student(id, name, login, password, groupName, admitted));
                    }

                    return student;
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
