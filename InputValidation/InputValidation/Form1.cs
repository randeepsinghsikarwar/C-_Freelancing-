using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputValidation
{
    public partial class Form1 : Form
    {
        int answer;
        public Form1()
        {
            

            InitializeComponent();
            maskedTextBox2.Mask = "00/00/0000";
            maskedTextBox2.ValidatingType = typeof(System.DateTime);
            toolTip1.IsBalloon = true;


            // Hint A1 -- set the mask and the validating type of the Age entry box here
            maskedTextBox3 = new MaskedTextBox();
            maskedTextBox3.Mask = "00";
            maskedTextBox3.ValidatingType = typeof(System.Int32);
            maskedTextBox3.Location = new Point(400, 300);
            maskedTextBox3.Size = new Size(50, 50);
            this.Controls.Add(maskedTextBox3);
            maskedTextBox3.MaskInputRejected += new MaskInputRejectedEventHandler(maskedTextBox3_MaskInputRejected);
            maskedTextBox3.TypeValidationCompleted += new TypeValidationEventHandler(maskedTextBox3_TypeValidationCompleted);

           
        }

       
        

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (maskedTextBox2.MaskFull)
            {
                toolTip2.ToolTipTitle = "Input Rejected -- Too much data";
                toolTip2.Show("input mask is full, stop typing!", maskedTextBox2, 0, 50, 2000);
            }
            else if (e.Position == maskedTextBox2.Mask.Length)
            {
                toolTip2.ToolTipTitle = "Input Rejected";
                toolTip2.Show("You cannot add any more characters at the end of this field", maskedTextBox2, 0, 50);

            }
            else
            {
                toolTip2.ToolTipTitle = "Input Rejected";
                toolTip2.Show("You can only add numeric characters into this date field", maskedTextBox2, 0, 50);
            }
        }

        // Hint A2 -- create a MaskInputRejected event handler for your age entry box
        // Just show a generic error message for anything saying that you can only enter numbers of up to two digits

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (maskedTextBox3.Text.Length>1)
            {
                MessageBox.Show("you are allowed to enter only a 2 digit number. ");
                maskedTextBox3.ResetText();
            }

            else
            {
                if (int.TryParse(maskedTextBox3.Text, out int result))
                {

                }
                else
                {
                    MessageBox.Show("Invalid input, enter a number");
                }
            }
        }




        private void maskedTextBox2_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if(!e.IsValidInput)
            {
                toolTip1.ToolTipTitle = "Invalid Date";
                toolTip1.Show("That isn't a valid date in the format specified", maskedTextBox2, 0, -70, 2000);
            } else
            {
                // Here we can perform additional validation -- e.g. make sure that the date is in the future
                DateTime userDate = (DateTime)e.ReturnValue;
                if(userDate < DateTime.Now)
                {
                    toolTip1.ToolTipTitle = "Invalid Date";
                    toolTip1.Show("Only dates in the future are accepted, sorry!", maskedTextBox2, 0, -70, 2000);
                    e.Cancel = true;
                }
            }
        }
        
        // Hint A3 -- create a TypeValidationCompleted handler for your age entry box
        // First, make sure that it is in fact an integer -- although hard to get that entry error past the entry mask
        // Second, make sure that the age is >=13 and if not show a tooltip about this being a PG-13 form.

        private void maskedTextBox3_TypeValidationCompleted(Object sender, TypeValidationEventArgs e)
        {
            ToolTip t = new ToolTip();
            if (!e.IsValidInput)
            {
                t.ToolTipTitle = "invalid age";
                t.Show("Please enter a valid age", maskedTextBox3, 0,-70, 2000);
            }

            int agee = Int32.Parse(maskedTextBox3.Text);
            if (agee < 13)
            {
                t.Show("this is a pg-13 form, invalid age", maskedTextBox3, 0, -70, 2000);
            }
           



        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidEmailAddress(textBox1.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                textBox1.Select(0, textBox1.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(textBox1, errorMsg);
            }
        }

        public bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            // Confirm that the email address string is not empty.
            if (emailAddress.Length == 0)
            {
                errorMessage = "email address is required.";
                return false;
            }

            // Confirm that there is an "@" and a "." in the email address, and in the correct order.
            if (emailAddress.IndexOf("@") > -1)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    errorMessage = "";
                    return true;
                }
            }

            errorMessage = "email address must be valid email address format.\n" +
               "For example 'someone@example.com' ";
            return false;
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            // If all conditions have been met, clear the ErrorProvider of errors.
            errorProvider1.SetError(textBox1, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int firstValue;
            if(!Int32.TryParse(textBox2.Text,out firstValue))
            {
                textBox2.BackColor = Color.Red;
                textBox4.Text = "Error: first operand could not be parsed";
                return;
            }

            textBox2.BackColor = Color.White;
            int secondValue;

            try
            {
                secondValue = Int32.Parse(textBox3.Text);

                if (secondValue < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                int answer = firstValue + secondValue;

                textBox4.Text = answer.ToString();
            } catch (FormatException ex)
            {
                MessageBox.Show("Second operand is not well formatted, please in put an integer!", "Format Error");
            } catch (OverflowException ex)
            {
                MessageBox.Show("Second operand is either too large or too small!", "Overflow Error");
            } catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Second operand is below zero.  I don't add negatives.", "Negative Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unanticipated Error", "Weird Error");
            }


        }

        private int calculateFunction(string[] formulaBits)
        {
           

            // Hint B3 -- the formula should be an integer, then a + or -, then another integer.
            // So we are expecting an array of length 3
            // If the array is *not* of length 3, throw a new  Argument exception

            


            int firstValue = Int32.Parse(formulaBits[0]);
            int secondValue = Int32.Parse(formulaBits[2]);

            try
            {
                if(firstValue<0 || secondValue < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch(ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            // Hint B5 -- if either are below zero, throw the ArgumentOutOfRange Exception

            try
            {
                if (formulaBits[1] == "-" || formulaBits[1] == "+") {
                    
                    switch (formulaBits[1])
                    {
                        case "+":
                            answer = firstValue + secondValue;
                            break;
                        case "-":
                            answer = firstValue - secondValue;
                            break;

                        /*default:
                            answer = 0;*/
                            // Hint B4 -- If we didn't get an expected Operator, let's not calculate
                            // Instead, we can throw a new InvalidOperationException
                          /*  break;*/
                    }
                }

                else
                {
                    switch (formulaBits[1])
                    {
                        case "*":
                            MessageBox.Show("invalid * operator ");
                            break;
                        case "/":
                            MessageBox.Show("invalid / operator");
                            break;
                        case "%":
                            MessageBox.Show("invalid % operator ");
                        break;
                        default:
                            MessageBox.Show("inputs like A, $, #, etc are invalid");
                            break;
                    }
                    
                    throw new InvalidOperationException();
                }
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }





            // Hint B6 -- if the answer is below zero, throw a new ArgumentOutOfRange Exception

            try {

                if (answer < 0)
                {
                    throw new ArgumentOutOfRangeException();
                    
                }

                
            }
            catch(ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return answer;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string formula = textBox5.Text;
            string[] formulaBits = formula.Split(' ');

            // Hint B1 Need to wrap the below in a try - catch block

            try
            {
                if (formulaBits.Length != 3)
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("length of expression exceeded");
            }

            try
            {
                textBox6.Text = calculateFunction(formulaBits).ToString();

            }
            catch (FormatException ex)
            {
                MessageBox.Show("format exception");
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("overflow exception");
            }
           


            // Hint B2  
            // Make sure you catch the FormatException and OverflowException that might be thrown by the Parse function

            // Make sure you catch the Argument exception in case there formula doesn't have the right structure  Hint B3a
            // Make sure you catch the InvalidOperationException in case the formula doesn't have the right symbol. Hint B4a
            // Make sure you catch the ArgumentOutOfRangeException because we don't like numbers below zero. Hint B5a / B6a

            // In each different exception that you catch, use a MessageBox.Show() statement like in the button1_Click handler.

        }
    }
}
