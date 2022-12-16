using System;
using System.Windows.Forms;
using Keyshift.FormatHelpers;

namespace Keyshift.Forms.Windows
{
    public partial class LengthChangeFrm : Form {
        private float _fps;
        public int Length { get; private set; }

        public LengthChangeFrm(float fps, int current, bool wholeOnly)
        {
            InitializeComponent();
            _fps = fps;
            lbFps.Text = $"frames @ {fps} FPS";
            nudFrame.Value = current;

            lbNoFractional.Visible = wholeOnly;
        }

        private void nudFrame_ValueChanged(object sender, EventArgs e) {
            Length = (int)nudFrame.Value;
            lbTimecode.Text = $"Equivalent to: {Timecode.FramesToTimecode(Length)}";
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
