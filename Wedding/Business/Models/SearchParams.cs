namespace Business.Models
{
    public class SearchParams
    {
        public int? Start { get; set; }
        public int Count { get; set; }
        public string Filter { get; set; }
        public string CurrentSource { get; set; }
        public SearchParams(int? start, int count, string filter, string currentSource = "")
        {
            Start = start;
            Count = count;
            Filter = filter;
            CurrentSource = currentSource;
        }

        public int CurrentOffset {
            get {
                return ((Start != null ? Start.Value : 1) - 1) * Count;
            }
        }
    }
}
