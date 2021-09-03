using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Program
{
	public static void Main()
	{
		var ironValue = new CountEntries("HELLO world. I am Iron Man!", true);
		ironValue.GetAllValues();
		ironValue.GetValueCounts(true);
		var myValue = new CountEntries("Hello there. General Kenobi!", false);
		myValue.GetValueCounts(false);
	}
}

public class CountEntries
{
	public IEnumerable<string> Values {get; private set;}
	
	public CountEntries(string val, bool cleanSpecial)
	{
		if(cleanSpecial)
		{
			val = _CleanString(val);
		}
		Values = String.Concat(val.Where(c => !Char.IsWhiteSpace(c))).OrderBy(c => c).Select(x => x.ToString());
	}
	private static string _CleanString(string val) => new Regex("[^a-zA-Z0-9]").Replace(val, "");
	
	public void GetValueCounts(bool isCaseSensitive)
	{
		foreach(KeyValuePair<string, int> val in setDictionary(isCaseSensitive))
		{
			Console.WriteLine($"{val.Key}: {val.Value}");
		}
	}
	
	public void GetAllValues() => Console.WriteLine(string.Join("", Values));
	
	private Dictionary<string, int> setDictionary(bool isCaseSensitive)
	{
		var myValues = new Dictionary<string, int>();
		foreach(var key in Values)
		{
			var tempKey = isCaseSensitive ? key : key.ToUpper();
			if(myValues.ContainsKey(tempKey))
			{
				myValues[tempKey]++;
				continue;
			}
			myValues.Add(tempKey, 1);
		}
		return myValues;
	}
}
