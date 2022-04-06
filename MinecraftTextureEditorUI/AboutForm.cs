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
        private readonly List<Point> _directions = new List<Point>();
        private readonly List<Image> _images = new List<Image>();
        private readonly ILog _log;
        private readonly List<Point> _points = new List<Point>();
        private readonly Timer _timer = new Timer();

        private float _angle = 1F;

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

                Random rnd = new Random();

                for (int i = 0; i < 12; i++)
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

                _timer.Start();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #region Private form events

        private void AboutFormFormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Stop();
        }

        private void AboutFormPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.DrawImage(Properties.Resources.wallpaper6, ClientRectangle);

            var index = 0;

            for (int i = 0; i < _points.Count; i++)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                var newBitmap = RotateBitmap((Bitmap)_images[index], _angle);

                g.DrawImage(newBitmap, _points[i].X, _points[i].Y, 15, 15);

                index++;

                if (index.Equals(_images.Count))
                {
                    index = 0;
                }
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

        /// <summary>
        /// Timer tick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            _angle += 5F;

            if (_angle > 360F)
            {
                _angle = 0;
            }

            for (int i = 0; i < _points.Count; i++)
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

        #endregion Private form events

        #region Private methods

        /// <summary>
        /// Rotate the bitmap
        /// </summary>
        /// <param name="bitmap">The bitmap</param>
        /// <param name="angle">The angle</param>
        /// <returns>Bitmap</returns>
        private Bitmap RotateBitmap(Bitmap bitmap, float angle)
        {
            var w = bitmap.Width;
            var h = bitmap.Height;

            //Create a new empty bitmap to hold rotated image.
            var returnBitmap = new Bitmap(w, h);

            //Make a graphics object from the empty bitmap.
            var g = Graphics.FromImage(returnBitmap);

            //move rotation point to center of image.
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var x = w / 2F;
            var y = h / 2F;

            g.TranslateTransform(x, y);
            //Rotate.
            g.RotateTransform(angle);
            //Move image back.
            g.TranslateTransform(-x, -y);
            //Draw passed in image onto graphics object.
            g.DrawImage(bitmap, new Point(0, 0));

            return returnBitmap;
        }

        #endregion Private methods

        #region Assembly Attribute Accessors

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

        #endregion Assembly Attribute Accessors
    }
}