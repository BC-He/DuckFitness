namespace DuckFitness;

public partial class History : ContentPage
{
	public History()
	{
		InitializeComponent();
        BindingContext = new HistoryViewModel();
    }
}