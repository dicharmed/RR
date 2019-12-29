using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rr_program
{
    public partial class Form4 : Form
    {
        public int id, test1, test2,test3;
        public bool isAdmitted, theory;
        Student student = new Student();
        Progress progress = new Progress();

        public Form4(int id) // get id student from auth Form
        {
            InitializeComponent();            
            this.id = id;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            studentsInfo();
            progressInfo();
        }

        private void button1_Click(object sender, EventArgs e) //THEORY
        {           
            if (isAdmitted == true) //if admittted to theory
            {                
                Help.ShowHelp(this, "helpDocs.chm", "helpDoc3.htm");
            }
            else
            {
                MessageBox.Show("Преподаватель не предоставил Вам доступ");
            }
        }
       
        private void button3_Click(object sender, EventArgs e) //demo
        {
            if (test1 >= 0) //&& test2 >= 0 && test3 >= 0
            {
                this.Close();
                Form1 formDemo = new Form1(id);
                formDemo.Show();
            }
            else
            {
                MessageBox.Show("Сначала пройдите тестирование!");
            }
        }        

        private void button2_Click(object sender, EventArgs e) //test
        {
            if(theory == true)
            {
                this.Close();
                Test formTest = new Test(id);
                formTest.Show();
            }
            else
            {
                MessageBox.Show("Сначала пройдите теорию!");
            }

        }
        private void studentsInfo()
        {            
            List<Student> st = student.getStudentById(id);
            label1.Text = st[0].name + ", " + st[0].groupName;
            isAdmitted = st[0].admitted;
        }

        private void progressInfo()
        {           
            List<Progress> p = progress.getStudentProgress(id);
            test1 = p[0].test1;
            test2 = p[0].test2;
            test3 = p[0].test3;
            theory = p[0].theory;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label2_Click(object sender, EventArgs e) //back to auth
        {
            student.Dispose();
            progress.Dispose();
            this.Close();

            Form3 form3 = new Form3();
            form3.Show();
        }

    }
}
