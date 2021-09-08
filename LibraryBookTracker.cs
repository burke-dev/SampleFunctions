using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
	public static void Main()
	{
		var myLibrary = _SetLibrary();
		myLibrary.CheckoutBook("NotAFan-000-000", "TolkienJRR-000-000");
		myLibrary.CheckoutBook("NotAFan-000-000", "HellerJ-000-000");
		myLibrary.CheckoutBook("NotAFan-000-000", "MelvilleH-000-000");
		myLibrary.CheckinBook("NotAFan-000-000", "HellerJ-000-000");
		myLibrary.CheckoutBook("NotAFan-000-000", "CarmackJ-000-000");
		myLibrary.CheckoutBook("RabbidLuigi-000-000", "JoyceJ-000-000");
		ConsoleLibrary(myLibrary);
	}
	
	private static Library _SetLibrary()
	{
		var myLibrary = new Library();
		myLibrary.AddBooks(new List<Book> {
			new Book(141203, "Joseph Heller", "Catch-22", new DateTime(1961, 11, 10), "asd-456"),
			new Book(617203, "John Carmack", "Doom", new DateTime(1993, 12, 1), "nfc-532"),
			new Book(320100, "J. R. R. Tolkien", "The Lord of the Rings", new DateTime(1955, 10, 20), "raw-731"),
			new Book(511327, "James Joyce", "Ulysses", new DateTime(1922, 2, 2), "pal-736"),
			new Book(341320, "Herman Melville", "Moby-Dick", new DateTime(1851, 10, 18), "alw-560")

		});
		myLibrary.AddMembers(new List<Member>{
			new Member(456, "Not A Fan"),
			new Member(213, "Rabbid Luigi")
		});
		return myLibrary;
	}
	
	public static void ConsoleLibrary(Library library)
	{
		Console.WriteLine("Members of Library");
		foreach(var meme in library.Members)
		{
			Console.WriteLine($"M > {meme.Key}");
		}
		Console.WriteLine();
		Console.WriteLine("Books in Library");
		foreach(var book in library.Books)
		{
			Console.WriteLine($"B > \"{book.Value.Title}\" ({book.Key}) is {book.Value.Status}");
		}
	}
}

public class Library
{
	public Dictionary<string, Book> Books {get; private set;}
	public Dictionary<string, Member> Members {get; private set;}
	
	public Library()
	{
		Books = new Dictionary<string, Book>();
		Members = new Dictionary<string, Member>();
	}
	
	public void AddMembers(List<Member> LibraryMembers)
	{
		var offset = 0;
		foreach(var member in LibraryMembers)
		{
			_AddMember(member, offset++);
		}
	}
	public void AddMembers(Member member) => _AddMember(member, 11);
	
	public void _AddMember(Member member, int offset)
	{
		var key = KeyCode.GetCode(member.Name, offset * 3);
		if(!Books.ContainsKey(key))
		{
			Members.Add(key, new Member(member.Id, member.Name));
			return;
		}
		Console.WriteLine($"Members already contains {member.Name}");
	}

	public void AddBooks(List<Book> Books)
	{
		var offset = 0;
		foreach(var book in Books)
		{
			_AddBook(book, offset++);
		}
	}
	public void AddBooks(Book book, string copyId) => _AddBook(book, 11);
	
	private void _AddBook(Book book, int offset)
	{
		var key = KeyCode.GetCode(new Regex($"[^A-Za-z0-9]").Replace(_GetFormattedAuthor(book), ""), offset * 7);
		if(!Books.ContainsKey(key))
		{
			Books.Add(key, new Book(book.Id, book.Author, book.Title, book.Published, book.Copies[0].Id));
			return;
		}
		Console.WriteLine($"Books already contains {book.Author}, \"{book.Title}\"");
	}
	
	private static string _GetFormattedAuthor(Book book)
	{
		var nameList = book.Author.Split(" ").Select(q => new Regex($"[^A-Za-z0-9]").Replace(q, "")).ToList();
		var lastName = nameList.Count() - 1;
		var newAuthor = nameList[lastName];
		nameList.RemoveAt(lastName);
		foreach(var name in nameList)
		{
			newAuthor += name.ToCharArray()[0].ToString();
		}
		return newAuthor;
	}
	
	public void CheckinBook(string memberId, string bookId)
	{
		if(Members.ContainsKey(memberId) && Books[bookId].Status != BookStatus.Available)
		{
			Members[memberId].CheckoutBook(bookId, new DateTime());
			Books[bookId].CheckinBook();
			return;
		}
	}
	public void CheckoutBook(string memberId, string bookId)
	{
		if(Members.ContainsKey(memberId) && Books.ContainsKey(bookId) && Books[bookId].Status == BookStatus.Available)
		{
			Members[memberId].CheckoutBook(bookId, new DateTime());
			Books[bookId].CheckoutBook();
			return;
		}
		if(!Books.ContainsKey(bookId) || Books[bookId].Status != BookStatus.Available)
		{
			Console.WriteLine($"Book \"{Books[bookId].Title}\" is not available to checkout.");
		}
		if(!Members.ContainsKey(memberId))
		{
			Console.WriteLine($"Member does not exist.");
		}
	}
	//add method to check for OverDue books ~> Member
}

