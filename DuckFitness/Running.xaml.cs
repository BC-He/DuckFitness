using Javax.Security.Auth;
using System.Diagnostics;
using System.Timers;
namespace DuckFitness;

public partial class Running : ContentPage
{
    public double DistanceGoal { get; set; }
    public double TimeGoal { get; set; }
    public System.Timers.Timer timerMain;
    Stopwatch _timer;
    public Running()
	{
		InitializeComponent();
        TimeUsedLabel.IsVisible = false;
        DistanceGoal = 5.0;
        TimeGoal = 15.0;
        Dispatcher.Dispatch(() =>
        {
            DistanceGoalLabel.Text = $"Distance: {DistanceGoal:F1}km";
        });
        Dispatcher.Dispatch(() =>
        {
            TimeGoalLabel.Text = $"Time: {TimeGoal:F1} min";
        });
        BindingContext = this;
        timerMain = new System.Timers.Timer(1000); // 1 second interval
        timerMain.Elapsed += OnTimerElapsed;
        _timer = new Stopwatch();
    }
    private void OnDistanceStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        DistanceGoal = e.NewValue;
        Dispatcher.Dispatch(() =>
        {
            DistanceGoalLabel.Text = $"{DistanceGoal:F1}km";
        });
    }

    private void OnTimeStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        TimeGoal = e.NewValue;
        Dispatcher.Dispatch(() =>
        {
            TimeGoalLabel.Text = $"{TimeGoal:F1} min";
        });
    }

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
            TimeStepper.IsVisible = false;
            DistanceStepper.IsVisible = false;
            btnStart.IsVisible = false;
            DistanceGoalLabel.IsVisible = false;
            TimeGoalLabel.IsVisible = false;
            TimeUsedLabel.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Invalid Input", "Please set valid values for distance and time.", "OK");
        }
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        int min = (int)(_timer.Elapsed.TotalSeconds/60);
        int sec = (int)(_timer.Elapsed.TotalSeconds - min * 60.0);
        Dispatcher.Dispatch(() =>
        {
            TimeUsedLabel.Text = $"{min:D2} m {sec:D2} s";
        });
    }
}