using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	public static void Main()
	{
		var averageValue = new AverageValue("8;6/7-5,30,9");
		averageValue.RemoveLowest(2);
		averageValue.ConsoleNewValues();
		averageValue.ConsoleAverage();
		var averageValue2 = new AverageValue("8;6/7-5,30,9", 3);
	}
}

public class AverageValue
{
	public IEnumerable<float> NewValues { get; private set; }
	
	public AverageValue(string rawValue)
	{
		this.NewValues = SetNewValues(rawValue);
	}
	
	public AverageValue(string rawValue, int entriesToRemove)
	{
		this.NewValues = SetNewValues(rawValue);
		RemoveLowest(entriesToRemove);
		ConsoleNewValues();
		ConsoleAverage();		
	}
	
	public void RemoveLowest(int entriesToRemove)
	{
		this.NewValues = this.NewValues.Where((_, index) => index >= entriesToRemove);
	}
	
	public void ConsoleAverage()
	{
		Console.WriteLine($"Output Average => {SetAverageValue(this.NewValues)}");
	}

	public void ConsoleNewValues()
	{
		Console.WriteLine($"[{String.Join(", ", NewValues.ToArray())}]");
	}

	private static IEnumerable<float> SetNewValues(string rawValue)
	{
		string delimitersUsed = "-;/,";
		return rawValue
			.Split(delimitersUsed.ToCharArray())
			.Where(val => IsValidEntry(val))
			.Select(val => float.Parse(val, CultureInfo.InvariantCulture.NumberFormat))
			.OrderBy(val => val);
	}
	
	private static bool IsValidEntry(string val)
	{
		if(float.TryParse(val, out _))
		{
			return true;
		}
		Console.WriteLine($"Invalid entry '{val}'. Not added to the List of values");
		return false;		
	}

	private static float SetAverageValue(IEnumerable<float> newValues)
	{
		return newValues.Count() > 0 ? newValues.Sum() / newValues.Count() : 0;
	}	
}
