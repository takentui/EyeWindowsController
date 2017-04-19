using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EyeTrackerProject
{
    class BrightnessContrast
    {
        //якрость
        public static UInt32 Brightness(UInt32 point, int poz, int lenght)
        {
            int R;
            int G;
            int B;

            int N = (100 / lenght) * poz; //кол-во процентов

            R = (int)(((point & 0x00FF0000) >> 16) + N * 128 / 100);
            G = (int)(((point & 0x0000FF00) >> 8) + N * 128 / 100);
            B = (int)((point & 0x000000FF) + N * 128 / 100);

            //контролируем переполнение переменных
            if (R < 0) R = 0;
            if (R > 255) R = 255;
            if (G < 0) G = 0;
            if (G > 255) G = 255;
            if (B < 0) B = 0;
            if (B > 255) B = 255;

            point = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);

            return point;
        }

        //контрастность
        public static UInt32 Contrast(UInt32 point, int poz, int lenght)
        {
            int R;
            int G;
            int B;

            int N = (100 / lenght) * poz; //кол-во процентов

            if (N >= 0)
            {
                if (N == 100) N = 99;
                R = (int)((((point & 0x00FF0000) >> 16) * 100 - 128 * N) / (100 - N));
                G = (int)((((point & 0x0000FF00) >> 8) * 100 - 128 * N) / (100 - N));
                B = (int)(((point & 0x000000FF) * 100 - 128 * N) / (100 - N));
            }
            else
            {
                R = (int)((((point & 0x00FF0000) >> 16) * (100 - (-N)) + 128 * (-N)) / 100);
                G = (int)((((point & 0x0000FF00) >> 8) * (100 - (-N)) + 128 * (-N)) / 100);
                B = (int)(((point & 0x000000FF) * (100 - (-N)) + 128 * (-N)) / 100);
            }

            //контролируем переполнение переменных
            if (R < 0) R = 0;
            if (R > 255) R = 255;
            if (G < 0) G = 0;
            if (G > 255) G = 255;
            if (B < 0) B = 0;
            if (B > 255) B = 255;

            point = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);

            return point;
        }

        //бинаризация Бредли
        public static Bitmap Bradley_threshold(Bitmap Img)
        {
            Bitmap result = new Bitmap(Img);
            int width = Img.Width;
            int height = Img.Height;
            int S = (int)(width / 8);
            int s2 = S / 2;
            double t = 0.07;
            Int32[] integral_image;
            Int32 sum = 0;
            int count = 0;
            int index;
            int x1, y1, x2, y2;

            //рассчитываем интегральное изображение 
            integral_image = new Int32[width * height];

            for (int i = 0; i < width; i++)
            {
                sum = 0;
                for (int j = 0; j < height; j++)
                {
                    index = j * width + i;
                    sum += Img.GetPixel(i, j).ToArgb();
                    if (i == 0)
                        integral_image[index] = sum;
                    else
                        integral_image[index] = integral_image[index - 1] + sum;
                }
            }

            //находим границы для локальные областей
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    index = j * width + i;

                    x1 = i - s2;
                    x2 = i + s2;
                    y1 = j - s2;
                    y2 = j + s2;

                    if (x1 < 0)
                        x1 = 0;
                    if (x2 >= width)
                        x2 = width - 1;
                    if (y1 < 0)
                        y1 = 0;
                    if (y2 >= height)
                        y2 = height - 1;

                    count = (x2 - x1) * (y2 - y1);

                    sum = integral_image[y2 * width + x2] - integral_image[y1 * width + x2] -
                                integral_image[y2 * width + x1] + integral_image[y1 * width + x1];
                    if ((long)(Img.GetPixel(i, j).ToArgb() * count) < (long)(sum * (1.0 - t)))
                        result.SetPixel(i, j, Color.White);
                    else
                        result.SetPixel(i, j, Color.Black);
                }
            }

            return result;
        }
        public static Bitmap InvertImageColorMatrix(Image originalImg)
        {
            float[][] sepiaValues = {
                new float[]{.393f, .349f, .272f, 0, 0},
                new float[]{.769f, .686f, .534f, 0, 0},
                new float[]{.189f, .168f, .131f, 0, 0},
                new float[]{0, 0, 0, 1, 0},
                new float[]{0, 0, 0, 0, 1}
            };
            //float[][] sepiaValues = {
            //  new float[] { 0, 0, 0, 0, 0},
            //  new float[] { 0, 0, 0, 0, 0},
            //  new float[] { 0, 0, 0, 0, 0},
            //  new float[] { 0, 0, 0, 0.25f, 0},
            //  new float[] { 0, 0, 0, 0, 0},
            //};
            System.Drawing.Imaging.ColorMatrix sepiaMatrix = new System.Drawing.Imaging.ColorMatrix(sepiaValues);
            System.Drawing.Imaging.ImageAttributes IA = new System.Drawing.Imaging.ImageAttributes();
            IA.SetColorMatrix(sepiaMatrix);
            Bitmap sepiaEffect = (Bitmap)originalImg.Clone();
            using (Graphics G = Graphics.FromImage(sepiaEffect))
            {
                G.DrawImage(originalImg, new Rectangle(0, 0, sepiaEffect.Width, sepiaEffect.Height), 0, 0, sepiaEffect.Width, sepiaEffect.Height, GraphicsUnit.Pixel, IA);
            }
            return sepiaEffect;
        }

        public static Bitmap HistogramEqualization(Bitmap Img)
        {
            // create filter
            HistogramEqualization filter = new HistogramEqualization();
            // process image
            filter.ApplyInPlace(Img);
            return Img;
        }

        public static Image<Bgr, Byte> ShowCircles(Image<Gray, Byte> Img_Source_Gray)
        {
            Image<Bgr, byte> Img_Result_Bgr = new Image<Bgr, byte>(Img_Source_Gray.Width, Img_Source_Gray.Height);
            CvInvoke.cvCvtColor(Img_Source_Gray.Ptr, Img_Result_Bgr.Ptr, Emgu.CV.CvEnum.COLOR_CONVERSION.CV_GRAY2BGR);
            Gray cannyThreshold = new Gray(10);
            Gray circleAccumulatorThreshold = new Gray(16);
            double Resolution = 5.90;
            double MinDistance = 10.0;
            int MinRadius = 0;
            int MaxRadius = 0;

            CircleF[] HoughCircles = Img_Source_Gray.Clone().HoughCircles(
                                    cannyThreshold,
                                    circleAccumulatorThreshold,
                                    Resolution, //Resolution of the accumulator used to detect centers of the circles
                                    MinDistance, //min distance 
                                    MinRadius, //min radius
                                    MaxRadius //max radius
                                    )[0]; //Get the circles from the first channel
            #region draw circles
            foreach (CircleF circle in HoughCircles)
                Img_Result_Bgr.Draw(circle, new Bgr(Color.Red), 1);
            #endregion

            return Img_Result_Bgr;
        }
        public static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
            return bmp;
        }
    }
}
