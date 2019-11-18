using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rr_program
{
    public partial class Test : Form
    {
        public int id;

        string file_name = "t.txt";
        string[,] array;
        int total, amount_of_questions, correct_answers, wrong_answers, questionsAmount;
        static Random rand = new Random();
        public Test(int id)
        {
            InitializeComponent();
            this.id = id;
            init_test();
        }
        private void init_test()
        {
            button1.Text = "Следующий вопрос";
            amount_of_questions = 6;
            questionsAmount = amount_of_questions;
            correct_answers = 0;
            wrong_answers = 0;
            load_file();
            radio_checked();
            radio_turn_on_off();
            label1.Text = "";
            amount_of_questions--;
            show_question();
        }

        private void load_file()
        {
            try
            {
                string[] lines = File.ReadAllLines(file_name, Encoding.UTF8);
                total = lines.Length / 4; //total amount of questions in file 
                array = new string[amount_of_questions, 4];

                int[] temp = new int[amount_of_questions];
                int j;
                int k = 0;
                do
                {
                    j = rand.Next(0, total) * 4;
                    if (!temp.Contains(j))
                    {
                        array[k, 0] = lines[j];
                        for (int i = 1; i < 4; i++)
                            array[k, i] = lines[j + i];
                        temp[k] = j;
                        k++;
                    }
                } while (!(k == amount_of_questions));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void show_question()
        {
            radio_checked();
            label5.Text = array[amount_of_questions, 0];
            radio_tags(rand.Next(1, 7));
            radioButton1.Text = array[amount_of_questions, Convert.ToInt16(radioButton1.Tag)];
            radioButton2.Text = array[amount_of_questions, Convert.ToInt16(radioButton2.Tag)];
            radioButton3.Text = array[amount_of_questions, Convert.ToInt16(radioButton3.Tag)];
        }

        private void Test_Load(object sender, EventArgs e)
        {

        }

        private void radio_turn_on_off()
        {
            if (amount_of_questions < 0)
            {
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
            }
            else
            {
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e) //save result
        {
            try
            {
                Progress progress = new Progress();
                progress.updateStudentsTest(id, correct_answers);
                MessageBox.Show("Результат сохранен");
            }catch(Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label4_Click(object sender, EventArgs e)
        {            
            
            this.Close();

            Form3 form3 = new Form3();
            form3.Show();
        }

        private void radio_checked()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }

        private void radio_tags(int i)
        {
            switch (i)
            {
                case 1: radioButton1.Tag = 1; radioButton2.Tag = 2; radioButton3.Tag = 3; break;
                case 2: radioButton1.Tag = 1; radioButton2.Tag = 3; radioButton3.Tag = 2; break;
                case 3: radioButton1.Tag = 2; radioButton2.Tag = 1; radioButton3.Tag = 3; break;
                case 4: radioButton1.Tag = 2; radioButton2.Tag = 3; radioButton3.Tag = 1; break;
                case 5: radioButton1.Tag = 3; radioButton2.Tag = 1; radioButton3.Tag = 2; break;
                case 6: radioButton1.Tag = 3; radioButton2.Tag = 2; radioButton3.Tag = 1; break;
            }
        }

        private void show_result()
        {
            double percent = (correct_answers * 100) / questionsAmount;

            label1.Text = "Правильных ответов: " + correct_answers.ToString() + "\n" +
                "Неправильных ответов: " + wrong_answers.ToString() + "\n" + "Вы ответили на: " + percent + "%";

            button2.Enabled = true;

        }
        private void button1_Click(object sender, EventArgs e) // ok
        {
            if (amount_of_questions < 0) { init_test(); return; }

            if (!(radioButton1.Checked || radioButton2.Checked || radioButton3.Checked))
            {
                MessageBox.Show("Выберите вариант ответа");
                return;
            }
            if ((radioButton1.Checked & Convert.ToInt16(radioButton1.Tag) == 1) ||
                (radioButton2.Checked & Convert.ToInt16(radioButton2.Tag) == 1) ||
                (radioButton3.Checked & Convert.ToInt16(radioButton3.Tag) == 1))
            {
                correct_answers++;
                amount_of_questions--;
            }
            else
            {
                MessageBox.Show("Правильный вариант ответа: " + array[amount_of_questions, 1]);
                wrong_answers++;
                amount_of_questions--;
            }

            if (amount_of_questions < 0)
            {
                show_result();
                label5.Text = "Результат теста";
                button1.Text = "Тест заново";
                radio_turn_on_off();
                return;
            }
            show_question();
        }
    }
}
