using MinecraftTextureEditorAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    public partial class TexturePickerForm : Form
    {
        /// <summary>
        /// The texture clicked event delegate
        /// </summary>
        /// <param name="filename"></param>
        public delegate void TextureClickedEventHandler(string filename);

        public bool FromWizard { get; set; }

        /// <summary>
        /// The current texture filename
        /// </summary>
        public string CurrentTexture { get; set; }

        /// <summary>
        /// The current path
        /// </summary>
        public string CurrentPath { get { return _currentPath; } set { _currentPath = value; } }

        /// <summary>
        /// The list of files we can edit
        /// </summary>
        public List<string> Files { get; set; }

        /// <summary>
        /// The texture clicked event
        /// </summary>
        public event TextureClickedEventHandler TextureClicked;

        private string _currentPath;

        private int _itemSize;

        /// <summary>
        /// The constructor
        /// </summary>
        public TexturePickerForm()
        {
            InitializeComponent();

            LoadTextures();
        }

        /// <summary>
        /// Override the tool tip drawing function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolTipDraw(object sender, DrawToolTipEventArgs e)
        {
            var g = e.Graphics;

            using (StringFormat sf = new StringFormat())
            {
                e.DrawBackground();

                // Top.
                sf.LineAlignment = StringAlignment.Center;

                // Top/Left.
                sf.Alignment = StringAlignment.Center;

                g.DrawString(e.ToolTipText, new Font("Minecraft", 6F), Brushes.Black, e.Bounds, sf);

                e.DrawBorder();
            }

            g.Flush();
        }

        /// <summary>
        /// Filter the images when enter is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxFilterKeyDown(object sender, KeyEventArgs e)
        {
            UpdateCursor(true);

            if (e.KeyCode.Equals(Keys.Enter))
            {
                var textBox = (TextBox)sender;

                flowLayoutPanelTextures.Visible = false;

                foreach (Button item in flowLayoutPanelTextures.Controls)
                {
                    item.Visible = item.Text.Contains(textBox.Text) || string.IsNullOrEmpty(textBox.Text);
                }

                flowLayoutPanelTextures.Visible = true;
            }

            UpdateCursor(false);
        }

        /// <summary>
        /// The constructor
        /// </summary>
        public TexturePickerForm(string currentPath)
        {
            InitializeComponent();

            _currentPath = currentPath;

            LoadTextures();
        }

        #region Form events

        /// <summary>
        /// Fire the texture clicked event
        /// </summary>
        /// <param name="filename"></param>
        private void OnTextureClicked(string filename)
        {
            TextureClicked?.Invoke(filename);
        }

        /// <summary>
        /// Refresh button clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRefreshClick(object sender, EventArgs e)
        {
            LoadTextures(true);
        }

        #endregion Form events

        #region Private methods

        /// <summary>
        /// Load the textures
        /// </summary>
        /// <param name="refresh">Refresh only?</param>
        private async void LoadTextures(bool refresh = false)
        {
            var settingsPath = ConfigurationHelper.LoadSetting("MinecraftDefaultFolder");
            var assetsPath = ConfigurationHelper.LoadSetting("AssetsFolder");
            var texturesPath = ConfigurationHelper.LoadSetting("TexturesFolder");

            // Only grab it from settings once
            CurrentPath = string.IsNullOrEmpty(CurrentPath) ? settingsPath : CurrentPath;

            if (!refresh && !FromWizard)
            {
                CurrentPath = FileHelper.OpenFolderName(CurrentPath);
            }

            // if the user cancels, exit the application, as no textures will be loaded and it will not be usable
            if (string.IsNullOrEmpty(CurrentPath))
            {
                Application.Exit();
                return;
            }

            UpdateText("Loading textures...");

            _itemSize = flowLayoutPanelTextures.ClientRectangle.Width / 5;

            Files = await Task.Run(() => FileHelper.GetFiles(Path.Combine(CurrentPath, assetsPath, texturesPath), "*.png", true)).ConfigureAwait(false);

            await UpdateFlowLayoutPanel(Files).ConfigureAwait(false);

            UpdateText("Texture Picker");
        }

        /// <summary>
        /// Update the listview with files
        /// </summary>
        /// <param name="files">The files</param>
        private async Task UpdateFlowLayoutPanel(List<string> files)
        {
            if (files is null || files.Count.Equals(0))
            {
                throw new ArgumentNullException(nameof(files));
            }

            UpdateCursor(true);

            await Task.Run(() =>
            {               
                int row = 0;
                int col = 0;

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);

                    Bitmap tmp;

                    var itemText = fileInfo.Name.Length > 8 ? $"{fileInfo.Name.Substring(0, 8)}..." : fileInfo.Name;

                    var item = new Button() { Tag = file, Text = itemText, Location = new Point(0, 0), Size = new Size(_itemSize, _itemSize), Font = new Font("Minecraft", 6F) };

                    toolTip1.SetToolTip(item, fileInfo.Name);

                    item.Click += LoadTexture;
                    item.Paint += ItemPaint;

                    try
                    {
                        using (var image = (Image)FileHelper.LoadFile(file))
                        {
                            tmp = new Bitmap(image);
                        }
                    }
                    catch (Exception exc)
                    {
                        Debug.WriteLine(exc.Message);
                        continue;
                    }

                    item.Image = tmp;

                    AddFlowLayoutTexture(item);

                    col++;

                    if (col.Equals(5))
                    {
                        col = 0;

                        row++;
                    }
                }

                FinishLayout();

            });

            UpdateCursor(false);

        }

        /// <summary>
        /// Paint the item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemPaint(object sender, PaintEventArgs e)
        {
            var button = (Button)sender;

            var g = e.Graphics;

            g.Clear(button.BackColor);

            var rectangle = new Rectangle(button.ClientRectangle.Width / 6, button.ClientRectangle.Height / 6, button.ClientRectangle.Width / 6 * 4, button.ClientRectangle.Height / 6 * 4);

            g.DrawImage(button.Image, rectangle);

            g.DrawString(button.Text, new Font(button.Font.Name, 6F), new SolidBrush(button.ForeColor), new Point(2, button.ClientRectangle.Height - 12));

            g.DrawRectangle(button.Focused ? Pens.Red : Pens.Black, new Rectangle(0,0,button.ClientRectangle.Width-1, button.ClientRectangle.Height -1));

            g.Flush();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadTexture(object sender, EventArgs e)
        {
            var button = (Button)sender;
            OnTextureClicked(Convert.ToString(button.Tag));
        }

        #endregion Private methods

        #region Threadsafe methods

        /// <summary>
        /// Threadsafe method for completing the layout
        /// </summary>
        private void FinishLayout()
        {
            if (flowLayoutPanelTextures.InvokeRequired)
            {
                var d = new Action(FinishLayout);

                Invoke(d);
            }
            else
            {

                flowLayoutPanelTextures.PerformLayout();

                flowLayoutPanelTextures.Visible = true;

                if (flowLayoutPanelTextures.HasChildren)
                {
                    flowLayoutPanelTextures.ScrollControlIntoView(flowLayoutPanelTextures.Controls[0]);
                }

            }
        }

        /// <summary>
        /// Thread safe method for preparing the flow layout panel
        /// </summary>
        private void PrepareFlowLayoutPanel()
        {
            if (flowLayoutPanelTextures.InvokeRequired)
            {
                Action d = new Action(PrepareFlowLayoutPanel);

                Invoke(d);
            }
            else
            {
                flowLayoutPanelTextures.Visible = false;

                flowLayoutPanelTextures.VerticalScroll.Enabled = true;

                flowLayoutPanelTextures.Controls.Clear();
            }
        }

        /// <summary>
        /// Threadsafe method for changing the cursor
        /// </summary>
        /// <param name="waiting"></param>
        private void UpdateCursor(bool waiting)
        {
            if (InvokeRequired)
            {
                var d = new Action<bool>(UpdateCursor);
                Invoke(d, new object[] { waiting });
            }
            else
            {
                Cursor = waiting ? Cursors.WaitCursor : Cursors.Default;
            }
        }

        /// <summary>
        /// Thread safe method for updating form text
        /// </summary>
        /// <param name="text"></param>
        private void UpdateText(string text)
        {
            if (labelTitle.InvokeRequired)
            {
                var d = new Action<string>(UpdateText);
                Invoke(d, new object[] { text });
            }
            else
            {
                labelTitle.Text = text;
            }
        }

        /// <summary>
        /// Threadsafe method for adding new controls to the flow panel
        /// </summary>
        /// <param name="item"></param>
        private void AddFlowLayoutTexture(Control item)
        {
            if (flowLayoutPanelTextures.InvokeRequired)
            {
                Action<Control> d = new Action<Control>(AddFlowLayoutTexture);

                Invoke(d, new object[] { item });
            }
            else
            {
                flowLayoutPanelTextures.Controls.Add(item);

            }
        }

        #endregion Threadsafe methods
    }
}