using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Program
{
	public static void Main()
	{
		var myValue = FancyString.CleanString("Hel,l!o. World.");
		Console.WriteLine(myValue);
		Console.WriteLine(FancyString.Yelling(myValue));
		Console.WriteLine(FancyString.Sarcasm(myValue));
		Console.WriteLine(FancyString.ReverseLetters(myValue));
		Console.WriteLine(FancyString.ReverseWords(myValue));
		Console.WriteLine(FancyString.Sarcasm(FancyString.ReverseLetters(myValue)));
	}
}

public static class FancyString
{
	public static string CleanString(string val)
	{
		return Regex.Replace(val, @"[.,!]", "", RegexOptions.IgnorePatternWhitespace);
	}

	public static string Yelling(string val)
	{
		return $"{val.ToUpper()}!";
	}

	public static string Sarcasm(string val)
	{
		var msPacman = String.Empty;
		for(var i=0; i < val.Length; i++)
		{
			var v = val[i].ToString();
			msPacman += i % 2 == 0 ? v.ToUpper() : v.ToLower();
		}
		return msPacman + (val.Length % 2 == 0 ? "!1!" : "1!1");
	}

	public static string ReverseLetters(string val)
	{
		string reverseString = string.Empty;
		for (var i = val.Length - 1; i >= 0; i--)
		{
			reverseString += val[i];
		}
		return reverseString;
	}

	public static string ReverseWords(string val)
	{
		var theValues = val.Split(" ");
		var reverseWords = new List<string>();
		for (var i = theValues.Length - 1; i >= 0; i--)
		{
			reverseWords.Add(theValues[i]);
		}
		return string.Join(" ", reverseWords);
	}
}
