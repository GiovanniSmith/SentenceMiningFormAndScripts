using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnkiAudioSentenceCardScript
{
    public partial class ResetToDefaultConfirmation : Form
    {
        public static Boolean resetWasClicked;

        public ResetToDefaultConfirmation()
        {
            InitializeComponent();
        }

        private void ResetToDefaultConfirmation_Load(object sender, EventArgs e)
        {
            resetWasClicked = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetWasClicked = true;
            Form1.updateFileWithText(Form1.filePathForHotkeyForTakeScreenshotAndRecordAudioWithShareX, "^+F1");
            Form1.updateFileWithText(Form1.filePathForHotkeyForPasteImageAndAudioWithClipboardSoftware, "^+F2");
            Form1.updateFileWithText(Form1.filePathForHotkeyForPasteMultipleImagesWithClipboardSoftware, "^+F3");
            Form1.updateFileWithText(Form1.filePathForWhenToTakeScreenshotWithShareX, "0");
            Form1.updateFileWithText(Form1.filePathForTakeScreenshot, Form1.send + "^!s");
            Form1.updateFileWithText(Form1.filePathForToggleRecordAudio, Form1.send + "^!r");
            Form1.updateFileWithText(Form1.filePathForActivateDitto, Form1.send + "^+!d");
            Form1.updateFileWithText(Form1.filePathForActivateWindowsClipboard, Form1.send + "#v");
            Form1.updateFileWithText(Form1.filePathForClipboardSoftware, "ditto");
            Form1.updateFileWithText(Form1.filePathForPlayPauseVideo, "MouseClick, left");
            Form1.updateFileWithText(Form1.filePathForIsRecordingAudio, "0");
            Form1.updateFileWithText(Form1.filePathForGeneralDelay, "250");
            Form1.updateFileWithText(Form1.filePathForDelayForRecordingToStart, "300");

            Form1.closeAllScripts();
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
