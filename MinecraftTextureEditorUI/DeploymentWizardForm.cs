using MinecraftTextureEditorAPI;
using MinecraftTextureEditorAPI.Helpers;
using MinecraftTextureEditorAPI.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZipFileManagerAPI;

namespace MinecraftTextureEditorUI
{
    public partial class DeploymentWizardForm : Form
    {
        /// <summary>
        /// The deployment path
        /// </summary>
        public string DeploymentPath { get; set; }

        private bool _deployed;

        private bool _unpack;

        /// <summary>
        /// Constructor
        /// </summary>
        public DeploymentWizardForm()
        {
            InitializeComponent();

            tabControlDeploy.SelectedIndexChanged += TabControlSelectedIndexChanged;

            PopulateVersions();
        }

        #region Form events

        /// <summary>
        /// A file has been processed, so update the progress bar
        /// </summary>
        /// <param name="value"></param>
        private void FileProcessed(string filename)
        {
            IncrementProgressBarValue(filename);
        }

        /// <summary>
        /// Dynamically process the buttons for each tab index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            var tabControl = (TabControl)sender;

            var lastTab = tabControl.TabCount - 1;

            switch (tabControl.SelectedIndex)
            {
                case 0:
                    buttonPrevious.Enabled = false;
                    buttonNext.Enabled = true;
                    buttonFinish.Enabled = false;
                    break;

                default:
                    if (tabControl.SelectedIndex == lastTab)
                    {
                        buttonPrevious.Enabled = true;
                        buttonNext.Enabled = false;
                        buttonFinish.Enabled = true;
                    }
                    else
                    {
                        buttonPrevious.Enabled = true;
                        buttonNext.Enabled = true;
                        buttonFinish.Enabled = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// Previous button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPreviousClick(object sender, EventArgs e)
        {
            if (tabControlDeploy.SelectedIndex > 0)
            {
                tabControlDeploy.SelectedIndex--;
            }
        }

        /// <summary>
        /// Next button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonNextClick(object sender, EventArgs e)
        {
            try
            {
                switch (tabControlDeploy.SelectedIndex)
                {
                    case 1:
                        // Validate inputs before trying to create pack
                        if (!int.TryParse(comboBoxFormat.Text.Split(':')[0], out int format))
                        {
                            throw new Exception("Invalid format");
                        }
                        if (string.IsNullOrEmpty(textBoxDescription.Text))
                        {
                            throw new Exception("Description is empty");
                        }
                        if (string.IsNullOrEmpty(textBoxPackName.Text))
                        {
                            throw new Exception("Pack name is empty");
                        }
                        if (!CreateMetaFile(textBoxDescription.Text, format))
                        {
                            throw new Exception("Could not create meta file");
                        }

                        // Clone to prevent threading issues
                        string PackName = (string)textBoxPackName.Text.Clone();

                        IncrementTabControl();

                        _deployed = await CreatePackZipFile(PackName).ConfigureAwait(false);

                        if (!_deployed)
                        {
                            throw new Exception("Could not create resource pack!");
                        }
                        else
                        {
                            UpdateProgressLabel("Unpacking zip file to resource pack folder...");
                            await UnpackZipFile(PackName).ConfigureAwait(false);
                            IncrementTabControl();
                        }
                        break;

                    case 0:
                        IncrementTabControl();
                        break;
                }
            }
            catch (Exception exc)
            {
                ShowErrorBox(exc.Message);
            }
        }

        /// <summary>
        /// Create meta file and deploy it to current path
        /// </summary>
        /// <param name="description">the description</param>
        /// <param name="format">The format</param>
        private bool CreateMetaFile(string description, int format)
        {
            try
            {
                var fileName = Path.Combine(DeploymentPath, "pack.mcmeta");

                var pack = new Pack() { Description = description, Format = format };

                var file = new MetaFile() { Pack = pack };

                var jsonString = JsonSerializer.Serialize(file, new JsonSerializerOptions { WriteIndented = true });

                using (TextWriter fs = File.CreateText(fileName))
                {
                    fs.Write(jsonString);

                    fs.Flush();
                }

                return true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
                return false;
            }
        }

        /// <summary>
        /// Create a new pack file in the resources folder
        /// </summary>
        /// <param name="packName">The zip file name</param>
        /// <returns>bool</returns>
        private async Task<bool> CreatePackZipFile(string packName)
        {
            try
            {
                UpdateCursor(true);

                var resourcePackFolder = ConfigurationHelper.LoadSetting("ResourcePackFolder");
                var assetsPath = ConfigurationHelper.LoadSetting("AssetsFolder");

                var filesPath = Path.Combine(DeploymentPath, assetsPath);

                var outputFile = Path.Combine(resourcePackFolder, string.Concat(packName, ".zip"));

                var files = await Task.Run(() => FileHelper.GetFiles(filesPath, "*.*", true)).ConfigureAwait(false);
                files.Add(Path.Combine(DeploymentPath, "pack.mcmeta"));

                UpdateProgressBarMin(0);
                UpdateProgressBarValue(0);
                UpdateProgressBarMax(files.Count);

                var zipFileManager = new ZipFileManager();

                zipFileManager.FileProcessed += FileProcessed;

                _deployed = await zipFileManager.ZipFiles(outputFile, DeploymentPath, files).ConfigureAwait(false);

                return _deployed;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
                return false;
            }
            finally
            {
                if (_deployed)
                {
                    DisablePreviousButton();
                }

                UpdateProgressLabel(_deployed ? "Deployment Complete" : "Deployment Failed");

                UpdateCursor(false);
            }
        }

        private async Task UnpackZipFile(string packName)
        {
            try
            {
                UpdateCursor(true);

                if (!_unpack)
                {
                    return;
                }

                if (!_deployed)
                {
                    UpdateProgressLabel("Zip file was not deployed properly. Aborting unpacking.");
                    return;
                }

                var resourcePackFolder = ConfigurationHelper.LoadSetting("ResourcePackFolder");

                var input = Path.Combine(resourcePackFolder, string.Concat(packName, ".zip"));

                var zipFileManager = new ZipFileManager();

                var result = await zipFileManager.UnZipFiles(input, resourcePackFolder);

                UpdateProgressLabel(result ? "Deployment complete!" : "Zip file was not unpacked properly.");
                _deployed = result;
            }
            catch (Exception exc)
            {
                UpdateProgressLabel(exc.Message);
            }
            finally
            {
                UpdateCursor(false);
            }
        }

        /// <summary>
        /// Finish button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFinishClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            Close();
        }

        /// <summary>
        /// Captures the check changed event for unpacking zip files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            _unpack = checkBoxUnpackZipFile.Checked;
        }

