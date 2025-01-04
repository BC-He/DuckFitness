using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DuckFitness;

namespace DuckFitness
{
    public class _FitnessRecord : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Activity { get; set; } // "Running" or "Weight Training"
        public double? Distance { get; set; } // Distance for running
        public string WeightTrainingType { get; set; } // Type of weight training

        public bool IsRunning => Activity == "Running";
        public bool IsWeightTraining => Activity == "Weight Training";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class HistoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FitnessRecord> FitnessRecords { get; set; }
        public FitnessRecord SelectedRecord { get; set; }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public HistoryViewModel()
        {
            FitnessRecords = new ObservableCollection<FitnessRecord>();
            LoadRecords();

            EditCommand = new Command<FitnessRecord>(OnEdit);
            DeleteCommand = new Command<FitnessRecord>(OnDelete);
        }

        private async void LoadRecords()
        {
            var records = await App.Database.GetRecordsAsync();
            foreach (var record in records)
            {
                FitnessRecords.Add(record);
            }
        }

        private void OnEdit(FitnessRecord record)
        {
            // Handle edit logic here
            // For example, navigate to a detail page for editing
        }

        private async void OnDelete(FitnessRecord record)
        {
            await App.Database.DeleteRecordAsync(record);
            FitnessRecords.Remove(record);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

