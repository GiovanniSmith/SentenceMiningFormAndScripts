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
            Form1.clearFileAndWrite(@"helper/hotkeyForTakeScreenshotAndRecordAudioWithShareX.txt", "^+F1");
            Form1.clearFileAndWrite(@"helper/hotkeyForPasteImageAndAudioFromDitto.txt", "^+F2");
            Form1.clearFileAndWrite(@"helper/hotkeyForPasteMultipleImagesFromDitto.txt", "^+F3");
            Form1.clearFileAndWrite(@"helper/whenToTakeScreenshotWithShareX.txt", "0");
            Form1.clearFileAndWrite(@"helper/TakeScreenshotWithShareX.ahk", Form1.send + "^!s");
            Form1.clearFileAndWrite(@"helper/ToggleRecordAudioWithShareX.ahk", Form1.send + "^!r");
            Form1.clearFileAndWrite(@"helper/ActivateDitto.ahk", Form1.send + "^+!d");
            Form1.clearFileAndWrite(@"helper/ActivateWindowsClipboard.ahk", Form1.send + "#v");
            Form1.clearFileAndWrite(@"helper/clipboardSoftware.txt", "ditto");
            Form1.clearFileAndWrite(@"helper/PlayPauseVideo.ahk", "MouseClick, left");
            Form1.clearFileAndWrite(@"helper/isRecordingAudio.txt", "0");
            Form1.clearFileAndWrite(@"helper/delayGeneral.txt", "250");
            Form1.clearFileAndWrite(@"helper/delayForRecordingToStart.txt", "300");

            Form1.closeAllScripts();
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
