namespace PigTournament
{
    partial class Form1
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
            this.StartTournamentButton = new System.Windows.Forms.Button();
            this.tournamentReport = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StartTournamentButton
            // 
            this.StartTournamentButton.Location = new System.Drawing.Point(307, 92);
            this.StartTournamentButton.Name = "StartTournamentButton";
            this.StartTournamentButton.Size = new System.Drawing.Size(147, 23);
            this.StartTournamentButton.TabIndex = 0;
            this.StartTournamentButton.Text = "Start Tournament";
            this.StartTournamentButton.UseVisualStyleBackColor = true;
            this.StartTournamentButton.Click += new System.EventHandler(this.StartTournamentButton_Click);
            // 
            // tournamentReport
            // 
            this.tournamentReport.Location = new System.Drawing.Point(160, 162);
            this.tournamentReport.Multiline = true;
            this.tournamentReport.Name = "tournamentReport";
            this.tournamentReport.Size = new System.Drawing.Size(476, 234);
            this.tournamentReport.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tournamentReport);
            this.Controls.Add(this.StartTournamentButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartTournamentButton;
        public System.Windows.Forms.TextBox tournamentReport;
    }
}