public class Book : Library
{
	public int Id {get; private set;}
	public string Author {get; private set;}
	public string Title {get; private set;}
	public DateTime Published {get; private set;}
	public BookStatus Status {get; private set;}
	public List<BookCopy> Copies {get; private set;}
	
	public Book(int id, string author, string title, DateTime published, string copyId)
	{
		Id = id;
		Author = author;
		Title = title;
		Published = published;
		Status = BookStatus.Available;
		Copies = new List<BookCopy>{ new BookCopy(copyId) };
	}
	
	public void AddCopy(string copyId)
	{
		if(!Copies.Exists(c => c.Id == copyId))
		{
			Copies.Add(new BookCopy(copyId));
		}
		Console.WriteLine($"\"{Title}\" alreadycontains {copyId}");
	}
	
	public void CheckoutBook() => Status = BookStatus.CheckedOut;
	public void CheckinBook() => Status = BookStatus.Available;
}

public class BookCopy
{
	public string Id {get; private set;}
	public DateTime DateAdded {get; private set;}

	public BookCopy(string id)
	{
		Id = id;
		DateAdded = DateTime.Now;
	}
}

public class Member : Library
{
	public int Id {get; private set;}
	public string Name {get; private set;}
	public MemberStatus Status {get; private set;}
	public Dictionary<string, MemberBook> MemberBooks {get; private set;}
	public int MaxBooks {get; private set;}
	public List<MemberNote> Notes {get; private set;}
	
	public Member(int id, string name)
	{
		Id = id;
		Name = name;
		Status = MemberStatus.Active;
		MemberBooks = new Dictionary<string, MemberBook>();
		MaxBooks = 2;
		Notes = new List<MemberNote>();
	}
	
	public void CheckinBook(string bookKey) => MemberBooks.Remove(bookKey);

	public void CheckoutBook(string bookId, DateTime checkOut)
	{
		switch(Status)
		{
			case MemberStatus.Active:
				MemberBooks.Add(bookId, new MemberBook(checkOut));	
				Status = MemberStatus.CheckedOut;
				break;
			case MemberStatus.CheckedOut:
				if(MemberBooks.Count < MaxBooks)
				{
					MemberBooks.Add(bookId, new MemberBook(checkOut));					
					break;
				}
				var keyBooks = string.Join(", ", new List<string>(MemberBooks.Keys));
				Console.WriteLine($"Unable to checkout {bookId}.\nMax Books reached.\n({keyBooks}) are currently checked out.\n");
				break;
			case MemberStatus.Overdue:
				//add method to list overdue books.
				Console.WriteLine($"Booked Overdue. Currently checked out books.\n");
				break;
			case MemberStatus.Suspended:
				//add method notify that the account is suspended.
				break;
		}				
	}
}

public class MemberNote
{
	public DateTime Date {get; private set;}
	public string Note {get; private set;}
	
	public MemberNote(string note)
	{
		Date = DateTime.Now;
		Note = note;
	}
}

public class MemberBook
{
	public DateTime CheckOut {get; private set;}
	public DateTime Due {get; private set;}
	
	public MemberBook(DateTime checkOut)
	{
		CheckOut = checkOut;
		Due = checkOut.AddDays(14);
	}
}

public static class KeyCode
{
	public static string GetCode(string rawKey, int offset = 0, bool debug = true) => _SetKey($"{rawKey}-{(debug ? "000-000" : _FormatCode(6, offset))}");
	private static string _FormatCode(int chunkSize, int offset)
	{
		var rawCode = _BaseCode();
		return _NewSections(rawCode.Select((x, i) => i).Where(i => i % chunkSize == 0).Select(i => _CodeSection(rawCode, chunkSize, i)).ToList()[0], offset);
	}
	private static string _BaseCode()
	{
		var now = DateTime.Now;
		var yearTicks = new DateTime(now.Year, now.Month, now.Day).Ticks;
		return (now.Ticks-yearTicks).ToString();
	}
	
	private static string _SetKey(string rawKey) => String.Concat(rawKey.Where(key => !Char.IsWhiteSpace(key)));
	private static string _NewSections(string newStr, int offset) => string.Join("-", newStr.Select((x, i) => i)
			.Where(i => i % 3 == 0).Select(i => _CodeSection(newStr, 3, i)).Select((x, i) => _FormatSection(x, i, offset)).ToList());
	private static string _CodeSection(string section, int chunkSize, int i) => section.Substring(i, section.Length - i >= chunkSize ? chunkSize : section.Length - i);
	
	private static string _FormatSection(string x, int i, int offset)
	{
		var section = Convert.ToInt32(x) + (i % 2 == 1 ? offset : 0);
		var newSection = (section >= 1000 ? (section - 123) : section).ToString();
		while(newSection.Length < 3)
		{
			newSection = $"0{newSection}";
		}
		return newSection;
	}
}

public enum BookStatus
{
	Available, CheckedOut, Overdue, Damaged
}

public enum MemberStatus
{
	Active, CheckedOut, Overdue, Suspended
}
