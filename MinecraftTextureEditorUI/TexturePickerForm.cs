using log4net;
using MinecraftTextureEditorAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    public partial class TexturePickerForm : Form
    {
        #region Public delegates

        /// <summary>
        /// The texture clicked event delegate
        /// </summary>
        /// <param name="filename"></param>
        public delegate void TextureClickedEventHandler(string filename);

        #endregion Public delegates

        #region Events

        /// <summary>
        /// The texture clicked event
        /// </summary>
        public event TextureClickedEventHandler TextureClicked;

        #endregion Events

        #region Public properties

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
        public IList<string> Files { get; set; }

        #endregion Public properties

        #region Private properties

        private string _currentPath;

        private int _itemSize;

        private readonly List<Image> _images;

        private int _imageCounter = 0;

        private bool _loading;

        private readonly ILog _log;

        #endregion Private properties

        /// <summary>
        /// The constructor
        /// </summary>
        public TexturePickerForm(string currentPath, ILog log)
        {
            _log = log;

            try
            {
                InitializeComponent();

                _images = new List<Image>
                {
                    Properties.Resources.destroy_stage_0,
                    Properties.Resources.destroy_stage_1,
                    Properties.Resources.destroy_stage_2,
                    Properties.Resources.destroy_stage_3,
                    Properties.Resources.destroy_stage_4,
                    Properties.Resources.destroy_stage_5,
                    Properties.Resources.destroy_stage_6,
                    Properties.Resources.destroy_stage_7,
                    Properties.Resources.destroy_stage_8,
                    Properties.Resources.destroy_stage_9
                };

                Paint += TexturePickerFormPaint;

                _currentPath = currentPath;

                // Reduce display flicker
                SetStyle(ControlStyles.AllPaintingInWmPaint & ControlStyles.UserPaint & ControlStyles.OptimizedDoubleBuffer & ControlStyles.ResizeRedraw, true);
            }
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Paints block breaking image when loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TexturePickerFormPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.DrawImage(Properties.Resources.texturewallpaper2, e.ClipRectangle.X, e.ClipRectangle.Y);

            if (_loading)
            {
                g.DrawImage(_images[_imageCounter], new Rectangle(40, 80, ClientRectangle.Width - 80, ClientRectangle.Width - 80));
            } 

            g.Flush();
        }

        #region Public methods

        /// <summary>
        /// Load the textures
        /// </summary>
        /// <param name="refresh">Refresh only?</param>
        public async void LoadTextures(bool refresh = false)
        {
            try
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
                    _log.Debug("No path selected to load textures. Exiting application");
                    Application.Exit();
                    return;
                }

                UpdateText("Loading textures...");

                _loading = true;

                _itemSize = (flowLayoutPanelTextures.ClientRectangle.Width / 6);

                Files = await Task.Run(() => FileHelper.GetFiles(Path.Combine(CurrentPath, assetsPath, texturesPath), "*.png", true)).ConfigureAwait(false);

                if (Files.Count > 0)
                {
                    await UpdateFlowLayoutPanel(Files).ConfigureAwait(false);
                }

                _loading = false;

                UpdateText("Texture Picker");
            }
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Refresh a given image
        /// </summary>
        /// <param name="filename">The filename</param>
        public void RefreshImage(string filename)
        {
            foreach (var item in flowLayoutPanelTextures.Controls)
            {
                var button = (Button)item;

                var imageFilename = (string)button.Tag;
                if (imageFilename.Equals(filename, StringComparison.InvariantCultureIgnoreCase))
                {
                    button.Image = (Image)FileHelper.LoadFile(imageFilename);
                    return;
                }
            }
        }

        #endregion Public methods

        #region Private form events

        /// <summary>
        /// Override the tool tip drawing function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolTipDraw(object sender, DrawToolTipEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Filter the images when enter is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TextBoxFilterKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                UpdateCursor(true);

                if (e.KeyCode.Equals(Keys.Enter))
                {
                    var textBox = (TextBox)sender;

                    var text = (string)textBox.Text.Clone();

                    await Task.Run(() => FilterImages(text)).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
            finally
            {
                UpdateCursor(false);
            }
        }

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

        /// <summary>
        /// Update the listview with files
        /// </summary>
        /// <param name="files">The files</param>
        private async Task UpdateFlowLayoutPanel(IList<string> files)
        {
            try
            {
                if (files is null || files.Count.Equals(0))
                {
                    throw new ArgumentNullException(nameof(files));
                }

                UpdateCursor(true);

                PrepareFlowLayoutPanel();

                int row = 0;
                int col = 0;

                int inc = 0;

                var items = new List<Control>();

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);

                    Bitmap tmp;

                    var itemText = fileInfo.Name.Length > 6 ? $"{fileInfo.Name.Substring(0, 7)}..." : fileInfo.Name;

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
                    catch (Exception ex)
                    {
                        _log.Debug(ex.Message);
                        continue;
                    }

                    item.Image = tmp;

                    items.Add(item);

                    col++;

                    inc++;

                    // Update block breaking graphic
                    if (inc.Equals(100))
                    {
                        inc = 0;
                        _imageCounter++;

                        if (_imageCounter.Equals(10))
                        {
                            _imageCounter = 0;
                        }

                        ThreadsafeInvalidate();
                    }

                    if (col.Equals(5))
                    {
                        col = 0;

                        row++;
                    }
                }

                await Task.Run(()=>AddFlowLayoutTexture(items)).ConfigureAwait(false);

                FinishLayout();
            }
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
            finally
            {
                UpdateCursor(false);
            }
        }

        /// <summary>
        /// Paint the item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemPaint(object sender, PaintEventArgs e)
        {
            try
            {
                var button = (Button)sender;

                var g = e.Graphics;

                g.Clear(Color.LightGray);

                var rectangle = new Rectangle(button.ClientRectangle.Width / 6, button.ClientRectangle.Height / 6, button.ClientRectangle.Width / 6 * 4, button.ClientRectangle.Height / 6 * 4);

                g.DrawImage(button.Image, rectangle);

                g.DrawString(button.Text, new Font(button.Font.Name, 6F), new SolidBrush(button.ForeColor), new Point(2, button.ClientRectangle.Height - 12));

                g.DrawRectangle(button.Focused ? Pens.Red : Pens.Black, new Rectangle(0, 0, button.ClientRectangle.Width - 1, button.ClientRectangle.Height - 1));

                g.Flush();

            }
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Load a texture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadTexture(object sender, EventArgs e)
        {
            var button = (Button)sender;
            OnTextureClicked(Convert.ToString(button.Tag));
        }

        #endregion Private form events

        #region Threadsafe methods

        /// <summary>
        /// Threadsafe method for completing the layout
        /// </summary>
        private void FinishLayout()
        {
            try
            {
                if (flowLayoutPanelTextures.InvokeRequired)
                {
                    var d = new Action(FinishLayout);

                    Invoke(d);
                }
                else
                {
                    flowLayoutPanelTextures.PerformLayout();

                    if (flowLayoutPanelTextures.HasChildren)
                    {
                        flowLayoutPanelTextures.ScrollControlIntoView(flowLayoutPanelTextures.Controls[0]);
                    }

                    flowLayoutPanelTextures.Visible = true;
                }
            }
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Thread safe method for preparing the flow layout panel
        /// </summary>
        private void PrepareFlowLayoutPanel()
        {
            try
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
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Threadsafe method for changing the cursor
        /// </summary>
        /// <param name="waiting"></param>
        private void UpdateCursor(bool waiting)
        {
            try
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
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Threadsafe method for invalidating/redrawing the form
        /// </summary>
        private void ThreadsafeInvalidate()
        {
            try
            {
                if (InvokeRequired)
                {
                    var d = new Action(ThreadsafeInvalidate);
                    Invoke(d);
                }
                else
                {
                    Invalidate(true);
                }
            }
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Thread safe method for updating form text
        /// </summary>
        /// <param name="text"></param>
        private void UpdateText(string text)
        {
            try
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
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Threadsafe method for adding new controls to the flow panel
        /// </summary>
        /// <param name="item"></param>
        private void AddFlowLayoutTexture(List<Control> items)
        {
            try
            {
                if (flowLayoutPanelTextures.InvokeRequired)
                {
                    Action<List<Control>> d = new Action<List<Control>>(AddFlowLayoutTexture);

                    Invoke(d, new object[] { items });
                }
                else
                {
                    flowLayoutPanelTextures.Controls.AddRange(items.ToArray());
                }
            }
            catch (Exception ex)
            {
                _log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Threadsafe method for filtering the images
        /// </summary>
        /// <param name="text"></param>
        private void FilterImages(string text)
        {
            if (flowLayoutPanelTextures.InvokeRequired)
            {
                var d = new Action<string>(FilterImages);
                Invoke(d, new object[] { text });
            }
            else
            {
                flowLayoutPanelTextures.Visible = false;

                foreach (Button item in flowLayoutPanelTextures.Controls)
                {
                    item.Visible = item.Text.Contains(text) || string.IsNullOrEmpty(text);
                }

                flowLayoutPanelTextures.Visible = true;
            }
        }

        #endregion Threadsafe methods
    }
}