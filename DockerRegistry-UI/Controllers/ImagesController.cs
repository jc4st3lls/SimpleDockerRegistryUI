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
    public class ImagesController : ControllerBase
    {
        IRepository _repository = null;
        public ImagesController(ILogger<ImagesController> logger, IRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Images
        [HttpGet(Name="Images")]
        public IList<Image> Get()
        {
            return _repository.Catalog().GetAwaiter().GetResult();
        }

        // GET: api/Images/5
        [HttpGet("{id}", Name = "GetImage")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Images
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
