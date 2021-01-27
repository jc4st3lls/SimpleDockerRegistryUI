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
                    httpclient.DefaultRequestHeaders.Add("Authorization", _credentials);
                    var response = await httpclient.GetAsync(path);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ret = await response.Content.ReadAsStringAsync();
                    }

                }
            }

            return ret;
        }

        public async Task<string> RemoveImage(string id, string tag)
        {
            //DELETE /v2/<name>/manifests/<reference>
            // Per agafar el digest cal aquesta capçalera
            // Accept: application/vnd.docker.distribution.manifest.v2+json
            // Ex: curl -v --silent -H "Accept: application/vnd.docker.distribution.manifest.v2+json" -X GET http://localhost:5000/v2/<name>/manifests/<tag> 2>&1 | grep Docker-Content-Digest | awk '{print ($3)}'
            // Response sha256:6de813fb93debd551ea6781e90b02f1f93efab9d882a6cd06bbd96a07188b073
            // curl -v --silent -H "Accept: application/vnd.docker.distribution.manifest.v2+json" -X DELETE http://127.0.0.1:5000/v2/<name>/manifests/sha256:6de813fb93debd551ea6781e90b02f1f93efab9d882a6cd06bbd96a07188b073

            string ret = string.Empty;
            string path = $"v2/{id}/manifests/{tag}";
            

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
                    httpclient.DefaultRequestHeaders.Add("Authorization", _credentials);
                    httpclient.DefaultRequestHeaders.Add("Accept", "application/vnd.docker.distribution.manifest.v2+json");
                    string manifestid = string.Empty;
                    var response = await httpclient.GetAsync(path);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string manifest= await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(manifest))
                        {
                            JObject jobj = JObject.Parse(manifest);

                            manifestid = (string)jobj.SelectToken("config.digest");
                            if (!string.IsNullOrEmpty(manifestid))
                            {
                                string path2 = $"v2/{id}/manifests/{manifestid}";
                                var deresponse = await httpclient.DeleteAsync(path2);
                                if (deresponse.StatusCode== System.Net.HttpStatusCode.OK)
                                {
                                    ret = "OK";
                                }


                            }
                        }
                    
                    
                    }

                }
            }


            return ret;

        }
    }
}
