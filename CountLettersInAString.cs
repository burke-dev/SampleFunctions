using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
	public static void Main()
	{
		var myValue = new CountCharacters("Hello there. General Kenobi! R2D2 C-3PO");
		myValue.ConsoleAllValues(true, true);
		myValue.ConsoleValueCounts(false, true, false);
	}
}

public class CountCharacters
{
	public string MyValue {get; private set;}
	
	public CountCharacters(string myValue) => MyValue = myValue;
	public void ConsoleAllValues(bool cleanSpecial, bool hasNumbers) => Console.WriteLine($"{MyValue} ~> {string.Join("", _SplitValue(cleanSpecial, hasNumbers))}");
	public void ConsoleValueCounts(bool isCaseSensitive, bool cleanSpecial, bool hasNumbers) => SetValues(isCaseSensitive, cleanSpecial, hasNumbers).OrderBy(x => x.Key).ToList().ForEach(val => Console.WriteLine($"{val.Key}: {val.Value}"));

	private static string _CleanString(string val, bool hasNumbers = false) 
	{
		var validCharsList = "a-zA-Z";
		if(hasNumbers)
		{
			validCharsList += "0-9";
		}
		return new Regex($"[^{validCharsList}]").Replace(val, "");
	}
	private IEnumerable<string> _SplitValue(bool cleanSpecial, bool hasNumbers) => String.Concat((cleanSpecial ? _CleanString(MyValue, hasNumbers) : MyValue).Where(v => !Char.IsWhiteSpace(v))).OrderBy(v => v).Select(v => v.ToString());
	private Dictionary<string, int> SetValues(bool isCaseSensitive, bool cleanSpecial, bool hasNumbers)
	{
		var myValues = new Dictionary<string, int>();
		foreach (var key in _SplitValue(cleanSpecial, hasNumbers))
		{
			var tempKey = isCaseSensitive ? key : key.ToUpper();
			if (myValues.ContainsKey(tempKey))
			{
				myValues[tempKey]++;
				continue;
			}
			myValues.Add(tempKey, 1);
		}
		return myValues;
	}
}
