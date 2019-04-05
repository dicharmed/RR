using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rr_program
{
    public class RoundRobin
    {
        DataGridView dataGridView;
         
        public PictureBox pictureBox1 { get; set; }

        //----------------RoundRobin Class Constructor-------------------
        public RoundRobin(ref DataGridView temp_dataGridView, PictureBox p)
        {
            dataGridView = temp_dataGridView;
            pictureBox1 = p;
        }

        //----------------Main Round Robin Algorithm Method-------------------
        public void runRoundRobin(ref NewProcess[] multiNewProcesses, int quantum)
        {
            int hv = 0, wh = 0, ww = 0; //for drawing rect; theyre like x, y, x.
          
            //Assign Each Process Its Execution Time
            foreach (var NewProcess in multiNewProcesses)
            {
                //RemaingTime = Total Time @ when process starts execution
                //because of the switch time (время переключения) its given in % so like if, for example, sw =10% from time, so remaining time =
                NewProcess.remainingTime = NewProcess.time - (NewProcess.time * NewProcess.switchTime / 100);

                drawRect(NewProcess.name, 0 + ww, (pictureBox1.Height - 30), Brushes.DarkKhaki); //show names of processes
                ww += 80; //in order to draw them with spaces, its like margin-right
            }
            
            while (true)
            {
                //Close loop on default, if the value is not changed due to available processes executions
                bool executionFinished = true;
                
                //Loop through all processes until the loop ends
                foreach (var NewProcess in multiNewProcesses)
                {
                    if (NewProcess.remainingTime == 0)
                    {
                        NewProcess.status = "Завершен";
                        updateDataGridView(dataGridView, multiNewProcesses);
                    }
                    //Check if the process has any burst time left
                    else if (NewProcess.remainingTime > 0)
                    {
                        //Continue the loop, as a process is executing now and we need to recheck for others
                        executionFinished = false;

                        //Check if the process remaining time is greater than quantum
                        if (NewProcess.remainingTime > quantum)
                        {
                            //Process Status to Running as its Under Execution
                            NewProcess.status = "Выполняется";
                            updateDataGridView(dataGridView, multiNewProcesses);
                            executionTimer(quantum);

                            //Remove the quantum time from the remaining time
                            NewProcess.remainingTime = NewProcess.remainingTime - quantum;

                            //Swap Process to Ready State after execution and continue for next
                            NewProcess.status = "Готов";
                            updateDataGridView(dataGridView, multiNewProcesses);

                            string str = (NewProcess.remainingTime).ToString();
                            drawRect(str, 0 + hv, (pictureBox1.Height - 70) - wh, Brushes.BlueViolet); //draw rects with remaining times
                                                     
                        }
                        //Only runs when the process is on its last cpu burst cycle
                        else
                        {
                            //Check if the process has an IO left before it finishes its last cpu burst cycle
                            //while (NewProcess.switchTime > 0)
                            //{
                            //    ioExecution(multiNewProcesses, NewProcess.ID, NewProcess.switchTime);
                            //    NewProcess.switchTime = NewProcess.switchTime - 1;
                            //}

                            //Process Status to Running as its Under Execution
                            NewProcess.status = "Выполняется";
                            updateDataGridView(dataGridView, multiNewProcesses);
                            executionTimer(NewProcess.remainingTime);

                            //Set remaining time to 0, as the last cpu burst ended
                            NewProcess.remainingTime = 0;

                            //Change Process Status to Completed
                            NewProcess.status = "Завершен";
                            updateDataGridView(dataGridView, multiNewProcesses);

                            string str = (NewProcess.remainingTime).ToString();
                            drawRect(str, 0 + hv, (pictureBox1.Height - 70) - wh, Brushes.BlueViolet);
                        }  
                    }
                    ////Execute Single IO after every CPU burst cycle
                    //if (NewProcess.IO > 0)
                    //{
                    //    ioExecution(multiNewProcesses, NewProcess.ID, NewProcess.IO);
                    //    NewProcess.IO = NewProcess.IO - 1;
                    //}

                    NewProcess last_one = multiNewProcesses.Last(); // this is for building rects in columns, so if its the last process  so the next one is gonna be set above the first one

                    if (NewProcess.ID == last_one.ID)
                    {
                        hv = 0;
                        wh += 30; // y 
                    }
                    else
                    {
                        hv += 80; // x                        
                    }
  
                }

                //When All Processes have completed their execution
                if (executionFinished == true)
                {                    
                    break;
                }
            }


        }        

        private void drawRect(string str, int x, int y, Brush color) // drawing rects with text
        {
            Graphics G = pictureBox1.CreateGraphics();
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Rectangle Rect = new Rectangle(x, y, 70, 29);
            //G.FillRectangle(Brushes.DarkGreen, Rect);
            G.FillRectangle(color, Rect);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            G.DrawString(str, new Font("Times", 12), Brushes.White, Rect, sf);
        }

        //----------------Update Data Grid View Method-------------------------------
        public void updateDataGridView(DataGridView dataGridView, NewProcess[] multiNewProcesses)
        {
            //Remove Current Data Grid Rows and Refresh it
            dataGridView.Rows.Clear();
            dataGridView.Refresh();

            //Add Processes rows again to data grid view with updated values
            foreach (var NewProcess in multiNewProcesses)
            {
                string[] row = { NewProcess.ID.ToString(), NewProcess.name, NewProcess.remainingTime.ToString(), NewProcess.switchTime.ToString(), NewProcess.status };
                dataGridView.Rows.Add(row);
            }
        }

        ////----------------Process IO Execution Method
        //public void ioExecution(NewProcess[] multiNewProcesses, int id, int interupt)
        //{
        //    //Change Process State to Waiting when it goes to IO
        //    foreach (var NewProcess in multiNewProcesses)
        //    {
        //        if (NewProcess.ID == id && NewProcess.status != "Completed")
        //        {
        //            NewProcess.status = "Waiting";
        //        }
        //    }
        //    updateDataGridView(dataGridView, multiNewProcesses);

        //    //Execute the IO for 1 second
        //    executionTimer(1);

        //    //Change Process back to Ready State after IO has completed
        //    foreach (var NewProcess in multiNewProcesses)
        //    {
        //        if (NewProcess.ID == id && NewProcess.status!="Completed")
        //        {
        //            NewProcess.status = "Ready";
        //        }
        //    }
        //    updateDataGridView(dataGridView, multiNewProcesses);
        //}

        //----------------Process Execution Timer Method
        public void executionTimer(int tempTime)
        {
            int executionTime = tempTime * 1000;
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (executionTime == 0 || executionTime < 0)
            {
                return;
            }
            timer1.Interval = executionTime; //set timer interval to execution time. задает интервал, через который будет периодически вызываться обработчик таймера.
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) => //adds the event/обработчик таймера/ s-sender
            {
                timer1.Enabled = false;
                timer1.Stop();
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
    }
}