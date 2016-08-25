/*http://jsbin.com/loxetegixe/edit?js,console*/

/*jshint esnext: true */

class Book {
  //(string title, string author, decimal price, DateTime publish_date, string description, bool paperBack, string isbn10, string isbn13)
  constructor(title,author,price,publish_date,description,paperBack,isbn10,isbn13,stock) {
    this.title = title;
    this.author = author;
    this.price = price;
    this.publish_date = publish_date;
    this.description = description;
    this.paperBack = paperBack;
    this.isbn10 = isbn10;
    this.isbn13 = isbn13;
    this.stock = stock;
  }
  
  get info() {
    return this.title;
  }

  calcArea() {
    return this.height * this.width;
  }
}

class BookDB{
  
  constructor(){
    this.books = [];
  }
  
  addBook(title){
    var book = new Book(title);
    this.books.push(book);
  }
  debug(){
    console.log(this.books);
  }
  
  get sum(){
    return this.books.length;
  }

}

const bookdb = new BookDB();
bookdb.addBook("hoge");
bookdb.addBook("fuga");
console.log(bookdb.sum);
bookdb.debug();
