namespace DuckFitness
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Running), typeof(Running));
            Routing.RegisterRoute(nameof(WeightTraining), typeof(WeightTraining));
            Routing.RegisterRoute(nameof(History), typeof(History));
        }
    }
}
