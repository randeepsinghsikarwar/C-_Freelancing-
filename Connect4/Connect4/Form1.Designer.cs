namespace Connect4
{
    partial class Connect4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.boardBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.playerBox = new System.Windows.Forms.PictureBox();
            this.CurrentPlayer = new System.Windows.Forms.Label();
            this.winnerLabel = new System.Windows.Forms.Label();
            this.winnerBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.boardBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.winnerBox)).BeginInit();
            this.SuspendLayout();
            // 
            // boardBox
            // 
            this.boardBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.boardBox.Location = new System.Drawing.Point(100, 100);
            this.boardBox.Name = "boardBox";
            this.boardBox.Size = new System.Drawing.Size(700, 600);
            this.boardBox.TabIndex = 0;
            this.boardBox.TabStop = false;
            this.boardBox.Paint += new System.Windows.Forms.PaintEventHandler(this.boardBox_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "\\/";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(235, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 31);
            this.button2.TabIndex = 2;
            this.button2.Text = "\\/";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(335, 50);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 31);
            this.button3.TabIndex = 3;
            this.button3.Text = "\\/";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(435, 50);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 31);
            this.button4.TabIndex = 4;
            this.button4.Text = "\\/";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(535, 50);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(30, 31);
            this.button5.TabIndex = 5;
            this.button5.Text = "\\/";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(635, 50);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(30, 31);
            this.button6.TabIndex = 6;
            this.button6.Text = "\\/";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(735, 50);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(30, 31);
            this.button7.TabIndex = 7;
            this.button7.Text = "\\/";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // playerBox
            // 
            this.playerBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.playerBox.Location = new System.Drawing.Point(900, 400);
            this.playerBox.Name = "playerBox";
            this.playerBox.Size = new System.Drawing.Size(100, 100);
            this.playerBox.TabIndex = 8;
            this.playerBox.TabStop = false;
            this.playerBox.Paint += new System.Windows.Forms.PaintEventHandler(this.playerBox_Paint);
            // 
            // CurrentPlayer
            // 
            this.CurrentPlayer.AutoSize = true;
            this.CurrentPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.CurrentPlayer.Location = new System.Drawing.Point(896, 377);
            this.CurrentPlayer.Name = "CurrentPlayer";
            this.CurrentPlayer.Size = new System.Drawing.Size(109, 20);
            this.CurrentPlayer.TabIndex = 9;
            this.CurrentPlayer.Text = "Current Player";
            // 
            // winnerLabel
            // 
            this.winnerLabel.AutoSize = true;
            this.winnerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.winnerLabel.Location = new System.Drawing.Point(920, 544);
            this.winnerLabel.Name = "winnerLabel";
            this.winnerLabel.Size = new System.Drawing.Size(63, 20);
            this.winnerLabel.TabIndex = 11;
            this.winnerLabel.Text = "Winner!";
            // 
            // winnerBox
            // 
            this.winnerBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.winnerBox.Location = new System.Drawing.Point(900, 567);
            this.winnerBox.Name = "winnerBox";
            this.winnerBox.Size = new System.Drawing.Size(100, 100);
            this.winnerBox.TabIndex = 10;
            this.winnerBox.TabStop = false;
            this.winnerBox.Paint += new System.Windows.Forms.PaintEventHandler(this.winnerBox_Paint);
            // 
            // Connect4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 769);
            this.Controls.Add(this.winnerLabel);
            this.Controls.Add(this.winnerBox);
            this.Controls.Add(this.CurrentPlayer);
            this.Controls.Add(this.playerBox);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.boardBox);
            this.Name = "Connect4";
            this.Text = "Connect 4";
            ((System.ComponentModel.ISupportInitialize)(this.boardBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.winnerBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox boardBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.PictureBox playerBox;
        private System.Windows.Forms.Label CurrentPlayer;
        private System.Windows.Forms.Label winnerLabel;
        private System.Windows.Forms.PictureBox winnerBox;
    }
}

