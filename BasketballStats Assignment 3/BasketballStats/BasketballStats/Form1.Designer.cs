namespace BasketballStats
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
            this.teamDataGridView = new System.Windows.Forms.DataGridView();
            this.teamTabControl = new System.Windows.Forms.TabControl();
            this.teamTabPage = new System.Windows.Forms.TabPage();
            this.regularSeasonPPGLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.regularSeasonRecordLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.arenaName = new System.Windows.Forms.Label();
            this.teamName = new System.Windows.Forms.Label();
            this.teamGamesTabPage = new System.Windows.Forms.TabPage();
            this.gameGridView = new System.Windows.Forms.DataGridView();
            this.opponentComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.teamDataGridView)).BeginInit();
            this.teamTabControl.SuspendLayout();
            this.teamTabPage.SuspendLayout();
            this.teamGamesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // teamDataGridView
            // 
            this.teamDataGridView.AllowUserToAddRows = false;
            this.teamDataGridView.AllowUserToDeleteRows = false;
            this.teamDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.teamDataGridView.Location = new System.Drawing.Point(13, 13);
            this.teamDataGridView.Name = "teamDataGridView";
            this.teamDataGridView.ReadOnly = true;
            this.teamDataGridView.RowHeadersWidth = 51;
            this.teamDataGridView.Size = new System.Drawing.Size(450, 552);
            this.teamDataGridView.TabIndex = 0;
            // 
            // teamTabControl
            // 
            this.teamTabControl.Controls.Add(this.teamTabPage);
            this.teamTabControl.Controls.Add(this.teamGamesTabPage);
            this.teamTabControl.Location = new System.Drawing.Point(492, 13);
            this.teamTabControl.Name = "teamTabControl";
            this.teamTabControl.SelectedIndex = 0;
            this.teamTabControl.Size = new System.Drawing.Size(720, 552);
            this.teamTabControl.TabIndex = 1;
            // 
            // teamTabPage
            // 
            this.teamTabPage.Controls.Add(this.regularSeasonPPGLabel);
            this.teamTabPage.Controls.Add(this.label3);
            this.teamTabPage.Controls.Add(this.regularSeasonRecordLabel);
            this.teamTabPage.Controls.Add(this.label1);
            this.teamTabPage.Controls.Add(this.arenaName);
            this.teamTabPage.Controls.Add(this.teamName);
            this.teamTabPage.Location = new System.Drawing.Point(4, 22);
            this.teamTabPage.Name = "teamTabPage";
            this.teamTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.teamTabPage.Size = new System.Drawing.Size(712, 526);
            this.teamTabPage.TabIndex = 0;
            this.teamTabPage.Text = "Team";
            this.teamTabPage.UseVisualStyleBackColor = true;
            // 
            // regularSeasonPPGLabel
            // 
            this.regularSeasonPPGLabel.AutoSize = true;
            this.regularSeasonPPGLabel.Location = new System.Drawing.Point(122, 107);
            this.regularSeasonPPGLabel.Name = "regularSeasonPPGLabel";
            this.regularSeasonPPGLabel.Size = new System.Drawing.Size(28, 13);
            this.regularSeasonPPGLabel.TabIndex = 5;
            this.regularSeasonPPGLabel.Text = "0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Points Per Game";
            // 
            // regularSeasonRecordLabel
            // 
            this.regularSeasonRecordLabel.AutoSize = true;
            this.regularSeasonRecordLabel.Location = new System.Drawing.Point(122, 84);
            this.regularSeasonRecordLabel.Name = "regularSeasonRecordLabel";
            this.regularSeasonRecordLabel.Size = new System.Drawing.Size(22, 13);
            this.regularSeasonRecordLabel.TabIndex = 3;
            this.regularSeasonRecordLabel.Text = "0-0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Regular Season";
            // 
            // arenaName
            // 
            this.arenaName.AutoSize = true;
            this.arenaName.Location = new System.Drawing.Point(21, 46);
            this.arenaName.Name = "arenaName";
            this.arenaName.Size = new System.Drawing.Size(62, 13);
            this.arenaName.TabIndex = 1;
            this.arenaName.Text = "arenaName";
            // 
            // teamName
            // 
            this.teamName.AutoSize = true;
            this.teamName.Location = new System.Drawing.Point(21, 16);
            this.teamName.Name = "teamName";
            this.teamName.Size = new System.Drawing.Size(58, 13);
            this.teamName.TabIndex = 0;
            this.teamName.Text = "teamName";
            // 
            // teamGamesTabPage
            // 
            this.teamGamesTabPage.Controls.Add(this.label4);
            this.teamGamesTabPage.Controls.Add(this.label2);
            this.teamGamesTabPage.Controls.Add(this.opponentComboBox);
            this.teamGamesTabPage.Controls.Add(this.gameGridView);
            this.teamGamesTabPage.Location = new System.Drawing.Point(4, 22);
            this.teamGamesTabPage.Name = "teamGamesTabPage";
            this.teamGamesTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.teamGamesTabPage.Size = new System.Drawing.Size(712, 526);
            this.teamGamesTabPage.TabIndex = 1;
            this.teamGamesTabPage.Text = "Games";
            this.teamGamesTabPage.UseVisualStyleBackColor = true;
            // 
            // gameGridView
            // 
            this.gameGridView.AllowUserToAddRows = false;
            this.gameGridView.AllowUserToDeleteRows = false;
            this.gameGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gameGridView.Location = new System.Drawing.Point(6, 81);
            this.gameGridView.Name = "gameGridView";
            this.gameGridView.ReadOnly = true;
            this.gameGridView.RowHeadersWidth = 51;
            this.gameGridView.Size = new System.Drawing.Size(694, 439);
            this.gameGridView.TabIndex = 1;
            // 
            // opponentComboBox
            // 
            this.opponentComboBox.FormattingEnabled = true;
            this.opponentComboBox.Location = new System.Drawing.Point(22, 42);
            this.opponentComboBox.Name = "opponentComboBox";
            this.opponentComboBox.Size = new System.Drawing.Size(144, 21);
            this.opponentComboBox.TabIndex = 2;
            this.opponentComboBox.SelectedIndexChanged += new System.EventHandler(this.opponentComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filters";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Opponent";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 657);
            this.Controls.Add(this.teamTabControl);
            this.Controls.Add(this.teamDataGridView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.teamDataGridView)).EndInit();
            this.teamTabControl.ResumeLayout(false);
            this.teamTabPage.ResumeLayout(false);
            this.teamTabPage.PerformLayout();
            this.teamGamesTabPage.ResumeLayout(false);
            this.teamGamesTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView teamDataGridView;
        private System.Windows.Forms.TabControl teamTabControl;
        private System.Windows.Forms.TabPage teamTabPage;
        private System.Windows.Forms.TabPage teamGamesTabPage;
        private System.Windows.Forms.Label arenaName;
        private System.Windows.Forms.Label teamName;
        private System.Windows.Forms.Label regularSeasonRecordLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label regularSeasonPPGLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gameGridView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox opponentComboBox;
    }
}

