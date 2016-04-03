using ContractorsApp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ContractorsApp.Controllers
{
    public class ClContractorsController : ApiController
    {
        public readonly string[] MasterCacheKeyArray = { "ContractorsCache" };

        // GET: api/ClContractors
        public IEnumerable<Contractor> Get()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            List<Contractor> list = memoryCache.Get(MasterCacheKeyArray[0]) as List<Contractor>;
            return list;
        }

        // GET: api/ClContractors/5
        public async Task<IEnumerable<Contractor>> Get(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            List<Contractor> contractors = memoryCache.Get(MasterCacheKeyArray[0]) as List<Contractor>;
            List<Contractor> result = new List<Contractor>();
            result.Add(contractors.Find(x => x.id == id));
            return result;
        }

        // POST: api/ClContractors
        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Remove(MasterCacheKeyArray[0]);
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (MultipartFileData file in provider.FileData)
                {
                    var filePath = HttpContext.Current.Server.MapPath("~/file.txt");
                    filePath = file.LocalFileName;
                    var docfiles = new List<string>();
                    docfiles.Add(filePath);
                    ClParser parser = new ClParser(filePath);
                    List<Contractor> contractors = parser.GetContractors();
                    memoryCache.Add(MasterCacheKeyArray[0], contractors, DateTimeOffset.UtcNow.AddHours(1));
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


    }
}
