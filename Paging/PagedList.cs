namespace FogoCertApi.API.Paging
{
	public class PagedList<T> : List<T>
	{
		public int CurrentPage { get; private set; }
		public int PageSize { get; private set; }

		public PagedList(List<T> items, int pageNumber, int pageSize)
		{
			PageSize = pageSize > 0 ? pageSize : items.Count;
			CurrentPage = pageSize > 0 ? pageNumber : 1;
			AddRange(items);
		}
	}
}
