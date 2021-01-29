using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerRegistry_UI.Models;
using DockerRegistry_UI.Models.Reposytories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DockerRegistry_UI.Controllers
{
    [Route("v2/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        IRepository _repository = null;
        public CatalogController(ILogger<CatalogController> logger, IRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Catalog
        [HttpGet(Name="Catalog")]
        public CatalogList Get()
        {
            return new CatalogList()
            {
                Server = AppSet.RegistryUrl.Split(@"//")[1],

                Images = _repository.Catalog().GetAwaiter().GetResult()
            };

        }

 

    }
}
