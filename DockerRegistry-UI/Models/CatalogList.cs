using System;
using System.Collections.Generic;

namespace DockerRegistry_UI.Models
{
    public class CatalogList
    {
        public string Server { get; set; }
        public IList<Image> Images { get; set; }

    }
}
