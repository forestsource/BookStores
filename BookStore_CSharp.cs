/*  https://msdn.microsoft.com/en-us/library/aa288459(v=vs.71).aspx*/

// bookstore.cs
using System;
using System.Data.SQLite;

// A set of classes for handling a bookstore:
namespace Bookstore 
{
   using System.Collections;

   // Describes a book in the book list:
   public struct Book
   {
      public string Title;        // Title of the book.
      public string Author;       // Author of the book.
      public decimal Price;       // Price of the book.
      public DateTime Publish_Date;       // publish_date of the book.
      public string Description;       // Description of the book.
      public bool Paperback;      // Is it paperback?
      public string ISBN10;       // ISBN10 of the book.
      public string ISBN13;       // ISBN13 of the book.
      public int Stock;           // amount of stock

      public Book(string title, string author, decimal price, DateTime publish_date, string description, bool paperBack, string isbn10, string isbn13, int stock)
      {
         Title = title;
         Author = author;
         Price = price;
         Publish_Date = publish_date;
         Description = description;
         Paperback = paperBack;
         ISBN10 = isbn10;
         ISBN13 = isbn13;
         Stock = stock;
      }
   }

   // Declare a delegate type for processing a book:
   public delegate void ProcessBookDelegate(Book book);
   

   // Maintains a book database.
   public class BookDB
   {
      // List of all books in the database:
      ArrayList list = new ArrayList();   

      // Add a book to the database:
      public void AddBook(string title, string author, decimal price, DateTime publish_date, string description, bool paperBack, string isbn10, string isbn13, int stock)
      {
         list.Add(new Book(title, author, price, publish_date, description, paperBack, isbn10, isbn13, stock));
      }

      // Call a passed-in delegate on each paperback book to process it: 
      public void ProcessPaperbackBooks(ProcessBookDelegate processBook)
      {
         foreach (Book b in list) 
         {
            if (b.Paperback)
            // Calling the delegate:
               processBook(b);
         }
      }
      // Connect SQLite DB
      internal void ConnectSqlite()
      {
            string dbConnectionString = "Data Source=E:\\sample.db";
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString)) {
                  try {
                        cn.Open();
                        Console.WriteLine("Connected");
                  } catch (Exception exception) {
                        Console.WriteLine("Can't Connect  ",exception);
                  }
            }
      }
      // class
   }
}
namespace Client
{
      using Bookstore;
      class PriceTotaller
   {
      int countBooks = 0;
      decimal priceBooks = 0.0m;

      internal void AddBookToTotal(Book book)
      {
         countBooks += 1;
         priceBooks += book.Price;
      }

      internal decimal AveragePrice()
      {
         return priceBooks / countBooks;
      }
   }
      class Admin{
            static void Main(){
                  Console.WriteLine("start");
                  BookDB bookDB = new BookDB();
                  AddBooks(bookDB);      
                  Console.WriteLine("Paperback Book Titles:");
                  bookDB.ProcessPaperbackBooks(new ProcessBookDelegate(PrintTitle));
                  PriceTotaller totaller = new PriceTotaller();
                  bookDB.ProcessPaperbackBooks(new ProcessBookDelegate(totaller.AddBookToTotal));
                  Console.WriteLine("Average Paperback Book Price: ${0:#.##}",totaller.AveragePrice());
            }
            static void AddBooks(BookDB bookDB)
            {
                  //(string title, string author, decimal price, DateTime publish_date, string description, bool paperBack, string isbn10, string isbn13)
                  bookDB.AddBook("The C Programming Language", "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, new DateTime(1988,3,22), "hoge" , true, "0131103628", "978-0131103627");
                  bookDB.AddBook("The Unicode Standard 2.0", "The Unicode Consortium", 39.95m, new DateTime(2000,10,01), "fuga", true, "0131103628", "978-0131103627");
                  bookDB.AddBook("The MS-DOS Encyclopedia", "Ray Duncan", 129.95m, new DateTime(2000,10,01), "piyo", false, "0131103628", "978-0131103627");
                  bookDB.AddBook("Dogbert's Clues for the Clueless", "Scott Adams", 12.00m, new DateTime(2000,10,01), "piyopiyo", true, "0131103628", "978-0131103627");
            }
      }
}
/*
// Using the Bookstore classes:
namespace BookTestClient
{
   using Bookstore;

   // Class to total and average prices of books:
   class PriceTotaller
   {
      int countBooks = 0;
      decimal priceBooks = 0.0m;

      internal void AddBookToTotal(Book book)
      {
         countBooks += 1;
         priceBooks += book.Price;
      }

      internal decimal AveragePrice()
      {
         return priceBooks / countBooks;
      }
   }

   // Class to test the book database:
   class Test
   {
      // Print the title of the book.
      static void PrintTitle(Book b)
      {
         Console.WriteLine("   {0}", b.Title);
      }

      // Execution starts here.
      static void Main()
      {
         BookDB bookDB = new BookDB();

         // Initialize the database with some books:
         AddBooks(bookDB);      

         // Print all the titles of paperbacks:
         Console.WriteLine("Paperback Book Titles:");
         // Create a new delegate object associated with the static 
         // method Test.PrintTitle:
         bookDB.ProcessPaperbackBooks(new ProcessBookDelegate(PrintTitle));

         // Get the average price of a paperback by using
         // a PriceTotaller object:
         PriceTotaller totaller = new PriceTotaller();
         // Create a new delegate object associated with the nonstatic 
         // method AddBookToTotal on the object totaller:
         bookDB.ProcessPaperbackBooks(new ProcessBookDelegate(totaller.AddBookToTotal));
         Console.WriteLine("Average Paperback Book Price: ${0:#.##}",
            totaller.AveragePrice());
      }

      // Initialize the book database with some test books:
      static void AddBooks(BookDB bookDB)
      {
            //(string title, string author, decimal price, DateTime publish_date, string description, bool paperBack, string isbn10, string isbn13)
         bookDB.AddBook("The C Programming Language", "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, new DateTime(1988,3,22), "hoge" , true, "0131103628", "978-0131103627");
         bookDB.AddBook("The Unicode Standard 2.0", "The Unicode Consortium", 39.95m, new DateTime(2000,10,01), "fuga", true, "0131103628", "978-0131103627");
         bookDB.AddBook("The MS-DOS Encyclopedia", "Ray Duncan", 129.95m, new DateTime(2000,10,01), "piyo", false, "0131103628", "978-0131103627");
         bookDB.AddBook("Dogbert's Clues for the Clueless", "Scott Adams", 12.00m, new DateTime(2000,10,01), "piyopiyo", true, "0131103628", "978-0131103627");
      }
   }
}*/