using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAlbum.Models
{
    interface IAlbum
    {
        ImageClass GetImageClass(string image);
        ImageClass Delete(string image);
    }
}
