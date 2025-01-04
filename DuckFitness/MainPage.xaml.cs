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

        private async void OnWeightTrainingButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WeightTraining());
        }
        private async void OnHistoryButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new History());
        }
    }

}
