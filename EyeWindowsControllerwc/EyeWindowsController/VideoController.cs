using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Math.Geometry;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Xml.Serialization;

//Remove ambiguousness between AForge.Image and System.Drawing.Image
using Point = System.Drawing.Point; //Remove ambiguousness between AForge.Point and System.Drawing.Point

namespace EyeWindowsController
{
    public partial class VideoController : Form
    {
        private FilterInfoCollection VideoCapTureDevices;
        private VideoCaptureDevice Finalvideo;
        private ControllerSettings settings;
        MainFormControl owner_form;
        int ScreenWidth = SystemInformation.PrimaryMonitorSize.Width;
        int ScreenHeight = SystemInformation.PrimaryMonitorSize.Height;


        public VideoController()
        {
            InitializeComponent();
        }

        int R; //Trackbarın değişkeneleri
        int G;
        int B;

        int H;
        int W;
        private void VideoController_Load(object sender, EventArgs e)
        {
            VideoCapTureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in VideoCapTureDevices)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }
            comboBox1.SelectedIndex = 0;
            try
            {
                owner_form = (MainFormControl)this.Owner as MainFormControl;
                deserializeSettings();
                StartTracking();
            }
            catch (Exception exce)
            {
            }
            H = ScreenHeight / pictureBox1.Height + 3;
            W = ScreenWidth / pictureBox1.Width;
        }

        private void StartTracking()
        {
            if ( Finalvideo != null )
                Finalvideo.Stop();
            Finalvideo = new VideoCaptureDevice(VideoCapTureDevices[comboBox1.SelectedIndex].MonikerString);
            Finalvideo.NewFrame -= Finalvideo_NewFrame;
            Finalvideo.NewFrame += new NewFrameEventHandler(Finalvideo_NewFrame);
            Finalvideo.DesiredFrameRate = 20;// FPS
            Finalvideo.DesiredFrameSize = new Size(320, 240);
            Finalvideo.Start();
        }

        private void StartSettingButton_Click(object sender, EventArgs e)
        {
            StartTracking();
        }

        void Finalvideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

            Bitmap image = (Bitmap)eventArgs.Frame.Clone();
            Bitmap image1 = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = image;

            if (rdiobtnRed.Checked)
            {

                //создадим фильтр
                EuclideanColorFiltering filter = new EuclideanColorFiltering();
                //Задаем фильтр
                filter.CenterColor = new RGB(Color.FromArgb(215, 0, 0));
                filter.Radius = 100;
                //Применим фильтр
                filter.ApplyInPlace(image1);
                Tracking(image1);

            }

            if (rdiobtnBlue.Checked)
            {

                //создадим фильтр
                EuclideanColorFiltering filter = new EuclideanColorFiltering();
                //Задаем фильтр
                filter.CenterColor = new RGB(Color.FromArgb(30, 144, 255));
                filter.Radius = 100;
                //Применим фильтр
                filter.ApplyInPlace(image1);

                Tracking(image1);
            }
            if(rdiobtnGreen.Checked){

                //создадим фильтр
                EuclideanColorFiltering filter = new EuclideanColorFiltering();
                // set center color and radius
                filter.CenterColor = new RGB(Color.FromArgb(0, 215, 0));
                filter.Radius = 100;
                //Применим фильтр
                filter.ApplyInPlace(image1);

                Tracking(image1);
            }


            if (rdiobtnHandSet.Checked)
            {

                 //создадим фильтр
                EuclideanColorFiltering filter = new EuclideanColorFiltering();
                //Задаем фильтр
                filter.CenterColor = new RGB(Color.FromArgb(R, G, B));
                filter.Radius = 100;
                //Применим фильтр
                filter.ApplyInPlace(image1);

                Tracking(image1);
            }

        }
        public void Tracking(Bitmap image)
        {
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinWidth = 5;
            blobCounter.MinHeight = 5;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;

            BitmapData objectsData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            // grayscaling
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            UnmanagedImage grayImage = grayscaleFilter.Apply(new UnmanagedImage(objectsData));
            // unlock image
            image.UnlockBits(objectsData);
            Bitmap newClone = new Bitmap(image);

            blobCounter.ProcessImage(image);
            Rectangle[] rects = blobCounter.GetObjectsRectangles();
            Blob[] blobs = blobCounter.GetObjectsInformation();
            pictureBox2.Image = image;
            owner_form.videoTranslator.Image = newClone;

            if (!chkBoxSettingsMode.Checked)
            {
                //Трекинг за одним объектом
                foreach (Rectangle recs in rects)
                {
                    if (rects.Length > 0)
                    {
                        Rectangle objectRect = rects[0];
                        //Graphics g = Graphics.FromImage(image);
                        Graphics g = pictureBox1.CreateGraphics();
                        using (Pen pen = new Pen(Color.FromArgb(252, 3, 26), 2))
                        {
                            g.DrawRectangle(pen, objectRect);
                        }
                        //Координаты прямоугольника
                        int objectX = objectRect.X + (objectRect.Width / 2);
                        int objectY = objectRect.Y + (objectRect.Height / 2);
                        //  g.DrawString(objectX.ToString() + "X" + objectY.ToString(), new Font("Arial", 12), Brushes.Red, new System.Drawing.Point(250, 1));
                        g.Dispose();
                        this.Invoke((MethodInvoker)delegate
                        {
                            richTextBox1.Text = objectRect.Location.ToString() + "\n" + richTextBox1.Text + "\n";
                            try
                            {
                                owner_form.setMouseCoordinates(ScreenWidth - objectRect.Location.X * W, objectRect.Location.Y * H);
                            }
                            catch (Exception except)
                            { }
                        });
                    }
                }
            }
        }

        // Conver list of AForge.NET's points to array of .NET points
        private Point[] ToPointsArray(List<IntPoint> points)
        {
            Point[] array = new Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new Point(points[i].X, points[i].Y);
            }

            return array;
        }

        private void PauseSettingButton_Click(object sender, EventArgs e)
        {
            if (Finalvideo.IsRunning)
            {
                Finalvideo.Stop();
            }
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            R = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            G = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            B = trackBar3.Value;
        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            settings = new ControllerSettings();
            settings.Red = R;
            settings.Green = G;
            settings.Blue = B;

            if (rdiobtnRed.Checked)
                settings.CheckSettings = "Red";
            if (rdiobtnGreen.Checked)
                settings.CheckSettings = "Green";
            if (rdiobtnBlue.Checked)
                settings.CheckSettings = "Blue";
            if (rdiobtnHandSet.Checked)
                settings.CheckSettings = "HandSet";

            using (Stream fStream = new FileStream("XMLSettings.xml",
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ControllerSettings));
                xmlFormat.Serialize(fStream, settings );
            }
        }

        public void deserializeSettings()
        {
            FileStream fs = new FileStream("XMLSettings.xml", FileMode.Open);
            try
            {

                XmlSerializer xml = new XmlSerializer(typeof(ControllerSettings));
                settings = (ControllerSettings)xml.Deserialize(fs);

                R = settings.Red;
                G = settings.Green;
                B = settings.Blue;

                rdiobtnRed.Checked = false;
                rdiobtnGreen.Checked = false;
                rdiobtnBlue.Checked = false;
                rdiobtnHandSet.Checked = false;

                if (settings.CheckSettings == "Red")
                    rdiobtnRed.Checked = true;
                if (settings.CheckSettings == "Green")
                    rdiobtnGreen.Checked = true;
                if (settings.CheckSettings == "Blue")
                    rdiobtnBlue.Checked = true;
                if (settings.CheckSettings == "HandSet")
                {
                    rdiobtnHandSet.Checked = true;
                    trackBar1.Value = settings.Red;
                    trackBar2.Value = settings.Green;
                    trackBar3.Value = settings.Blue;
                }

            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }

            finally
            {
                fs.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartTracking();
        }

    }
}
