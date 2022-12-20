namespace UltimoLeague.Minimal.Blazor.Shared
{
    public class DtoBase
    {
        public string Id { get; set; }
        public string Display { get; set; }
        public string MasterId { get; set; }

        public DtoBase(string id, string display, string masterId = "")
        {
            Id = id;
            Display = display;
            MasterId = masterId;
        }

        public override bool Equals(object o)
        {
            var compare = o as DtoBase;
            return compare?.Id == Id;
        }
        public override int GetHashCode() => Id?.GetHashCode() ?? 0;

        public override string ToString() => Display;
    }
}
