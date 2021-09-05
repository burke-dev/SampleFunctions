using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
	public static void Main()
	{
		var myValue = new CountCharacters("Kenobi: Hello there. \nGreivous: General Kenobi! \nR2D2@#$ \nC-3PO?\n");
		Console.WriteLine(myValue.MyValue);
		myValue.ConsoleValueCounts(FilterTypeEnum.UppercaseLettersOnly, true);
		var staticString = "\nVader: No, I am your father.\nLuke: That's IMPOSSIBLE!\n";
		Console.WriteLine(staticString);
		CountCharacters.ConsoleValueCounts(staticString, FilterTypeEnum.NoFilter, false);
	}
}

public class CountCharacters
{
	public string MyValue {get; private set;}
	public CountCharacters(string myValue) => MyValue = myValue;
	
	public void ConsoleValueCounts(FilterTypeEnum filterType, bool isCaseSensitive) => _ConsoleValues(MyValue, filterType, isCaseSensitive);
	public static void ConsoleValueCounts(string myValue, FilterTypeEnum filterType, bool isCaseSensitive) => _ConsoleValues(myValue, filterType, isCaseSensitive);

	private static void _ConsoleValues(string myValue, FilterTypeEnum filterType, bool isCaseSensitive) => _SetDictionary(myValue, filterType, isCaseSensitive).OrderBy(x => x.Key).ToList().ForEach(val => Console.WriteLine($"{val.Key}: {val.Value}"));
	
	private static Dictionary<string, int> _SetDictionary(string myValue, FilterTypeEnum filterType, bool isCaseSensitive)
	{
		var dictionaryValues = new Dictionary<string, int>();
		foreach (var key in _SplitKeys(myValue, filterType))
		{
			var tempKey = key == "\n" ? "\\n" : isCaseSensitive ? key : key.ToUpper();
			if (dictionaryValues.ContainsKey(tempKey))
			{
				dictionaryValues[tempKey]++;
				continue;
			}
			dictionaryValues.Add(tempKey, 1);
		}
		return dictionaryValues;
	}
	private static IEnumerable<string> _SplitKeys(string myValue, FilterTypeEnum filterType) => _FilteredValue(myValue, filterType).Select(val => val.ToString());
	private static string _FilteredValue(string myValue, FilterTypeEnum filterType)
	{
		if(filterType != FilterTypeEnum.NoFilter)
		{
			var filterArr = new string[5] {"a-zA-Z0-9", "A-Z", "a-z", "a-zA-Z", "0-9"};
			return new Regex($"[^{filterArr[(int)filterType]}]").Replace(myValue, "");
		}
		return myValue;
	}	
}

public enum FilterTypeEnum
{
	AllLettersAndNumbers, UppercaseLettersOnly, LowercaseLettersOnly, AllLetters, NumbersOnly, NoFilter
}
