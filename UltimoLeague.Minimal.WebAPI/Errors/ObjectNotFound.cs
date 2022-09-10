namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public class ObjectNotFound<T> : BaseError
    {   
        public ObjectNotFound()
        {
            this.Message = $"{ typeof(T).Name } with the specificed value(s) was not Found in the collection!";
        }   
    }
}
