using HermesDataAccess.Enums;
using HermesDataAccess.Interfaces;
using HermesDataAccess.Tables;
using HermesModels.Base;
using HermesQueriesCommands.Commands;
using HermesShared.Mappers;
using System.IO;
using System.Linq;

namespace HermesLogic.Files
{
    /// <summary>
    /// File logic.
    /// </summary>
    public class FileLogic : IFileLogic
    {
        /// <summary>
        /// Databse connection.
        /// </summary>
        private readonly ISqlDb _sqlDb;

        /// <summary>
        /// File logic.
        /// </summary>
        public FileLogic(ISqlDb sqlDb)
        {
            _sqlDb = sqlDb;
        }

        /// <summary>
        /// Uploads file together with data in database.
        /// </summary>
        /// <param name="file">File to upload.</param>
        /// <returns></returns>
        public long UploadFile(FileBase file)
        {
            if (file == null || file.Data.Length == 0)
            {
                return 0;
            }

            if (file.FileId.HasValue && file.FileId != 1) // Hardcoded default image, not to overwrite... temp solution
            {
                _sqlDb.Command(new FILES_PHOTO_Commands(), CommandTypes.Update, file.ToFilePhoto());
                return file.FileId.Value;
            }
            else
            {
                return _sqlDb.Command(new FILES_PHOTO_Commands(), CommandTypes.Create, new FILES_PHOTO()
                {
                    FILENAME = file.Filename,
                    DATA = file.Data,
                    ASPNET_USER_ID = file.UserId,
                    IS_ACCOUNT_IMAGE = file.IsAccountImage,
                }).Id;
            }
        }

        /// <summary>
        /// Returns bytes for default image that is set every new chat user.
        /// </summary>
        /// <returns>Data of image as byte array.</returns>
        public byte[] GetDefaultAccountImageBytes()
        {
            return GetBytesFromFilePath(GetPathToDefaultAccountImage());
        }

        /// <summary>
        /// Returns bytes red from file by path.
        /// </summary>
        /// <param name="path">Path in which file content should be red.</param>
        /// <returns>Returns bytes red from file by path.</returns>
        private byte[] GetBytesFromFilePath(string path)
        {
            return System.IO.File.ReadAllBytes(path);
        }

        /// <summary>
        /// Returns path to default account profile picture in project.
        /// </summary>
        /// <returns></returns>
        private string GetPathToDefaultAccountImage()
        {
            var currentDirectory = this.GetDirectoryToSolution();
            string directoryWithModelProject = Path.Combine(currentDirectory, "HermesModels");
            string directoryWithFiles = Path.Combine(directoryWithModelProject, "Files");
            string fileName = "no-user-image-icon.jpg";
            return Path.Combine(directoryWithFiles, fileName);
        }

        /// <summary>
        /// Gets directory to working solution.
        /// </summary>
        /// <returns>Path to solution as string.</returns>
        private string GetDirectoryToSolution()
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory.Parent.FullName;
        }
    }
}
