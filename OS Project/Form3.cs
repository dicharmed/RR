using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace rr_program
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = "";

            if (comboBox1.Text == "Студент")
            {
                Student p = new Student();
                List<Student> student = p.getStudentByLogin(getHash(textBox1.Text), getHash(textBox2.Text));

                if(student != null)
                {                    
                    Form4 studentForm = new Form4(student[0].id); //передача id студента
                    studentForm.Show();
                    p.Dispose(); //connection close
                    this.Hide();                    
                }
                else
                {
                    label4.Text = "Неверный логин/пароль!";
                    ClearTextBoxes();
                }         
               
            }
            else //Преподаватель
            {
                Teacher p = new Teacher();
                List<Teacher> teacher = p.getTeacherByLogin(getHash(textBox1.Text), getHash(textBox2.Text));

                if (teacher != null)
                {
                    Form5 teacherForm = new Form5(teacher[0].id); //передача id студента
                    teacherForm.Show();
                    p.Dispose(); //connection close
                    this.Hide();
                }
                else
                {
                    label4.Text = "Неверный логин/пароль!";
                    ClearTextBoxes();
                }
            }

            

        }       
   
        private string getHash(string str)
        {
            byte[] hash = Encoding.ASCII.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
            {
                result += b.ToString("x2");//форматирует строку как два строчных шестнадцатеричных символа в нижнем регистре
            }
            return result;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
