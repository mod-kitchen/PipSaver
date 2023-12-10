namespace PSTK
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            listBox1 = new ListBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            openFileDialog1 = new OpenFileDialog();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(48, 48, 67);
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            listBox1.ForeColor = Color.FromArgb(224, 224, 224);
            listBox1.FormattingEnabled = true;
            listBox1.HorizontalScrollbar = true;
            listBox1.ItemHeight = 21;
            listBox1.Location = new Point(12, 10);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(308, 420);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(48, 48, 67);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(12, 493);
            button1.Name = "button1";
            button1.Size = new Size(308, 37);
            button1.TabIndex = 1;
            button1.Text = "Add Image(s)...";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(48, 48, 67);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(12, 537);
            button2.Name = "button2";
            button2.Size = new Size(308, 37);
            button2.TabIndex = 2;
            button2.Text = "Remove Image(s)";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(48, 48, 67);
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(337, 493);
            button3.Name = "button3";
            button3.Size = new Size(415, 81);
            button3.TabIndex = 3;
            button3.Text = "Generate Pack Mod";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(48, 48, 67);
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Location = new Point(337, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(415, 420);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(337, 430);
            label1.Name = "label1";
            label1.Size = new Size(415, 60);
            label1.TabIndex = 5;
            label1.Text = "Press Add Image(s) to add images to the project.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "png";
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "PNG Image|*.png";
            openFileDialog1.Multiselect = true;
            openFileDialog1.RestoreDirectory = true;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 430);
            label2.Name = "label2";
            label2.Size = new Size(308, 60);
            label2.TabIndex = 6;
            label2.Text = "0/500 slots used";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(764, 587);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listBox1);
            ForeColor = Color.FromArgb(224, 224, 224);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Pip-Saver ToolKit";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private PictureBox pictureBox1;
        private Label label1;
        private OpenFileDialog openFileDialog1;
        private Label label2;
    }
}