using System.Windows.Forms;

namespace EvidenceAppli
{
    public partial class ScreenForm : Form
    {
        public ScreenForm()
        {
            InitializeComponent();
            // ウィンドウいっぱいに表示
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            // ちらつき防止
            this.DoubleBuffered = true;
        }
    }
}