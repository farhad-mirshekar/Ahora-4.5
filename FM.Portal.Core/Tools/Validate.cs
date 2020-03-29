
using System.IO;
using System.Web;

namespace FM.Portal.Core.Tools
{
    public static class Validate
    {
        public static bool IsValidFile(this HttpPostedFileBase file, HttpRequestBase Request, string test = "")
        {
            bool isValidType = false;
            bool isValidExtension = false;
            bool isDangerous = false;
            string fileName = string.Empty;
            string fileExtension = string.Empty;
            string[] validTypes = {
                            "application/vnd.ms-excel"
                            , "application/msexcel"
                            , "application/x-msexcel"
                            , "application/x-ms-excel"
                            , "application/x-excel"
                            , "application/x-dos_ms_excel"
                            , "application/xls"
                            , "application/x-xls"
                            , "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"

                            , "application/msword"
                            , "application/vnd.openxmlformats-officedocument.wordprocessingml.document"

                            , "application/pdf"

                            , "application/zip"
                            , "application/x-zip-compressed"
                            , "application/x-7z-compressed"
                            , "application/x-rar-compressed"

                            , "image/jpeg"
                            , "image/x-citrix-jpeg"
                            , "image/png"
                            , "image/x-citrix-png"
                            , "image/x-png"
                            , "image/tiff"
                            , "image/gif"
                            , "image/bmp"
                            , "image/svg+xml"
                            ,"video/mp4"
                        };
            string[] validExtensions = {
                ".xls"
                , ".xlsx"
                , ".doc"
                , ".docx"
                , ".pdf"
                , ".zip"
                , ".rar"
                , ".7z"
                , ".jpeg"
                , ".jpg"
                , ".png"
                , ".tiff"
                , ".gif"
                , ".bmp"
                , ".svg"
                , ".mp4"
            };

            // check content type
            foreach (string validType in validTypes)
            {
                if (validType == file.ContentType)
                {
                    isValidType = true;
                    break;
                }
            }

            // check extension
            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
            {
                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                fileName = testfiles[testfiles.Length - 1];
            }
            else
                fileName = file.FileName;

            fileExtension = Path.GetExtension(fileName);

            foreach (string validExtension in validExtensions)
            {
                if (validExtension == fileExtension.ToLower())
                {
                    isValidExtension = true;
                    break;
                }
            }

            return isValidType && isValidExtension && !isDangerous;
        }
    }
}
