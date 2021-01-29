using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerRegistry_UI.Models.Reposytories
{
    public interface IRepository
    {
        Task<IList<Image>> Catalog();
        Task<string> TaskList(string id);

        
    }
}
