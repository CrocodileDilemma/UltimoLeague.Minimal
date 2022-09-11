namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public static class BaseErrors
    {
        public static string InvalidObjectId(string id)
        {
            return $"{id} is not a valid Id (value should contain only hexadecimal digits)!";
        }
        public static string ObjectExists<T>()
        {
            return $"{typeof(T).Name} with the specificed value(s) already Exists the collection!";
        }
        public static string ObjectNotFound<T>()
        {
            return $"{typeof(T).Name} with the specificed value(s) was not Found in the collection!";
        }
        public static string ObjectNotFoundWithId<T>(object id)
        {
            return $"{typeof(T).Name} with Id {id} was not Found in the collection!";
        }
        public static string ObjectsNotFound<T>()
        {
            return $"There were no {typeof(T).Name}s with the specificed value(s) Found in the collection!";
        }

        public static string OperationFailed(Exception ex)
        {
            return $"Operation failed with Exception: { (ex.InnerException == null ? ex.Message : ex.InnerException.Message) }";
        }
    }
}
