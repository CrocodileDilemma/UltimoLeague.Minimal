namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public class ObjectExists<T> : BaseError
    {
        public ObjectExists()
        {
            this.Message = $"{typeof(T).Name} with the specificed value(s) already Exists the collection!";
        }
    }
}
