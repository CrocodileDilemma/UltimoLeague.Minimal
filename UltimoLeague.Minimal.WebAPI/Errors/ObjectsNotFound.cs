namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public class ObjectsNotFound<T> : BaseError
    {
        public ObjectsNotFound()
        {
            this.Message = $"There were no { typeof(T).Name}s with the specificed value(s) Found in the collection!";
        }
    }
}
