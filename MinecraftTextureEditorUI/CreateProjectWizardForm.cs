using log4net;
using MinecraftTextureEditorAPI.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZipFileManagerAPI;

namespace MinecraftTextureEditorUI
{
    public partial class CreateProjectWizardForm : Form
    {
        /// <summary>
        /// The deployment path
        /// </summary>
        public string CurrentPath { get; set; }

        private readonly ILog _log;

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateProjectWizardForm(ILog log)
        {
            _log = log;

            try
            {
                InitializeComponent();

                tabControlDeploy.SelectedIndexChanged += TabControlSelectedIndexChanged;

                PopulateVersions();

                textBoxProjectPath.Text = FileHelper.GetMineCraftFolder();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #region Form events

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
                        // Validate inputs before trying to create project
                        if (string.IsNullOrEmpty(comboBoxVersion.Text))
                        {
                            throw new ArgumentException("Version is empty or not selected");
                        }
                        if (string.IsNullOrEmpty(textBoxProjectPath.Text))
                        {
                            throw new ArgumentException("Project path is empty");
                        }
                        if (!Directory.Exists(textBoxProjectPath.Text))
                        {
                            throw new ArgumentException("Project path is invalid");
                        }

                        // Clone to prevent threading issues
                        var packVersion = (string)comboBoxVersion.Text.Clone();
                        var projectPath = (string)textBoxProjectPath.Text.Clone();

                        IncrementTabControl();

                        UpdateProgressLabel("Unpacking minecraft version file to project folder...");

                        await UnpackZipFile(packVersion, projectPath).ConfigureAwait(false);

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
        /// Unpack the version .jar file to the chosen project directory
        /// </summary>
        /// <param name="packVersion">The pack version</param>
        /// <param name="outputPath">The output path</param>
        private async Task UnpackZipFile(string packVersion, string outputPath)
        {
            try
            {
                UpdateCursor(true);

                var projectFolder = Path.Combine(outputPath);

                var packFile = Path.Combine(FileHelper.GetMineCraftFolder(), "versions", packVersion, string.Concat(packVersion, ".jar"));

                if (!File.Exists(packFile))
                {
                    throw new FileNotFoundException($"Could not find .jar file for version {packVersion} in {packFile}");
                }

                var zipFileManager = new ZipFileManager(_log);

                var result = await zipFileManager.UnZipFiles(packFile, projectFolder);

                UpdateProgressLabel(result ? "Project Creation complete!" : "Project was not unpacked correctly.\nPlease check your paths and files.");

                if (result)
                {
                    CurrentPath = Path.Combine(projectFolder, packFile);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.Abort;
                }
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
        /// Capture the path browser button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonProjectPathBrowserClick(object sender, EventArgs e)
        {
            try
            {
                string folder = FileHelper.OpenFolderName(FileHelper.GetMineCraftFolder());

                textBoxProjectPath.Text = folder;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #endregion Form events

        #region Threadsafe methods

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
                    var versions = FileHelper.GetVersions();
                    comboBoxVersion.Items.AddRange(versions);
                    comboBoxVersion.SelectedIndex = 0;
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
        private void DecrementTabControl()
        {
            try
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

        #endregion Threadsafe methods
    }
}