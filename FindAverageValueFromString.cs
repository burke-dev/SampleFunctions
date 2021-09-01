using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	public static void Main()
	{
		var netAverage = SetNetAverageFromString("8,x,6,y,7,5,z,30,9");
		Console.WriteLine("Output Average => " + netAverage);
	}

	private static float SetNetAverageFromString(string rawValue)
	{
		var newValues = SetNewValues(rawValue, 2);
		return AverageValue(newValues);
	}

	private static List<float> SetNewValues(string rawValue, int removeLowest)
	{
		string delimitersUsed = "-;/,";
		return rawValue
			.Split(delimitersUsed.ToCharArray())
			.Where(val => ValidEntry(val))
			.Select(val => float.Parse(val, CultureInfo.InvariantCulture.NumberFormat))
			.OrderBy(val => val)
			.Where((val, index) => index >= removeLowest)
			.ToList();
	}
	
	private static bool ValidEntry(string val)
	{
		if(float.TryParse(val, out _))
		{
			return true;
		}
		Console.WriteLine("Invalid entry '" + val + "'. Not added to the List of values");
		return false;		
	}

	private static float AverageValue(List<float> newValues)
	{
		return newValues.Count() > 0 ? newValues.Sum() / newValues.Count() : 0;
	}
}
