using System;

namespace HermesDataAccess.Tables
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Readability")]
    public class MESSAGES
    {
        public long ID { get; set; }

        public string USERNAME { get; set; }

        public string TEXT { get; set; }

        public DateTime CREATION_TIME { get; set; }

        public long ASPNET_USER_ID { get; set; }
    }
}
