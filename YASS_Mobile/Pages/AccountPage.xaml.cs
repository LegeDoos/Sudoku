namespace YASS_Mobile.Pages;

public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Click the login button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void LoginBtn_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Login", $"Je klikte op login! Je gebruikersnaam is {UsernameEntry.Text}", "OK");
        await Navigation.PopAsync();
    }
}