namespace ItemizedThumbnailsList.ViewModels;

public class ItemDetailViewModel : BaseViewModel
{
	static ItemModel? itemDetail = null;
	public ItemModel? ItemDetail
	{
		get => itemDetail;
		set
		{
			if (itemDetail == value) { return; }
			itemDetail = value;
			OnPropertyChanged();
		}
	}
}
