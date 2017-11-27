namespace ControlIdX
{
    public partial class Principal : AssepontoRep.Principal
    {
        Bridge bridge;
        public Principal()
        {
            InitializeComponent();
            bridge = new Bridge(edLog);
            assignBridge(bridge);
        }

        private void Principal_Load(object sender, System.EventArgs e)
        {

        }
    }
}
