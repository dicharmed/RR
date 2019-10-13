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
        public string User = "student";
        public Form3()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e) //войти
        {
            label4.Text = "";

            if (User == "student")
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

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        //design
        private void Form3_Load(object sender, EventArgs e)
        {
            //form
            this.BackColor = Color.FromArgb(0, 38, 77);

            //textBoxes
            this.textBox1.BackColor = Color.FromArgb(0, 38, 77);
            this.textBox2.BackColor = Color.FromArgb(0, 38, 77);

            //button Вход
            this.button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 79, 153);
            this.button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 66, 128);

            //button Student
            this.button2.BackColor = Color.FromArgb(153, 206, 255);
            this.button2.FlatAppearance.BorderColor = Color.FromArgb(153, 206, 255);
            this.button2.ForeColor = Color.FromArgb(0, 38, 77);
        }

        private void button3_Click(object sender, EventArgs e)//teacher
        {
            User = "teacher";

            //teacher active
            this.button3.BackColor = Color.FromArgb(153, 206, 255);
            this.button3.FlatAppearance.BorderColor = Color.FromArgb(153, 206, 255);
            this.button3.ForeColor = Color.FromArgb(0, 38, 77);

            //student disactivated
            this.button2.BackColor = Color.FromArgb(0, 38, 77);
            this.button2.FlatAppearance.BorderColor = Color.FromArgb(255, 255, 255);
            this.button2.ForeColor = Color.FromArgb(255, 255, 255);

            ClearTextBoxes();
            label4.Text = "";
        }

        private void button2_Click(object sender, EventArgs e) //student
        {
            User = "student";

            //student active
            this.button2.BackColor = Color.FromArgb(153, 206, 255);
            this.button2.FlatAppearance.BorderColor = Color.FromArgb(153, 206, 255);
            this.button2.ForeColor = Color.FromArgb(0, 38, 77);

            //teacher disactivated
            this.button3.BackColor = Color.FromArgb(0, 38, 77);
            this.button3.FlatAppearance.BorderColor = Color.FromArgb(255, 255, 255);
            this.button3.ForeColor = Color.FromArgb(255, 255, 255);

            ClearTextBoxes();
            label4.Text = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
