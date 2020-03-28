//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace FM.Portal.Core.Tools
//{
//    public class FileConverter
//    {
//        private HttpServerUtilityBase Server { get; }
//        private byte[] bytes;
//        private string filePath = string.Empty;
//        public byte[] ToByteArray(string fileName)
//        {
//            // Set Path  
//            filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/TemporaryFiles/"), fileName);

//            // Convert to Bytes
//            if (File.Exists(filePath))
//            {
//                bytes = System.IO.File.ReadAllBytes(filePath);
//            }
//            else
//            {
//                return bytes;
//            }

//            // Delete the file
//            //if (System.IO.File.Exists(filePath))
//            //{
//            //    System.IO.File.Delete(filePath);
//            //}

//            return bytes;
//        }
//        public string ToFile(byte[] bytes, string fileName)
//        {
//            fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + "-" +
//                    Guid.NewGuid().ToString() +
//                    fileName.Substring(fileName.LastIndexOf("."));

//            filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/TemporaryFiles/"), fileName);

//            File.WriteAllBytes(filePath, bytes);

//            return fileName;
//        }

//        public string Save(HttpRequestBase Request, HttpServerUtilityBase Server, string path = "~/TemporaryFiles/")
//        {
//            string fileName = string.Empty;
//            string filePath = string.Empty;

//            if (Request.Files.Count > 0)
//            {
//                if (!Request.Files[0].IsValidFile(Request))
//                    return  "فرمت فایل پشتیبانی نمی شود";
//                //throw new HttpException(401, "File type is not acceptable.");

//                // Checking for Internet Explorer  
//                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
//                {
//                    string[] testfiles = Request.Files[0].FileName.Split(new char[] { '\\' });
//                    fileName = testfiles[testfiles.Length - 1];
//                }
//                else
//                    fileName = Request.Files[0].FileName;

//                // Change file name to guid
//                string extension = Path.GetExtension(fileName);
//                fileName = Guid.NewGuid().ToString() + extension;

//                // Set Path 
//                filePath = Path.Combine(Server.MapPath(path), fileName);

//                // Save the file
//                Request.Files[0].SaveAs(filePath);
//            }

//            return fileName;
//        }
//    }
//}
