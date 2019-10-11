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
                List<Student> students = p.getStudents();
          
                for (int y = 0; y < students.Count; y++)
                {
                    if (students[y].login == getHash(textBox1.Text) && students[y].password == getHash(textBox2.Text))
                    {
                        label4.Text = students[y].name;
                    }

                    if (students[y].login != getHash(textBox1.Text) || students[y].password != getHash(textBox2.Text))
                    {
                        label4.Text = "Неверный логин/пароль!";
                        ClearTextBoxes();
                    }

                }

                //p.Dispose();

                //connection.close!!!!!!!!!!!!!!!1
            }
            else
            {
                //textBox2.Text = comboBox1.Text;
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
