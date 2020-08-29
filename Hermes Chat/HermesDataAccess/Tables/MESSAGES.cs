using System;

namespace HermesDataAccess.Table
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Readability")]
    public class MESSAGES
    {
        public int ID { get; set; }

        public string USERNAME { get; set; }

        public string TEXT { get; set; }

        public DateTime CREATIONTIME { get; set; }

        public int USERID { get; set; }
    }
}
