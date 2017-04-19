using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Math.Geometry;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Diagnostics;

namespace EyeTrackerProject
{
    public partial class Form1 : Form
    {
        //Declararation of all variables, vectors and haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        HaarCascade eye;
        public static UInt32[,] pixel;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;


        public Form1()
        {
            InitializeComponent();
            //Load haarcascades for face detection
            //face = new HaarCascade("haarcascade_frontalface_default.xml");
            eye = new HaarCascade("haarcascade_mcs_eyepair_big.xml");
            //Initialize the capture device
            grabber = new Capture();
            grabber.QueryFrame();
            //Initialize the FrameGraber event
            Application.Idle += new EventHandler(FrameGrabber);
        }
       

        void FrameGrabber(object sender, EventArgs e)
        {
            //label4.Text = "";
            NamePersons.Add("");
            Invert filterInvert = new Invert();

            IFilter filterGrayscale = Grayscale.CommonAlgorithms.RMY;
            Threshold th = new Threshold(225);
            BlobCounter bl;
            
            ExtractBiggestBlob fil2;
            
            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().Resize(800, 600, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convert it to Grayscale
            gray = currentFrame.Convert<Gray, Byte>();

            //Face Detector
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
          eye,
          1.2,
          10,
          Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
          new Size(20, 20));
            Bitmap[] test = new Bitmap[5];
            //Action for each element detected
            int width = 0, height = 0;
            int kek = 0;
            UInt32 point = new UInt32();
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //draw the face detected in the 0th (gray) channel with blue color
                width = f.rect.Width / 2 - 5;
                height = f.rect.Height - 5;
                //width = f.rect.Width;
                //height = f.rect.Height;
                test[0] = new Bitmap(width, height);
                test[1] = new Bitmap(width, height);
                for (var i = 0; i < width; i++)
                {
                    for (var j = 0; j < height ; j++)
                    {
                        test[0].SetPixel(i, j, currentFrame.Bitmap.GetPixel(f.rect.X + i, f.rect.Y + j));
                    }
                }
                for (var i = 0; i < width; i++)
                {
                    for (var j = 3; j < height; j++)
                    {

                        test[1].SetPixel(i, j, currentFrame.Bitmap.GetPixel(f.rect.X + width / 2 + i, f.rect.Y + j));
                    }
                }
                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);
                kek++;
            }
            t = 0;

            //Show the faces procesed and recognized
            imageBoxFrameGrabber.Image = currentFrame;
            //создадим фильтр
            EuclideanColorFiltering filter = new EuclideanColorFiltering();
            //Задаем фильтр
            //Emgu.CV.CvInvoke.cvGoodFeaturesToTrack(test[0], 
            //Применим фильтр
            try
            {
                //filter.ApplyInPlace(test[0]);
                //filter.ApplyInPlace(test[1]);
                first_eye.Image = BrightnessContrast.Bradley_threshold(test[0]);
            }
            catch (Exception ex)
            {
            }
            if (test[0] != null)
            {
                //test[0] = BrightnessContrast.InvertImageColorMatrix(test[0]);
                //test[0] = BrightnessContrast.HistogramEqualization(test[0]);
                eye1.Image = test[0];
                System.Drawing.Bitmap kek123 = test[0].Clone(new Rectangle(0, 0, width, height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                kek123 = filterInvert.Apply((Bitmap)kek123);
                AForge.Imaging.Image.FormatImage(ref kek123);
                eye2.Image = kek123;
                kek123 = filterGrayscale.Apply(kek123); // ДОБАВИМ ПОИСК КРУГА
                first_eye.Image = kek123;

                kek123 = BrightnessContrast.HistogramEqualization(kek123);
                kek123 = th.Apply(kek123);
                
                // РИСУЕМ КРУГИ
                Image<Bgr, Byte> imgOriginal = new Image<Bgr, byte>(kek123);
                Image<Gray, Byte> imgProcessed = (new Image<Gray, Byte>(kek123));

                //imgProcessed = imgOriginal.InRange(new Bgr(0, 0, 175), new Bgr(100, 100, 256));

                imgProcessed = imgProcessed.SmoothGaussian(9);

                CircleF[] circles = imgProcessed.HoughCircles(new Gray(40), new Gray(20), 2, imgProcessed.Height / 4, 2, 40)[0];

                foreach (CircleF circle in circles)
                {

                    if (textBox1.Text != "") textBox1.AppendText(Environment.NewLine);
                    textBox1.AppendText("x =" + circle.Center.X.ToString().PadLeft(4) + ", y =" + circle.Center.Y.ToString().PadLeft(4) + ", radius =" + circle.Radius.ToString("###.000").PadLeft(7));
                    textBox1.ScrollToCaret();

                    CvInvoke.cvCircle(imgOriginal, new System.Drawing.Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);

                    imgOriginal.Draw(circle, new Bgr(Color.Red), 3);
                }
                sec_eye.Image = imgOriginal.ToBitmap();
                // 
                //ПОПРОБОВАТЬ поиграть с радиусом, если радиус малый то смотрим вниз
                //ПОРА переносить на проект


                //test[0] = BrightnessContrast.HistogramEqualization(kek123);
                //kek123 = th.Apply(kek123);
                //sec_eye.Image = kek123;
                ////TODO POISK KRYGA I ZRACHKA
                //bl = new BlobCounter(kek123);
                //int i = bl.ObjectsCount;
                //fil2 = new ExtractBiggestBlob();
                //try
                //{
                //    fil2.Apply(kek123);
                //    int x = 0;
                //    int y = 0;
                //    int h = 0;
                //    if (i > 0)
                //    {
                //        fil2.Apply(kek123);
                //        x = fil2.BlobPosition.X;
                //        y = fil2.BlobPosition.Y;
                //        h = fil2.Apply(kek123).Height;
                //        test[0] = kek123.Clone(new Rectangle(0, 0, width, height), System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                //        test[0].SetPixel(x + height / 2, y + height / 2, Color.Red);
                //        test[0] = BrightnessContrast.Bradley_threshold(test[0]);
                //        sec_eye.Image = test[0];
                //        label1.Text = " x = " + (x + height / 2).ToString() + ", y = " + (y + height / 2).ToString() + ", h = " + h.ToString();
                //    }
                //}
                //catch (Exception exc) { }
                //test[0] = BrightnessContrast.Bradley_threshold(test[0]);
                //first_eye.Image = test[0];
                //Image<Bgr, Byte> keka = BrightnessContrast.ShowCircles(new Image<Gray, Byte>(test[0]));

                //sec_eye.Image = (Bitmap)keka.Bitmap.Clone();
            }
            //Clear the list(vector) of names
            NamePersons.Clear();

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            FrameGrabber(sender, e);
        }

    }
}