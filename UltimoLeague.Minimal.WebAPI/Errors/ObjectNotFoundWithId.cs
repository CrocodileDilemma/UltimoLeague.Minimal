namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public class ObjectNotFoundWithId<T> : BaseError
    {
        public ObjectNotFoundWithId(object id)
        {
            this.Message = $"{ typeof(T).Name } with Id {id} was not Found in the collection!";
        }
    }
}
