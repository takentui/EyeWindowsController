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
    public partial class EyeVisionController : Form
    {
        //Declararation of all variables, vectors and haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade eye;
        public static UInt32[,] pixel;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<PictureBox> pictures = new List<System.Windows.Forms.PictureBox>();
        MathModule coords;
        MainFormControl owner_form;
        PointF[][] corners;
        public EyeVisionController()
        {
            InitializeComponent();
            //Создадим картинки для передачи изображения
            for (int i = 0; i < 8; i++)
            {
                int y = (int)(i / 4);
                pictures.Add(new System.Windows.Forms.PictureBox());
                pictures[i].Location = new System.Drawing.Point(10 + (i % 4) * 110, 30 + y * 100);
                pictures[i].Name = "pictureBox" + i.ToString();
                pictures[i].Size = new System.Drawing.Size(100, 100);
                pictures[i].TabIndex = 10;
                pictures[i].TabStop = false;
                this.Controls.Add(pictures[i]);
            }
            coords = new MathModule(SystemInformation.PrimaryMonitorSize.Width * 41 / (800 * 30), SystemInformation.PrimaryMonitorSize.Height);
            //Каскад хаара
            eye = new HaarCascade("haarcascade_mcs_eyepair_big.xml");
            //Камера
            grabber = new Capture();
            grabber.QueryFrame();
            //Подписка на событие
            Application.Idle += new EventHandler(FrameGrabber);
        }
       

        void FrameGrabber(object sender, EventArgs e)
        {
            Invert filterInvert = new Invert();

            IFilter filterGrayscale = Grayscale.CommonAlgorithms.RMY;
            Threshold th = new Threshold(240);
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
            int t = 0;
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //draw the face detected in the 0th (gray) channel with blue color
                width = f.rect.Width / 2;
                height = f.rect.Height;
                //width = f.rect.Width;
                //height = f.rect.Height;
                test[0] = new Bitmap(width, height);
                test[1] = new Bitmap(width, height);
                for (var i = 0; i < width; i++)
                {
                    for (var j = 0; j < height; j++)
                    {
                        test[0].SetPixel(i, j, currentFrame.Bitmap.GetPixel(f.rect.X + i, f.rect.Y + j));
                    }
                }
                for (var i = 0; i < width; i++)
                {
                    for (var j = 0; j < height; j++)
                    {

                        test[1].SetPixel(i, j, currentFrame.Bitmap.GetPixel(f.rect.X + width + i, f.rect.Y + j));
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
                //first_eye.Image = BrightnessContrast.Bradley_threshold(test[0]);
                //sec_eye.Image = BrightnessContrast.Bradley_threshold(test[1]);
            }
            catch (Exception ex)
            {
            }
            int x_l, y_l, x_r = 0, y_r = 0;
            for (var index = 0; index < test.Length; index++)
            {
                if (test[index] != null)
                {
                    pictures[index * 4].Image = test[index];
                    //test[0] = BrightnessContrast.InvertImageColorMatrix(test[0]);
                    //test[0] = BrightnessContrast.HistogramEqualization(test[0]);
                    //eye1.Image = test[index];
                    System.Drawing.Bitmap kek123 = test[index].Clone(new Rectangle(0, 0, width, height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    kek123 = filterInvert.Apply((Bitmap)kek123);
                    AForge.Imaging.Image.FormatImage(ref kek123);
                    pictures[index * 4 + 1].Image = kek123; // Принт изображения
                    kek123 = filterGrayscale.Apply(kek123); // Наложим грейскейл
                    pictures[index * 4 + 2].Image = kek123; // Принт изображения
                    kek123 = BrightnessContrast.Bradley_threshold(kek123);
                    //kek123 = th.Apply(kek123);

                    // РИСУЕМ КРУГИ
                    Image<Bgr, Byte> imgOriginal = new Image<Bgr, byte>(kek123);
                    Image<Gray, Byte> imgProcessed = (new Image<Gray, Byte>(kek123));

                    
                    imgProcessed = imgProcessed.SmoothGaussian(9);
                    //imgProcessed = imgProcessed.InRange(new Gray(0), new Gray(70));

                    CircleF[] circles = imgProcessed.HoughCircles(new Gray(70), new Gray(20), 1.5, imgProcessed.Height, 3, 12)[0];
                    foreach (CircleF circle in circles)
                    {
                        CvInvoke.cvCircle(imgOriginal, new System.Drawing.Point((int)circle.Center.X, (int)circle.Center.Y), 3, new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                        if (index == 0)
                        {
                            x_r = (int)circle.Center.X;
                            y_r = (int)circle.Center.Y;
                        }
                        else
                        {
                            x_l = (int)circle.Center.X;
                            y_l = (int)circle.Center.Y;
                            coords.addPoint(x_l, y_l, x_r, y_r);
                        }


                        imgOriginal.Draw(circle, new Bgr(Color.Red), 2);

                        if (textBox1.Text != "") textBox1.AppendText(Environment.NewLine);
                        textBox1.AppendText("x =" + coords.getX() + ", y =" + coords.getY());
                        textBox1.ScrollToCaret();
                        break;
                    }
                    try
                    {
                        int cornerCount = 2;
                        corners = setCornerRoi(imgOriginal, index.Equals(0)).GoodFeaturesToTrack(3, 0.8, imgOriginal.Width, 3);
                        
                        if (index.Equals(0))
                        {
                            coords.setLeftCorner(corners[0][0].X, corners[0][0].Y);
                            owner_form.left_eye.Image = imgOriginal.ToBitmap();
                        }
                        else
                        {
                            coords.setRightCorner(corners[0][0].X, corners[0][0].Y);
                            owner_form.right_eye.Image = imgOriginal.ToBitmap();
                        }
                        owner_form.setMouseCoordinates(coords.getX(), coords.getY());
                    }
                    catch (Exception except)
                    {
                        owner_form.logger_tb.Text = except.Message;
                    }
                    pictures[index * 4 + 3].Image = imgProcessed.ToBitmap(); //imgOriginal.ToBitmap();// Принт изображения
                }
            }
        }

        private Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> setCornerRoi(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>  imgOriginal, bool left)
        {
            Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img = imgOriginal.Clone();
            int x, y, width, height;
            if (left)
            {
                x = (int)(img.Width * 0.2);
                y = (int) (img.Height * 0.3);
            }
            else
            {
                x = (int)(img.Width * 0.4);
                y = (int)(img.Height * 0.3);
            }
            width = (int)(img.Width * 0.4);
            height = (int)(img.Height * 0.5);
            img.ROI = new Rectangle(x, y, width, height);
            imgOriginal.Draw(new Rectangle(x, y, width, height), new Bgr(Color.Aquamarine), 2);
            return img;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FrameGrabber(sender, e);
        }

        private void EyeVisionController_Load(object sender, EventArgs e)
        {
            owner_form = (MainFormControl)this.Owner as MainFormControl;

        }

    }
}