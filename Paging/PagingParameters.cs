namespace FogoCertApi.Paging
{
    public abstract class PagingParameters<TEntity> where TEntity : class
    {
        public static int MaxPageSize { get; protected set; } = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value < 0) ? 0 : (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
    }
}
