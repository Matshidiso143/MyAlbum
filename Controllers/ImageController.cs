using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyAlbum.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace MyAlbum.Controllers
{
    public class ImageController : Controller
    {

        private readonly IWebHostEnvironment _iweb;

        public ImageController(IWebHostEnvironment iweb)
        {
            _iweb = iweb;

        }

        public IActionResult Index()
        {
            ImageClass ic = new ImageClass();
            var displayImage = Path.Combine(_iweb.WebRootPath, "images");
            DirectoryInfo di = new DirectoryInfo(displayImage);
            FileInfo[] fileInfo = di.GetFiles();
            ic.FileImage = fileInfo;
            return View(ic);
        }
        [HttpPost]
        public async Task<ActionResult> Index(IFormFile imagefile)
        {
            string ext = Path.GetExtension(imagefile.FileName);
            if (ext == ".jpg")
            {
                var imagesave = Path.Combine(_iweb.WebRootPath, "images", imagefile.FileName);
                var stream = new FileStream(imagesave, FileMode.Create);
                await imagefile.CopyToAsync(stream);
                stream.Close();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string imagedel)

        {
            imagedel = Path.Combine(_iweb.WebRootPath, "images", imagedel);
            FileInfo fl = new FileInfo(imagedel);
            if (fl != null)
            {
                System.IO.File.Delete(imagedel);
                fl.Delete();
            }
            return RedirectToAction("Index");
        }
    }
}
