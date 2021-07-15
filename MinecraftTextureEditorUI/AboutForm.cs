using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    partial class AboutForm : Form
    {
        private readonly ILog _log;

        private readonly List<Point> _points = new List<Point>();

        private readonly List<Point> _directions = new List<Point>();

        private readonly List<Image> _images = new List<Image>();

        private float _angle = 1F;

        private readonly Timer _timer = new Timer();

        /// <summary>
        /// Constructor
        /// </summary>
        public AboutForm(ILog log)
        {
            _log = log;

            try
            {
                _timer.Interval = 33;
                _timer.Tick += TimerTick;

                _images = new List<Image> {
                    Properties.Resources.wooden_pickaxe,
                    Properties.Resources.stone_pickaxe,
                    Properties.Resources.iron_pickaxe,
                    Properties.Resources.golden_pickaxe,
                    Properties.Resources.diamond_pickaxe,
                    Properties.Resources.netherite_pickaxe
                };

                Random rnd = new Random();

                for (int i = 0; i < 6; i++)
                {
                    var x = rnd.Next(ClientRectangle.Width - 32);
                    var y = rnd.Next(ClientRectangle.Height - 32);

                    _points.Add(new Point(x, y));

                    var dX = 0;
                    var dY = 0;

                    while (dX.Equals(0) || dY.Equals(0))
                    {
                        dX = rnd.Next(-5, 5);
                        dY = rnd.Next(-5, 5);
                    }

                    _directions.Add(new Point(dX, dY));
                }

                InitializeComponent();

                // Reduce display flicker
                SetStyle(
                    ControlStyles.AllPaintingInWmPaint & 
                    ControlStyles.UserPaint & 
                    ControlStyles.OptimizedDoubleBuffer & 
                    ControlStyles.SupportsTransparentBackColor & 
                    ControlStyles.ResizeRedraw, true);

                Paint += AboutFormPaint;

                FormClosing += AboutFormFormClosing;

                labelProductName.Text = AssemblyTitle;
                labelVersion.Text = $"Version {AssemblyVersion}";
                labelCopyright.Text = AssemblyCopyright;
                labelCompanyName.Text = $"Produced by {AssemblyCompany}";
                labelDescription.Text = AssemblyDescription;

                labelDescription.Visible = false;
                labelProductName.Visible = false;
                labelVersion.Visible = false;
                labelCopyright.Visible = false;
                labelCompanyName.Visible = false;
                labelAbout.Visible = false;

                _timer.Start();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        private void AboutFormFormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Stop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _angle += 5F;

            if (_angle > 360F)
            {
                _angle = 0;
            }

            for (int i = 0; i < _images.Count; i++)
            {
                var x = _points[i].X;
                var y = _points[i].Y;
                var dX = _directions[i].X;
                var dY = _directions[i].Y;

                if (x + dX > ClientRectangle.Width - 16 || x + dX < Math.Abs(dX))
                {
                    dX = -dX;
                }

                if (y + dY > ClientRectangle.Height - 16 || y + dY < Math.Abs(dY))
                {
                    dY = -dY;
                }

                x += dX;
                y += dY;

                _directions[i] = new Point(dX, dY);
                _points[i] = new Point(x, y);
            }

            Invalidate(true);
        }

        private void AboutFormPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.DrawImage(Properties.Resources.steve, ClientRectangle);

            for (int i = 0; i < _images.Count; i++)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                var newBitmap = RotateImage((Bitmap)_images[i], _angle);
                
                g.DrawImage(newBitmap, _points[i].X, _points[i].Y, 15, 15);

            }

            foreach (Control control in Controls)
            {
                if (control.GetType().Equals(typeof(PictureBox)))
                {
                    var label = (PictureBox)control;

                    var rectangle = new Rectangle(label.Left, label.Top, label.Width, label.Height);
                    var shadowRectangle = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width, rectangle.Height);

                    g.DrawString(label.Text, label.Font, Brushes.Black, shadowRectangle);

                    g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), rectangle);
                }
            }

            g.Flush();
        }

        // Your method, not mine.
        private Bitmap RotateImage(Bitmap b, float angle)
        {
            //Create a new empty bitmap to hold rotated image.
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //Make a graphics object from the empty bitmap.
            Graphics g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of image.
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            //Rotate.        
            g.RotateTransform(angle);
            //Move image back.
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            //Draw passed in image onto graphics object.
            g.DrawImage(b, new Point(0, 0));
            return returnBitmap;
        }

        #region Assembly Attribute Accessors

        /// <summary>
        /// The assembly title
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// The assembly version
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// The assembly description
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// The assembly product
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// The assembly copyright
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// The assembly company
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion Assembly Attribute Accessors
    }
}