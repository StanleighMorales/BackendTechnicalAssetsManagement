using Microsoft.AspNetCore.Authorization;

namespace BackendTechnicalAssetsManagement.src.Authorization
{
    public class SuperAdminBypassHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (context.User.IsInRole("SuperAdmin"))
            {
                foreach (var requirement in context.PendingRequirements.ToList())
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
