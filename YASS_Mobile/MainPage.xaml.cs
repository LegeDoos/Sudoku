using YASS_Mobile.Pages;

namespace YASS_Mobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        /// <summary>
        /// Open the AccountPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AccountBtn_Clicked(object sender, EventArgs e)
        {
            // create the page
            var accountPage = new AccountPage();
            // open the page
            await Navigation.PushAsync(accountPage);
        }
    }

}
