using log4net;
using MinecraftTextureEditorAPI.Helpers;
using MinecraftTextureEditorAPI.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MinecraftTextureEditorAPI.Constants;

using Constants = MinecraftTextureEditorAPI.Constants;

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

        /// <summary>
        /// The list of files we can edit
        /// </summary>
        public IList<string> Files { get; set; }

        #endregion Public properties

        #region Private properties

        private readonly List<Image> _images;
        private readonly ILog _log;
        private int _imageCounter = 0;
        private bool _loading;

        #endregion Private properties

        /// <summary>
        /// The constructor
        /// </summary>
        public TexturePickerForm(ILog log)
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

                // Reduce display flicker
                SetStyle(ControlStyles.AllPaintingInWmPaint & ControlStyles.UserPaint & ControlStyles.OptimizedDoubleBuffer & ControlStyles.ResizeRedraw, true);
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
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
        public async Task LoadTextures()
        {
            try
            {
                UpdateText("Loading textures...");

                _loading = true;

                Files = await Task.Run(() => FileHelper.GetFiles(State.Path, "*.png", true)).ConfigureAwait(false);

                if (Files.Count > 0)
                {
                    await UpdateFlowLayoutPanel(Files).ConfigureAwait(false);
                }

                _loading = false;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);

                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ThreadsafeInvalidate();

                UpdateText("Texture Picker");
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
                if (item.GetType().Equals(typeof(Button)))
                {
                    var button = (Button)item;

                    var imageInfo = (ImageInfo)button.Tag;
                    if (imageInfo.FullPath.Equals(filename, StringComparison.InvariantCultureIgnoreCase))
                    {
                        button.Image = (Image)FileHelper.LoadFile(filename);
                        return;
                    }
                }
            }
        }

        #endregion Public methods

        #region Private form events

        /// <summary>
        /// Refresh button clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonRefreshClick(object sender, EventArgs e)
        {
            await LoadTextures().ConfigureAwait(false);
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
                if (e.ClipRectangle.IntersectsWith(flowLayoutPanelTextures.ClientRectangle))
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
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
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

            var imageInfo = (ImageInfo)button.Tag;

            OnTextureClicked(imageInfo.FullPath);
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
                _log?.Error(ex.Message);
            }
            finally
            {
                UpdateCursor(false);
            }
        }

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

                    sf.LineAlignment = StringAlignment.Center;

                    sf.Alignment = e.ToolTipText.Length > 30 ? StringAlignment.Near : StringAlignment.Center;

                    // Create margin
                    var rectangle = new Rectangle(3, 3, e.Bounds.Width - 6, e.Bounds.Height - 6);

                    g.DrawString(e.ToolTipText, new Font("Minecraft", 6F), Brushes.Black, rectangle, sf);

                    e.DrawBorder();
                }

                g.Flush();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
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

                var categoryCounter = string.Empty;

                var items = new List<Control>();

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);

                    Bitmap tmp;

                    var itemText = fileInfo.Name.Length > 6 ? $"{fileInfo.Name.Substring(0, 7)}..." : fileInfo.Name;

                    var item = new Button() { Text = itemText, Location = new Point(0, 0), Size = new Size(Constants.ItemSize, Constants.ItemSize), Font = new Font("Minecraft", 6F) };

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
                        _log.Error(ex.Message);
                        continue;
                    }

                    var category = new DirectoryInfo(fileInfo.DirectoryName).Name;

                    if (!categoryCounter.Equals(category))
                    {
                        items.Add(new Label()
                        {
                            Name = category,
                            Width = (int)(ItemSize * 6.7F),
                            Height = 25,
                            Text = category,
                            Tag = new ImageInfo() { Category = category },
                            ForeColor = Color.Yellow,
                            BackColor = Color.FromArgb(50, 0, 0, 0)
                        });
                        categoryCounter = category;
                    }

                    var imageInfo = new ImageInfo() { FullPath = file, Filename = fileInfo.Name, Size = tmp.Size, Category = category };

                    item.Tag = imageInfo;

                    item.Image = tmp;

                    items.Add(item);

                    // Keeping these in helps the block breaking graphic update. I guess it breaks up the loop?
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

                    // Keeping these in helps the block breaking graphic update. I guess it breaks up the loop?
                    if (col.Equals(5))
                    {
                        col = 0;

                        row++;
                    }
                }

                await Task.Run(() => AddFlowLayoutTexture(items)).ConfigureAwait(false);

                FinishLayout();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
            finally
            {
                UpdateCursor(false);
            }
        }

        #endregion Private form events

        #region Threadsafe methods

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
                _log?.Error(ex.Message);
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
                UpdateCursor(true);

                flowLayoutPanelTextures.Visible = false;

                var filters = text.Split(FilterTypeDelimiter);

                foreach (Control imageButton in flowLayoutPanelTextures.Controls)
                {
                    var imageInfo = (ImageInfo)imageButton.Tag;
                    var size = imageInfo.Size;

                    var width = size.Width.ToString();
                    var height = size.Height.ToString();

                    var filename = imageInfo.Filename;
                    var category = imageInfo.Category;

                    var isCategory = false;
                    var isHeight = false;
                    var isName = false;
                    var isWidth = false;

                    var categoryList = new List<string>();

                    foreach (var filter in filters)
                    {
                        // Invalid filter name, move on
                        if (!filter.Equals(string.Empty))
                        {
                            var filterSplit = filter.Split(FilterValueDelimiter);

                            if (filterSplit.Length > 1)
                            {
                                var tuple = Tuple.Create(filterSplit[0], filterSplit[1]);

                                if (Enum.TryParse<FilterType>(tuple.Item1, true, out var filtertype))
                                {
                                    switch (filtertype)
                                    {
                                        case FilterType.Width:
                                            isWidth = width.Equals(tuple.Item2);
                                            break;

                                        case FilterType.Height:
                                            isHeight = height.Equals(tuple.Item2);
                                            break;

                                        case FilterType.Category:
                                            isCategory = category.Contains(tuple.Item2);
                                            break;

                                        case FilterType.Name:
                                            isName = filename.Contains(tuple.Item2);
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                isName = filename.Contains(text);
                            }
                        }

                        var visible = isName || isCategory || isWidth || isHeight || string.IsNullOrEmpty(text);

                        imageButton.Visible = visible;

                        // Add assigned category to list that should be shown
                        if (visible)
                        {
                            categoryList.Add(imageInfo.Category);
                        }
                    }

                    // Show labels if apropriate
                    var labels = flowLayoutPanelTextures.Controls.Cast<Control>().ToList();

                    // Shrink it down a bit
                    var categories = categoryList.Distinct().ToList();

                    foreach (var control in labels.Where(x => x.GetType().Equals(typeof(Label))))
                    {
                        var label = (Label)control;

                        var labelInfo = (ImageInfo)label.Tag;

                        if (categories.Contains(labelInfo.Category))
                        {
                            label.Visible = true;
                        }
                    }
                }

                flowLayoutPanelTextures.Visible = true;

                UpdateCursor(false);
            }
        }

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
                _log?.Error(ex.Message);
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
                _log?.Error(ex.Message);
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
                _log?.Error(ex.Message);
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
                    UseWaitCursor = waiting;

                    Cursor = waiting ? Cursors.WaitCursor : Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
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
                _log?.Error(ex.Message);
            }
        }

        #endregion Threadsafe methods
    }
}