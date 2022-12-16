using System.Windows.Forms;

namespace Keyshift.Forms.Controls
{
    internal class ClearablePanel: Panel {
        public bool PaintBackground { get; set; } = true;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000020;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
    }
}
