using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
namespace Classwork14thcurs
{


    abstract class Filters
    {


        protected abstract Color calculateNewPixelColor(Bitmap soucreImage, int x, int y);
        /*public Bitmap processImage(Bitmap sourceImage)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;

        }*/
        public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {

            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)

            {

                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                if (worker.CancellationPending)

                    return null;

                for (int j = 0; j < sourceImage.Height; j++)

                {

                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));

                }

            }

            return resultImage;

        }
        /*{

        Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
        for (int i = 0; i < sourceImage.Width; i++)
        {
            worker.ReportProgress((int)((float)i / resultImage.Width * 100));
            if (worker.CancellationPending)
                return null;
            for (int j = 0; j < sourceImage.Height; j++)
            {
                resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
            }
        }
        return resultImage;

    }*/
        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    }
    class InvertFilter : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 - sourceColor.R,
                                                255 - sourceColor.G,
                                                255 - sourceColor.B);
            return resultColor;
        }

    }
    
    class MatrixFilter : Filters

    {

        protected float[,] kernel = null;

        protected MatrixFilter() { }

        public MatrixFilter(float[,] karnel)//karnel

        {

            this.kernel = karnel; // karnel

        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)

        {

            int radiusX = kernel.GetLength(0) / 2;

            int radiusY = kernel.GetLength(1) / 2;

            float resultR = 0;

            float resultG = 0;

            float resultB = 0;

            for (int l = -radiusY; l <= radiusY; l++)

                for (int k = -radiusX; k <= radiusX; k++)

                {

                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);

                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);

                    Color neightborColor = sourceImage.GetPixel(idX, idY);

                    resultR += neightborColor.R * kernel[k + radiusX, l + radiusY];

                    resultG += neightborColor.G * kernel[k + radiusX, l + radiusY];

                    resultB += neightborColor.B * kernel[k + radiusX, l + radiusY];

                }

            return Color.FromArgb(

            Clamp((int)resultR, 0, 255),

            Clamp((int)resultG, 0, 255),

            Clamp((int)resultB, 0, 255)

            );

        }
    }
    
    class BlurFilter : MatrixFilter

    {

        public BlurFilter()

        {

            int sizeX = 3;

            int sizeY = 3;

            kernel = new float[sizeX, sizeY];

            for (int i = 0; i < sizeX; i++)

                for (int j = 0; j < sizeY; j++)

                    kernel[i, j] = 1.0f / (float)(sizeX * sizeY);

        }

    }
    
    class GaussianFilter : MatrixFilter

    {

        public void createGaussiankernel(int radius, float sigma)

        {

            int size = 2 * radius + 1;

            kernel = new float[size, size];

            float norm = 0;

            for (int i = -radius; i <= radius; i++)

                for (int j = -radius; j <= radius; j++)

                {

                    kernel[i + radius, j + radius] = (float)(Math.Exp(-(i * i + j * j) / (sigma * sigma)));

                    norm += kernel[i + radius, j + radius];

                }

            for (int i = 0; i < size; i++)

                for (int j = 0; j < size; j++)

                    kernel[i, j] /= norm;

        }

        public GaussianFilter()

        {

            createGaussiankernel(3, 2);

        }

    }
    
    class GrayScaleFilter : Filters

    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)

        {

            Color sourceColor = sourceImage.GetPixel(x, y);

            int intensity = (int)(sourceColor.R * 0.36) + (int)(sourceColor.G * 0.53) + (int)(sourceColor.B * 0.11);

            Color resultColor = Color.FromArgb(intensity, intensity, intensity);

            return resultColor;

        }

    }
    
    class SepiaFilter : Filters

    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)

        {

            Color sourceColor = sourceImage.GetPixel(x, y);

            int k = 4;

            int intensity = (int)(sourceColor.R * 0.36) + (int)(sourceColor.G * 0.53) + (int)(sourceColor.B * 0.11);

            int resultR = (int)(intensity + 2 * k);

            int resultG = (int)(intensity + 0.5 * k);

            int resultB = (int)(intensity - 1 * k);

            return Color.FromArgb(

            Clamp((int)resultR, 0, 255),

            Clamp((int)resultG, 0, 255),

            Clamp((int)resultB, 0, 255)

            );

        }

    }

    class BrightnessFuilter : Filters

    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)

        {

            Color sourceColor = sourceImage.GetPixel(x, y);

            const int k = 100;

            int resultR = (int)(sourceColor.R + k);

            int resultG = (int)(sourceColor.G + k);

            int resultB = (int)(sourceColor.B + k);

            return Color.FromArgb(

            Clamp((int)resultR, 0, 255),

            Clamp((int)resultG, 0, 255),

            Clamp((int)resultB, 0, 255)

            );

        }

    }
    class SobelFilter : MatrixFilter

    {

        public SobelFilter()

        {

            float[,] SobelX = { { -1, -2, -1 }, { -1, 1, 0 }, { 1, 2, 1 } };

            float[,] SobelY = { { -1, 0, -1 }, { -2, 0, 2 }, { -1, 0, 1 } };

            int size = 3;

            float[,] Gx = new float[size, size];

            float[,] Gy = new float[size, size];

            float[,] resG = new float[size, size];

            for (int i = 0; i < size; i++)

                for (int j = 0; j < size; j++)

                {

                    resG[i, j] = SobelX[i, j] + SobelY[i, j];

                }

            kernel = resG;

        }

    }
    class RezkFilter : MatrixFilter

    {

        public RezkFilter()

        {

            kernel = new float[,] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };

        }

    }
    class TisnenieFilter : MatrixFilter

    {

        public TisnenieFilter()

        {

            kernel = new float[,] { { 0, 1, 0 }, { 1, 0, -1 }, { 0, -1, 0 } };

        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)

        {

            Color sourceColor = sourceImage.GetPixel(x, y);

            const int k = -40;

            int resultR = (int)(sourceColor.R + k);

            int resultG = (int)(sourceColor.G + k);

            int resultB = (int)(sourceColor.B + k);

            return Color.FromArgb(

            Clamp((int)resultR, 0, 255),

            Clamp((int)resultG, 0, 255),

            Clamp((int)resultB, 0, 255)

            );

        }

    }
    class SvetKrai : MatrixFilter

    {

        public SvetKrai()

        {

            int size = 3;
            float[,] resG = new float[size, size];

            int[,] SobelY = { { 3, 10, 3 }, { 0, 0, 0 }, { -3, -10, -3 } };

            int[,] SobelX = { { 3, 0, -3 }, { 10, 0, -10 }, { 3, 0, -3 } };

            for (int i = 0; i < size; i++)

                for (int j = 0; j < size; j++)

                {

                    resG[i, j] = SobelX[i, j] + SobelY[i, j];

                }

            kernel = resG;

        }

    }
    class Povorot : MatrixFilter
    {
        protected double z = Math.PI / 8;
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int i, int j)
        {
            Color swColor = new Color();
            int x0 = sourceImage.Width / 2;
            int y0 = sourceImage.Height / 2;

            int x = (int)((i - x0) * Math.Cos(z) - (j - y0) * Math.Sin(z) + x0);
            int y = (int)((i - x0) * Math.Sin(z) - (j - y0) * Math.Cos(z) + y0);
            
            if ((x > (sourceImage.Width - 1)) || (x < 0 ) || (y > (sourceImage.Height - 1)) || (y < 0))
            {
                swColor = Color.FromArgb(0, 0, 0);
            }
            else
            {
                swColor = sourceImage.GetPixel(x, y);
            }
            return swColor;
        }
    }

    class MirrorRL : MatrixFilter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {


            Color sourceColor = sourceImage.GetPixel(Clamp(sourceImage.Width - x - 1, 0, sourceImage.Width - 1), Clamp(y, 0, sourceImage.Height - 1));
            Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
            return resultColor;
        }
    }
    class MirrorTB : MatrixFilter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {


            Color sourceColor = sourceImage.GetPixel(Clamp(x, 0, sourceImage.Height - 1), Clamp(sourceImage.Height - y - 1, 0, sourceImage.Height - 1));
            Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
            return resultColor;
        }
    }


    /*class MirrorTB : MatrixFilter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {


            Color sourceColor = sourceImage.GetPixel(Clamp(sourceImage.Height - y - 1, 0, sourceImage.Height - 1), Clamp(x, 0, sourceImage.Height - 1));
            Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
            return resultColor;
        }
    }*/


    /*class Waves : MatrixFilter
    {
        public void createGaussianKernel(int radius, float sigma)
        {
            int size = 2 * radius + 1;
            kernel = new float[size, size];

            float norm = 0;

            for (int i = -radius; i <= radius; i++)
                for (int j = radius; j <= radius; j++)
                {
                    kernel[i + radius, j + radius] = (float)(Math.Exp(-(i * i + j * j) / (sigma * sigma)));
                    norm += kernel[i + radius, j + radius];
                }
            //нормируем ядро
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    kernel[i, j] /= norm;

        }
        public Waves() { createGaussianKernel(3, 2); }
    }*/
    class MedianFilter : MatrixFilter

    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)

        {

            int radius = 3;

            int size = (2 * radius + 1) * (2 * radius + 1);

            Color neightborColor;

            int[] oldR = new int[size + 1];

            int[] oldB = new int[size + 1];

            int[] oldG = new int[size + 1];

            for (int i = 0; i < size + 1; i++)

            {

                oldR[i] = 0;

                oldG[i] = 0;

                oldB[i] = 0;

            }

            int k = 1;

            for (int i = x - radius; (i < x + radius + 1); i++)

                for (int j = y - radius; (j < y + radius + 1); j++)

                {

                    neightborColor = sourceImage.GetPixel(

                    Clamp(i, 0, sourceImage.Width - radius),

                    Clamp(j, 0, sourceImage.Height - radius));

                    oldR[k] = (int)(neightborColor.R);

                    oldG[k] = (int)(neightborColor.G);

                    oldB[k] = (int)(neightborColor.B);

                    k++;

                }

            sort(oldR, 0, size - 1);

            sort(oldG, 0, size - 1);

            sort(oldB, 0, size - 1);

            return Color.FromArgb(

            Clamp((int)oldR[(int)(oldR.Length / 2 + 1)], 0, 255),

            Clamp((int)oldG[(int)(oldG.Length / 2 + 1)], 0, 255),

            Clamp((int)oldB[(int)(oldB.Length / 2 + 1)], 0, 255)

            );

        }

        public void sort(int[] array, int i, int j)

        {

            if (i < j)

            {

                int k = growth(array, i, j);

                sort(array, i, k - 1);

                sort(array, k + 1, j);

            }

        }

        public int growth(int[] array, int i, int j)

        {

            int x = array[j];

            int z = i - 1;

            int tmp;

            for (int k = i; k < j; k++)

            {

                if (array[k] <= z)

                {

                    z++;

                    tmp = array[z];

                    array[z] = array[k];

                    array[k] = tmp;

                }

            }

            tmp = array[j];

            array[j] = array[z + 1];

            array[z + 1] = tmp;

            return (i + 1);

        }

    }
   
}