using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotMachineStarterCode
{
    public partial class Form1 : Form
    {
        //new lables added
        Label cBalance = new Label();
        Label wonLabel = new Label();
        Label spentLabel = new Label();
        Label keepTrack = new Label();

        //new buttons added
        Button addFive = new Button();
        Button reset = new Button();


        int currentBalance = 25;
        int won = 0;
        int spent = 25;

        //variables for keeling track are created
        int one = 0, ten = 0, tFive = 0, spin = 0;

        int timerCounter;
        Image seven;
        Image lemon;
        Image grape;
        Image pineapple;

        public Form1()
        {
            addFive.Text = "Add 5$";
            addFive.Visible = true;
            addFive.Location = new Point(200, 200);
            addFive.Click += new EventHandler(addFive_Click);
            addFive.Visible = false;
            this.Controls.Add(addFive);

            reset.Text = "Restart";
            reset.Visible = true;
            reset.Location = new Point(450, 199);
            reset.Click += new EventHandler(reset_Click);
            reset.Visible = false;
            this.Controls.Add(reset);




            InitializeComponent();

            
            render();

            // Load my slot machine images from disk
            seven = Image.FromFile("../../Images/seven.png");
            grape = Image.FromFile("../../Images/grape.png");
            lemon = Image.FromFile("../../Images/lemon.png");
            pineapple = Image.FromFile("../../Images/pineapple.png");

            // we have 512 x 512 pixel images, make them fit the 128 x 128 pictureBoxes
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            // Start all Images as seven
            pictureBox1.Image = seven;
            pictureBox2.Image = lemon;
            pictureBox3.Image = seven;



        }

        //this function will render the labels. 
        private void render()
        {
            keepTrack.Text = spin.ToString() + " spins: " + one.ToString() + " x $1 and " + ten.ToString() + " x $10 and " + tFive.ToString() + " x $25. \nWinnings: $" + won.ToString() + " Spin Cost: $" + (spent).ToString();
            keepTrack.Location = new Point(50, 400);
            keepTrack.AutoSize = true;
            this.Controls.Add(keepTrack);

            cBalance.Text = "Current balance: " + currentBalance.ToString();
            cBalance.Location = new Point(150, 250);
            cBalance.AutoSize = true;
            cBalance.Font = new Font("Times New Roman", 14);
            cBalance.BackColor = Color.Black;
            cBalance.ForeColor = Color.White;
            this.Controls.Add(cBalance);

            wonLabel.Text = "Won: " + won;
            wonLabel.Location = new Point(350, 250);
            wonLabel.Font = new Font("Times New Roman", 14);
            wonLabel.BackColor = Color.Black;
            wonLabel.ForeColor = Color.White;
            wonLabel.AutoSize = true;
            this.Controls.Add(wonLabel);

            spentLabel.Text = "Spent: " + spent;
            spentLabel.Location = new Point(450, 250);
            spentLabel.BackColor = Color.Black;
            spentLabel.Font = new Font("Times New Roman", 14);
            spentLabel.ForeColor = Color.White;
            spentLabel.AutoSize = true;
            this.Controls.Add(spentLabel);

        }

        private void setImage(PictureBox pb, int n)
        {
            switch (n)
            {
                case 1:
                    pb.Image = grape;
                    break;
                case 2:
                    pb.Image = lemon;
                    break;
                case 3:
                    pb.Image = seven;
                    break;
                case 4:
                    pb.Image = pineapple;
                    break;
            }
        }
        private void rotateImages()
        {
            Random rnd = new Random();
            setImage(pictureBox1, rnd.Next(1, 5));
            setImage(pictureBox2, rnd.Next(1, 5));
            setImage(pictureBox3, rnd.Next(1, 5));
        }

        //logic for showing warning message on insufficient funds added. 
        private void spinButton_Click(object sender, EventArgs e)
        {
            if(currentBalance < 2 )
            {
                addFive.Visible = true;
                reset.Visible = true;
                render();
                MessageBox.Show("Insufficient balance, please add money");
            }

            //driver code
            else
            {
                        // Start time and reset timerCounter
                        timerCounter = 0;
                        timer1.Interval = 100; // 100 ms or 1/10 of a second
                        timer1.Start();
                        currentBalance = currentBalance - 2;
                        spin++;


                /* logic for reward allotment */
                if(pictureBox1.Image == seven)
                {
                    if(pictureBox2.Image == seven)
                    {
                        if (pictureBox3.Image == seven)
                            tFive++;
                            won = won + 25;
                    }
                }

                else if(pictureBox1.Image == lemon)
                {
                    if(pictureBox2.Image == lemon)
                    {
                        if (pictureBox3.Image == lemon)
                        {
                            ten++;
                            won = won + 10;
                        }

                        else if (pictureBox3.Image == grape)
                        {
                            one++;
                            won = won + 1;
                        }

                        else if (pictureBox3.Image == pineapple)
                        {
                            one++;
                            won = won + 1;
                        }
                    }

                    else if (pictureBox2.Image == grape)
                    {
                        if (pictureBox3.Image != seven)
                        {
                            one++;
                            won = won + 1;
                        }
                    }

                    else if (pictureBox2.Image == pineapple)
                    {
                        if (pictureBox3.Image != seven)
                        {
                            one++;
                            won = won + 1;
                        }
                    }
                }

                else if (pictureBox1.Image == grape)
                {
                    if (pictureBox2.Image == grape)
                    {
                        if (pictureBox3.Image == grape)
                        {
                            ten++;
                            won = won + 10;
                        }

                        else if (pictureBox3.Image == lemon)
                        {
                            one++;
                            won = won + 1;
                        }

                        else if (pictureBox3.Image == pineapple)
                        {
                            one++;
                            won = won + 1;
                        }
                    }

                    else if (pictureBox2.Image == lemon)
                    {
                        if (pictureBox3.Image != seven)
                        {
                            one++;
                            won = won + 1;
                        }
                    }

                    else if (pictureBox2.Image == pineapple)
                    {
                        if (pictureBox3.Image != seven)
                        {
                            one++;
                            won = won + 1;
                        }
                    }
                }

                else if (pictureBox1.Image == pineapple)
                {
                    if (pictureBox2.Image == pineapple)
                    {
                        if (pictureBox3.Image == pineapple)
                        {
                            ten++;
                            won = won + 10;
                        }

                        else if (pictureBox3.Image == grape)
                        {
                            one++;
                            won = won + 1;
                        }

                        else if (pictureBox3.Image == lemon)
                        {
                            one++;
                            won = won + 1;
                        }
                    }

                    else if (pictureBox2.Image == grape)
                    {
                        if (pictureBox3.Image != seven)
                        {
                            one++;
                            won = won + 1;
                        }
                    }

                    else if (pictureBox2.Image == lemon)
                    {
                        if (pictureBox3.Image != seven)
                        {
                            one++;
                            won = won + 1;
                        }
                    }
                }

                render();
                    }
        }

        //function to add 5$ in current balance amount
        private void addFive_Click(object sender, EventArgs e)
        {
            currentBalance = currentBalance + 5;
            spent = spent + 5;
            render();
        }

        //function to reset the values to default values
        private void reset_Click(object sender, EventArgs e)
        {
            spin = 0; one = 0; ten = 0; tFive = 0;   
            currentBalance = 25;
            won = 0;
            spent = 0;
            addFive.Visible = false;
            reset.Visible = false;
            pictureBox1.Image = seven;
            pictureBox2.Image = lemon;
            pictureBox3.Image = seven;
            render();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerCounter += 1;

            // first 10 ticks are fast (1/10 of a second), second two are slower (400ms)
            if (timerCounter == 10)
            {
                timer1.Interval = 400;
            }

            if (timerCounter <= 12)
            {
                // rotate all the images every tick for the first 12 ticks
                rotateImages();
            }

            if (timerCounter > 12)
            {
                // stop the timer, we are done
                timer1.Stop();
            }
        }
    }
}



