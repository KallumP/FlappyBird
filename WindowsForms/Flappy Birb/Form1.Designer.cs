namespace Flappy_Birb
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
            this.components = new System.ComponentModel.Container();
            this.gameLoop = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.fps_txt = new System.Windows.Forms.Label();
            this.fps_lbl = new System.Windows.Forms.Label();
            this.gameOver_lbl = new System.Windows.Forms.Label();
            this.score_txt = new System.Windows.Forms.Label();
            this.yourScore_lbl = new System.Windows.Forms.Label();
            this.restart_btn = new System.Windows.Forms.Button();
            this.clickToStart_lbl = new System.Windows.Forms.Label();
            this.showScore_txt = new System.Windows.Forms.Label();
            this.addToLead_btn = new System.Windows.Forms.Button();
            this.leaderBoard_lBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.name_tBox = new System.Windows.Forms.TextBox();
            this.yourName_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gameLoop
            // 
            this.gameLoop.Interval = 10;
            this.gameLoop.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Location = new System.Drawing.Point(15, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(364, 545);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // fps_txt
            // 
            this.fps_txt.AutoSize = true;
            this.fps_txt.BackColor = System.Drawing.Color.Transparent;
            this.fps_txt.Location = new System.Drawing.Point(38, 12);
            this.fps_txt.Name = "fps_txt";
            this.fps_txt.Size = new System.Drawing.Size(13, 13);
            this.fps_txt.TabIndex = 10;
            this.fps_txt.Text = "0";
            // 
            // fps_lbl
            // 
            this.fps_lbl.AutoSize = true;
            this.fps_lbl.BackColor = System.Drawing.Color.Transparent;
            this.fps_lbl.Location = new System.Drawing.Point(19, 12);
            this.fps_lbl.Name = "fps_lbl";
            this.fps_lbl.Size = new System.Drawing.Size(24, 13);
            this.fps_lbl.TabIndex = 9;
            this.fps_lbl.Text = "fps:";
            // 
            // gameOver_lbl
            // 
            this.gameOver_lbl.AutoSize = true;
            this.gameOver_lbl.BackColor = System.Drawing.Color.Transparent;
            this.gameOver_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameOver_lbl.Location = new System.Drawing.Point(78, 96);
            this.gameOver_lbl.Name = "gameOver_lbl";
            this.gameOver_lbl.Size = new System.Drawing.Size(245, 46);
            this.gameOver_lbl.TabIndex = 11;
            this.gameOver_lbl.Text = "Game Over!";
            this.gameOver_lbl.Visible = false;
            // 
            // score_txt
            // 
            this.score_txt.AutoSize = true;
            this.score_txt.BackColor = System.Drawing.Color.Transparent;
            this.score_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score_txt.Location = new System.Drawing.Point(227, 165);
            this.score_txt.Name = "score_txt";
            this.score_txt.Size = new System.Drawing.Size(24, 25);
            this.score_txt.TabIndex = 14;
            this.score_txt.Text = "0";
            this.score_txt.Visible = false;
            // 
            // yourScore_lbl
            // 
            this.yourScore_lbl.AutoSize = true;
            this.yourScore_lbl.BackColor = System.Drawing.Color.Transparent;
            this.yourScore_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yourScore_lbl.Location = new System.Drawing.Point(105, 165);
            this.yourScore_lbl.Name = "yourScore_lbl";
            this.yourScore_lbl.Size = new System.Drawing.Size(126, 25);
            this.yourScore_lbl.TabIndex = 13;
            this.yourScore_lbl.Text = "Your Score:";
            this.yourScore_lbl.Visible = false;
            // 
            // restart_btn
            // 
            this.restart_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restart_btn.Location = new System.Drawing.Point(28, 443);
            this.restart_btn.Name = "restart_btn";
            this.restart_btn.Size = new System.Drawing.Size(332, 51);
            this.restart_btn.TabIndex = 12;
            this.restart_btn.Text = "Restart game";
            this.restart_btn.UseVisualStyleBackColor = true;
            this.restart_btn.Visible = false;
            this.restart_btn.Click += new System.EventHandler(this.restart_btn_Click);
            // 
            // clickToStart_lbl
            // 
            this.clickToStart_lbl.AutoSize = true;
            this.clickToStart_lbl.BackColor = System.Drawing.Color.Transparent;
            this.clickToStart_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clickToStart_lbl.Location = new System.Drawing.Point(122, 12);
            this.clickToStart_lbl.Name = "clickToStart_lbl";
            this.clickToStart_lbl.Size = new System.Drawing.Size(145, 24);
            this.clickToStart_lbl.TabIndex = 15;
            this.clickToStart_lbl.Text = "Jump To Start!";
            // 
            // showScore_txt
            // 
            this.showScore_txt.AutoSize = true;
            this.showScore_txt.BackColor = System.Drawing.Color.Transparent;
            this.showScore_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showScore_txt.Location = new System.Drawing.Point(308, 8);
            this.showScore_txt.Name = "showScore_txt";
            this.showScore_txt.Size = new System.Drawing.Size(20, 24);
            this.showScore_txt.TabIndex = 8;
            this.showScore_txt.Text = "0";
            // 
            // addToLead_btn
            // 
            this.addToLead_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addToLead_btn.Location = new System.Drawing.Point(394, 501);
            this.addToLead_btn.Name = "addToLead_btn";
            this.addToLead_btn.Size = new System.Drawing.Size(244, 51);
            this.addToLead_btn.TabIndex = 16;
            this.addToLead_btn.Text = "Add to leader board";
            this.addToLead_btn.UseVisualStyleBackColor = true;
            this.addToLead_btn.Click += new System.EventHandler(this.addToLead_btn_Click);
            // 
            // leaderBoard_lBox
            // 
            this.leaderBoard_lBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.leaderBoard_lBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leaderBoard_lBox.FormattingEnabled = true;
            this.leaderBoard_lBox.Location = new System.Drawing.Point(394, 30);
            this.leaderBoard_lBox.Name = "leaderBoard_lBox";
            this.leaderBoard_lBox.Size = new System.Drawing.Size(244, 431);
            this.leaderBoard_lBox.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(400, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 22);
            this.label1.TabIndex = 18;
            this.label1.Text = "Leader Board";
            // 
            // name_tBox
            // 
            this.name_tBox.Location = new System.Drawing.Point(524, 475);
            this.name_tBox.Name = "name_tBox";
            this.name_tBox.Size = new System.Drawing.Size(114, 20);
            this.name_tBox.TabIndex = 19;
            // 
            // yourName_lbl
            // 
            this.yourName_lbl.AutoSize = true;
            this.yourName_lbl.BackColor = System.Drawing.Color.Transparent;
            this.yourName_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yourName_lbl.Location = new System.Drawing.Point(395, 470);
            this.yourName_lbl.Name = "yourName_lbl";
            this.yourName_lbl.Size = new System.Drawing.Size(123, 25);
            this.yourName_lbl.TabIndex = 20;
            this.yourName_lbl.Text = "Your name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 561);
            this.Controls.Add(this.yourName_lbl);
            this.Controls.Add(this.name_tBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.leaderBoard_lBox);
            this.Controls.Add(this.addToLead_btn);
            this.Controls.Add(this.clickToStart_lbl);
            this.Controls.Add(this.score_txt);
            this.Controls.Add(this.yourScore_lbl);
            this.Controls.Add(this.restart_btn);
            this.Controls.Add(this.gameOver_lbl);
            this.Controls.Add(this.fps_txt);
            this.Controls.Add(this.fps_lbl);
            this.Controls.Add(this.showScore_txt);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Flappy Bird";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameLoop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label fps_txt;
        private System.Windows.Forms.Label fps_lbl;
        private System.Windows.Forms.Label gameOver_lbl;
        private System.Windows.Forms.Label score_txt;
        private System.Windows.Forms.Label yourScore_lbl;
        private System.Windows.Forms.Button restart_btn;
        private System.Windows.Forms.Label clickToStart_lbl;
        private System.Windows.Forms.Label showScore_txt;
        private System.Windows.Forms.Button addToLead_btn;
        private System.Windows.Forms.ListBox leaderBoard_lBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name_tBox;
        private System.Windows.Forms.Label yourName_lbl;
    }
}

