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


namespace WordFrequency
{
    public partial class Form1 : Form
    {

        

        Dictionary<string, int> wordCounts= new Dictionary<string, int>();


        Dictionary<TextBox, Label> index = new Dictionary<TextBox, Label>();

        int lineCount;

        int wordCount;

        public Form1()
        {
            InitializeComponent();

           

            fileList.Items.Add("AnneOfGreenGables.txt");
            fileList.Items.Add("TheAdventuresOfSherlockHolmes.txt");

     

            index.Add(textBox1, label1);
            index.Add(textBox2, label2);
            index.Add(textBox3, label3);
            index.Add(textBox4, label4);
            index.Add(textBox5, label5);
            index.Add(textBox6, label6);
            index.Add(textBox7, label7);
            index.Add(textBox8, label8);
            index.Add(textBox9, label9);
            index.Add(textBox10, label10);

  

            foreach (TextBox indexWordBox in index.Keys)
            {
                indexWordBox.TextChanged += new System.EventHandler(this.indexWordBox_TextChanged);
            }
        }

  

        private void updateAWord(TextBox indexWordBox)
        {


            string indexWord = indexWordBox.Text;

     

            Label indexCountLabel = index[indexWordBox];


 
            if (wordCounts.ContainsKey(indexWord))
            {
     
                double perc = wordCounts[indexWord];
     
                indexCountLabel.Text = wordCounts[indexWord].ToString();
                indexCountLabel.Text += " (" + ((perc*100)/wordCount).ToString("G3") + "%)";
            }
            else
            {
                indexCountLabel.Text = "0";
            }
        }

        private void updateAllWords()
        {


            foreach(TextBox indexWordBox in index.Keys)
            {
                updateAWord(indexWordBox);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            lineCount = 0;
            wordCount = 0;

 
            wordCounts = new Dictionary<string, int>();


            string filename = fileList.SelectedItem.ToString();

     
            foreach (var line in File.ReadLines("../../Input/" + filename))
            {
                lineCount += 1;

             

                processLine(line);
            }



            linesProcessedLabel.Text = lineCount.ToString();
            wordsProcessedLabel.Text = wordCount.ToString();

            updateAllWords();
        }


        private void processLine(string myLine)
        {
           
            foreach (string word in myLine.Split(' '))
            {
                wordCount += 1;

                string words = word.Replace("?", "").Replace(".", "").Replace("!", "").Replace(",", "").Replace(".", "").Replace(":", "");
             
       

                if (wordCounts.ContainsKey(words))
                {
                    wordCounts[words] = wordCounts[words] + 1;
                }
                else
                {
       
                    wordCounts.Add(words, 1);
                }
            }
        }



        private void indexWordBox_TextChanged(object sender, EventArgs e)
        {
            TextBox indexWordBox =  (TextBox) sender;


            updateAWord(indexWordBox);
        }
    }
}
