namespace Maui_DatabaseApp.View;

public partial class TransferPageView : ContentPage
{
	public TransferPageView(TransferPageVM transferPageVM)
	{
		InitializeComponent();
		BindingContext= transferPageVM;
	}
}