using System.Diagnostics;
using System.Timers;

namespace DuckFitness;

public partial class Running : ContentPage
{
    public double DistanceGoal { get; set; }
    public double TimeGoal { get; set; }
    public double VeloGoal { get; set; }
    public double dTime_Plan { get; set; }
    public double DistanceNow { get; set; }
    public System.Timers.Timer timerMain;
    Stopwatch _timer;

    public Running()
    {
        InitializeComponent();
        TimeUsedLabel.IsVisible = false;
        btnEndTraining.IsVisible = false;
        DistanceGoal = 5.0;
        TimeGoal = 15.0;

        Dispatcher.Dispatch(() =>
        {
            DistanceGoalLabel.Text = $"Distance: {DistanceGoal:F1} km";
        });
        Dispatcher.Dispatch(() =>
        {
            TimeGoalLabel.Text = $"Time: {TimeGoal:F0} min";
        });

        BindingContext = this;

        timerMain = new System.Timers.Timer(1000); // 1 second interval
        timerMain.Elapsed += OnTimerElapsed;
        _timer = new Stopwatch();
    }

    // Distance Change Handlers
    private void OnDistanceIncreased(object sender, EventArgs e)
    {
        DistanceGoal += 0.5; // Increment by 0.5 km
        UpdateDistanceLabel();
    }

    private void OnDistanceDecreased(object sender, EventArgs e)
    {
        if (DistanceGoal > 0) // Prevent going below 0
        {
            DistanceGoal -= 0.5; // Decrement by 0.5 km
            UpdateDistanceLabel();
        }
    }

    private void UpdateDistanceLabel()
    {
        Dispatcher.Dispatch(() =>
        {
            DistanceGoalLabel.Text = $"Distance: {DistanceGoal:F1} km";
        });
    }

    // Time Change Handlers
    private void OnTimeIncreased(object sender, EventArgs e)
    {
        TimeGoal += 1; // Increment by 1 minute
        UpdateTimeLabel();
    }

    private void OnTimeDecreased(object sender, EventArgs e)
    {
        if (TimeGoal > 0) // Prevent going below 0
        {
            TimeGoal -= 1; // Decrement by 1 minute
            UpdateTimeLabel();
        }
    }

    private void UpdateTimeLabel()
    {
        Dispatcher.Dispatch(() =>
        {
            TimeGoalLabel.Text = $"Time: {TimeGoal:F0} min";
        });
    }

    // Start Training Handler
    private async void OnStartButtonClicked(object sender, EventArgs e)
    {
        if (DistanceGoal > 0 && TimeGoal > 0)
        {
            // Start the training with the specified distance and time
            await DisplayAlert("Training Started", $"Distance: {DistanceGoal:F1} km\nTime: {TimeGoal:F0} minutes", "OK");
            timerMain.Start();
            _timer.Start();

            Dispatcher.Dispatch(() =>
            {
                lbGoal.Text = $"Goal: run {DistanceGoal:F2} km in {TimeGoal:F1} min";
            });

            VeloGoal = DistanceGoal / TimeGoal / 60.0; // Speed goal in km/min
            //TimeStepper.IsVisible = false;
            //DistanceStepper.IsVisible = false;
            btnStart.IsVisible = false;
            DistanceGoalLabel.IsVisible = false;
            TimeGoalLabel.IsVisible = false;
            TimeUsedLabel.IsVisible = true;
            btnEndTraining.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Invalid Input", "Please set valid values for distance and time.", "OK");
        }
    }

    // Timer Elapsed Event
    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        int min = (int)(_timer.Elapsed.TotalSeconds / 60);
        int sec = (int)(_timer.Elapsed.TotalSeconds - min * 60.0);
        Random r = new Random();
        int rInt = r.Next(-60, 60);

        // Calculate distance covered and time plan difference
        DistanceNow = VeloGoal * (_timer.Elapsed.TotalSeconds + rInt);
        dTime_Plan = _timer.Elapsed.TotalSeconds - DistanceNow / VeloGoal;

        // Update UI based on the difference in time
        if (dTime_Plan < 0)
        {
            Dispatcher.Dispatch(() =>
            {
                TimeUsedLabel.Text = $"{min:D2} m {sec:D2} s";
                CurrentDistanceLabel.Text = $"Distance: {DistanceNow:F2} km";
                PlanNoticeLabel.Text = $"{-dTime_Plan:F1}s faster";
            });
        }
        else
        {
            Dispatcher.Dispatch(() =>
            {
                TimeUsedLabel.Text = $"{min:D2} m {sec:D2} s";
                CurrentDistanceLabel.Text = $"Distance: {DistanceNow:F2} km";
                PlanNoticeLabel.Text = $"{dTime_Plan:F1}s slower";
            });
        }
    }

    // End Training Handler
    private async void EndTraining(object sender, EventArgs e)
    {
        _timer.Stop();
        timerMain.Stop();
        await Navigation.PushAsync(new MainPage());
    }
}
