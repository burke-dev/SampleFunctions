using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Program
{
	public static void Main()
	{
		MutateString.ShowAllSets("Hel,l!o. World. -Mr. PLinke#tt");
	}
}

public static class MutateString
{
	public static void ShowAllSets(string val)
	{
		Console.WriteLine($"{val} <~ raw entry");
		_ShowSet(val, true);
		_ShowSet(val, false);
	}

	public static string ReverseLetters(string val, string yellSarcasm, bool isClean) => MutateRawString(_ReverseLetters(val), yellSarcasm, isClean);
	public static string ReverseWords(string val, string yellSarcasm, bool isClean) => MutateRawString(_ReverseWords(val), yellSarcasm, isClean);
	
	public static string MutateRawString(string val, string yellSarcasm, bool isClean)
	{
		val = isClean ? _CleanString(val) : val;
		switch (yellSarcasm.ToLower())
		{
			case "caps": return $"{val.ToUpper()}!";
			case "sarcasm": return _Sarcasm(val);
			default: return val;
		}
	}

	private static string _ReverseLetters(string val)
	{
		var reverseString = string.Empty;
		for (var i = val.Length - 1; i >= 0; i--)
		{
			reverseString += val[i];
		}
		return reverseString;
	}

	private static string _ReverseWords(string val)
	{
		var splitValues = val.Split(" ");
		var reverseWords = new List<string>();
		for (var i = splitValues.Length - 1; i >= 0; i--)
		{
			reverseWords.Add(splitValues[i]);
		}
		return string.Join(" ", reverseWords);
	}

	private static void _ShowEach(string val, string yellSarcasm, bool isClean)
	{
		Console.WriteLine($"{(isClean ? "Clean" : "Dirty")} ~ {(yellSarcasm.Length > 0 ? yellSarcasm : "regular")}");
		Console.WriteLine($"{MutateRawString(val, yellSarcasm, isClean)} <~ order unmodified");
		Console.WriteLine($"{ReverseLetters(val, yellSarcasm, isClean)} <~ letter order reversed");
		Console.WriteLine($"{ReverseWords(val, yellSarcasm, isClean)} <~ word order reversed");
	}
	
	private static string _Sarcasm(string val)
	{
		var arr = val.ToLower().ToCharArray();
		Array.ForEach(arr, element =>
		{
			var index = Array.IndexOf(arr, element);
			arr[index] = _SarcasmElement(element, index % 2 == 0);
		});
		return string.Join("", arr) + (val.Length % 2 == 0 ? "!1!" : "1!1");
	}
	private static List<string> AllSets = new List<string>{"", "caps", "sarcasm"};
	private static char _SarcasmElement(char element, bool isEven) => (isEven ? char.Parse(element.ToString().ToUpper()) : element);
	private static void _ShowSet(string val, bool isClean) => AllSets.ForEach(type => _ShowEach(val, type, isClean));
	private static string _CleanString(string val) => Regex.Replace(val, @"[.,!#]", "", RegexOptions.IgnorePatternWhitespace);
}
