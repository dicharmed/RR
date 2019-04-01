using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void onlyDigits(KeyPressEventArgs e)
        {            
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        void readOnly_txtBoxes(bool n, int c) // n(show/hide); c(amount of process)
        {
            textBox2.ReadOnly = n;

            if (c == 2)
            {
                textBox2.ReadOnly = n;
                textBox3.ReadOnly = n;
            }
            else if (c == 3)
            {
                textBox2.ReadOnly = n;
                textBox3.ReadOnly = n;
                textBox4.ReadOnly = n;
            }
            else if(c == 4)
            {
                textBox2.ReadOnly = n;
                textBox3.ReadOnly = n;
                textBox4.ReadOnly = n;
                textBox5.ReadOnly = n;
            }else
            {
                textBox2.ReadOnly = n;
                textBox3.ReadOnly = n;
                textBox4.ReadOnly = n;
                textBox5.ReadOnly = n;
                textBox6.ReadOnly = n;
            }

        }
      
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) 
        {
            readOnly_txtBoxes(true, 5); //hide txtBoxes if value's changed

            int num = 1; //amount of proccess by default

            if (numericUpDown1 != null)
            {
                num = Convert.ToInt32(numericUpDown1.Value);
                readOnly_txtBoxes(false, num); //show txtBoxes
            }
           
        }

        int[] readTxtBoxes()
        {
            //read quantum & time of running of processes
            int processes = Convert.ToInt32(numericUpDown1.Value);

            int p1, p2, p3, p4, p5;

            
            int[] btime = new int[processes];


            if (processes == 5)
            {
                p1 = Convert.ToInt32(textBox2.Text);
                p2 = Convert.ToInt32(textBox3.Text);
                p3 = Convert.ToInt32(textBox4.Text);
                p4 = Convert.ToInt32(textBox5.Text);
                p5 = Convert.ToInt32(textBox6.Text);

                btime[0] = p1;
                btime[1] = p2;
                btime[2] = p3;
                btime[3] = p4;
                btime[4] = p5;
            }
            else if (processes == 4)
            {
                p1 = Convert.ToInt32(textBox2.Text);
                p2 = Convert.ToInt32(textBox3.Text);
                p3 = Convert.ToInt32(textBox4.Text);
                p4 = Convert.ToInt32(textBox5.Text);

                btime[0] = p1;
                btime[1] = p2;
                btime[2] = p3;
                btime[3] = p4;
            }
            else if (processes == 3)
            {
                p1 = Convert.ToInt32(textBox2.Text);
                p2 = Convert.ToInt32(textBox3.Text);
                p3 = Convert.ToInt32(textBox4.Text);

                btime[0] = p1;
                btime[1] = p2;
                btime[2] = p3;

            }
            else if (processes == 2)
            {
                p1 = Convert.ToInt32(textBox2.Text);
                p2 = Convert.ToInt32(textBox3.Text);

                btime[0] = p1;
                btime[1] = p2;

            }
            else
            {
                p1 = Convert.ToInt32(textBox2.Text);

                btime[0] = p1;

            }

            return btime;
        }

        private void button1_Click(object sender, EventArgs e) // start
        {    
            int quantum = 1;
            quantum = Convert.ToInt32(textBox7.Text);

            int processes = Convert.ToInt32(numericUpDown1.Value);
           
            int[] rtime = readTxtBoxes();              

            int rp = processes;
            int i = 0;
            int time = 0;

            do
            {
                if (rtime[i] > quantum)//if time of process's running is bigger than quantum
                {
                    rtime[i] = rtime[i] - quantum;                    
                    time += quantum;
                    //cout << time;                    
                    textBox1.Text += "p" + (i+1) + ": " + time + " ";
                }
                else if (rtime[i] <= quantum && rtime[i] > 0)
                {
                    time += rtime[i];
                    rtime[i] = rtime[i] - rtime[i];                    
                    rp--;
                    // cout << time;
                    if(rp!=0)textBox1.Text += "p" + (i+1) + ": " + time + " ";
                }

                i++;
                if (i == processes)
                {
                    i = 0;
                }

            } while (rp != 0); //till all processes is done
        }

        private void button2_Click(object sender, EventArgs e) //clear all data
        {
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;

            //pictureBox
        }

        //----------------
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyDigits(e);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyDigits(e);
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyDigits(e);
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyDigits(e);
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyDigits(e);
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e) //txtBox quantum
        {           
            onlyDigits(e);
            
            button1.Enabled = true;
            button2.Enabled = true;
        }
    }
}
