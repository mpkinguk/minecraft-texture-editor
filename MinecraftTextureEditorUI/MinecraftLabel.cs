using System.Drawing;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    public partial class MinecraftLabel : Label
    {
        public MinecraftLabel()
        {
            InitializeComponent();

            Font = new Font("Minecraft", 10F);
        }

        /// <summary>
        /// Override the Paint function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private new void Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(BackColor);

            g.DrawString(Text, Font, new SolidBrush(ForeColor), 0, 0);

            g.Flush();
        }
    }
}