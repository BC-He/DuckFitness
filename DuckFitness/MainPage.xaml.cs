namespace DuckFitness
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnRunningButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Running());
        }

        private void OnWeightTrainingButtonClicked(object sender, EventArgs e)
        {

        }
        private void OnHistoryButtonClicked(object sender, EventArgs e)
        {

        }
    }

}
