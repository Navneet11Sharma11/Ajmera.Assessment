using System;
using HashidsNet;

namespace Ajmera.Assessment.API.Logging
{
    public static class IdEncoder
    {
        private static Hashids HashIds = new("Ajmera.Assessment.Logging.IdEncoder", 8);

        public static string EncodeId(int id)
        {
            return HashIds.Encode(id);
        }

        public static int DecodeId(string encodedId)
        {
            return HashIds.DecodeSingle(encodedId);
        }
    }
}

