using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Program
{
	public static void Main()
	{
		var myValue = new CountEntries("Hello there. General Kenobi!");
		myValue.GetAllValues(true);
		myValue.GetValueCounts(true, true);
	}
}

public class CountEntries
{
	public string MyValue {get; private set;}
	
	public CountEntries(string val) => MyValue = val;
	
	public void GetValueCounts(bool isCaseSensitive, bool cleanSpecial) => _SetDictionary(isCaseSensitive, cleanSpecial).ToList().ForEach(val => Console.WriteLine($"{val.Key}: {val.Value}"));
	public void GetAllValues(bool cleanSpecial) => Console.WriteLine($"{MyValue} ~> {string.Join("", _SplitValue(cleanSpecial))}");
	
	private IEnumerable<string> _SplitValue(bool cleanSpecial) => String.Concat((cleanSpecial ? _CleanString(MyValue) : MyValue).Where(v => !Char.IsWhiteSpace(v))).OrderBy(v => v).Select(v => v.ToString());
	private static string _CleanString(string val) => new Regex("[^a-zA-Z0-9]").Replace(val, "");

	private Dictionary<string, int> _SetDictionary(bool isCaseSensitive, bool cleanSpecial)
	{
		var myValues = new Dictionary<string, int>();
		foreach(var key in _SplitValue(cleanSpecial))
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
