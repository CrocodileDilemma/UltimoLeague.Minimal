using MongoDB.Bson;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public static class GlobalValidators
    {
        public static bool BeValidObjectId(string id)
        {   
            return ObjectId.TryParse(id, out _);
        }

        public static bool BeValidObjectIdOrNull(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return true;
            }

            return ObjectId.TryParse(id, out _);
        }
    }
}
