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
                  select @name, @inn, @KPP, @settlement_account, @bank, @city, @corr_account, @BIK, @full_name
                  where not exists (
                    select 1
                    from dbo.Contractors c1
                    where c1.name like @name
                      and ((c1.inn like @inn) or ((c1.inn is null) and (@inn is null)))
                      and ((c1.kpp = @kpp) or ((c1.kpp is null) and (@kpp is null)))
                      and ((c1.settlement_account = @settlement_account) or ((c1.settlement_account is null) and (@settlement_account is null)))
                      and ((c1.bank = @bank) or ((c1.bank is null) and (@bank is null)))
                      and ((c1.city = @city) or ((c1.city is null) and (@city is null)))
                      and ((c1.corr_account = @corr_account) or ((c1.corr_account is null) and (@corr_account is null)))
                      and ((c1.bik = @bik) or ((c1.bik is null) and (@bik is null)))
                      and ((c1.full_name = @full_name) or ((c1.full_name is null) and (@full_name is null)))
                    ) "
                , list
                );
            });

        }
    }
}