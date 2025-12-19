using Microsoft.EntityFrameworkCore;

namespace Flashcards.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
    }
}

// #pragma warning disable CS8618
// using Microsoft.EntityFrameworkCore;

// namespace Flashcards.Models;

// public class MyContext : DbContext
// {
//     public MyContext(DbContextOptions<MyContext> options) : base(options) { }

//     public DbSet<User> Users { get; set; }
//     public DbSet<Folder> Folders { get; set; }
//     public DbSet<Flashcard> Flashcards { get; set; }
// }


// #pragma warning disable CS8618

// using Microsoft.EntityFrameworkCore;

// namespace Flashcards.Models;

// public class MyContext : DbContext
// {
//     // Constructor used at runtime with DI
//     public MyContext(DbContextOptions<MyContext> options) : base(options) { }

//     // Parameterless constructor for design-time tools (migrations)
//     public MyContext() { }

//     public DbSet<User> Users { get; set; }
//     public DbSet<Folder> Folders { get; set; }
//     public DbSet<Flashcard> Flashcards { get; set; }
// }


// #pragma warning disable CS8618

// using Microsoft.EntityFrameworkCore;

// namespace Flashcards.Models;

// public class MyContext : DbContext 
// {   
//     public MyContext(DbContextOptions options) : base(options) { }    

//     public DbSet<User> Users { get; set; }
//     public DbSet<Folder> Folders { get; set; }
//     public DbSet<Flashcard> Flashcards { get; set; }
// }