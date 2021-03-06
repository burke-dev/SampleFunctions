using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	public static void Main()
	{
		var averageValue = new AverageValue("8;6,7-5,30,9", 2);
		var averageValue2 = new AverageValue("8,6/7-5,30,9");
		averageValue2.AddNewValues("6,32,2,44,8,4,1,1,111");
		averageValue2.ConsoleNewValues();
		averageValue2.ConsoleAverage();
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
	
	public void AddNewValues(string rawValue)
	{
		this.NewValues = this.NewValues.Concat(SetNewValues(rawValue)).OrderBy(val => val);
	}
	
	public void RemoveLowest(int entriesToRemove)
	{
		this.NewValues = this.NewValues.Where((_, index) => index >= entriesToRemove);
	}
	
	public void ConsoleAverage()
	{
		var average = this.NewValues.Count() > 0 ? this.NewValues.Sum() / this.NewValues.Count() : 0;
		Console.WriteLine($"Output Average => {average}");
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
}
