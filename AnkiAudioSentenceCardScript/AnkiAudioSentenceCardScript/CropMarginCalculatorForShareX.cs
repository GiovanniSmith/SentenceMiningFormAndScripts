using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnkiAudioSentenceCardScript
{
    public partial class CropMarginCalculatorForShareX : Form
    {
        public CropMarginCalculatorForShareX()
        {
            InitializeComponent();
        }

        private void CropMarginCalculatorForShareX_Load(object sender, EventArgs e)
        {
            /*
             * 
                X: 416 | Y: 312 | Right: 1127 | Bottom: 647
                Width: 712 px | Height: 336 px | Area: 239232 px | Perimeter: 2096 px
                Distance: 785.97 px | Angle: 25.23°

                1920, 1080

                416,312,793,433
             */
            txtMonitorWidth.Text = 1920 + "";
            txtMonitorHeight.Text = 1080 + "";
        }

        private void btnCalculateValues_Click(object sender, EventArgs e)
        {
            string[] pastedValues = txtPastedText.Text.Split('|');
            string xValue = pastedValues[0].Substring(2).Replace(" ", "");
            string yValue = pastedValues[1].Substring(3).Replace(" ", "");
            int rightValue;
            int bottomValue;
            int.TryParse(pastedValues[2].Substring(8).Replace(" ", ""), out rightValue);
            int.TryParse(pastedValues[3].Substring(9).Replace(" ", ""), out bottomValue);

            int intMonitorWidth;
            int intMonitorHeight;
            int.TryParse(txtMonitorWidth.Text, out intMonitorWidth);
            int.TryParse(txtMonitorHeight.Text, out intMonitorHeight);

            txtValues.Text = xValue + ", " + yValue + ", " + (intMonitorWidth - rightValue) + ", " + (intMonitorHeight - bottomValue);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
