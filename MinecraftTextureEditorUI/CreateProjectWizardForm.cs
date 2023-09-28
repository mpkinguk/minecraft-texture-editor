using log4net;
using MinecraftTextureEditorAPI;
using MinecraftTextureEditorAPI.Helpers;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZipFileManagerAPI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MinecraftTextureEditorUI
{
    public partial class CreateProjectWizardForm : Form
    {
        /// <summary>
        /// Was the creation successful?
        /// </summary>
        public bool Success { get; set; }

        private readonly ILog _log;

        private bool _downloading;

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateProjectWizardForm(ILog log)
        {
            _log = log;

            try
            {
                InitializeComponent();

                tabControlCreate.SelectedIndexChanged += TabControlSelectedIndexChanged;

                tabControlCreate.ItemSize = new Size(0, 1);
                tabControlCreate.SizeMode = TabSizeMode.Fixed;

                PopulateVersions();

                textBoxProjectPath.Text = FileHelper.GetDefaultProjectFolder();

                if (Constants.LessLag)
                {
                    foreach (TabPage tabControlPage in tabControlCreate.TabPages)
                    {
                        tabControlPage.BackgroundImage = null;
                        tabControlPage.BackColor = Color.DimGray;
                    }
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #region Form events

        /// <summary>
        /// Finish button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFinishClick(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
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
                switch (tabControlCreate.SelectedIndex)
                {
                    case 1:
                        // Validate inputs before trying to create project
                        if (string.IsNullOrEmpty(comboBoxVersion.Text))
                        {
                            throw new ArgumentException("Version is empty or not selected");
                        }

                        IncrementTabControl();

                        break;

                    case 2:

                        if (string.IsNullOrEmpty(textBoxProjectPath.Text))
                        {
                            throw new ArgumentException("Project path is empty");
                        }
                        if (!Directory.Exists(textBoxProjectPath.Text))
                        {
                            throw new ArgumentException("Project path is invalid");
                        }
                        if (string.IsNullOrEmpty(textBoxPackName.Text))
                        {
                            throw new ArgumentException("Pack Name is empty");
                        }

                        State.IsJava = radioButtonJava.Checked;

                        // Clone to prevent threading issues
                        var packVersion = (string)comboBoxVersion.Text.Clone();

                        var packName = (string)textBoxPackName.Text.Clone();

                        var projectPath = (string)textBoxProjectPath.Text.Clone();

                        var newProjectPath = Path.Combine(projectPath, packName);

                        IncrementTabControl();

                        UpdateProgressLabel("Unpacking minecraft version file to project folder...");

                        var result = await UnpackZipFile(packVersion, newProjectPath).ConfigureAwait(false);

                        Success = result;

                        IncrementTabControl();

                        break;

                    case 0:
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
            if (tabControlCreate.SelectedIndex > 0)
            {
                tabControlCreate.SelectedIndex--;
            }
        }

        /// <summary>
        /// Capture the path browser button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonProjectPathBrowserClick(object sender, EventArgs e)
        {
            try
            {
                string folder = FileHelper.SelectFolder(textBoxProjectPath.Text);

                textBoxProjectPath.Text = folder;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Dynamically process the buttons for each tab index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Unpack the version .jar file to the chosen project directory
        /// </summary>
        /// <param name="packVersion">The pack version</param>
        /// <param name="outputPath">The output path</param>
        private async Task<bool> UnpackZipFile(string packVersion, string outputPath)
        {
            bool result;

            try
            {
                UpdateCursor(true);

                var projectFolder = Path.Combine(outputPath);

                string packFile;

                if (State.IsJava) {

                    packFile= Path.Combine(FileHelper.GetMineCraftFolder(), "versions", packVersion, string.Concat(packVersion, ".jar"));

                    if (!File.Exists(packFile))
                    {
                        throw new FileNotFoundException($"Could not find .jar file for version {packVersion} in {packFile}");
                    }
                } else
                {
                    packFile = Path.Combine(Constants.BedrockResourcePackFolder, string.Concat(packVersion, ".zip"));

                    outputPath = string.Concat(outputPath, ".zip");

                    using (var client = new HttpClient())
                    {
                        using (var s = await client.GetStreamAsync(packFile))
                        {
                            using (var fs = new FileStream(outputPath, FileMode.OpenOrCreate))
                            {
                                UpdateProgressLabel("Downloading pack from Mojang github server...");

                                await s.CopyToAsync(fs).ConfigureAwait(false);
                            }
                        }
                    }

                    packFile = outputPath;
                }

                UpdateProgressLabel("Unpacking file...");

                var zipFileManager = new ZipFileManager(_log);

                result = await zipFileManager.UnZipFiles(packFile, projectFolder);

                UpdateProgressLabel(Success ? "Project Creation complete!" : "Project was not unpacked correctly.\nPlease check your paths and files.");

                if (result)
                {
                    State.Path = Path.Combine(projectFolder);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.Abort;
                }

                return result;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
                UpdateProgressLabel(ex.Message);
                return false;
            }
            finally
            {
                UpdateCursor(false);
            }
        }

        // Event to track when file has finished downloading
        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _downloading = false;
        }

        // Event to track the progress
        void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            UpdateProgressLabel($"Downloading bedrock example pack: {e.ProgressPercentage}");
        }

        /// <summary>
        /// Populate the versions dropdown when checkbox value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonJavaCheckedChanged(object sender, EventArgs e)
        {
            EditionChanged();
        }

        #endregion Form events

        #region Threadsafe methods

        /// <summary>
        /// Threadsafe method for increment tab selected index
        /// </summary>
        private void DecrementTabControl()
        {
            try
            {
                if (tabControlCreate.InvokeRequired)
                {
                    var d = new Action(DecrementTabControl);
                    Invoke(d);
                }
                else
                {
                    if (tabControlCreate.SelectedIndex > 0)
                    {
                        tabControlCreate.SelectedIndex--;
                    }
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Threadsafe method for disabling previous button
        /// </summary>
        private void DisablePreviousButton()
        {
            try
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
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Threadsafe method for increment tab selected index
        /// </summary>
        private void IncrementTabControl()
        {
            try
            {
                if (tabControlCreate.InvokeRequired)
                {
                    var d = new Action(IncrementTabControl);
                    Invoke(d);
                }
                else
                {
                    if (tabControlCreate.SelectedIndex < tabControlCreate.TabCount - 1)
                    {
                        tabControlCreate.SelectedIndex++;
                    }
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Threadsafe method for populating versions combo box
        /// </summary>
        private void PopulateVersions()
        {
            try
            {
                if (comboBoxVersion.InvokeRequired)
                {
                    var d = new Action(PopulateVersions);
                    Invoke(d);
                }
                else
                {
                    var versions = ConfigurationHelper.LoadSetting("Versions", Constants.BedrockSettings);

                    var versionSplit = State.IsJava? FileHelper.GetVersions(): versions.Split(";".ToCharArray());

                    comboBoxVersion.Items.Clear();

                    comboBoxVersion.Items.AddRange(versionSplit);

                    comboBoxVersion.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Threadsafe method for displaying error messagebox
        /// </summary>
        /// <param name="error">The error message</param>
        private void ShowErrorBox(string error)
        {
            try
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
                    Cursor = waiting ? Cursors.WaitCursor : Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Threadsafe method for updating progress label text
        /// </summary>
        /// <param name="message">The message</param>
        private void UpdateProgressLabel(string message)
        {
            try
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
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Threadsafe method for increment tab selected index
        /// </summary>
        private void EditionChanged()
        {
            try
            {
                if (radioButtonJava.InvokeRequired)
                {
                    var d = new Action(EditionChanged);
                    Invoke(d);
                }
                else
                {
                    State.IsJava = radioButtonJava.Checked;
                    PopulateVersions();
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