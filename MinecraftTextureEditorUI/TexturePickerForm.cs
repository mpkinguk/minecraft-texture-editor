using MinecraftTextureEditorAPI;
using MinecraftTextureEditorAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        /// <summary>
        /// The constructor
        /// </summary>
        public TexturePickerForm()
        {
            InitializeComponent();

            LoadTextures();

            listViewTextureList.DrawItem += ListViewDrawItem;

            listViewTextureList.DoubleClick += ListViewTextureListDoubleClick;
        }

        /// <summary>
        /// The constructor
        /// </summary>
        public TexturePickerForm(string currentPath)
        {
            InitializeComponent();

            _currentPath = currentPath;

            LoadTextures();

            listViewTextureList.DrawItem += ListViewDrawItem;

            listViewTextureList.DoubleClick += ListViewTextureListDoubleClick;
        }

        private void ListViewTextureListDoubleClick(object sender, EventArgs e)
        {
            if (listViewTextureList.SelectedItems.Count == 0)
            {
                return;
            }

            var tag = (string)listViewTextureList.SelectedItems[0].Tag;

            OnTextureClicked(tag);
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
        /// Filter the items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxFilterTextChanged(object sender, EventArgs e)
        {
            listViewTextureList.RedrawItems(0, listViewTextureList.Items.Count - 1, true);
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

        private void ListViewDrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var tag = (string)e.Item.Tag;

            var filter = textBoxFilter.Text;

            if (tag.Contains(filter) || string.IsNullOrEmpty(filter))
            {
                var g = e.Graphics;

                // Prevents crashing while loading the imagelist
                if (!(e.Item.ImageList is null))
                {
                    g.DrawImage(listViewTextureList.LargeImageList.Images[e.Item.ImageIndex], e.Item.Bounds.X, e.Item.Bounds.Y, 32, 32);
                }

                g.DrawString(e.Item.Text.Substring(0, 4) + "...", e.Item.Font, Brushes.Black, e.Item.Bounds.X, e.Item.Bounds.Bottom - 7);

                g.Flush();
            }
            else
            {
                e.DrawDefault = false;
            }
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

            listViewTextureList.Items.Clear();

            Files = await Task.Run(() => FileHelper.GetFiles(Path.Combine(CurrentPath, assetsPath, texturesPath), "*.png", true)).ConfigureAwait(false);

            UpdateListView(Files);

            UpdateText("Texture Picker");
        }

        /// <summary>
        /// Update the listview with files
        /// </summary>
        /// <param name="files"></param>
        private void UpdateListView(List<string> files)
        {
            UpdateCursor(true);

            if (files is null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            var imageList = new ImageList();

            BeginUpdate();

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                var groupName = fileInfo.Directory.Name;

                ListViewGroup group = new ListViewGroup(groupName);

                Bitmap tmp;

                var item = new ListViewItem() { Tag = file, Text = fileInfo.Name, ImageIndex = imageList.Images.Count, Group = group };

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

                imageList.Images.Add(tmp);

                AddListTextureItem(item);
            }

            AssignImageList(imageList);

            EndUpdate();

            UpdateCursor(false);
        }

        #endregion Private methods

        #region Threadsafe methods

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
        /// Threadsafe method for BeginUpdate
        /// </summary>
        private void BeginUpdate()
        {
            if (listViewTextureList.InvokeRequired)
            {
                var d = new Action(BeginUpdate);
                Invoke(d);
            }
            else
            {
                listViewTextureList.BeginUpdate();
            }
        }

        /// <summary>
        /// Threadsafe method for EndUpdate
        /// </summary>
        private void EndUpdate()
        {
            if (listViewTextureList.InvokeRequired)
            {
                var d = new Action(EndUpdate);
                Invoke(d);
            }
            else
            {
                listViewTextureList.EndUpdate();
            }
        }

        /// <summary>
        /// Threadsafe method for adding listview items during async operations
        /// </summary>
        /// <param name="item">The item</param>
        private void AddListTextureItem(ListViewItem item)
        {
            if (listViewTextureList.InvokeRequired)
            {
                var d = new Action<ListViewItem>(AddListTextureItem);
                Invoke(d, new object[] { item });
            }
            else
            {
                listViewTextureList.Items.Add(item);
            }
        }

        /// <summary>
        /// Thread safe method for assigning imagelist to listview
        /// </summary>
        /// <param name="imageList"></param>
        private void AssignImageList(ImageList imageList)
        {
            if (listViewTextureList.InvokeRequired)
            {
                var d = new Action<ImageList>(AssignImageList);
                Invoke(d, new object[] { imageList });
            }
            else
            {
                listViewTextureList.LargeImageList = imageList;
            }
        }

        #endregion Threadsafe methods
    }
}