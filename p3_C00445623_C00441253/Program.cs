/*
 * Author:      Trevor Karl and Rodney Harris
 * CLID:        C00441253 and C00445623
 * Class:       CMPS 358
 * Assignment:  Project 3
 * Due Date:    November 1, 2022
 * Description: The code in this file contains the menu for the user to input 
 *              choices to interact with the Dictionary database along with the
 *              methods to list, add and delete words/definitions.
 */

using Dictionary;
using Microsoft.EntityFrameworkCore;

var db = new DictionaryDbContext();

while(true) {
    string choice = Menu();
    if(choice == "0")
    {
        break;
    }

    if(choice == "1") 
    {
        Console.WriteLine();
        Console.WriteLine("Words in the Dictionary");
        Console.WriteLine("-----------------------");
        ShowDefinitions(db);
    }
    else if(choice == "2")
    {
        Console.Write("\nEnter Word to Add: ");
        var addWord = Console.ReadLine();
        Console.Write($"\nEnter Definition of {addWord}: ");
        var addDef = Console.ReadLine();

        foreach(var w in db.Definitions) 
        {
            if(w.word == addWord)
            {
                if(w.definition != addDef)
                    AddDefinition(db, addWord, addDef);
            }
            else if(w.word != addWord)
                AddDefinition(db, addWord, addDef);
        }
    }
    else if(choice == "3")
    {
        Console.Write("\nEnter Word to Delete: ");
        var delWord = Console.ReadLine();

        foreach(var w in db.Definitions)
        {
            if(w.word == delWord)
                DeleteDefinition(db, delWord);
        }
    }
}

/*
 * Menu
 *
 * Prints lines to the console so that users know what options they have to interact
 * with the database
*/
static string Menu() {
    Console.WriteLine();
    Console.WriteLine("1 - List Words and Definitions in Ascending Word Order");
    Console.WriteLine("2 - Enter a Word and Definition to Add to the Dictionary");
    Console.WriteLine("3 - Enter a Word to Delete From the Dictionary");
    Console.WriteLine("0 - Exit");
    Console.Write("Enter Your Choice: ");
    return Console.ReadLine();
}

/*
 * ShowDefinitions
 *
 * Shows all entries in the Definitions table AKA the Word and Definition
 *
 * Input: db (Dictionary database in use)
 *
 * Output: Words and their definitions
 */
static void ShowDefinitions(DictionaryDbContext db) 
{
    var definitionsList = db
        .Definitions
        .OrderBy(_ => _.word);

    foreach(var w in definitionsList)
        Console.WriteLine(w.word + ": " + w.definition);
    Console.WriteLine();
}

/*
 * AddDefinition
 *
 * Adds a word and its definition to the Definitions table in the Dictionary DB
 *
 * Input: db (Dictionary database in use), word, definition
 *
 * Output: None, but an entry is added to the Definitions table
 */
static void AddDefinition(DictionaryDbContext db, string word, string definition)
{
    if(db.Definitions.FirstOrDefault(_ => _.word == word && _.definition == definition) != null) return;

    try
    {
        db.Definitions.Add(new Dictionary.Dictionary {word = word, definition = definition});
        db.SaveChanges();
    }
    catch
    {
        Console.WriteLine($"Adding {word} failed\n");
    }
}

/*
 * DeleteDefinition
 *
 * Deletes all instances of a word and its definitions in the Definitions table in 
 * the Dictionary DB
 *
 * Input: db (Dictionary database in use), word
 *
 * Output: None, but all entries of a word are deleted from Definitions table
 */
static void DeleteDefinition(DictionaryDbContext db, string word)
{
    var delete = db.Definitions.FirstOrDefault(_ => _.word == word);
    try
    {
        while(delete != null) 
        {
            db.Entry(delete).State = EntityState.Modified;
            db.Definitions.Remove(delete);
            db.SaveChanges();

            delete = db.Definitions.FirstOrDefault(_ => _.word == word);
        }
    }
    catch
    {
        Console.WriteLine($"{word} cannot be deleted\n");
    }
}
