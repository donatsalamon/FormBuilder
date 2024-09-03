using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using formBuilder.Models;

namespace formBuilder.Data
{
    public class formBuilderContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public formBuilderContext (DbContextOptions<formBuilderContext> options)
            : base(options)
        {
        }

        public DbSet<formBuilder.Models.QuestionSheet> QuestionSheet { get; set; } = default!;
        public DbSet<formBuilder.Models.User> Users { get; set; } = default!;
        public DbSet<formBuilder.Models.Answer> Answers{ get; set; } = default!;
        public DbSet<formBuilder.Models.Form> Forms{ get; set; } = default!;
        public DbSet<formBuilder.Models.Statement> Statements{ get; set; } = default!;
    }
}
