using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAlbum.Infrastructure
{
    interface IAlbum
    {
        
        void Upload(Album album);
        void Delete(Album album);
        void Share(Album album);

    }
}
