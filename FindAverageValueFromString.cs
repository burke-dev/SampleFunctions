using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public class Program
{
	public static void Main()
	{
		var netAverage = SetNetAverageFromString("8;6/7-5,30,9");
		Console.WriteLine("Output Average => " + netAverage);
	}
	
	private static float SetNetAverageFromString(string rawValue)
	{
		var newValues = SetNewValues(rawValue);
		RemoveLowestValues(newValues, 0);
		return AverageValue(newValues);
	}
	
	private static List<float> SetNewValues(string rawValue)
	{
		var newValues = new List<float>();
		foreach(var val in Regex.Split(rawValue, "[-;/,]+"))
		{
			try
			{
				newValues.Add(float.Parse(val, CultureInfo.InvariantCulture.NumberFormat));
			}
			catch(Exception ex)
			{
				Console.WriteLine("Invalid value entered => \"" + val + "\"\r\n" + ex);
			}
		}
		return newValues;
	}
	
	private static float AverageValue(List<float> newValues)
	{
		return newValues.Count > 0 ? newValues.Sum() / newValues.Count : 0;
	}
	
	private static void RemoveLowestValues(List<float> newValues, int removeLowest)
	{
		newValues.Sort();
		for(var i = 0; i < removeLowest; i++)
		{
			if(newValues.Count > 0)
			{
				newValues.RemoveAt(0);
			}
		}
	}
}
