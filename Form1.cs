using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Classwork14thcurs
{
    public partial class x : Form
        {
        Bitmap image;
        List<Bitmap> AllImages = new List<Bitmap>(); // НЕ НАДО, ВОЗМОЖНО

        int h = 0;
        public x()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           /* int a = 5;
            this.Text = "Досвидания";
            System.Threading.Thread.Sleep(2000);*/
            this.Close();
        }

        //private void button1_MouseMove(object sender, MouseEventArgs e)                 !!!!!!!!!!!делает двигающуюся кнопку!!!!!!!!!!!
        //{
        //    Random r = new Random();
        //    int width = this.Size.Width - this.button1.Size.Width;
        //    int height = this.Size.Height - this.button1.Size.Height; 

        //    int newX = r.Next(0, width); /*некст создается бесконечная последовательна я последовательность чисел*/
        //    int newY = r.Next(0, height);                 

        //    this.button1.Location = new Point(newX, newY);
        //}

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        public void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Image files | *.png; *.cel; *.jpg; *.bmp | All Files (*.*) | *.*";

            if (dialog.ShowDialog() == DialogResult.OK)

            {

                image = new Bitmap(dialog.FileName);

            }

            h++;

            pictureBox1.Image = image;

            pictureBox1.Refresh();

            AllImages.Add(image);
            /* OpenFileDialog dialog = new OpenFileDialog();

             DialogResult result = dialog.ShowDialog(); //открывает нам нашу форму
             if (result == DialogResult.OK)
             {
                 image = new Bitmap(dialog.FileName);

                 pictureBox1.Image = image;
                 pictureBox1.Refresh();
             }*/
        }

        /*private void x_Load(object sender, EventArgs e)
        {

        }*/

       

      /* private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }*/
    
        

        private void инверсияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InvertFilter filter = new InvertFilter();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image); //НЕ НАДО, ВОЗМОЖНО
            

            /* Bitmap resultImage = filter.processImage(image, backgroundWorker1);

            pictureBox1.Image = resultImage;

            pictureBox1.Refresh();*/
            //InvertFilter filter = new InvertFilter();

            //backgroundWorker1.RunWorkerAsync(filter);
            //AllImage.Add(image);
            //Bitmap resultImage = filter.processImage(image);
            // pictureBox1.Image = resultImage;
            // pictureBox1.Refresh();
            //Filters filter = new InvertFilter();



        }
        private void x_Load(object sender, EventArgs e)
        {

        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);

            if (backgroundWorker1.CancellationPending != true)

                image = newImage;/*Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;*/
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)

        {

            progressBar1.Value = e.ProgressPercentage;

        }
        /* private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }*/
        /*private void backgroundWorker1_RunWorkerComleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }*/
        
        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            
        }

        private void матричныеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void точечныеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       




        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

                BlurFilter filter = new BlurFilter();

                backgroundWorker1.RunWorkerAsync(filter);

                AllImages.Add(image);

                //h++;

            
        }

        private void гауссToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GaussianFilter filter = new GaussianFilter();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);

            //h++;
        }

        private void чернобелыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrayScaleFilter filter = new GrayScaleFilter();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);

           // h++;
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SepiaFilter filter = new SepiaFilter();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);

            //h++;
        }

        private void яркостьИзображToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrightnessFuilter filter = new BrightnessFuilter();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);

           // h++;
        }

        private void собельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SobelFilter filter = new SobelFilter();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RezkFilter filter = new RezkFilter();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TisnenieFilter filter = new TisnenieFilter();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);
        }

        private void светкрайToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SvetKrai filter = new SvetKrai();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);
        }

        private void медианныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedianFilter filter = new MedianFilter();

            backgroundWorker1.RunWorkerAsync(filter);

            //AllImages.Add(image);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
                if (AllImages.Count - 1 > 0)
                {
                AllImages.RemoveAt(AllImages.Count - 1);
                image = AllImages[AllImages.Count - 1];
                pictureBox1.Image = AllImages[AllImages.Count - 1]; ;
                pictureBox1.Refresh();
                }
            } catch { }
           

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "Images|*.png;*.bmp;*.jpg";

            if (dialog.ShowDialog() == DialogResult.OK)

            {

                pictureBox1.Image.Save(dialog.FileName);

            }
        }

        private void поворотToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Povorot filter = new Povorot();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);
            h++;
        }

        private void зеркалоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MirrorRL filter = new MirrorRL();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);
            h++;
        }

        private void зерклалоВерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MirrorTB filter = new MirrorTB();

            backgroundWorker1.RunWorkerAsync(filter);

            AllImages.Add(image);
            h++;
        }

       

        /*  private void волныToolStripMenuItem_Click(object sender, EventArgs e)
          {
              Waves filter = new Waves();

              backgroundWorker1.RunWorkerAsync(filter);

              AllImages.Add(image);
          }*/



        /* private void блюрToolStripMenuItem_Click(object sender, EventArgs e)
         {
             BlurFilter filter = new BlurFilter();

             backgroundWorker1.RunWorkerAsync(filter);

             AllImages.Add(image);
             h++;
         }*/

        /*  private void волныToolStripMenuItem_Click(object sender, EventArgs e)
          {
              Waves filter = new Waves();

              backgroundWorker1.RunWorkerAsync(filter);

              AllImages.Add(image);
          }*/
        // TisnenieFilter

    }
}
