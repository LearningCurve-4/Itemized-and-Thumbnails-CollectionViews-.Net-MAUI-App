namespace ItemizedThumbnailsList.ViewModels;

public class ItemListViewModel : BaseViewModel
{
	public ItemListViewModel()
	{
		ScreenHeight -= 232;  //minus screen objects height before/after of the list
		GetDataCommand.Execute("itemdata.json");  //json file saved in Resources > Raw folder
	}

	bool isItemizedView = true;
	public bool IsItemizedView
	{
		get => isItemizedView;
		set
		{
			if (isItemizedView == value) { return; }
			isItemizedView = value;
			OnPropertyChanged();
			OnPropertyChanged(nameof(IsThumbnailView));
		}
	}
	public bool IsThumbnailView => !isItemizedView;

	public ObservableCollection<ItemModel>? ItemList { get; set; } = [];

	public Command GetDataCommand => new Command<string>(async (filename) =>
	{
		try
		{
			IsBusy = true;
			ItemList?.Clear();
			using Stream stream = await FileSystem.OpenAppPackageFileAsync(filename);
			using StreamReader reader = new(stream);
			string jsonResponse = await reader.ReadToEndAsync();

			if (!string.IsNullOrEmpty(jsonResponse))
			{
				ItemList = JsonSerializer.Deserialize<ObservableCollection<ItemModel>>(jsonResponse)!;
			}
			OnPropertyChanged(nameof(ItemList));
		}
		catch (Exception ex) { await Shell.Current.DisplayAlert("Error:", ex.Message, "OK"); }
		finally
		{
			IsBusy = false;
		}
	}, (filename) => IsNotBusy);

	public Command ItemDetailCommand => new Command<string>((str) =>
	{
		try
		{
			IsBusy = true;
			string[] substrings = str.Split(',');
			_ = new ItemDetailViewModel { ItemDetail = ItemList?.FirstOrDefault(i => i.ItemCode == substrings[1]) };
			GoToPageCommand.Execute(substrings[0]);
		}
		catch (Exception ex) { Shell.Current.DisplayAlert("Error:", ex.Message, "OK"); }
		finally
		{
			IsBusy = false;
		}
	}, (str) => IsNotBusy);

	public Command SwitchViewCommand => new Command<string>((str) =>
	{
		IsBusy = true;
		IsItemizedView = !IsItemizedView;
		IsBusy = false;
	}, (str) => IsNotBusy);
}
