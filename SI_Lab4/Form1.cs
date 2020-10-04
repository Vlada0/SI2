using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using System.IO;

namespace SI_Lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DSA dsa = new DSA();

        private void button_Encript_Click(object sender, EventArgs e)
        {
            dsa.getSignature(richTextBox1.Text);
            textBox1.Text = dsa.p.ToString();
            textBox2.Text = dsa.q.ToString();
            textBox3.Text = dsa.g.ToString();
            textBox4.Text = dsa.x.ToString();
            textBox5.Text = dsa.y.ToString();
            textBox6.Text = dsa.r.ToString();
            textBox7.Text = dsa.s.ToString();
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            bool check = dsa.check(richTextBox1.Text, BigInteger.Parse(textBox6.Text), BigInteger.Parse(textBox7.Text));
            if (check==true)
                textBox8.Text = "yes";
            else
                textBox8.Text = "no";
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            openFileDialog1.Filter = "All Files (*.*)|*.*";
            openFileDialog1.FileName = "";
            try
            {
                if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {

                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    richTextBox1.Text = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            //label2.Text = "Ready";
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = textBox6.Text +"   "+ textBox7.Text;
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, str, Encoding.GetEncoding(1251));//Encoding.ASCII/*Encoding.GetEncoding("UTF-8")*/);
                //this.Close();
            }
            else
                ;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = textBox6.Text + "   " + textBox7.Text;
            if (exitToolStripMenuItem.Text.Length == 4)
            {
                if (str.Length != 0)
                {
                    // Initializes the variables to pass to the MessageBox.Show method.

                    string message = "Сохранить изменения в файле?";
                    string caption = "Выход";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;

                    // Displays the MessageBox.

                    result = MessageBox.Show(message, caption, buttons);

                    if (result == System.Windows.Forms.DialogResult.No)
                    {

                        // Closes the parent form.

                        this.Close();

                    }
                    else
                    {
                        DialogResult dr = saveFileDialog1.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            File.WriteAllText(saveFileDialog1.FileName, str, Encoding.GetEncoding(1251));//Encoding.GetEncoding("UTF-8"));
                            this.Close();
                        }
                        else
                            ;
                    }
                }
                else
                    this.Close();
            }
        }

        

    }
}
