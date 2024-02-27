namespace Keyer
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
            button1 = new Button();
            openFileDialog1 = new OpenFileDialog();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button3 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            colorDialog1 = new ColorDialog();
            button5 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 368);
            button1.Name = "button1";
            button1.Size = new Size(109, 29);
            button1.TabIndex = 0;
            button1.Text = "Загрузить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(694, 368);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 1;
            button2.Text = "Выбрать";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(499, 345);
            label1.Name = "label1";
            label1.Size = new Size(289, 20);
            label1.TabIndex = 2;
            label1.Text = "Выберите место сохранения результата";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 345);
            label2.Name = "label2";
            label2.Size = new Size(247, 20);
            label2.TabIndex = 3;
            label2.Text = "Загрузить исходное изображение";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(41, 279);
            label3.Name = "label3";
            label3.Size = new Size(177, 20);
            label3.TabIndex = 5;
            label3.Text = "Исходное изображение";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(584, 279);
            label4.Name = "label4";
            label4.Size = new Size(75, 20);
            label4.TabIndex = 6;
            label4.Text = "Результат";
            // 
            // button3
            // 
            button3.Location = new Point(305, 368);
            button3.Name = "button3";
            button3.Size = new Size(137, 53);
            button3.TabIndex = 7;
            button3.Text = "Автоматическое удаление фона";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button5
            // 
            button5.Location = new Point(254, 313);
            button5.Name = "button5";
            button5.Size = new Size(252, 29);
            button5.TabIndex = 9;
            button5.Text = "Удаление выбранного цвета";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Текстуля";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private OpenFileDialog openFileDialog1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button3;
        private FolderBrowserDialog folderBrowserDialog1;
        private PictureBox pictureBox1;
        private ColorDialog colorDialog1;
        private Button button5;
    }
}
