using log4net;
using MinecraftTextureEditorAPI;
using MinecraftTextureEditorAPI.Helpers;
using MinecraftTextureEditorAPI.Model;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZipFileManagerAPI;

namespace MinecraftTextureEditorUI
{
    public partial class DeploymentWizardForm : Form
    {
        private readonly ILog _log;

        /// <summary>
        /// Was the packaged deployed?
        /// </summary>
        public bool Deployed => _deployed;

        /// <summary>
        /// Was the package unpacked?
        /// </summary>
        public bool UnPacked => _unpack;

        /// <summary>
        /// The zip file path created
        /// </summary>
        public string ZipFilePath => _zipFilePath;

        private bool _deployed;
        private bool _onlyTextures;
        private bool _unpack;
        private bool _updateDescription;

        private string _zipFilePath;

        /// <summary>
        /// Constructor
        /// </summary>
        public DeploymentWizardForm(ILog log)
        {
            _log = log;

            InitializeComponent();

            tabControlDeploy.SelectedIndexChanged += TabControlSelectedIndexChanged;

            tabControlDeploy.ItemSize = new Size(0, 1);
            tabControlDeploy.SizeMode = TabSizeMode.Fixed;

            PopulateVersions();

            textBoxPackName.PreviewKeyDown += TextBoxPackName_PreviewKeyDown;

            _unpack = checkBoxUnpackZipFile.Checked;
            _onlyTextures = checkBoxOnlyIncludeTextures.Checked;

            if (Constants.LessLag)
            {
                foreach (TabPage tabControlPage in tabControlDeploy.TabPages)
                {
                    tabControlPage.BackgroundImage = null;
                    tabControlPage.BackColor = Color.DimGray;
                }
            }
        }

        #region Form events

        /// <summary>
        /// Preview key down event for pack name textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPackName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            _updateDescription = (textBoxPackName.Text.Equals(textBoxDescription.Text));
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
                        if (string.IsNullOrEmpty(textBoxDescription.Text))
                        {
                            throw new Exception("Description is empty");
                        }
                        if (string.IsNullOrEmpty(textBoxPackName.Text))
                        {
                            throw new Exception("Pack name is empty");
                        }

                        IncrementTabControl();

                        break;

                    case 2:
                        IncrementTabControl();

                        // Validate inputs before trying to create pack
                        if (!int.TryParse(comboBoxFormat.Text.Split(':')[0], out int format))
                        {
                            throw new Exception("Invalid format");
                        }

                        // If we cannot create the pack file, abort!
                        if (!CreateMetaFile(textBoxDescription.Text, format))
                        {
                            throw new Exception("Could not create meta file");
                        }

                        // Clone to prevent threading issues
                        string PackName = (string)textBoxPackName.Text.Clone();

                        var resourcePackFolder = FileHelper.GetResourcePackFolder();

                        var outputFile = Path.Combine(resourcePackFolder, string.Concat(PackName, ".zip"));

                        _zipFilePath = outputFile;

                        var unpackDirectory = outputFile.Replace(".zip", "");

