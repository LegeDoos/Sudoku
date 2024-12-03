namespace NonAppShellPractice
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

        private async void OpenPageBtn_Clicked(object sender, EventArgs e)
        {
            var page = new ContentPageOne();
            await Navigation.PushAsync(page);

        }

        private async void OpenPageTwoBtn_Clicked(object sender, EventArgs e)
        {
            var page = new ContentPageTwo();
            await Navigation.PushAsync(page);
        }
    }

}
