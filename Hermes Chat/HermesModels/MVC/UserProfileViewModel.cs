using HermesModels.Base;
using HermesModels.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HermesModels.MVC
{
    public class UserProfileModel : IValidationObject
    {
        public long Id { get; set; }

        public long AspNetUserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string AboutMe { get; set; }

        public FileBase AccountImage { get; set; }

        public IFormFileCollection UploadedFiles { get; set; }
    }
}