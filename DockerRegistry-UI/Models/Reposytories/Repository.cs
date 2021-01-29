using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using System.Threading.Tasks;

namespace DockerRegistry_UI.Models.Reposytories
{
    public class Repository : IRepository
    {
        private string _registryurl = string.Empty;
        private string _credentials = string.Empty;
        public Repository(string registryUrl, string credentials)
        {
            _registryurl = registryUrl;
            _credentials = credentials;
        }
        //public async Task<string> Catalog()
        //{
        //    return await _GET("v2/_catalog");
        //}

        public async Task<string> TaskList(string id)
        {
            return await _GET($"v2/{id}/tags/list");
        }

        public async Task<IList<Image>> Catalog()
        {
            IList<Image> images = null;
            var _catalog= await _GET("v2/_catalog");
            if (!string.IsNullOrEmpty(_catalog))
            {
                Catalog catalog = JsonConvert.DeserializeObject<Catalog>(_catalog);
                if (catalog?.repositories.Length > 0)
                {
                    images = new List<Image>();
                    foreach(var ima in catalog.repositories)
                    {
                        var tags = await _GET($"v2/{ima}/tags/list");

                        if (!string.IsNullOrEmpty(tags))
                        {
                            Image image = JsonConvert.DeserializeObject<Image>(tags);
                            images.Add(image);
                        }

                    }
                }

            }
            return images;
        }

        private async Task<string> _GET(string path)
        {
            string ret = string.Empty;

            using (var handler = new HttpClientHandler())
            {
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };

                using (var httpclient = new HttpClient(handler))
                {
                    httpclient.BaseAddress = new Uri(_registryurl);
                    if (!string.IsNullOrEmpty(AppSet.RegistryCredentials))
                    {
                        httpclient.DefaultRequestHeaders.Add("Authorization", _credentials);
                    }
                   
                    var response = await httpclient.GetAsync(path);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ret = await response.Content.ReadAsStringAsync();
                    }

                }
            }

            return ret;
        }

        
    }
}
