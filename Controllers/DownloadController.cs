using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace MyAlbum.Controllers
{
    public class DownloadController : Controller

    {

        private readonly IWebHostEnvironment _iweb;


        public DownloadController(IWebHostEnvironment iweb)
        {
            _iweb = iweb;

        }
        public IActionResult Index()
        {
            return View();
        }

        public FileResult GenerateAndDownloadZip()
        {
            var webRoot = _iweb.WebRootPath;
            var filename = "MyZip.zip";
            var tempOutput = webRoot + "/images/" + filename;

            using (ZipOutputStream oZipOutputStream = new ZipOutputStream(System.IO.File.Create(tempOutput)))
            {
                oZipOutputStream.SetLevel(9);

                byte[] buffer = new byte[4096];

                var ImageList = new List<string>();

                ImageList.Add(webRoot + "/images/Tower.jpg");
                ImageList.Add(webRoot + "/images/Sky.jpg");
                ImageList.Add(webRoot + "/images/Nature.jpg");

                for (int i =0; i<ImageList.Count;i++)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(ImageList[i]));
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    oZipOutputStream.PutNextEntry(entry);

                    using(FileStream oFileStream = System.IO.File.OpenRead(ImageList[i]))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = oFileStream.Read(buffer, 0, buffer.Length);
                            oZipOutputStream.Write(buffer, 0, sourceBytes);

                        } while (sourceBytes > 0);
                    }
                }
                oZipOutputStream.Finish();
                oZipOutputStream.Flush();
                oZipOutputStream.Close();
            }
            byte[] finalResult = System.IO.File.ReadAllBytes(tempOutput);
            if(System.IO.File.Exists(tempOutput))
            {
                System.IO.File.Delete(tempOutput);
            }
        if(finalResult == null || !finalResult.Any())
            {
                throw new Exception(String.Format("Nothing Found"));
            }
            return File(finalResult,"application/zip",filename);
        }
    }
}
