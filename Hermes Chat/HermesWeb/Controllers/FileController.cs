using HermesLogic.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HermesWeb.Controllers
{
    /// <summary>
    /// Currently not used...
    /// </summary>
    [AllowAnonymous]
    public partial class FileController : HermesApplicationController
    {
        /// <summary>
        /// Logic for files.
        /// </summary>
        private readonly IFileLogic _fileLogic;

        /// <summary>
        /// Currently not used...
        /// </summary>
        public FileController(IFileLogic fileLogic)
        {
            _fileLogic = fileLogic;
        }

        [HttpPost]
        public virtual IActionResult UploadFile(IFormFile file)
        {
            // validation
            //_fileLogic.UploadFile(file);
            return Ok();
        }
    }
}