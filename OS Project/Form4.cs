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
        public int id;        

        public Form4(int id) // get id student from auth Form
        {
            InitializeComponent();
            this.id = id;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            studentsInfo();
        }

        private void button1_Click(object sender, EventArgs e) //THEORY
        {

        }

        private void studentsInfo()
        {
            Student student = new Student();
            List<Student> st = student.getStudentById(id);
            label1.Text = st[0].name + ", " + st[0].groupName;
            textBox1.Text = id.ToString();
        }
    }
}
