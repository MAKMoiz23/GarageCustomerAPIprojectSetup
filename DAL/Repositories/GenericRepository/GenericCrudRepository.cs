using Dapper;
using System.Data;
using System.Data.SqlClient;
using DAL.Repositories.IRepository;
using static Dapper.SqlMapper;

namespace DAL.Repositories.Repository
{
    public class GenericCrudRepository : IGenericCrudRepository
    {
        protected string? _connectionString;
        public async Task<IEnumerable<T>> LoadData<T, U>(
            string SP,
            U parameters)
        {
            using IDbConnection con = new SqlConnection(_connectionString);
            if (con.State != ConnectionState.Open) con.Open();

            return await con.QueryAsync<T>(SP,
                                            parameters,
                                            commandType: CommandType.StoredProcedure);
        } // GENERIC GET ALL

        public async Task<List<List<dynamic>>> LoadMultipleData<U>(
            string SP,
            U parameters)
        {
            List<List<dynamic>> resultSets = new();
            using IDbConnection con = new SqlConnection(_connectionString);
            if (con.State != ConnectionState.Open) con.Open();

            var res = await con.QueryMultipleAsync(SP,
                                                    parameters,
                                                    commandType: CommandType.StoredProcedure);

            while (!res.IsConsumed)
            {
                var resultSet = (await res.ReadAsync()).ToList();
                resultSets.Add(resultSet);
            }

            return resultSets;
        } // GENERIC GET MULTIPLE QUERY

        public async Task<T> LoadSingleOrDefaultData<T, U>(
            string SP,
            U parameters)
        {
            using IDbConnection con = new SqlConnection(_connectionString);
            if (con.State != ConnectionState.Open) con.Open();

            return await con.QueryFirstOrDefaultAsync<T>(SP,
                                                        parameters,
                                                        commandType: CommandType.StoredProcedure);

        } // GENERIC GET SINGLE OR DEFAULT

        public async Task SaveData<T>(
            string SP,
            T parameters)
        {
            using IDbConnection con = new SqlConnection(_connectionString);
            if (con.State != ConnectionState.Open) con.Open();

            var trans = con.BeginTransaction();
            try
            {
                await con.ExecuteAsync(SP,
                                       parameters,
                                       commandType: CommandType.StoredProcedure,
                                       transaction: trans);
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
            }
        } // GENERIC SAVE UPDATE AND DELETE 

        public async Task<S?> SaveSingleQueryable<S, T>(
            string SP,
            T parameters)
        {
            using IDbConnection con = new SqlConnection(_connectionString);
            if (con.State != ConnectionState.Open) con.Open();

            var trans = con.BeginTransaction();
            S? res = default;
            try
            {
                res = await con.QuerySingleOrDefaultAsync<S>(SP,
                                                             parameters,
                                                             commandType: CommandType.StoredProcedure,
                                                             transaction: trans);
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
            }
            return res;
        } // GENERIC SAVE UPDATE AND DELETE SINGLE QUERYABLE 
        public async Task<IEnumerable<S>> SaveQueryable<S, T>(
            string SP,
            T parameters)
        {
            using IDbConnection con = new SqlConnection(_connectionString);
            if (con.State != ConnectionState.Open) con.Open();

            var trans = con.BeginTransaction();
            IEnumerable<S> res = Enumerable.Empty<S>();
            try
            {
                res = await con.QueryAsync<S>(SP,
                                                parameters,
                                                commandType: CommandType.StoredProcedure,
                                                transaction: trans);
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
            }
            return res;
        } // GENERIC SAVE UPDATE AND DELETE QUERYABLE 
    }
}
