using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Laba1
{
    public partial class Form1 : Form
    {
        ParamSerialized paramSerialized = new ParamSerialized();
        int lengthKeys = 8;
        int[] Keys;
        public Form1()
        {
            InitializeComponent();
        }
        private void onlyDigits(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

    
        public bool isPowerTwo(int num)
        {
            for(int i =0; ; i++)
            {
                int twoInPower = (int)Math.Pow(2, i);
                if (twoInPower == num)
                {
                    return true;
                }
                if (twoInPower > num)
                {
                    return false;
                }
            }
        }
        public bool IsCoprime(int num1, int num2)
        {
            if (num1 == num2)
            {
                return num1 == 1;
            }
            else
            {
                if (num1 > num2)
                {
                    return IsCoprime(num1 - num2, num2);
                }
                else
                {
                    return IsCoprime(num2 - num1, num1);
                }
            }
        }
        public bool isOddNumber(int num)
        {
            if (num % 2 == 1)
            {
                return true;
            }
            return false;
        }
        public bool isCorrectY0(int num, int m)
        {
            return (0 <= num && num<m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = String.Empty;
            richTextBox2.Text = String.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt;";
            string result = String.Empty;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);

            }
            int countOfSpace = 8 - richTextBox1.Text.Length % 8;
                for (int j = 0; j < countOfSpace; j++)
                {
                    richTextBox1.Text += " ";

                }
            
            for (int i = 0; i<richTextBox1.Text.Length; i+=8)
            {
                string temp = richTextBox1.Text.Substring(i, 8);
                for(int k = 0; k<temp.Length; k++)
                {

                    result += (char)(temp[k] ^ Keys[k]);
                   
                }
            }
            richTextBox2.Text = result;
            File.WriteAllText("encryptedText.txt", result);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if (File.Exists("encryptedText.txt"))
            {
                string result = String.Empty;
                richTextBox1.Text = File.ReadAllText("encryptedText.txt");
           
          
                for (int i = 0; i < richTextBox1.Text.Length; i += 8)
                {
                    string temp = richTextBox1.Text.Substring(i, 8);
                    for (int k = 0; k < temp.Length; k++)
                    {
                        result += (char)(temp[k] ^ Keys[k]);
                    }
                }
                richTextBox2.Text = result;
            }
            else
            {
                MessageBox.Show("Файл для дешифровки отсутствует!");
            }
      
       
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if (isPowerTwo(Int32.Parse(textBox2.Text)) && IsCoprime(Int32.Parse(textBox3.Text), Int32.Parse(textBox2.Text)) && isOddNumber(Int32.Parse(textBox1.Text)) && 
                    isCorrectY0(Int32.Parse(textBox4.Text), Int32.Parse(textBox2.Text)))
                {
                    paramSerialized.setA(Int32.Parse(textBox1.Text));
                    paramSerialized.setB(Int32.Parse(textBox3.Text));
                    paramSerialized.setM(Int32.Parse(textBox2.Text));
                    paramSerialized.setY0(Int32.Parse(textBox4.Text));
                    paramSerialized.serialize();
              
                 
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Вы ввели неверные параметры!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                paramSerialized = (ParamSerialized)paramSerialized.deserialize();
                textBox1.Text = paramSerialized.getA().ToString();
                textBox3.Text = paramSerialized.getB().ToString();
                textBox2.Text = paramSerialized.getM().ToString();
                textBox4.Text = paramSerialized.getY0().ToString();
                Keys = new int[lengthKeys];
                Keys[0] = paramSerialized.getY0();
                for (int i = 1; i < Keys.Length; i++)
                {
                    Keys[i] = (paramSerialized.getA() * Keys[i - 1] + paramSerialized.getB()) % paramSerialized.getM();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;

        }



        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyDigits(e);
            
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyDigits(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyDigits(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyDigits(e);
        }
    }
}
