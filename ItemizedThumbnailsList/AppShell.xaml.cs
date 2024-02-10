namespace ItemizedThumbnailsList
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			InitializeRouting();
		}

		public void InitializeRouting()
		{
			Routing.RegisterRoute(nameof(ItemListPage), typeof(ItemListPage));
			Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
		}
	}
}
