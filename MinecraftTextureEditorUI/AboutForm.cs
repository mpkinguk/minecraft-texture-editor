using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    partial class AboutForm : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();

            // Reduce display flicker
            SetStyle(ControlStyles.AllPaintingInWmPaint & ControlStyles.UserPaint & ControlStyles.OptimizedDoubleBuffer & ControlStyles.ResizeRedraw, true);

            labelProductName.Text = AssemblyTitle;
            labelVersion.Text = $"Version {AssemblyVersion}";
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = $"Produced by {AssemblyCompany}";
            labelDescription.Text = AssemblyDescription;

            labelDescription.Paint += LabelPaint;
            labelProductName.Paint += LabelPaint;
            labelVersion.Paint += LabelPaint;
            labelCopyright.Paint += LabelPaint;
            labelCompanyName.Paint += LabelPaint;
            labelAbout.Paint += LabelPaint;
        }

        /// <summary>
        /// Override the paint commands of each 'Label' so we can add shadows!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelPaint(object sender, PaintEventArgs e)
        {
            var label = (PictureBox)sender;

            var g = e.Graphics;

            var shadowRectangle = new Rectangle(1, 1, e.ClipRectangle.Width, e.ClipRectangle.Height);

            g.DrawString(label.Text, label.Font, Brushes.Black, shadowRectangle);

            g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), e.ClipRectangle);

            g.Flush();
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