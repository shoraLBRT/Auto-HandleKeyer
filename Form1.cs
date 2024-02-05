using BackgroundRemovalSample.App;
using BackgroundHandleRemover;

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
        private void Form1_Load(object sender, EventArgs e)
        {

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
                    MessageBox.Show("Изображение загружено успешно");

                    pb1.ImageLocation = inputFilePath;
                    pb1.Location = new Point(29, 3);
                    pb1.Size = new Size(300, 250);
                    pb1.SizeMode = PictureBoxSizeMode.Zoom;
                    pb1.BorderStyle = BorderStyle.FixedSingle;
                    Controls.Add(pb1);
                }
            }
            else MessageBox.Show("Фотография выбрана не была. Загрузите фотографию!");
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
                    MessageBox.Show("Папка для сохранения изображения выбрана");
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали папку для сохранения изображения");
                folderBrowserDialog1.ShowDialog();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (inputFilePath == null)
            {
                MessageBox.Show("Выберите изображение для обработки");
            }
            else if (outputFilePath == null)
            {
                MessageBox.Show("Выберите место сохранения результата обработки изображения");
            }
            else
            {
                Remover remover = new Remover(inputFilePath, outputFilePath);
                await remover.Run();
                ShowResultOfKeying(outputFilePath);
                label4.Text = "Результат сохранен \n" +
                    "в выбранной папке!";
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show(@$"Выбран {colorDialog1.Color} цвет");

                HandleKeying handleKeying = new();
                Bitmap bitmap = new Bitmap(openFileDialog1.FileName);
                bitmap = handleKeying.MakeTransparent(bitmap, colorDialog1.Color, 100);
                bitmap.Save(outputFilePath);

                ShowResultOfKeying(outputFilePath);

            }
            else MessageBox.Show("Цвет не выбран");
        }
    }
}
