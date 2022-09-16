using MongoDB.Bson;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public static class GlobalValidators
    {
        public static bool BeValidObjectId(string id)
        {
            ObjectId result;
            if (ObjectId.TryParse(id, out result))
            {
                 return result != ObjectId.Empty;
            }

            return false;
        }

        public static bool BeValidObjectIdOrNull(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return true;
            }

            return BeValidObjectId(id);
        }
    }
}
