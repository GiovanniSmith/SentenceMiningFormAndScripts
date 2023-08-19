using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace AnkiAudioSentenceCardScript
{
    public partial class Form1 : Form
    {
        private FileSystemWatcher fileWatcher;
        public static string send = "Send, ";

        public static string filePathForTakeScreenshotAndRecordAudioWithShareX = "TakeScreenshotAndRecordAudioWithShareX.ahk";
        public static string filePathForHotkeyForTakeScreenshotAndRecordAudioWithShareX = @"helper/hotkeyForTakeScreenshotAndRecordAudioWithShareX.txt";
        public static string filePathForPasteImageAndAudioWithClipboardSoftware = "PasteImageAndAudioWithClipboardSoftware.ahk";
        public static string filePathForHotkeyForPasteImageAndAudioWithClipboardSoftware = @"helper/hotkeyForPasteImageAndAudioWithClipboardSoftware.txt";
        public static string filePathForPasteMultipleImagesWithClipboardSoftware = "PasteMultipleImagesWithClipboardSoftware.ahk";
        public static string filePathForHotkeyForPasteMultipleImagesWithClipboardSoftware = @"helper/hotkeyForPasteMultipleImagesWithClipboardSoftware.txt";
        public static string filePathForIsRecordingAudio = @"helper/isRecordingAudio.txt";

        public static string filePathForTakeScreenshot = @"helper/TakeScreenshotWithShareX.ahk";
        public static string filePathForToggleRecordAudio = @"helper/ToggleRecordAudioWithShareX.ahk";
        public static string filePathForWhenToTakeScreenshotWithShareX = @"helper/whenToTakeScreenshotWithShareX.txt";

        public static string filePathForClipboardSoftware = @"helper/clipboardSoftware.txt";
        public static string filePathForActivateDitto = @"helper/ActivateDitto.ahk";
        public static string filePathForActivateWindowsClipboard = @"helper/ActivateWindowsClipboard.ahk";

        public static string filePathForPlayPauseVideo = @"helper/PlayPauseVideo.ahk";

        public static string filePathForGeneralDelay = @"helper/delayGeneral.txt";
        public static string filePathForDelayForRecordingToStart = @"helper/delayForRecordingToStart.txt";

        public Form1()// ChatGPT wrote everything in this method except for InitializeComponent()
        {
            InitializeComponent();
            fileWatcher = new FileSystemWatcher(Path.GetDirectoryName(filePathForIsRecordingAudio), Path.GetFileName(filePathForIsRecordingAudio));
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += fileWatcher_Changed;
            fileWatcher.EnableRaisingEvents = true;
            UpdateLabel();
        }
        private void fileWatcher_Changed(object sender, FileSystemEventArgs e)// ChatGPT wrote this
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
                this.Invoke(new Action(UpdateLabel));
        }

        private void UpdateLabel()// ChatGPT wrote this
        {
            string fileContent = File.ReadAllText(filePathForIsRecordingAudio);

            if (fileContent.Trim() == "0")
                txtIsRecording.Text = "False";
            else if (fileContent.Trim() == "1")
                txtIsRecording.Text = "True";
            else
                txtIsRecording.Text = "Error";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load();
            // https://stackoverflow.com/questions/1339524/how-do-i-add-a-tooltip-to-a-control#1339533
            ToolTip toolTip = new ToolTip();
            setToolTipProperties(toolTip);
            setToolTipTexts(toolTip);
        }
        private void setToolTipProperties(ToolTip toolTip)
        {
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
        }
        private void setToolTipTexts(ToolTip toolTip)
        {
            toolTip.SetToolTip(this.linkLblAutoHotkeyWebsite, "https://www.autohotkey.com/docs/v2/Hotkeys.htm#Symbols");
            toolTip.SetToolTip(this.grpPrimaryScriptHotkeys, "Hotkeys that the user presses");
            toolTip.SetToolTip(this.grpSecondaryScriptHotkeys, "Hotkeys that the script presses");
            toolTip.SetToolTip(this.grpDelays, "Programs need delays between actions so that they function correctly");
            toolTip.SetToolTip(this.lblIsRecording, "Pressing the hotkey for \"Take screenshot and record audio with ShareX\" should toggle this");
            toolTip.SetToolTip(this.txtIsRecording, "Pressing the hotkey for \"Take screenshot and record audio with ShareX\" should toggle this");
            toolTip.SetToolTip(this.btnResetIsRecording, "If you are not recording audio with ShareX and \"Is ShareX currently recording audio?\" is \"True,\"\n" +
                "then press this button.");
        }

        private void load()
        {
            fillTextBoxesWithInfoFromFiles();
            selectRadioButtonsFromInfoFromFiles();

            if (doesTextInFileEqualValue(filePathForClipboardSoftware, "ditto"))
            {
                radDitto.Checked = true;
                txtActivateClipboardSoftware.Text = readOnlyTheHotkeyOfDotAHKFile(filePathForActivateDitto);
            } else
            {
                radWindowsClipboard.Checked = true;
                txtActivateClipboardSoftware.Text = readOnlyTheHotkeyOfDotAHKFile(filePathForActivateWindowsClipboard);
            }

            btnSave.Enabled = false;
        }
        private void fillTextBoxesWithInfoFromFiles()
        {
            txtTakeScreenshotAndRecordAudioWithShareX.Text = readFile(filePathForHotkeyForTakeScreenshotAndRecordAudioWithShareX);
            txtPasteImageAndAudioWithClipboardSoftware.Text = readFile(filePathForHotkeyForPasteImageAndAudioWithClipboardSoftware);
            txtPasteMultipleImagesWithClipboardSoftware.Text = readFile(filePathForHotkeyForPasteMultipleImagesWithClipboardSoftware);
            txtGeneralDelay.Text = readFile(filePathForGeneralDelay);
            txtDelayForRecordingToStart.Text = readFile(filePathForDelayForRecordingToStart);
            txtTakeScreenshot.Text = readOnlyTheHotkeyOfDotAHKFile(filePathForTakeScreenshot);
            txtToggleRecordAudio.Text = readOnlyTheHotkeyOfDotAHKFile(filePathForToggleRecordAudio);
        }
        
        private void selectRadioButtonsFromInfoFromFiles()
        {
            if (doesTextInFileEqualValue(filePathForWhenToTakeScreenshotWithShareX, "0"))
                radBeginning.Checked = true;
            else if (doesTextInFileEqualValue(filePathForWhenToTakeScreenshotWithShareX, "1"))
                radEnd.Checked = true;
            else if (doesTextInFileEqualValue(filePathForWhenToTakeScreenshotWithShareX, "2"))
                radNoScreenshot.Checked = true;

            if (doesTextInFileEqualValue(filePathForPlayPauseVideo, "MouseClick, left"))
                radLeftMouse.Checked = true;
            else if (doesTextInFileEqualValue(filePathForPlayPauseVideo, "Send, {Space}"))
                radSpaceBar.Checked = true;
        }

        private string readFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
        private string readOnlyTheHotkeyOfDotAHKFile(string filePath)
        {
            return readFile(filePath).Substring(send.Length);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }
        private void save()
        {
            writeTextBoxInformationIntoFiles();
            writeRadioButtonSelectionsIntoFiles();
            openAllScripts();
            btnSave.Enabled = false;
        }
        private void writeTextBoxInformationIntoFiles()
        {
            updateFile(filePathForHotkeyForTakeScreenshotAndRecordAudioWithShareX);
            updateFile(filePathForHotkeyForPasteImageAndAudioWithClipboardSoftware);
            updateFile(filePathForHotkeyForPasteMultipleImagesWithClipboardSoftware);
            updateFile(filePathForTakeScreenshot);
            updateFile(filePathForToggleRecordAudio);
            updateFile(filePathForGeneralDelay);
            updateFile(filePathForDelayForRecordingToStart);
        }
        
        private void writeRadioButtonSelectionsIntoFiles()
        {
            updateFile(filePathForWhenToTakeScreenshotWithShareX);
            updateFile(filePathForPlayPauseVideo);

            if (radDitto.Checked)
                updateFile(filePathForActivateDitto);
            else if (radWindowsClipboard.Checked)
                updateFile(filePathForActivateWindowsClipboard);

            updateFile(filePathForClipboardSoftware);
        }
        private void updateFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (filePath.Equals(filePathForWhenToTakeScreenshotWithShareX))
                {
                    if (radBeginning.Checked)
                        updateFileWithText(filePathForWhenToTakeScreenshotWithShareX, "0");
                    else if (radEnd.Checked)
                        updateFileWithText(filePathForWhenToTakeScreenshotWithShareX, "1");
                    else if (radNoScreenshot.Checked)
                        updateFileWithText(filePathForWhenToTakeScreenshotWithShareX, "2");
                }
                else if (filePath.Equals(filePathForPlayPauseVideo))
                {
                    if (radLeftMouse.Checked)
                        updateFileWithText(filePathForPlayPauseVideo, "MouseClick, left");
                    else if (radSpaceBar.Checked)
                        updateFileWithText(filePathForPlayPauseVideo, "Send, {Space}");
                }
                else if (filePath.Equals(filePathForHotkeyForTakeScreenshotAndRecordAudioWithShareX))
                    updateFileWithText(filePathForHotkeyForTakeScreenshotAndRecordAudioWithShareX, txtTakeScreenshotAndRecordAudioWithShareX.Text);

                else if (filePath.Equals(filePathForHotkeyForPasteImageAndAudioWithClipboardSoftware))
                    updateFileWithText(filePathForHotkeyForPasteImageAndAudioWithClipboardSoftware, txtPasteImageAndAudioWithClipboardSoftware.Text);

                else if (filePath.Equals(filePathForHotkeyForPasteMultipleImagesWithClipboardSoftware))
                    updateFileWithText(filePathForHotkeyForPasteMultipleImagesWithClipboardSoftware, txtPasteMultipleImagesWithClipboardSoftware.Text);

                else if (filePath.Equals(filePathForTakeScreenshot))
                    updateFileWithText(filePathForTakeScreenshot, send + txtTakeScreenshot.Text);

                else if (filePath.Equals(filePathForToggleRecordAudio))
                    updateFileWithText(filePathForToggleRecordAudio, send + txtToggleRecordAudio.Text);

                else if (filePath.Equals(filePathForGeneralDelay))
                    updateFileWithText(filePathForGeneralDelay, txtGeneralDelay.Text);

                else if (filePath.Equals(filePathForDelayForRecordingToStart))
                    updateFileWithText(filePathForDelayForRecordingToStart, txtDelayForRecordingToStart.Text);

                else if (filePath.Equals(filePathForActivateDitto))
                    updateFileWithText(filePathForActivateDitto, send + txtActivateClipboardSoftware.Text);

                else if (filePath.Equals(filePathForClipboardSoftware) && radDitto.Checked)
                    updateFileWithText(filePathForClipboardSoftware, "ditto");

                else if (filePath.Equals(filePathForActivateWindowsClipboard))
                    updateFileWithText(filePathForActivateWindowsClipboard, send + txtActivateClipboardSoftware.Text);

                else if (filePath.Equals(filePathForClipboardSoftware) && radWindowsClipboard.Checked)
                    updateFileWithText(filePathForClipboardSoftware, "windowsClipboard");
            } else
            {
                Console.WriteLine("File does not exist");
            }
        }

        public void startAutohotkeyScript(String s)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C \"" + s + "\"";
            process.StartInfo = startInfo;
            process.Start();
        }
        public static void updateFileWithText(string address, string text)
        {
            File.WriteAllText(address, String.Empty);
            File.WriteAllText(address, text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Form form = new ResetToDefaultConfirmation();
            form.ShowDialog();
            if (ResetToDefaultConfirmation.resetWasClicked)
            {
                load();
                save();
            }
        }
        private void btnResetIsRecording_Click(object sender, EventArgs e)
        {
            Form form = new ResetIsRecording();
            form.ShowDialog();
            if (ResetIsRecording.resetForIsRecordingWasClicked)
            {
                load();
                save();
            }
        }

        private void txtTakeScreenshotAndRecordAudioWithShareX_TextChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void txtPasteImageAndAudioWithClipboardSoftware_TextChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void txtPasteMultipleImagesWithClipboardSoftware_TextChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void txtTakeScreenshot_TextChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void txtToggleRecordAudio_TextChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void radBeginning_CheckedChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void radEnd_CheckedChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void radNoScreenshot_CheckedChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void txtActivateClipboardSoftware_TextChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void radSpaceBar_CheckedChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void radLeftMouse_CheckedChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void txtGeneralDelay_TextChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void txtDelayForRecordingToStart_TextChanged(object sender, EventArgs e) { btnSave.Enabled = true; }
        private void linkLblAutoHotkeyWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://www.autohotkey.com/docs/v2/Hotkeys.htm#Symbols");
            Process.Start(sInfo);
        }

        private void openAllScripts()
        {
            startAutohotkeyScript(filePathForTakeScreenshotAndRecordAudioWithShareX);
            startAutohotkeyScript(filePathForPasteImageAndAudioWithClipboardSoftware);
            startAutohotkeyScript(filePathForPasteMultipleImagesWithClipboardSoftware);
        }

        public static void closeAllScriptsWindows10()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C taskkill /im \"autohotkey.exe\"";
            process.StartInfo = startInfo;
            process.Start();
        }

        public static void closeAllScriptsWindows11()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C taskkill /im \"autohotkeyu64.exe\"";
            process.StartInfo = startInfo;
            process.Start();
        }
        public static void closeAllScripts()
        {
            
        }
        private void btnOpenAllScripts_Click(object sender, EventArgs e)
        {
            openAllScripts();
        }

        private void btnCloseAllScripts_Click(object sender, EventArgs e)
        {
            closeAllScriptsWindows10();
            closeAllScriptsWindows11();
        }

        private void radDitto_CheckedChanged(object sender, EventArgs e)
        {
            if (radDitto.Checked)
            {
                txtActivateClipboardSoftware.Text = readOnlyTheHotkeyOfDotAHKFile(filePathForActivateDitto);
                radEnd.Enabled = true;
            }
        }

        private void radWindowsClipboard_CheckedChanged(object sender, EventArgs e)
        {
            if (radWindowsClipboard.Checked)
            {
                txtActivateClipboardSoftware.Text = readOnlyTheHotkeyOfDotAHKFile(filePathForActivateWindowsClipboard);
                disableTakeScreenshotAtBeginning();
            }
        }

        private void disableTakeScreenshotAtBeginning()
        {
            if (radEnd.Checked)
                radBeginning.Checked = true;

            radEnd.Enabled = false;
        }

        private Boolean doesTextInFileEqualValue(string textFilePath, string value)
        {
            int textFilePathLength = File.ReadAllText(textFilePath).Length;
            return File.ReadAllText(textFilePath).Substring(0, textFilePathLength).Equals(value);
        }
    }
}
