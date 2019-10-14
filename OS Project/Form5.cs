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
        Query query = new Query();
        Connection connection;
        bool isAdmitted = false;
        public Form5(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            textBox2.Text = "";
            teacherInfo();

            getListSpecialties();
            getStudentsInfoInBox();
        }

        private void teacherInfo()
        {
            Teacher teacher = new Teacher();
            List<Teacher> st = teacher.getTeacherById(id);
            label1.Text = st[0].fio + ", " + st[0].positionName;
        }

        private void getStudentsInfoInBox()
        {
            try
            {
                Progress st = new Progress();
                Student stud = new Student();
                int key = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;
                List<Progress> l = st.getStudentProgress(key);
                List<Student> d = stud.getStudentById(key);

                isAdmitted = d[0].admitted;

                if (isAdmitted == true)
                {
                    string theory = l[0].theory == true ? "Теория изучена" : "Теория не изучена";
                    string test1 = l[0].test1 >= 0 ? (l[0].test1).ToString() : "Тест не пройден";


                    textBox2.Text = theory + "\r\n" + "Тестирование: " + test1 + "\r\n";

                    button1.Text = "Запретить доступ студенту";
                }
                else
                {
                    textBox2.Text = "Студент не допущен до изучения теории";
                    button1.Text = "Допустить студента";
                }
            }catch
            {
                textBox2.Text = "Выберите студента для отображения информации о его успеваемости";
            }
            
            

        }
        private void getListSpecialties()
        {
            Dictionary<int, string> comboSource = new Dictionary<int, string>();
            try
            {
                UniStructure spec = new UniStructure();
                List<UniStructure> sp = spec.specialitiesData();
                

                for (int y = 0; y < sp.Count; y++)
                {
                    comboSource.Add(sp[y].id_spec, sp[y].name_spec);
                }

            }catch{
                comboSource.Add(-1, "Пусто");
            }finally
            {
                comboBox1.DataSource = new BindingSource(comboSource, null);
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
            }
                 
        }
        private void getListGroups()
        {
            Dictionary<int, string> comboSource1 = new Dictionary<int, string>();
            try
            {
                UniStructure group = new UniStructure();
                int key = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
                List<UniStructure> sp = group.groupsData(key);


                for (int y = 0; y < sp.Count; y++)
                {
                    comboSource1.Add(sp[y].id_group, sp[y].name_group);
                }
            }catch
            {
                comboSource1.Add(-1, "Пусто");
            }
            finally
            {
                comboBox2.DataSource = new BindingSource(comboSource1, null);
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";
            }

        }
        private void getListStudents()
        {

            Dictionary<int, string> comboSource2 = new Dictionary<int, string>();

            try
            {
                Student st = new Student();
                int key = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;
                List<Student> sp = st.getStudentByGroup(key);

                for (int y = 0; y < sp.Count; y++)
                {
                    comboSource2.Add(sp[y].id, sp[y].name);
                }
            }catch
            {
                comboSource2.Add(-1, "Пусто");
            }
            finally
            {
                comboBox3.DataSource = new BindingSource(comboSource2, null);
                comboBox3.DisplayMember = "Value";
                comboBox3.ValueMember = "Key";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox2.Items.Clear();
            getListGroups();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox3.Items.Clear();
            textBox2.Text = "";
            getListStudents();
            getStudentsInfoInBox();
        }
        private void label6_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 authForm = new Form3();
            authForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)//
        {
            if(isAdmitted == true) //допущен --> запретить
            {

            }else // ---> разрешить
            {

            }
        }
    }
}
