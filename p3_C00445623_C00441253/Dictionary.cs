/*
 * Author:      Trevor Karl and Rodney Harris
 * CLID:        C00441253 and C00445623
 * Class:       CMPS 358
 * Assignment:  Project 3
 * Due Date:    November 1, 2022
 * Description: The code in this file contains the classes to build and initialize
 *              the Dictionary database with the Id, word and definition for the
 *              definitions table.
 */

using Microsoft.EntityFrameworkCore;

namespace Dictionary;

/*
 * Dictionary
 *
 * Creates each attribute of an entry into the database: Id (int), word (string), definition (string)
*/
public class Dictionary
{
    public int Id { get; set; }
    public string word { get; set; }
    public string definition { get; set; }
}

/*
 * DictionaryDbContext
 *
 * Represents a session with the database and can be used to query and save instances of entities
 */
public class DictionaryDbContext : DbContext
{
    /*
     * OnConfiguring
     *
     * Lamda expression to establish Sqlite connection to dictionary.db
     */
    protected override void OnConfiguring(DbContextOptionsBuilder options) => 
    options.UseSqlite("Data Source = dictionary.db");

    /*
     * Definitions
     *
     * Creates the Definitions table along with a getter and setter
     */
    public DbSet<Dictionary> Definitions { get; set; }

    /*
     * OnModelCreating
     *
     * Defines the shape of entities, the relationships between them, and how they map to the database
     */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dictionary>().HasData(new Dictionary[]
        {
            new Dictionary {Id=1, word="Rapscallion", definition="A mischievous person"},
            new Dictionary {Id=2, word="Whippersnapper", definition="A young and inexperienced person considered to be presumptuous or overconfident"}
        });

        base.OnModelCreating(modelBuilder);
    }
}