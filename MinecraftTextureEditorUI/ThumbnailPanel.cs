using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI.ThumbnailPanel
{
    public class ThumbnailPanel : Panel
    {
        // The size of each thumbnail
        private const int ThumbnailWidth = 100;
        private const int ThumbnailHeight = 100;

        // The margin between thumbnails
        private const int ThumbnailMargin = 10;

        // The list of image files to display
        private string[] imageFiles;

        // The current column and row index of the selected thumbnail
        private int selectedColumn = -1;
        private int selectedRow = -1;

        // The constructor that takes the list of image files as a parameter
        public ThumbnailPanel(string[] imageFiles)
        {
            Font = new Font("Minecraft", 10F);

            // Set the image files
            this.imageFiles = imageFiles;

            // Set the auto scroll property to true
            this.AutoScroll = true;

            // Set the minimum size of the panel to the total image area
            int columns = (this.Width - ThumbnailMargin) / (ThumbnailWidth + ThumbnailMargin);
            int rows = (int)Math.Ceiling((double)imageFiles.Length / columns);
            this.AutoScrollMinSize = new Size(columns * (ThumbnailWidth + ThumbnailMargin), rows * (ThumbnailHeight + ThumbnailMargin));

            // Handle the mouse click event
            this.MouseClick += ThumbnailPanel_MouseClick;
        }

        // The method that handles the mouse click event
        private void ThumbnailPanel_MouseClick(object sender, MouseEventArgs e)
        {
            // Get the column and row index of the clicked thumbnail
            int column = (e.X + this.AutoScrollPosition.X - ThumbnailMargin) / (ThumbnailWidth + ThumbnailMargin);
            int row = (e.Y + this.AutoScrollPosition.Y - ThumbnailMargin) / (ThumbnailHeight + ThumbnailMargin);

            // Check if the column and row index are valid
            if (column >= 0 && row >= 0 && column * row < imageFiles.Length)
            {
                // Set the selected column and row index
                selectedColumn = column;
                selectedRow = row;

                // Invalidate the panel to redraw the thumbnails
                this.Invalidate();

                // Get the file name of the selected image
                string fileName = imageFiles[column * row];

                // Raise the file selected event with the file name as the argument
                OnFileSelected(fileName);
            }
        }

        // The event that is raised when a file is selected
        public event EventHandler<string> FileSelected;

        // The method that raises the file selected event
        protected virtual void OnFileSelected(string fileName)
        {
            // Invoke the event handler if it is not null
            FileSelected?.Invoke(this, fileName);
        }

        // The method that overrides the on paint method to draw the thumbnails
        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the base method
            base.OnPaint(e);

            // Get the graphics object
            Graphics g = e.Graphics;

            // Translate the graphics object according to the scroll position
            g.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);

            // Loop through the image files
            for (int i = 0; i < imageFiles.Length; i++)
            {
                // Get the column and row index of the current image
                int column = i % ((this.Width - ThumbnailMargin) / (ThumbnailWidth + ThumbnailMargin));
                int row = i / ((this.Width - ThumbnailMargin) / (ThumbnailWidth + ThumbnailMargin));

                // Get the x and y position of the current image
                int x = column * (ThumbnailWidth + ThumbnailMargin) + ThumbnailMargin;
                int y = row * (ThumbnailHeight + ThumbnailMargin) + ThumbnailMargin;

                // Draw the current image as a thumbnail
                using (Image image = Image.FromFile(imageFiles[i]))
                {
                    g.DrawImage(image, x, y, ThumbnailWidth, ThumbnailHeight);
                }

                // Draw a border around the selected image
                if (column == selectedColumn && row == selectedRow)
                {
                    g.DrawRectangle(Pens.Red, x, y, ThumbnailWidth, ThumbnailHeight);
                }
            }
        }
    }
}