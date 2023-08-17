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
        private string filePath = @"helper/isRecordingAudio.txt";
        public static string send = "Send, ";

        public Form1()// ChatGPT wrote everything in this method except for InitializeComponent()
        {
            InitializeComponent();
            // Initialize the FileSystemWatcher
            fileWatcher = new FileSystemWatcher(Path.GetDirectoryName(filePath), Path.GetFileName(filePath));
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += fileWatcher_Changed;

            // Start monitoring the file
            fileWatcher.EnableRaisingEvents = true;

            // Set the initial label value
            UpdateLabel();
            // ChatGPT ^^^
        }
        private void fileWatcher_Changed(object sender, FileSystemEventArgs e)// ChatGPT wrote this
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                // File content has changed, update the label
                this.Invoke(new Action(UpdateLabel));
            }
        }

        private void UpdateLabel()// ChatGPT wrote this
        {
            string fileContent = File.ReadAllText(filePath);

            if (fileContent.Trim() == "0") {
                txtIsRecording.Text = "False";
            }
            else if (fileContent.Trim() == "1") {
                txtIsRecording.Text = "True";
            }
            else {
                txtIsRecording.Text = "Error";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load();

            // https://stackoverflow.com/questions/1339524/how-do-i-add-a-tooltip-to-a-control#1339533
            ToolTip toolTip = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            // Set up the ToolTip text
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
            txtTakeScreenshotAndRecordAudioWithShareX.Text = File.ReadAllText(@"helper/hotkeyForTakeScreenshotAndRecordAudioWithShareX.txt");
            txtPasteImageAndAudioWithClipboardSoftware.Text = File.ReadAllText(@"helper/hotkeyForPasteImageAndAudioWithClipboardSoftware.txt");
            txtPasteMultipleImagesWithClipboardSoftware.Text = File.ReadAllText(@"helper/hotkeyForPasteMultipleImagesWithClipboardSoftware.txt");

            if (doesTextInFileEqualValue(@"helper/whenToTakeScreenshotWithShareX.txt", "0"))
                radBeginning.Checked = true;
            else if (doesTextInFileEqualValue(@"helper/whenToTakeScreenshotWithShareX.txt", "1"))
                radEnd.Checked = true;
            else if (doesTextInFileEqualValue(@"helper/whenToTakeScreenshotWithShareX.txt", "2"))
                radNoScreenshot.Checked = true;

            if (doesTextInFileEqualValue(@"helper/PlayPauseVideo.ahk", "MouseClick, left"))
                radLeftMouse.Checked = true;
            else if (doesTextInFileEqualValue(@"helper/PlayPauseVideo.ahk", "Send, {Space}"))
                radSpaceBar.Checked = true;

            // Files contain "Send, " at beginning, so cut it off
            txtTakeScreenshot.Text = File.ReadAllText(@"helper/TakeScreenshotWithShareX.ahk").Substring(send.Length);
            txtToggleRecordAudio.Text = File.ReadAllText(@"helper/ToggleRecordAudioWithShareX.ahk").Substring(send.Length);

            if (doesTextInFileEqualValue(@"helper/clipboardSoftware.txt", "ditto"))
            {
                radDitto.Checked = true;
                txtActivateClipboardSoftware.Text = File.ReadAllText(@"helper/ActivateDitto.ahk").Substring(send.Length);
            } else
            {
                radWindowsClipboard.Checked = true;
                txtActivateClipboardSoftware.Text = File.ReadAllText(@"helper/ActivateWindowsClipboard.ahk").Substring(send.Length);
            }

            txtGeneralDelay.Text = File.ReadAllText(@"helper/delayGeneral.txt");
            txtDelayForRecordingToStart.Text = File.ReadAllText(@"helper/delayForRecordingToStart.txt");
            
            btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }
        private void save()
        {
            // change all files to value in text boxes
            clearFileAndWrite(@"helper/hotkeyForTakeScreenshotAndRecordAudioWithShareX.txt", txtTakeScreenshotAndRecordAudioWithShareX.Text);
            clearFileAndWrite(@"helper/hotkeyForPasteImageAndAudioWithClipboardSoftware.txt", txtPasteImageAndAudioWithClipboardSoftware.Text);
            clearFileAndWrite(@"helper/hotkeyForPasteMultipleImagesWithClipboardSoftware.txt", txtPasteMultipleImagesWithClipboardSoftware.Text);

            if (radBeginning.Checked)
                clearFileAndWrite(@"helper/whenToTakeScreenshotWithShareX.txt", "0");
            else if (radEnd.Checked)
                clearFileAndWrite(@"helper/whenToTakeScreenshotWithShareX.txt", "1");
            else if (radNoScreenshot.Checked)
                clearFileAndWrite(@"helper/whenToTakeScreenshotWithShareX.txt", "2");

            if (radLeftMouse.Checked)
                clearFileAndWrite(@"helper/PlayPauseVideo.ahk", "MouseClick, left");
            else if (radSpaceBar.Checked)
                clearFileAndWrite(@"helper/PlayPauseVideo.ahk", "Send, {Space}");

            // "Send, " was cut off, so add it back
            clearFileAndWrite(@"helper/TakeScreenshotWithShareX.ahk", send + txtTakeScreenshot.Text);
            clearFileAndWrite(@"helper/ToggleRecordAudioWithShareX.ahk", send + txtToggleRecordAudio.Text);
            if (radDitto.Checked) {
                clearFileAndWrite(@"helper/ActivateDitto.ahk", send + txtActivateClipboardSoftware.Text);
                clearFileAndWrite(@"helper/clipboardSoftware.txt", "ditto");
            } else if (radWindowsClipboard.Checked)
            {
                clearFileAndWrite(@"helper/ActivateWindowsClipboard.ahk", send + txtActivateClipboardSoftware.Text);
                clearFileAndWrite(@"helper/clipboardSoftware.txt", "windowsClipboard");
            }
                

            clearFileAndWrite(@"helper/delayGeneral.txt", txtGeneralDelay.Text);
            clearFileAndWrite(@"helper/delayForRecordingToStart.txt", txtDelayForRecordingToStart.Text);

            // runs scripts
            openAllScripts();

            btnSave.Enabled = false;
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
        public static void clearFileAndWrite(string address, string text)
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
            startAutohotkeyScript("TakeScreenshotAndRecordAudioWithShareX.ahk");
            startAutohotkeyScript("PasteImageAndAudioWithClipboardSoftware.ahk");
            startAutohotkeyScript("PasteMultipleImagesWithClipboardSoftware.ahk");
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
                txtActivateClipboardSoftware.Text = File.ReadAllText(@"helper/ActivateDitto.ahk").Substring(send.Length);
                radEnd.Enabled = true;
            }
        }

        private void radWindowsClipboard_CheckedChanged(object sender, EventArgs e)
        {
            if (radWindowsClipboard.Checked)
            {
                txtActivateClipboardSoftware.Text = File.ReadAllText(@"helper/ActivateWindowsClipboard.ahk").Substring(send.Length);
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
