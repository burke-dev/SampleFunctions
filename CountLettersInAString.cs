using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
	public static void Main()
	{
		var myValue = new CountCharacters("Kenobi: Hello there. \r\nGreivous: General Kenobi! \r\nR2D2@#$ \r\nC-3PO?\r\n");
		Console.WriteLine(myValue.MyValue);
		myValue.ConsoleValueCounts(FilterTypeEnum.AllLettersAndNumbers, true);
	}
}

public class CountCharacters
{
	public string MyValue {get; private set;}
	public CountCharacters(string myValue) => MyValue = myValue;
	
	public void ConsoleValueCounts(FilterTypeEnum filterType, bool isCaseSensitive) => _SetDictionary(filterType, isCaseSensitive).OrderBy(x => x.Key).ToList().ForEach(val => Console.WriteLine($"{val.Key}: {val.Value}"));
	private Dictionary<string, int> _SetDictionary(FilterTypeEnum filterType, bool isCaseSensitive)
	{
		var dictionaryValues = new Dictionary<string, int>();
		foreach (var key in _SplitKeys(filterType))
		{
			var tempKey = isCaseSensitive ? key : key.ToUpper();
			if (dictionaryValues.ContainsKey(tempKey))
			{
				dictionaryValues[tempKey]++;
				continue;
			}
			dictionaryValues.Add(tempKey, 1);
		}
		return dictionaryValues;
	}
	private IEnumerable<string> _SplitKeys(FilterTypeEnum filterType) => _FilteredValue(filterType).Select(val => val.ToString());
	private string _FilteredValue(FilterTypeEnum filterType)
	{
		if(filterType == FilterTypeEnum.NoFilter)
		{
			return MyValue;
		}
		var filterArr = new string[5] {"a-zA-Z0-9", "A-Z", "a-z", "a-zA-Z", "0-9"};
		return new Regex($"[^{filterArr[(int)filterType]}]").Replace(MyValue, "");
	}	
}

public enum FilterTypeEnum
{
	AllLettersAndNumbers,
	UppercaseLettersOnly,
	LowercaseLettersOnly,
	AllLetters,
	NumbersOnly,
	NoFilter
}
