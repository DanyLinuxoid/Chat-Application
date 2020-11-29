using System;

namespace HermesModels.Base
{
    public class FileBase 
    {
        public long? FileId { get; set; }

        public string Filename { get; set; }

        public byte[] Data { get; set; }

        public long UserId { get; set; }

        public bool IsAccountImage { get; set; }

        public bool IsDefaultImage { get; set; }
    }
}