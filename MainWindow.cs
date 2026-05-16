namespace SimpleDonkeyManager
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            pnlButtons.Paint += PnlButtons_Paint;
            pnlConditionView.Paint += PnlConditionView_Paint;
        }

        private void PnlButtons_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(SystemColors.ControlDark, 1))
            {
                e.Graphics.DrawLine(pen, 0, pnlButtons.Height - 1, pnlButtons.Width, pnlButtons.Height - 1);
            }
        }

        private void PnlConditionView_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(SystemColors.ControlDark, 1))
            {
                e.Graphics.DrawLine(pen, 0, 0, pnlConditionView.Width, 0);
            }
        }
    }
}
