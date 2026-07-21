namespace _160425129_Laura_FinderApp
{
    partial class FormGuardian
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
            this.btnLeave = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.labelQuestion = new System.Windows.Forms.Label();
            this.labelGuardianDialog = new System.Windows.Forms.Label();
            this.pictureBoxGuardian = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGuardian)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLeave
            // 
            this.btnLeave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeave.Location = new System.Drawing.Point(667, 342);
            this.btnLeave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(139, 38);
            this.btnLeave.TabIndex = 11;
            this.btnLeave.Text = "LEAVE";
            this.btnLeave.UseVisualStyleBackColor = true;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(838, 342);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(139, 38);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "SUBMIT";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Location = new System.Drawing.Point(667, 295);
            this.textBoxAnswer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAnswer.Multiline = true;
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.Size = new System.Drawing.Size(310, 38);
            this.textBoxAnswer.TabIndex = 9;
            // 
            // labelQuestion
            // 
            this.labelQuestion.BackColor = System.Drawing.Color.White;
            this.labelQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelQuestion.Location = new System.Drawing.Point(667, 163);
            this.labelQuestion.Name = "labelQuestion";
            this.labelQuestion.Size = new System.Drawing.Size(310, 116);
            this.labelQuestion.TabIndex = 8;
            this.labelQuestion.Text = "labelQuestion";
            this.labelQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelGuardianDialog
            // 
            this.labelGuardianDialog.BackColor = System.Drawing.Color.Transparent;
            this.labelGuardianDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelGuardianDialog.ForeColor = System.Drawing.Color.White;
            this.labelGuardianDialog.Location = new System.Drawing.Point(121, 113);
            this.labelGuardianDialog.Name = "labelGuardianDialog";
            this.labelGuardianDialog.Size = new System.Drawing.Size(363, 339);
            this.labelGuardianDialog.TabIndex = 7;
            this.labelGuardianDialog.Text = "labelGuardianDialog";
            this.labelGuardianDialog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxGuardian
            // 
            this.pictureBoxGuardian.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxGuardian.Image = global::_160425129_Laura_FinderApp.Properties.Resources.Old_Guardian;
            this.pictureBoxGuardian.Location = new System.Drawing.Point(383, 251);
            this.pictureBoxGuardian.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxGuardian.Name = "pictureBoxGuardian";
            this.pictureBoxGuardian.Size = new System.Drawing.Size(322, 235);
            this.pictureBoxGuardian.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxGuardian.TabIndex = 6;
            this.pictureBoxGuardian.TabStop = false;
            // 
            // FormGuardian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::_160425129_Laura_FinderApp.Properties.Resources.GuardianBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1098, 598);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.textBoxAnswer);
            this.Controls.Add(this.labelQuestion);
            this.Controls.Add(this.labelGuardianDialog);
            this.Controls.Add(this.pictureBoxGuardian);
            this.KeyPreview = true;
            this.Name = "FormGuardian";
            this.Text = "FormGuardian";
            this.Load += new System.EventHandler(this.FormGuardian_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGuardian_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGuardian)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLeave;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox textBoxAnswer;
        private System.Windows.Forms.Label labelQuestion;
        private System.Windows.Forms.Label labelGuardianDialog;
        private System.Windows.Forms.PictureBox pictureBoxGuardian;
    }
}