using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
	public static void Main()
	{
		var myLibrary = new Library();
		myLibrary.AddMember(1, "Not A Fan");
		myLibrary.AddBook(123, "Doom Guy", "Doom", new DateTime(1993, 12, 1));
		foreach(var meme in myLibrary.Members)
		{
			Console.WriteLine(meme.Value.Name);
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
	
	public void AddMember(int id, string name)
	{
		var key = $"{name}-{id.ToString()}";
		if(!Books.ContainsKey(key))
		{
			Members.Add(_SetKey(key), new Member(id, name));
			return;
		}
		new Exception($"Members already contains {name}");
	}
	
	private string _SetKey(string rawKey) => String.Concat(rawKey.Where(key => !Char.IsWhiteSpace(key)));
	
	public void AddBook(int id, string author, string title, DateTime published)
	{
		var key = $"{author}-{title}-{id.ToString()}";
		if(!Books.ContainsKey(key))
		{
			Books.Add(_SetKey(key), new Book(id, author, title, published));
			return;
		}
		new Exception($"Books already contains {author}, \"{title}\"");
	}
	
	public void CheckOutBook(string memberId, string bookId)
	{
		if(Members.ContainsKey(memberId) && Books.ContainsKey(bookId))
		{
			Members[memberId].CheckOutBook(bookId, new DateTime());
			return;
		}
		new Exception($"Book \"{Books[bookId]}\" is not available to checkout.");		
	}
	
	//add method to check for OverDue books ~> Member
}

public class Book : Library
{
	public int Id {get; private set;}
	public string Author {get; private set;}
	public string Title {get; private set;}
	public DateTime Published {get; private set;}
	
	public Book(int id, string author, string title, DateTime published)
	{
		Id = id;
		Author = author;
		Title = title;
		Published = published;
	}
}

public class Member : Library
{
	public int Id {get; private set;}
	public string Name {get; private set;}
	public MemberStatus Status {get; private set;}
	public Dictionary<string, MemberBook> MemberBooks {get; private set;}
	public int MaxBooks {get; private set;}
	
	public Member(int id, string name)
	{
		Id = id;
		Name = name;
		Status = MemberStatus.Active;
		MemberBooks = new Dictionary<string, MemberBook>();
		MaxBooks = 4;
	}
	
	public void CheckInBook(string bookKey) => MemberBooks.Remove(bookKey);
	public void CheckOutBook(string bookId, DateTime checkOut)
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
				var keyBooks = string.Join(",", new List<string>(MemberBooks.Keys));
				Console.WriteLine($"Max Books reached.\n{keyBooks}");
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

public enum MemberStatus
{
	Active, CheckedOut, Overdue, Suspended
}
