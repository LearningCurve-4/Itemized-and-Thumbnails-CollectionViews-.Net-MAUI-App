namespace ItemizedThumbnailsList.Views.AuxiliaryViews;

public partial class ItemizedListView : Grid
{
	public ItemizedListView()
	{
		InitializeComponent();
	}

	private readonly int scrollTopPosition = 0;

	private void List_Scrolled(object sender, ItemsViewScrolledEventArgs e)
	{
		if (e.VerticalDelta == scrollTopPosition || e.FirstVisibleItemIndex == scrollTopPosition)
		{
			ScrollTop.IsVisible = false;
		}
		else if (scrollTopPosition < e.VerticalDelta)
		{
			ScrollTop.IsVisible = true;
		}
	}

	private void ScrollTop_Clicked(object sender, EventArgs e)
	{
		itemlist.ScrollTo(index: scrollTopPosition, animate: false);
		ScrollTop.IsVisible = false;
	}
}