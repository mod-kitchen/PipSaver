namespace PSTK
{
    partial class Form2
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(224, 224, 224);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(131, 21);
            label1.TabIndex = 0;
            label1.Text = "Asset Pack Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(224, 224, 224);
            label2.Location = new Point(12, 63);
            label2.Name = "label2";
            label2.Size = new Size(92, 21);
            label2.TabIndex = 1;
            label2.Text = "Description:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(224, 224, 224);
            label3.Location = new Point(12, 122);
            label3.Name = "label3";
            label3.Size = new Size(61, 21);
            label3.TabIndex = 2;
            label3.Text = "Author:";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(48, 48, 67);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.ForeColor = Color.FromArgb(224, 224, 224);
            textBox1.Location = new Point(12, 33);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(310, 22);
            textBox1.TabIndex = 3;
            textBox1.Text = "TestPack";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(48, 48, 67);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.ForeColor = Color.FromArgb(224, 224, 224);
            textBox2.Location = new Point(12, 91);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(310, 22);
            textBox2.TabIndex = 4;
            textBox2.Text = "Asset pack for PipSaver";
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(48, 48, 67);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.ForeColor = Color.FromArgb(224, 224, 224);
            textBox3.Location = new Point(12, 146);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(310, 22);
            textBox3.TabIndex = 5;
            textBox3.Text = "PipSaver ToolKit";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(48, 48, 67);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.FromArgb(224, 224, 224);
            button1.Location = new Point(12, 186);
            button1.Name = "button1";
            button1.Size = new Size(157, 46);
            button1.TabIndex = 6;
            button1.Text = "Generate Mod";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(48, 48, 67);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = Color.FromArgb(224, 224, 224);
            button2.Location = new Point(175, 186);
            button2.Name = "button2";
            button2.Size = new Size(147, 46);
            button2.TabIndex = 7;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(334, 244);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form2";
            Text = "Edit Metadata";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button1;
        private Button button2;
    }
}