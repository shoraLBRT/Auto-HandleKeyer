using BackgroundHandleRemover;
using Keyer.AutoRemover;

namespace Keyer
{
    public partial class Form1 : Form
    {
        private string inputFilePath;
        private string outputFilePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "image files (*.BMP; *.JPG; *.GIF; *.PNG)| *.BMP; *.JPG; *.GIF; *.PNG";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.Multiselect = false;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = null;

            PictureBox pb1 = new PictureBox();


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inputFilePath = openFileDialog1.FileName;
                if (openFileDialog1.FileName != null)
                {
                    MessageBox.Show("����������� ��������� �������");

                    pb1.ImageLocation = inputFilePath;
                    pb1.Location = new Point(29, 3);
                    pb1.Size = new Size(300, 250);
                    pb1.SizeMode = PictureBoxSizeMode.Zoom;
                    pb1.BorderStyle = BorderStyle.FixedSingle;
                    Controls.Add(pb1);
                }
            }
            else MessageBox.Show("���������� ������� �� ����. ��������� ����������!");
        }
        public void ShowResultOfKeying(string outputPath)
        {
            PictureBox pb2 = new();
            pb2.ImageLocation = outputPath;
            pb2.Location = new Point(475, 3);
            pb2.Size = new Size(300, 250);
            pb2.SizeMode = PictureBoxSizeMode.Zoom;
            pb2.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pb2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                outputFilePath = folderBrowserDialog1.SelectedPath + $"\\Result.png";
                outputFilePath.Trim();
                if (outputFilePath != null)
                {
                    MessageBox.Show("����� ��� ���������� ����������� �������");
                }
            }
            else
            {
                MessageBox.Show("�� �� ������� ����� ��� ���������� �����������");
                folderBrowserDialog1.ShowDialog();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (inputFilePath == null)
            {
                MessageBox.Show("�������� ����������� ��� ���������");
            }
            else if (outputFilePath == null)
            {
                MessageBox.Show("�������� ����� ���������� ���������� ��������� �����������");
            }
            else
            {
                Remover remover = new Remover(inputFilePath, outputFilePath);
                await remover.Run();
                ShowResultOfKeying(outputFilePath);
                label4.Text = "��������� �������� \n" +
                    "� ��������� �����!";
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show(@$"������ {colorDialog1.Color} ����");

                HandleKeying handleKeying = new();
                Bitmap bitmap = new Bitmap(openFileDialog1.FileName);
                bitmap = handleKeying.MakeTransparent(bitmap, colorDialog1.Color, 100);
                bitmap.Save(outputFilePath);

                ShowResultOfKeying(outputFilePath);

            }
            else MessageBox.Show("���� �� ������");
        }
    }
}
