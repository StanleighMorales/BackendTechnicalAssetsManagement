using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackendTechnicalAssetsManagement.src.Authorization
{
    /// <summary>
    /// Represents the requirement that a user must meet to view a user profile.
    /// This class acts as a marker for the authorization policy.
    /// </summary>
    public class ViewProfileRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// The handler that contains the logic to evaluate the <see cref="ViewProfileRequirement"/>.
        /// It checks if a user is authorized to view a specific user profile resource.
        /// </summary>
        public class ViewProfileHandler : AuthorizationHandler<ViewProfileRequirement, User>
        {
            /// <summary>
            /// Evaluates if the current user has permission to view the requested user profile resource.
            /// </summary>
            /// <param name="context">The authorization handler context, providing access to the current user.</param>
            /// <param name="requirement">The authorization requirement to be checked.</param>
            /// <param name="resource">The resource being accessed, which in this case is the user profile being requested.</param>
            /// <returns>A Task that completes the authorization check.</returns>
            protected override Task HandleRequirementAsync(
                AuthorizationHandlerContext context,
                ViewProfileRequirement requirement,
                User resource) // 'resource' is the user profile being requested
            {
                // Get the ID of the user making the request from their token claims.
                var currentUserIdString = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(currentUserIdString, out var currentUserId))
                {
                    // If the user's ID claim is missing or invalid, they cannot be authorized.
                    // Fail explicitly instead of just returning.
                    context.Fail();
                    return Task.CompletedTask;
                }

                // Rule 1: Admins, Staff, and SuperAdmins can always view any profile.
                if (context.User.IsInRole("Admin")
                    || context.User.IsInRole("Staff")
                    || context.User.IsInRole("SuperAdmin"))
                {
                    // If the user has a privileged role, the requirement is met.
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                // Rule 2: A user can view their own profile.
                // Check if the ID of the requested profile (the resource) matches the ID of the current user.
                if (resource.Id == currentUserId)
                {
                    // If the user is requesting their own profile, the requirement is met.
                    context.Succeed(requirement);
                }

                // If no success condition was met, the requirement is not satisfied, and access will be denied by default.
                return Task.CompletedTask;
            }
        }
    }
}