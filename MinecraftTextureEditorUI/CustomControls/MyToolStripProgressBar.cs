using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MinecraftTextureEditorUI.CustomControls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class MyToolStripProgressBar : ToolStripProgressBar
    {
        public MyToolStripProgressBar() : base()
        {
            Control.HandleCreated += Control_HandleCreated;
        }
        private void Control_HandleCreated(object sender, EventArgs e)
        {
            _ = new ProgressBarHelper((ProgressBar)Control);
        }
    }
    public class ProgressBarHelper : NativeWindow
    {
        private readonly ProgressBar _progressBar;

        public ProgressBarHelper(ProgressBar progressBar)
        {
            _progressBar = progressBar;
            AssignHandle(_progressBar.Handle);
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xF /*WM_PAINT*/)
            {
                using (var g = _progressBar.CreateGraphics())
                    TextRenderer.DrawText(g, _progressBar.Text,
                       _progressBar.Font, _progressBar.ClientRectangle, _progressBar.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter );
            }
        }
    }
}