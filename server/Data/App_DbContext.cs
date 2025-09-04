using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class App_DbContext : IdentityDbContext
{
    public App_DbContext(DbContextOptions<App_DbContext> options) : base(options)
    {
    }
}
