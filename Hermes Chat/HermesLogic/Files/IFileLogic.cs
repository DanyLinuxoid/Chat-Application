using HermesModels.Base;

namespace HermesLogic.Files
{
    /// <summary>
    /// File logic.
    /// </summary>
    public interface IFileLogic
    {
        /// <summary>
        /// Uploads file together with data in database.
        /// </summary>
        /// <param name="file">File to upload.</param>
        /// <returns></returns>
        long UploadFile(FileBase file);

        /// <summary>
        /// Returns bytes for default image that is set every new chat user.
        /// </summary>
        /// <returns>Data of image as byte array.</returns>
        byte[] GetDefaultAccountImageBytes();
    }
}