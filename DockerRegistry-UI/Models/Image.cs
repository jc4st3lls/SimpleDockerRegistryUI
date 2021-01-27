using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerRegistry_UI.Models
{
    public class Image
    {
        public string Name { get; set; }
        public string[] Tags { get; set; }
    }
}
