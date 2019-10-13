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
    public partial class Form5 : Form
    {
        public int id;
        public Form5(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            teacherInfo();
        }

        private void teacherInfo()
        {
            Teacher teacher = new Teacher();
            List<Teacher> st = teacher.getTeacherById(id);
            label1.Text = st[0].fio + ", " + st[0].positionName;
            textBox1.Text = id.ToString();
        }
    }
}
