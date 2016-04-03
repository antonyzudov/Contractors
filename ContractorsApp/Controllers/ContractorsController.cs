using ContractorsApp.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using System.Runtime.Caching;


namespace ContractorsApp.Controllers
{
    public class ContractorsController : ApiController
    {
        IEnumerable<Contractor> contractors = new Contractor[] { };
        public readonly string[] MasterCacheKeyArray = { "ContractorsCache" };
        private readonly string constring = ConfigurationManager.ConnectionStrings["ContractorsApp.Properties.Settings.ContractorsBase"].ConnectionString;

        // GET: api/Contractors
        public async Task<IEnumerable<Contractor>> GetAllContractors()
        {
            ContractorRepository rep = new ContractorRepository(constring);
            contractors = await rep.GetContractors();
            return contractors;
        }

        // GET: api/Contractors/5
        public async Task<IEnumerable<Contractor>> Get(int id)
        {
            string id_str = id.ToString();
            ContractorRepository rep = new ContractorRepository(constring);
            IEnumerable<Contractor> contractor = await rep.GetContractorById(id_str);
            return contractor;
        }

        // POST: api/Contractors
        public async void Post()
        {
            ContractorRepository rep = new ContractorRepository(constring);
            MemoryCache memoryCache = MemoryCache.Default;
            List<Contractor> list = memoryCache.Get(MasterCacheKeyArray[0]) as List<Contractor>;
            if (list != null)
            {
                await rep.UpdateContractors(list);
            }
            memoryCache.Remove(MasterCacheKeyArray[0]);
        }
    }
}
