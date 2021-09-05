using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
	public static void Main()
	{
		var myValue = new CountCharacters("Kenobi: Hello there. \r\nGreivous: General Kenobi! \r\nR2D2@#$ \r\nC-3PO?\r\n");
		myValue.ConsoleValue();
		myValue.ConsoleValueCounts(FilterTypeEnum.AllLettersAndNumbers, true);
	}
}

public class CountCharacters
{
	public string MyValue {get; private set;}
	public CountCharacters(string myValue) => MyValue = myValue;
	
	public void ConsoleValue() => Console.WriteLine(MyValue);
	public void ConsoleValueCounts(FilterTypeEnum filterTypeEnum, bool isCaseSensitive) => _SetDictionary(filterTypeEnum, isCaseSensitive).OrderBy(x => x.Key).ToList().ForEach(val => Console.WriteLine($"{val.Key}: {val.Value}"));
	private Dictionary<string, int> _SetDictionary(FilterTypeEnum filterTypeEnum, bool isCaseSensitive)
	{
		var dictionaryValues = new Dictionary<string, int>();
		foreach (var key in _SplitKeys(filterTypeEnum))
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
	
	private IEnumerable<string> _SplitKeys(FilterTypeEnum filterTypeEnum) => _FilteredValue(filterTypeEnum).Select(val => val.ToString());
	private string _FilteredValue(FilterTypeEnum filterTypeEnum)
	{
		switch(filterTypeEnum)
		{
			case FilterTypeEnum.AllLettersAndNumbers: return new Regex($"[^a-zA-Z0-9]").Replace(MyValue, "");
			case FilterTypeEnum.UppercaseLettersOnly: return new Regex($"[^A-Z]").Replace(MyValue, "");
			case FilterTypeEnum.LowercaseLettersOnly: return new Regex($"[^a-z]").Replace(MyValue, "");
			case FilterTypeEnum.AllLetters: return new Regex($"[^a-zA-Z]").Replace(MyValue, "");
			case FilterTypeEnum.NumbersOnly: return new Regex($"[^0-9]").Replace(MyValue, "");
			default: return MyValue;
		}
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
