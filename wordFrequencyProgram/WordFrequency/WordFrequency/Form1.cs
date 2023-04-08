using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Have to bring in system.IO to use the File class
using System.IO;


namespace WordFrequency
{
    public partial class Form1 : Form
    {

        // Create a dictionary that will store how many times each word
        // appears in a loaded file.   The word is the Key and is a string
        // and the count is the value and is an integer

        Dictionary<string, int> wordCounts= new Dictionary<string, int>();

        // Create a dictionary that will store an association between a 
        // Textbox that will have a word provided by the user and the label
        // that will show the output value.   The Textbox is the Key and the
        // Label is the value.   This is mostly to make the code less tedious
        // and show another foreach loop

        Dictionary<TextBox, Label> index = new Dictionary<TextBox, Label>();

        // Keep track of how many lines we've processed
        int lineCount;

        // Keep track of how many words we've processed
        // Note: this is overall words, not a count of the *different* words.
        int wordCount;

        public Form1()
        {
            InitializeComponent();

            // We have a file drop-down list.
            // Add the names of the two files in the Input Directory.
            

            // Note:  it is *possible* to use a Directory object to get a list
            // of all the files in a directory of a particular type and then 
            // build the fileList that way.  Which would be *another* loop.
            // But I only downloaded two files.

            fileList.Items.Add("AnneOfGreenGables.txt");
            fileList.Items.Add("TheAdventuresOfSherlockHolmes.txt");

            // Put our textbox-label pairs in the index Dictionary so that we 
            // know which label to update when a textbox changes without writing 
            // a big switch statement or having to have ten different event handlers

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

            // Add the indexWordBox_TextChanged event handler to each textBox in the index list
            // index.Keys gets a list of all the keys in our dictionary (e.g. all the Textboxes)
            // the foreach loop does something for each Textbox;  inside the loop, the name of 
            // the current textbox we are looking at is 'indexWordBox'

            // Note:  the first time through the loop, indexWordBox = textbox1
            //        the second time through the loop, indexWordBox = textbox2
            //        etc. until we've looked at every textbox that we put in the dictionary

            foreach (TextBox indexWordBox in index.Keys)
            {
                // Add the event handler to the current Textbox.
                indexWordBox.TextChanged += new System.EventHandler(this.indexWordBox_TextChanged);
            }
        }

        // If we have a textbox with a word in it, we need to find the count 
        // for that word from the wordCounts Dictionary, then update the text 
        // of the label associated with that textbox with the count.

        private void updateAWord(TextBox indexWordBox)
        {

            // First, we are going to want to know the word to get the count for
            // That is just the Text of the Textbox we are processing.
            string indexWord = indexWordBox.Text;

            // We are going to need to update the label associated with this textbox.
            // Fortunately, we put the Textbox as a Key in the index Dictionary.
            // If we look up our textbox in the index Dictionary, we get back the associated label
            // e.g. Label1 for Textbox1, Label2 for Textbox2, etc.

            Label indexCountLabel = index[indexWordBox];


            //tried to think about the idea of writing output in an output.txt file but faced some problems during execution. 
        

            // The wordCounts dictionary contains the counts of all the words that got
            // counted when the file was processed.   If no files have yet been processed,
            // then the dictionary is empty, and it contains no words.

            // First check if the word we are looking up is in the dictionary
            if (wordCounts.ContainsKey(indexWord))
            {
                // If it *is* in the dictionary, then get its Count (and turn it into a string)
                // And set the text of the label we want to change to the count.
                double perc = wordCounts[indexWord];
            /*    string fileName = "output.txt";
                StreamWriter writer = new StreamWriter(fileName);
                writer.Write(perc.ToString());*/

                indexCountLabel.Text = wordCounts[indexWord].ToString();
                indexCountLabel.Text += " (" + ((perc*100)/wordCount).ToString("G3") + "%)";
            }
            else
            {
                // If it is *not* in the dictionary, then we know the count for this word
                // is zero.  Note that this will happen for *every* word in an empty dictionary
                indexCountLabel.Text = "0";
            }
        }

        // We want to update all the words
        private void updateAllWords()
        {
            // The index dictionary has all the textboxes we are interested in.
            // Each one is a key in the dictionary
            // index.Keys gives us a collection of keys
            // Foreach key / textbox, we want to updateAWord

            foreach(TextBox indexWordBox in index.Keys)
            {
                updateAWord(indexWordBox);
            }
        }

        // This is the LoadFile button.  Should have a better name.
        // When it gets clicked, we want to get the selected file, load the file, 
        // and process each line in the file

        private void button1_Click(object sender, EventArgs e)
        {
            // We are counting lines and words for the file, so 
            // set them both to zero because this is a new file
            lineCount = 0;
            wordCount = 0;

            // Clear out the dictionary.   
            // wordCounts.Clear() would probably be better practice here.
            // Why create an entire new dictionary if we don't need to?
            wordCounts = new Dictionary<string, int>();

            // The name of the file is the SelectedItem of the fileList listBox
            // grab the SelectedItem
            string filename = fileList.SelectedItem.ToString();

            // The file path is ../../Input/ because that is where I put the files
            // File.ReadLines will open the file and then create a Collection of lines
            // Note that it reads one line each time through the loop -- the collection
            // gets created on demand rather than reading the entire file ahead of time
            foreach (var line in File.ReadLines("../../Input/" + filename))
            {
                // Increment the line count by 1 as we are reading a line
                lineCount += 1;

                // We *could* update the # of lines processed so far here, but that
                // turns out to be hideously slow. 
                //linesProcessedLabel.Text = lineCount.ToString();

                // Process the line
                processLine(line);
            }

            //  Now that we've processed all lines of the file,
            // update the labels with the counts of lines and words processed.

            linesProcessedLabel.Text = lineCount.ToString();
            wordsProcessedLabel.Text = wordCount.ToString();

            // Update all the words in the word list with the counts from the file
            updateAllWords();
        }


        // Process a Line of text from the file
        private void processLine(string myLine)
        {
            // We take the line and split it into a collection of words
            // We use String.Split and split on the space character

            // We want to process each 'word' in the collection of words in the line
            foreach (string word in myLine.Split(' '))
            {
                // Add one to the number of words seen
                wordCount += 1;

                string words = word.Replace("?", "").Replace(".", "").Replace("!", "").Replace(",", "").Replace(".", "").Replace(":", "");
             
                // Commented out for efficiency.  Updating the label 100,000+ times is *slow*
                //wordsProcessedLabel.Text = wordCount.ToString();

                // Is the word in the dictionary yet?
                if (wordCounts.ContainsKey(words))
                {
                    // If so, get its current value, add one to it, and set that as the new value
                    wordCounts[words] = wordCounts[words] + 1;
                }
                else
                {
                    // If it is not in the dictionary yet, add it with a count of 1
                    // (because this is the first time we've seen it)
                    wordCounts.Add(words, 1);
                }
            }
        }

        // If any textbox gets changed, we want to update the label
        // that shows the wordCount associated with the word in the 
        // textbox for the currently processed file.

        private void indexWordBox_TextChanged(object sender, EventArgs e)
        {
            // We know that the sender is a textbox.
            TextBox indexWordBox =  (TextBox) sender;

            // Call the updateAWord function to update the count for the 
            // word now in the textbox

            updateAWord(indexWordBox);
        }
    }
}
