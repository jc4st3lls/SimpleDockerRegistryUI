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
        public IList<Image> Get()
        {
            return _repository.Catalog().GetAwaiter().GetResult();
        }

        // GET: api/Catalog/5
        [HttpGet("{id}/tags/list", Name = "GetCatalog")]
        public string Get(string id)
        {
            return _repository.TaskList(id).GetAwaiter().GetResult();
        }

        // POST: api/Catalog
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Catalog/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}/{tag}")]
        public ActionResult<string> Delete(string id,string tag)
        {
            string res=_repository.RemoveImage(id, tag).GetAwaiter().GetResult();

            return Ok(res);


        }
    }
}
