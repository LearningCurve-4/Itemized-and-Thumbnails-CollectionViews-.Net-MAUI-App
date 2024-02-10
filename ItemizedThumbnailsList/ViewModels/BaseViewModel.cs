namespace ItemizedThumbnailsList.ViewModels;

public class BaseViewModel : NotifyPropertyChanged
{
	bool isBusy = false;
	public bool IsBusy
	{
		get => isBusy;
		set
		{
			if (isBusy == value) { return; }
			isBusy = value;
			OnPropertyChanged();
			OnPropertyChanged(nameof(IsNotBusy));
		}
	}
	public bool IsNotBusy => !IsBusy;

	double screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
	public double ScreenHeight
	{
		get => screenHeight;
		set
		{
			if (screenHeight == value) { return; }
			screenHeight = value;
			OnPropertyChanged();
		}
	}

	double screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
	public double ScreenWidth
	{
		get => screenWidth;
		set
		{
			if (screenWidth == value) { return; }
			screenWidth = value;
			OnPropertyChanged();
		}
	}

	public static string CurrentPage { get; set; } = string.Empty;
	public Command GoToPageCommand => new Command<string>(async (page) =>
	{
		try
		{
			IsBusy = true;
			if (CurrentPage != page)
			{
				var pageType = Type.GetType(GlobalVariables.pageFolder + page);
				if (pageType != null)
				{
					await Shell.Current.GoToAsync(page, true);
					CurrentPage = page;
				}
				else
				{
					await Shell.Current.DisplayAlert("Error:", $"{page} - Not available", "OK");
				}
			}
		}
		catch (Exception ex) { await Shell.Current.DisplayAlert("Error:", ex.Message, "OK"); }
		finally
		{
			IsBusy = false;
		}
	}, (page) => IsNotBusy);

	public Command GoBackToPageCommand => new Command<string>(async (page) =>
	{
		try
		{
			IsBusy = true;
			await Shell.Current.GoToAsync(page, true);
			CurrentPage = string.Empty;
		}
		catch (Exception ex) { await Shell.Current.DisplayAlert("Error:", ex.Message, "OK"); }
		finally
		{
			IsBusy = false;
		}
	}, (page) => IsNotBusy);
}
