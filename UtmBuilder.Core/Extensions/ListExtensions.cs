namespace UtmBuilder.Core.Extensions
{
	public static class ListExtensions
	{
		public static void AddIfNotNull<TItem>(this IList<TItem> list, TItem item)
		{
			if (item is not null)
				list.Add(item);
		}
	}
}