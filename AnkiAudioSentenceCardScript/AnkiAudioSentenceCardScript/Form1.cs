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
        /**
         * #SingleInstance, Force
            SendMode Input
            SetWorkingDir, %A_ScriptDir%
         */
        public static string send = "Send, ";

        public Form1()
        {
            InitializeComponent();
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
            toolTip.SetToolTip(this.grpPrimaryScriptHotkeys, "Hotkeys that the user presses");
            toolTip.SetToolTip(this.grpSecondaryScriptHotkeys, "Hotkeys that the script presses");
            toolTip.SetToolTip(this.grpDelays, "Programs need delays between actions so that they function correctly");
        }

        private void load()
        {
            txtTakeScreenshotAndRecordAudioWithShareX.Text = File.ReadAllText(@"helper/hotkeyForTakeScreenshotAndRecordAudioWithShareX.txt");
            txtPasteImageAndAudioFromDitto.Text = File.ReadAllText(@"helper/hotkeyForPasteImageAndAudioFromDitto.txt");
            txtPasteMultipleImagesFromDitto.Text = File.ReadAllText(@"helper/hotkeyForPasteMultipleImagesFromDitto.txt");

            if (File.ReadAllText(@"helper/whenToTakeScreenshotWithShareX.txt").Substring(0, 1).Equals("0"))
                radBeginning.Checked = true;
            else if (File.ReadAllText(@"helper/whenToTakeScreenshotWithShareX.txt").Substring(0, 1).Equals("1"))
                radEnd.Checked = true;
            else if (File.ReadAllText(@"helper/whenToTakeScreenshotWithShareX.txt").Substring(0, 1).Equals("2"))
                radNoScreenshot.Checked = true;

            if (File.ReadAllText(@"helper/PlayPauseVideo.ahk").Substring(0, 10).Equals("MouseClick"))
                radLeftMouse.Checked = true;
            else if (File.ReadAllText(@"helper/PlayPauseVideo.ahk").Substring(0, 13).Equals("Send, {Space}"))
                radSpaceBar.Checked = true;

            // Files contain "Send, " at beginning, so cut it off
            txtTakeScreenshot.Text = File.ReadAllText(@"helper/TakeScreenshotWithShareX.ahk").Substring(send.Length);
            txtToggleRecordAudio.Text = File.ReadAllText(@"helper/ToggleRecordAudioWithShareX.ahk").Substring(send.Length);
            txtActivateDitto.Text = File.ReadAllText(@"helper/ActivateDitto.ahk").Substring(send.Length);

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
            clearFileAndWrite(@"helper/hotkeyForPasteImageAndAudioFromDitto.txt", txtPasteImageAndAudioFromDitto.Text);
            clearFileAndWrite(@"helper/hotkeyForPasteMultipleImagesFromDitto.txt", txtPasteMultipleImagesFromDitto.Text);

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
            clearFileAndWrite(@"helper/ActivateDitto.ahk", send + txtActivateDitto.Text);

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

        private void btnOpenCropMarginCalculatorForShareX_Click(object sender, EventArgs e)
        {
            
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

        private void txtTakeScreenshotAndRecordAudioWithShareX_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void txtPasteImageAndAudioFromDitto_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void txtPasteMultipleImagesFromDitto_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void txtTakeScreenshot_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void txtToggleRecordAudio_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void radBeginning_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void radEnd_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void radNoScreenshot_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void txtActivateDitto_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void radSpaceBar_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void radLeftMouse_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void txtGeneralDelay_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void txtDelayForRecordingToStart_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void linkLblAutoHotkeyWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://www.autohotkey.com/docs/v2/Hotkeys.htm#Symbols");
            Process.Start(sInfo);
        }

        private void btnOpenTakeScreenshotAndRecordAudioWithShareX_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPasteScreenshotAndAudioWithDitto_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPasteMultipleScreenshotsWithDitto_Click(object sender, EventArgs e)
        {
            
        }

        private void openAllScripts()
        {
            startAutohotkeyScript("TakeScreenshotAndRecordAudioWithShareX.ahk");
            startAutohotkeyScript("PasteImageAndAudioWithDitto.ahk");
            startAutohotkeyScript("PasteMultipleImagesWithDitto.ahk");
        }

        private void btnOpenAllScripts_Click(object sender, EventArgs e)
        {
            openAllScripts();
        }

        private void btnCloseAllScripts_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C taskkill /im \"autohotkey.exe\"";
            process.StartInfo = startInfo;
            process.Start();
        }

        private void btnOpenCropMarginCalculatorForShareX_Click_1(object sender, EventArgs e)
        {
            // startAutohotkeyScript("CropMarginCalculatorForShareXRuler.ahk");
            Form form = new CropMarginCalculatorForShareX();
            form.ShowDialog();
            /*
            if (ResetToDefaultConfirmation.resetWasClicked)
            {
                load();
                save();
            }
            */
        }
    }
}
