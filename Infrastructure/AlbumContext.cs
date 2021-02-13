using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAlbum.Infrastructure
{
    public class AlbumContext:DbContext
    {
        public AlbumContext(DbContextOptions<AlbumContext>options):base(options)
        {

        }

        public object Album { get; internal set; }
    }
}
