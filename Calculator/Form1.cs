using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private static StringBuilder screen;
        private static StringBuilder ghostScreen;
        private bool FirstNum;
        private static double memory;
        public Form1()
        {
            InitializeComponent();
            screen = new StringBuilder("0");
            ghostScreen = new StringBuilder("");
            FirstNum = true;
            ResetMemory();
        }
        public void RefreshScreen()
        {
            lblScreen.Text = screen.ToString();
            lblGhostScreen.Text = ghostScreen.ToString();
        }
        private void AppendToScreen(string i)
        {
            if (screen.Length >= 1 && !double.TryParse(screen[0].ToString(),out double result))
            {
                if (screen[0] != '-')
                {
                    screen.Clear();
                }
              
            }
            if (FirstNum)
            {
                screen.Remove(0, 1);
                FirstNum = false;
            }
            screen.Append(i);
            RefreshScreen();
        }
        private void DeleteLast()
        {
            if (screen[screen.Length-1] != ' ')
            {
                screen.Remove(screen.Length - 1, 1);
                if (screen.Length >= 1 && screen[screen.Length-1] == '-')
                {
                    screen.Remove(screen.Length - 1, 1);
                }
                if (screen.Length == 0)
                {
                    FirstNum = false;
                    AppendToScreen("0");
                    FirstNum = true;
                }
            }
            else
            {
                screen.Remove(screen.Length - 3, 3);
            }
            RefreshScreen();
        }
        private void UpdateGhostScreen(StringBuilder i,bool putEquals)
        {
            ghostScreen.Clear();
            string[] temp = i.ToString().Split(" ");
            foreach (var item in temp)
            {
                ghostScreen.Append(item);
                ghostScreen.Append(" ");
            }
            if (putEquals)
            {
                ghostScreen.Append("=");
            }
            
            RefreshScreen();
        }
      
        private void button9_Click(object sender, EventArgs e)
        {

            AppendToScreen("9");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppendToScreen("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AppendToScreen("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AppendToScreen("3");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AppendToScreen("6");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AppendToScreen("5");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AppendToScreen("4");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AppendToScreen("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AppendToScreen("8");
        }
        private sbyte CheckForCalculations()
        {
            string[] temp = screen.ToString().Split(" ");
            if (temp.Length > 1)
            {
                if (temp[2] != "")
                {
                    UpdateGhostScreen(screen,true);
                    screen.Clear();
                    switch (temp[1])
                    {
                        case "+":                            
                            AppendToScreen((double.Parse(temp[0]) + double.Parse(temp[2])).ToString());
                            return 1;
                        case "-":                           
                            AppendToScreen((double.Parse(temp[0]) - double.Parse(temp[2])).ToString());
                            return 1;
                        case "*":                            
                            AppendToScreen((double.Parse(temp[0]) * double.Parse(temp[2])).ToString());
                            return 1;
                        case "/":                           
                            AppendToScreen((double.Parse(temp[0]) / double.Parse(temp[2])).ToString());
                            return 1;
                    }
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else if(temp[0] != "")
            {
                return 1;
            }
            else
            {
                return -1;
            }
           
        }
        private void ChangeOperation(string newOperation) 
        {
            FirstNum = false;
            string[] temp = screen.ToString().Split(" ");
            screen.Clear();
            AppendToScreen(temp[0]);
            AppendToScreen(newOperation);

        }
        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (CheckForCalculations() > 0)
            {
                FirstNum = false;
                AppendToScreen(" + ");
            }
            else if (CheckForCalculations() == 0)
            {
                ChangeOperation(" + ");
            }
           
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            CheckForCalculations();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (CheckForCalculations() > 0)
            {
                FirstNum = false;
                AppendToScreen(" - ");
            }
            else if (CheckForCalculations() == 0)
            {
                ChangeOperation(" - ");
            }
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            if (CheckForCalculations() > 0)
            {
                FirstNum = false;
                AppendToScreen(" * ");
            }
            else if (CheckForCalculations() == 0)
            {
                ChangeOperation(" * ");
            }
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {

            if (CheckForCalculations() > 0)
            {
                FirstNum = false;
                AppendToScreen(" / ");
            }
            else if (CheckForCalculations() == 0)
            {
                ChangeOperation(" / ");
            }
        }

        private void btnRoot_Click(object sender, EventArgs e)
        {

            string[] temp = screen.ToString().Split(" "); 
            StringBuilder tempStringBuilder = new StringBuilder();
            FirstNum = false;
            if (temp[temp.Length-1] != "")
            {
                if (temp.Length == 1 && double.Parse(temp[0])>= 0)
                {                  
                    screen.Clear();
                    AppendToScreen(Math.Sqrt(double.Parse(temp[0])).ToString());

                    tempStringBuilder.Append(Convert.ToChar(0x221A) + "(");
                    tempStringBuilder.Append(temp[0]);
                    tempStringBuilder.Append(")");
                    UpdateGhostScreen(tempStringBuilder,true);
                    UpdateScreenDataAfterRoot(temp, 0);
                    
                }
                else if (temp.Length == 3 && double.Parse(temp[2]) >= 0)
                {
                    screen.Clear();
                    AppendToScreen(Math.Sqrt(double.Parse(temp[2])).ToString());

                    tempStringBuilder.Append(temp[0] + " ");
                    tempStringBuilder.Append(temp[1] + " ");
                    tempStringBuilder.Append(Convert.ToChar(0x221A) + "(");
                    tempStringBuilder.Append(temp[2]);
                    tempStringBuilder.Append(")");
                    UpdateGhostScreen(tempStringBuilder,false);
                    UpdateScreenDataAfterRoot(temp, 2);
                }
            }
            else if(temp[0] != "" && double.Parse(temp[0]) >= 0)
            {
                screen.Clear();
                AppendToScreen(Math.Sqrt(double.Parse(temp[0])).ToString());

                tempStringBuilder.Append(temp[0] + " ");
                tempStringBuilder.Append(temp[1] + " ");
                tempStringBuilder.Append(Convert.ToChar(0x221A) + "(");
                tempStringBuilder.Append(temp[0]);
                tempStringBuilder.Append(")");
                UpdateGhostScreen(tempStringBuilder,false);
                UpdateScreenDataAfterRoot(temp,0);
            }
        }
        private void UpdateScreenDataAfterRoot(string[] data,int rootindex)
        {
            screen.Clear();
            if (data.Length == 1)
            {
                if (rootindex == 0)
                {
                    screen.Append(Math.Sqrt(double.Parse(data[0])));
                }
            }
            else if (data.Length == 3)
            {
                if (rootindex == 0)
                {
                    screen.Append(data[0] + " ");
                    screen.Append(data[1] + " ");
                    screen.Append(Math.Sqrt(double.Parse(data[0])));

                }
                else if (rootindex == 2)
                {
                    screen.Append(data[0] + " ");
                    screen.Append(data[1] + " ");
                    screen.Append(Math.Sqrt(double.Parse(data[2])));

                }
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            AppendToScreen("0");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            screen.Clear();
            ghostScreen.Clear();
            AppendToScreen("0");
            FirstNum = true;
        }
        private void ResetMemory()
        {
            memory = 0;
            btnMC.Enabled = false;
            btnMR.Enabled = false;
        }
        private void btnMC_Click(object sender, EventArgs e)
        {
            ResetMemory();
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            string[] temp = screen.ToString().Split(" ");
            if (temp.Length == 1)
            {
                if (temp[0] == "0")
                {
                    FirstNum = true;
                    AppendToScreen(memory.ToString());
                }
            }
            else if (temp.Length == 3)
            {
                if (temp[2] == "")
                {
                    AppendToScreen(memory.ToString());
                }
                else
                {
                    for (int i = 0; i < temp[2].Length; i++)
                    {
                        if (temp[2][0] == '-' && i+2 >=temp[2].Length)
                        {
                            i++;
                        }
                        DeleteLast();
                    }
                    AppendToScreen(memory.ToString());
                }
            }
            
        }

        private void btnMMinus_Click(object sender, EventArgs e)
        {
            string[] temp = screen.ToString().Split(" ");
            if (temp.Length == 1)
            {

                if (temp[0][0] == '-' || double.TryParse(temp[0][0].ToString(), out double notNeeded))
                {
                    memory -= double.Parse(temp[0]);
                    btnMC.Enabled = true;
                    btnMR.Enabled = true;
                }
            }
            else if (temp.Length == 3)
            {
                if (temp[2] != "")
                {
                    memory -= double.Parse(temp[2]);
                }
                else
                {
                    memory -= double.Parse(temp[0]);
                }
                btnMC.Enabled = true;
                btnMR.Enabled = true;
            }
          
        }

        private void btnMPlus_Click(object sender, EventArgs e)
        {
            string[] temp = screen.ToString().Split(" ");
            if (temp.Length == 1)
            {
                if (temp[0][0] =='-' || double.TryParse(temp[0][0].ToString(), out double notNeeded))
                {
                    memory += double.Parse(temp[0]);
                    btnMC.Enabled = true;
                    btnMR.Enabled = true;
                }
               
            }
            else if (temp.Length == 3)
            {
                if (temp[2] != "")
                {
                    memory += double.Parse(temp[2]);
                }
                else
                {
                    memory += double.Parse(temp[0]);
                }
                btnMC.Enabled = true;
                btnMR.Enabled = true;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DeleteLast();
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            string[] temp = screen.ToString().Split(" ");
            if (temp.Length == 1)
            {
                if (!temp[0].Contains('.'))
                {
                    FirstNum = false;
                    AppendToScreen(".");
                }
            }
            else if (temp.Length == 3)
            {
                if (temp[2] == "")
                {
                    AppendToScreen("0.");
                }
                else if(!temp[2].Contains('.'))
                {
                    AppendToScreen(".");
                }
            }
        }
    }
}
