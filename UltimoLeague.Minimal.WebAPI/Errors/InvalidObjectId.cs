namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public class InvalidObjectId : BaseError
    {
        public InvalidObjectId(string id)
        {
            this.Message = $"{ id } is not a valid Id (value should contain only hexadecimal digits)!";
        }
    }
}
