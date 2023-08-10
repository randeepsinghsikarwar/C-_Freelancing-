using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniExcel
{
    public partial class Form1 : Form
    {
        double A = 0, B = 0, C = 0, D = 0;
        public Form1()
        {
            InitializeComponent();
        }

        // If you pass this function the name of a textbox,
        // it will grab the current value of the textbox that matches
        // the name e.g. given 'A', returns the value of TextBoxA

        // Note:  if the text box is empty or you pass a character
        // that isn't a textbox name, this will return 0.

        private double getValue(char boxName)
        {
            string value;
            switch (boxName)
            {
                case 'A':
                    value = textBoxA.Text;
                    break;
                case 'B':
                    value = textBoxB.Text;
                    break;
                case 'C':
                    value = textBoxC.Text;
                    break;
                case 'D':
                    value = textBoxD.Text;
                    break;


                default:
                    value = "0";
                    break;
            }

            if (value == "") { value = "0"; }

            return Convert.ToDouble(value);
        }


        // If you pass this function a textbox, it will
        // look in the textbox, and it will pull out the 
        // first character and the third character, which 
        // should both be textbox names

        // It will pull out the second character as the operator

        // It will return the 'name' of the textbox in the formula
        // in either the first position or the second position.

        private char parseFormula(TextBox formulaBox, int n)
        {
            // grab the formula from the textbox
            string formula = formulaBox.Text;

            // If there aren't 3 characters in the formula, do nothing,
            // this isn't a valid formula
            if(formula.Length < 5)
            {
                return ' ';
            }

            // First character is the first value, and third character
            // is the second value.   the operator is in the second spot

            char firstValue = formula[0];
            char secondValue = formula[2];
            char op1 = formula[1];  // for core assignment, op is always +
            char thirdValue = formula[4];
            char textBoxName = firstValue;

            switch(n)
            {
                case 2:
                    textBoxName = secondValue;
                    break;
                case 3:
                    textBoxName = thirdValue;
                    break;
            }

           /* char textBoxName = firstValue;
            if (n==2)
            {
                textBoxName = secondValue;
            }*/

            return textBoxName;

        }

        // get the first name from a formula in a formula box (e.g. 'C' from C+D+D)
        private char getFirstFormulaBoxName(TextBox formulaBox)
        {
            return parseFormula(formulaBox, 1);
        }

        // get the second name from a formula in a formula box (e.g. 'D' from C+D+D)
        private char getSecondFormulaBoxName(TextBox formulaBox)
        {
            return parseFormula(formulaBox, 2);
        }

        // get the third name from a formula in a formula box (e.g. 'D' from C+D+D)
        private char getThridFormulaBoxName(TextBox formulaBox)
        {
            return parseFormula(formulaBox, 3);
        }
        public void Form1_Load(object sender, EventArgs e)
        {
           
        }

        /*
         down below, are the 4 event handlers which will be triggered when the values of the textBox[A-D] changes. 
         they will update the new value of the textbox with its respective global variables.
         */

        private void textBoxA_TextChanged(object sender, EventArgs e)
        {
            A = getValue('A');
            recalculate(A, B, C, D);
        }
        private void textBoxB_TextChanged(object sender, EventArgs e)
        {
            A = getValue('B');
            recalculate(A, B, C, D);
        }
        private void textBoxC_TextChanged(object sender, EventArgs e)
        {
            A = getValue('C');
            recalculate(A, B, C, D);
        }
        private void textBoxD_TextChanged(object sender, EventArgs e)
        {
            A = getValue('D');
            recalculate(A, B, C, D);
        }

/*
 this is the recalculate method. this will calculate the values for texBox[W-Z] as per the formula mentioned in the respective textBoxFormula
. this method will be called again and again when the value of the textBox[A-D] changes.
 */

        private void recalculate(double A, double B, double C, double D )
        {

            textBoxW.Text = (getValue(getFirstFormulaBoxName(TextBoxFormulaW)) + getValue(getSecondFormulaBoxName(TextBoxFormulaW)) + getValue(getThridFormulaBoxName(TextBoxFormulaW))).ToString();
            textBoxX.Text = (getValue(getFirstFormulaBoxName(textBoxFormulaX)) + getValue(getSecondFormulaBoxName(textBoxFormulaX)) + getValue(getThridFormulaBoxName(textBoxFormulaX))).ToString();
            textBoxY.Text = (getValue(getFirstFormulaBoxName(textBoxFormulaY)) + getValue(getSecondFormulaBoxName(textBoxFormulaY)) + getValue(getThridFormulaBoxName(textBoxFormulaY))).ToString();
            textBoxZ.Text = (getValue(getFirstFormulaBoxName(textBoxFormulaZ)) + getValue(getSecondFormulaBoxName(textBoxFormulaZ)) + getValue(getThridFormulaBoxName(textBoxFormulaZ))).ToString();
        }
    }
}

/*
 Extensions - 
made the formula work for a 5-Chatacter string
 */
