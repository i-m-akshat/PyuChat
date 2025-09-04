
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http; // for IFormFile
using Microsoft.AspNetCore.Mvc;  // for [FromForm]

public static class AccountEndPoints
{
    public static RouteGroupBuilder MapAccountEndPoint(this WebApplication app)
    {
        var group = app.MapGroup("/server/account").WithTags("accounts");

        group.MapPost("/register", async (
            HttpContext httpContext,
            UserManager<AppUser> _userManager,
            [FromForm] string FullName,
            [FromForm] string email,
            [FromForm] string password, [FromForm] IFormFile? profileImg) =>
        {
            var userFromDB = await _userManager.FindByEmailAsync(email);
            if (userFromDB is not null)
            {
                return Results.BadRequest(Response<string>.Failure("User is already registered."));
            }
            if (profileImg is null)
            {
                return Results.BadRequest(Response<string>.Failure("Profile img is required"));
            }
            var picture = await FileUpload.Upload(profileImg);
            picture = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/uploads/{picture}";
            var user = new AppUser
            {
                Email = email,
                FullName = FullName,
                ProfileImage = picture,

            };
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return Results.BadRequest(Response<string>.Failure(result.Errors.Select(x => x.Description).FirstOrDefault()!));
            }
            return Results.Ok(Response<string>.Success("", "User created successfully"));
        });

        return group;
    }
}
