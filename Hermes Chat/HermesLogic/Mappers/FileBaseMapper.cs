using HermesDataAccess.Tables;
using HermesModels.Base;
using HermesShared.Extensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HermesShared.Mappers
{
    public static class FileBaseMapper
    {
         public static FileBase ToFileBase(this IFormFile formFile)
         {
            return new FileBase()
            {
                Filename = formFile.FileName,
                Data = formFile.GetBytes(),
            };
         }

        public static FileBase ToFileBase(this FILES_PHOTO dbo)
        {
            if (dbo is null)
            {
                return new FileBase();
            }

            return new FileBase()
            {
                Data = dbo.DATA,
                FileId = dbo.ID,
                Filename = dbo.FILENAME,
                UserId = dbo.ASPNET_USER_ID,
                IsAccountImage = dbo.IS_ACCOUNT_IMAGE,
            };
        }

        public static List<FileBase> ToFileBase(this IList<FILES_PHOTO> dbo)
        {
            return dbo.ToGenericDtoList(ToFileBase);
        }

        public static FILES_PHOTO ToFilePhoto(this FileBase dto)
        {
            return new FILES_PHOTO()
            {
                DATA = dto.Data,
                ID = dto.FileId.Value,
                FILENAME = dto.Filename,
                ASPNET_USER_ID = dto.UserId,
                IS_ACCOUNT_IMAGE = dto.IsAccountImage,
            };
        }

        public static List<FILES_PHOTO> ToFilePhotoList(this IList<FileBase> dto)
        {
            return dto.ToGenericDtoList(ToFilePhoto);
        }
    }
}
