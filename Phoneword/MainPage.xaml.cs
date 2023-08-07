namespace Phoneword;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	string translatedNumber;

	private void OnTranslate(object sender, EventArgs e)
	{
		string enteredNumber = PhonewordNumberText.Text;
		translatedNumber = Core.PhoneWordTranslator.toNumber(enteredNumber);

		if (!string.IsNullOrEmpty(translatedNumber))
		{
			callButton.IsEnabled = true;
			callButton.Text = "Call " + translatedNumber;
		}
		else
		{
			callButton.IsEnabled = false;
			callButton.Text = "Call";
		}
	}

    async void OnCall(object sender, EventArgs e)
    {
		if (await this.DisplayAlert(
			"Dial Number",
			"Would You like to call " + translatedNumber + "?",
			"Yes",
			"No"))
		{
            try
            {
                if (PhoneDialer.Default.IsSupported)
                    PhoneDialer.Default.Open(translatedNumber);
				else
                    await DisplayAlert("Phone dialer is not supported", "Can not open phone dialer.", "OK");
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
            }
            catch (Exception)
            {
                // Other error has occurred.
                await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
    }
}

