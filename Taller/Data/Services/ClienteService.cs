using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Taller.Data.Model;

namespace Taller.Data.Services
{
    public class ClienteService : IClienteService
    {

        private readonly SqlConnectionConfiguration _configuration;
        public ClienteService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        //INSERT
        public async Task ClienteInsert(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdCliente", cliente.IdCliente, DbType.Int32);
                parameters.Add("NombreCliente", cliente.NombreCliente, DbType.String);
                parameters.Add("ApellidoCliente", cliente.ApellidoCliente, DbType.String);
                parameters.Add("EmailCliente", cliente.EmailCliente, DbType.String);

                const string query = @"INSERT INTO dbo.Taller (IdCliente, NombreCliente, ApellidoCliente, EmailCliente) VALUES (@IdCliente, @NombreCliente, @ApellidoCliente, @EmailCliente)";
                await conn.ExecuteAsync(query, new { cliente.IdCliente, cliente.NombreCliente, cliente.ApellidoCliente, cliente.EmailCliente }, commandType: CommandType.Text);
            }

        }
        //DELETE
        public async Task<bool> DeleteCliente(int id)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"DELETE FROM dbo.Taller WHERE IdCliente = @IdCliente";

                var result = await conn.ExecuteAsync(query.ToString(), new { IdCliente = id });
                return result > 0;
            }
        }
        //GET
        public async Task<IEnumerable<Cliente>> Get()
        {
            using (SqlConnection conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT IdCliente, NombreCliente, ApellidoCliente, EmailCliente  FROM dbo.Taller ";
                var resultCliente = await conn.QueryAsync<Cliente>(query);
                return resultCliente.ToList();
            }
        }
        //GET POR ID
        public async Task<Cliente> GetId(int Idcliente)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT IdCliente, NombreCliente, ApellidoCliente, EmailCliente FROM dbo.Taller WHERE IdCliente = @IdCliente ";
                return await conn.QuerySingleAsync<Cliente>(query, new { IdCliente = Idcliente });

            }
        }
        //UPDATE
        public async Task ClienteUpdate(Cliente cliente)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdCliente", cliente.IdCliente, DbType.Int32);
                parameters.Add("NombreCliente", cliente.NombreCliente, DbType.String);
                parameters.Add("ApellidoCliente", cliente.ApellidoCliente, DbType.String);
                parameters.Add("EmailCliente", cliente.EmailCliente, DbType.String);

                const string query = @"UPDATE dbo.Taller 
                    SET IdCliente = @IdCliente, 
                        NombreCliente = @NombreCliente,
                        ApellidoCliente = @ApellidoCliente, 
                        EmailCliente = @EmailCliente 
                    WHERE IdCliente = @IdCliente";

                await conn.ExecuteAsync(query, new { cliente.IdCliente, cliente.NombreCliente, cliente.ApellidoCliente, cliente.EmailCliente });
            }
               
        }

    }
}
