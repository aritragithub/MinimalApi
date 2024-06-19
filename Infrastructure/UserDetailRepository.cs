using Dapper;
using MinimalApi.Domain;
using System.Data;
using System.Data.SqlClient;

namespace MinimalApi.Infrastructure
{
    public class UserDetailRepository
    {
        private readonly IConfiguration _configuration;
        private const string DefaultConnection = "DefaultConnection";

        public UserDetailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<UserDetail> GetUserDetail(int id)
        {
            try
            {
                using (var connection = CrateConnection())
                {
                    connection.Open();

                    var userDetail = await connection.QuerySingleOrDefaultAsync<UserDetail>
                        ("SELECT * FROM UserDetail where Id = @id", new { id = id });

                    return userDetail;
                }
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

        public async Task<int> SaveUserDetail(UserDetail userDetail)
        {
            try
            {
                using (var connection = CrateConnection())
                {

                    var sql = "INSERT INTO UserDetail (Name, Email, Address) VALUES (@Name, @Email, @Address)";
                    connection.Open();
                    var result = await connection.ExecuteAsync
                        (sql, new { Name = userDetail.Name, Email = userDetail.Email, Address = userDetail.Address });
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private IDbConnection CrateConnection() =>
            new SqlConnection(_configuration.GetConnectionString(DefaultConnection));


    }

   


} 