        #endregion Form events

        #region Threadsafe methods

        /// <summary>
        /// Threadsafe method for displaying error messagebox
        /// </summary>
        /// <param name="error">The error message</param>
        private void ShowErrorBox(string error)
        {
            if (InvokeRequired)
            {
                var d = new Action<string>(ShowErrorBox);
                Invoke(d, new object[] { error });
            }
            else
            {
                MessageBox.Show(this, error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Threadsafe method for updating progressbar min
        /// </summary>
        /// <param name="value">The value</param>
        private void UpdateProgressBarMin(int value)
        {
            if (progressBarDeploymentProgress.InvokeRequired)
            {
                var d = new Action<int>(UpdateProgressBarMin);
                Invoke(d, new object[] { value });
            }
            else
            {
                progressBarDeploymentProgress.Minimum = value;
            }
        }

        /// <summary>
        /// Threadsafe method for updating progressbar max
        /// </summary>
        /// <param name="value">The value</param>
        private void UpdateProgressBarMax(int value)
        {
            if (progressBarDeploymentProgress.InvokeRequired)
            {
                var d = new Action<int>(UpdateProgressBarMax);
                Invoke(d, new object[] { value });
            }
            else
            {
                progressBarDeploymentProgress.Maximum = value;
            }
        }

        /// <summary>
        /// Threadsafe method for updating progressbar value
        /// </summary>
        /// <param name="value">the value</param>
        private void IncrementProgressBarValue(string filename)
        {
            if (progressBarDeploymentProgress.InvokeRequired)
            {
                var d = new Action<string>(IncrementProgressBarValue);
                Invoke(d, new object[] { filename });
            }
            else
            {
                UpdateProgressLabel(filename);

                if (progressBarDeploymentProgress.Value < progressBarDeploymentProgress.Maximum)
                {
                    progressBarDeploymentProgress.Value++;
                }
            }
        }

        /// <summary>
        /// Threadsafe method for updating progressbar value
        /// </summary>
        /// <param name="value">the value</param>
        private void UpdateProgressBarValue(int value)
        {
            if (progressBarDeploymentProgress.InvokeRequired)
            {
                var d = new Action<int>(UpdateProgressBarValue);
                Invoke(d, new object[] { value });
            }
            else
            {
                progressBarDeploymentProgress.Value = 0;
            }
        }

        /// <summary>
        /// Threadsafe method for updating progress label text
        /// </summary>
        /// <param name="message">The message</param>
        private void UpdateProgressLabel(string message)
        {
            if (labelProgress.InvokeRequired)
            {
                var d = new Action<string>(UpdateProgressLabel);
                Invoke(d, new object[] { message });
            }
            else
            {
                labelProgress.Text = message;
            }
        }

        /// <summary>
        /// Threadsafe method for disabling previous button
        /// </summary>
        private void DisablePreviousButton()
        {
            if (buttonPrevious.InvokeRequired)
            {
                var d = new Action(DisablePreviousButton);
                Invoke(d);
            }
            else
            {
                buttonPrevious.Enabled = false;
            }
        }

        /// <summary>
        /// Threadsafe method for increment tab selected index
        /// </summary>
        private void IncrementTabControl()
        {
            if (tabControlDeploy.InvokeRequired)
            {
                var d = new Action(IncrementTabControl);
                Invoke(d);
            }
            else
            {
                if (tabControlDeploy.SelectedIndex < tabControlDeploy.TabCount - 1)
                {
                    tabControlDeploy.SelectedIndex++;
                }
            }
        }

        /// <summary>
        /// Threadsafe method for populating versions combo box
        /// </summary>
        private void PopulateVersions()
        {
            if (comboBoxFormat.InvokeRequired)
            {
                var d = new Action(PopulateVersions);
                Invoke(d);
            }
            else
            {
                var versions = ConfigurationHelper.LoadSetting("Versions");

                var versionSplit = versions.Split(';');

                comboBoxFormat.Items.AddRange(versionSplit);

                comboBoxFormat.SelectedIndex = versionSplit.Length - 1;
            }
        }

        /// <summary>
        /// Threadsafe method for increment tab selected index
        /// </summary>
        private void DecrementTabControl()
        {
            if (tabControlDeploy.InvokeRequired)
            {
                var d = new Action(DecrementTabControl);
                Invoke(d);
            }
            else
            {
                if (tabControlDeploy.SelectedIndex > 0)
                {
                    tabControlDeploy.SelectedIndex--;
                }
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

        #endregion Threadsafe methods

    }
}