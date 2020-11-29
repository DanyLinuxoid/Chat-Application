using System;

namespace HermesDataAccess.Tables
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Readability")]
    public class ASPNET_USER
    {
        public long ID { get; set; }

        public DateTime MODIFICATION_TIME { get; set; }

        public DateTimeOffset? LOCKOUT_END { get; set; }

        public bool IS_LOCKOUT_ENABLED { get; set; }

        public int ACCESS_FAILED_COUNT { get; set; }

        public DateTime CREATION_TIME { get; set; }
    }
}