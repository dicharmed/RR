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
    public partial class Form1 : Form
    {
        NewProcess p1;
        Form2 form;

        public Form1()
        {
            InitializeComponent();
            form = new Form2();
            form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            boxID.Text = "4";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int error = 0;
            try //if theres no exception in this block its going on
            {
                p1 = new NewProcess();
                p1.name = boxName.Text;
                p1.ID = Convert.ToInt32(boxID.Text);
                p1.time = Convert.ToInt32(boxTime.Text);
                p1.switchTime = Convert.ToInt32(boxSW.Text);

                form.AddProcess(p1);
            }
            catch (Exception) //if theres exception n the try block it'll proccess here
            {
                MessageBox.Show("Заполните все поля!");
                error = 1;
            }

            if (error != 1)
            {
                boxName.Text = "";
                boxTime.Text = "";
                boxSW.Enabled = false;
 
                int tempID = Convert.ToInt32(boxID.Text); //auto increment ID field
                tempID = tempID + 4;
                boxID.Text = tempID.ToString();
            }
        } //Сохранить

        private void btnReset_Click(object sender, EventArgs e) //Сбросить
        {
            boxName.Text = "";
            boxTime.Text = "";
            boxSW.Text = "";
            //boxSW.Enabled = true;
        }

        //only controls btn and digits
        private void boxID_KeyPress(object sender, KeyPressEventArgs e)  
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void boxTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void boxSW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void boxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }


    }
}