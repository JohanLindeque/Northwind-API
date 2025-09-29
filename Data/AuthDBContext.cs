using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Northwind_API.Data;

public class AuthDBContext : IdentityDbContext<IdentityUser>
{

    public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
    {

    }

}