                        if (File.Exists(outputFile))
                        {
                            switch (MessageBox.Show(this, Constants.PackageExistsCreateBackupMessage, Constants.Warning, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                            {
                                case DialogResult.Yes:
                                    File.Move(outputFile, $"{outputFile}.bak");
                                    if (_unpack)
                                    {
                                        if (Directory.Exists(unpackDirectory))
                                        {
                                            Directory.Move(unpackDirectory, $"{unpackDirectory}_bak");
                                        }
                                    }
                                    break;

                                case DialogResult.No:
                                    File.Delete(outputFile);
                                    if (_unpack)
                                    {
                                        if (Directory.Exists(unpackDirectory))
                                        {
                                            Directory.Delete(unpackDirectory);
                                        }
                                    }
                                    break;

                                case DialogResult.Cancel:
                                    throw new OperationCanceledException("Operation cancelled");
                            }
                        }

                        _deployed = await CreatePackZipFile(outputFile).ConfigureAwait(false);

                        if (!_deployed)
                        {
                            throw new Exception("Could not create resource pack!");
                        }
                        else
                        {
                            if (_unpack)
                            {
                                UpdateProgressLabel("Unpacking zip file to resource pack folder...");
                                await UnpackZipFile(PackName).ConfigureAwait(false);
                            }
                        }

                        IncrementTabControl();
                        break;

                    default:
                        IncrementTabControl();
                        break;
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
                ShowErrorBox(ex.Message);
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
        /// Captures the check changed event for unpacking zip files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            switch (checkBox.Name)
            {
                case nameof(checkBoxUnpackZipFile):
                    _unpack = checkBox.Checked;
                    break;

                case nameof(checkBoxOnlyIncludeTextures):
                    _onlyTextures = checkBox.Checked;
                    break;
            }
        }

        /// <summary>
        /// Only include textures
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxOnlyIncludeTexturesCheckedChanged(object sender, EventArgs e)
        {
            CheckBoxCheckedChanged(sender, e);
        }

        /// <summary>
        /// Unpack zip file in resources folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxUnpackZipFileCheckedChanged(object sender, EventArgs e)
        {
            CheckBoxCheckedChanged(sender, e);
        }

        /// <summary>
        /// Capture the text changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPackNameTextChanged(object sender, EventArgs e)
        {
            if (_updateDescription)
            {
                textBoxDescription.Text = textBoxPackName.Text;
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
                var filesPath = FileHelper.GetProjectRootFolder(State.Path);

                var fileName = Path.Combine(filesPath, "pack.mcmeta");

                var pack = new Pack() { Description = description, Format = format };

                var file = new MetaFile() { Pack = pack };

                var jsonString = FileHelper.Serialize(file);

                using (TextWriter fs = File.CreateText(fileName))
                {
                    fs.Write(jsonString);

                    fs.Flush();
                }

                return true;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Create a new pack file in the resources folder
        /// </summary>
        /// <param name="outputFile">The output file name</param>
        /// <returns>bool</returns>
        private async Task<bool> CreatePackZipFile(string outputFile)
        {
            try
            {
                UpdateCursor(true);

                var filesPath = FileHelper.GetProjectRootFolder(State.Path);

                var files = await Task.Run(() => FileHelper.GetFiles(filesPath, _onlyTextures ? "*.png" : "*.*", true)).ConfigureAwait(false);

                files.Add(Path.Combine(filesPath, "pack.mcmeta"));

                UpdateProgressBarMin(0);
                UpdateProgressBarValue(0);
                UpdateProgressBarMax(files.Count);

                var zipFileManager = new ZipFileManager(_log);

                zipFileManager.FileProcessed += FileProcessed;

                _deployed = await zipFileManager.ZipFiles(outputFile, filesPath, files).ConfigureAwait(false);

                return _deployed;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
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
        /// Upack a zip file
        /// </summary>
        /// <param name="packName">The pack name</param>
        /// <returns>Task</returns>
        private async Task UnpackZipFile(string packName)
        {
            try
            {
                if (!_unpack)
                {
                    return;
                }

                if (!_deployed)
                {
                    UpdateProgressLabel("Zip file was not deployed correctly. Aborting unpacking.");
                    return;
                }

                UpdateCursor(true);

                var resourcePackFolder = FileHelper.GetResourcePackFolder();

                var input = Path.Combine(resourcePackFolder, string.Concat(packName, ".zip"));

                var zipFileManager = new ZipFileManager(_log);

                var result = await zipFileManager.UnZipFiles(input, resourcePackFolder);

                UpdateProgressLabel(result ? "Deployment complete!" : "Zip file was not unpacked properly.");
                _deployed = result;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
                UpdateProgressLabel(ex.Message);
            }
            finally
            {
                UpdateCursor(false);
            }
        }

        #endregion Form events

        #region Threadsafe methods

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
                var versions = State.IsJava ? ConfigurationHelper.LoadSetting("Versions") : ConfigurationHelper.LoadSetting("Versions", Constants.BedrockSettings);

                var versionSplit = versions.Split(';');

                comboBoxFormat.Items.AddRange(versionSplit);

                comboBoxFormat.SelectedIndex = versionSplit.Length - 1;
            }
        }

        /// <summary>
        /// Threadsafe method for displaying error messagebox
        /// </summary>
        /// <param name="error">The error message</param>
        private void ShowErrorBox(string error)
        {
            if (InvokeRequired)
            {
                //var d = new Action<string>(ShowErrorBox);
                BeginInvoke((MethodInvoker)delegate { ShowErrorBox(error); });
                //Invoke(d, new object[] { error });
            }
            else
            {
                MessageBox.Show(this, error, Constants.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #endregion Threadsafe methods
    }
}