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
            Form1.updateFileWithText(@"helper/hotkeyForTakeScreenshotAndRecordAudioWithShareX.txt", "^+F1");
            Form1.updateFileWithText(@"helper/hotkeyForPasteImageAndAudioFromDitto.txt", "^+F2");
            Form1.updateFileWithText(@"helper/hotkeyForPasteMultipleImagesFromDitto.txt", "^+F3");
            Form1.updateFileWithText(@"helper/whenToTakeScreenshotWithShareX.txt", "0");
            Form1.updateFileWithText(@"helper/TakeScreenshotWithShareX.ahk", Form1.send + "^!s");
            Form1.updateFileWithText(@"helper/ToggleRecordAudioWithShareX.ahk", Form1.send + "^!r");
            Form1.updateFileWithText(@"helper/ActivateDitto.ahk", Form1.send + "^+!d");
            Form1.updateFileWithText(@"helper/ActivateWindowsClipboard.ahk", Form1.send + "#v");
            Form1.updateFileWithText(@"helper/clipboardSoftware.txt", "ditto");
            Form1.updateFileWithText(@"helper/PlayPauseVideo.ahk", "MouseClick, left");
            Form1.updateFileWithText(@"helper/isRecordingAudio.txt", "0");
            Form1.updateFileWithText(@"helper/delayGeneral.txt", "250");
            Form1.updateFileWithText(@"helper/delayForRecordingToStart.txt", "300");

            Form1.closeAllScripts();
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
