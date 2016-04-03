using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractorsApp.Models
{
    public class ContractorRepository : BaseRepository
    {
        public ContractorRepository(string connectionString) : base(connectionString) { }

        // /api/Contractors
        public async Task<IEnumerable<Contractor>> GetContractors()
        {
            return await WithConnection(async c =>
            {
                var contractors = await c.QueryAsync<Contractor>("select id as id, name as name, inn as inn from dbo.contractors order by id ");
                return contractors;
            });
        }

        // /api/Contractor/5
        public async Task<IEnumerable<Contractor>> GetContractorById(string id)
        {
            return await WithConnection(async c =>
            {
                var contractor = await c.QueryAsync<Contractor>(
                           @"select id as id 
                                  , name as name
                                  , inn as inn
                                  , KPP
                                  , settlement_account
                                  , bank
                                  , city
                                  , corr_account
                                  , BIK
                                  , full_name
                             from dbo.contractors
                             where id = @p_id
                             order by id "
                    , new { p_id = id });
                return contractor;
            });
        }

        internal async Task UpdateContractors(List<Contractor> list)
        {
            await WithConnection(async c =>
            {
                return await c.ExecuteAsync(
                @"insert into dbo.contractors (NAME, INN, KPP, settlement_account, bank, city, corr_account, BIK, full_name)
                  values (@name, @inn, @KPP, @settlement_account, @bank, @city, @corr_account, @BIK, @full_name)"
                , list
                );
            });

        }
    }
}