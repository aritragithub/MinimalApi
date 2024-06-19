using MinimalApi.Domain;
using MinimalApi.Infrastructure;

namespace MinimalApi.EndPoints
{
    public static class UserDetailEndpoints
    {
        public static void RegisterUserEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/getDetail/{id}", async (int id, UserDetailRepository repo) => 
            {
                return await repo.GetUserDetail(id);
            });

            routes.MapPost("/addDetail", async (UserDetail detail, UserDetailRepository repo) =>
            {
                return await repo.SaveUserDetail(detail);
            });
        }
    }
}
