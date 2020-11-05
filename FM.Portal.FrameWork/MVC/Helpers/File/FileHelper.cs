using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace FM.Portal.FrameWork.MVC.Helpers.Files
{
    public static class FileHelper
    {
        public static bool ExistingFolder(string type)
        {
            try
            {
                string mainPath = HttpContext.Current.Server.MapPath("~/content/TemporaryFiles/");
                if (type != string.Empty || type != "")
                {
                    if (!Directory.Exists(mainPath + type))
                    {
                        Directory.CreateDirectory(mainPath + type);
                        return true;
                    }
                }
                else
                    return false;
                return true;
            }
            catch (Exception e) { return false; }
        }
        public static void DeleteFile(string path)
        {
            string filePath = HttpContext.Current.Server.MapPath($@"~/content/TemporaryFiles/{path}");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }
            else
            {
                //not hosted. For example, run in unit tests
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, path);
            }
        }
    }
}
