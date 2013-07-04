using System;
using System.Collections.Specialized;

namespace DoubleFish
{
	public static class CString
	{
		public static sbyte ToInt8 (this string input)
		{
			return input.ToInt8(sbyte.MinValue);
		}

		public static sbyte ToInt8 (this string input, sbyte error)
		{
			sbyte num;
			if (sbyte.TryParse(input, out num))
				return num;

			return error;
		}

		public static short ToInt16 (this string input)
		{
			return input.ToInt16(short.MinValue);
		}

		public static short ToInt16 (this string input, short error)
		{
			short num;
			if (short.TryParse(input, out num))
				return num;

			return error;
		}

		public static int ToInt32 (this string input)
		{
			return input.ToInt32(int.MinValue);
		}

		public static int ToInt32 (this string input, int error)
		{
			int num;

			if (int.TryParse(input, out num))
				return num;

			return error;
		}

		public static int[] ToInt32 (this string[] array)
		{
			if (array == null)
				return null;

			int[] result = new int[array.Length];

			for (int i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToInt32(0);
			}
			return result;
		}

		public static long ToInt64 (this string input)
		{
			return input.ToInt64(long.MinValue);
		}

		public static long[] ToInt64 (this string[] array)
		{
			if (array == null)
				return null;

			long[] result = new long[array.Length];

			for (int i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToInt64(0L);
			}
			return result;
		}

		public static long ToInt64 (this string input, long error)
		{
			long num;

			if (long.TryParse(input, out num))
				return num;

			return error;
		}

		public static decimal ToDecimal (this string input)
		{
			return ToDecimal(input, decimal.MinValue);
		}

		public static decimal ToDecimal (this string input, decimal error)
		{
			decimal num;

			if (decimal.TryParse(input, out num))
				return num;

			return error;
		}

		public static double ToDouble (this string input)
		{
			return ToDouble(input, double.MinValue);
		}

		public static double ToDouble (this string input, double error)
		{
			double num;

			if (double.TryParse(input, out num))
				return num;

			return error;
		}

		public static float ToSingle (this string input)
		{
			return ToSingle(input, float.MinValue);
		}

		public static float ToSingle (this string input, float error)
		{
			float num;

			if (float.TryParse(input, out num))
				return num;

			return error;
		}

		public static DateTime ToDateTime (this string input)
		{
			return ToDateTime(input, DateTime.MinValue);
		}

		public static DateTime ToDateTime (this string input, DateTime error)
		{
			DateTime time;

			if (DateTime.TryParse(input, out time))
				return time;

			return error;
		}

		public static TimeSpan ToTimeSpan (this string input)
		{
			return ToTimeSpan(input, TimeSpan.MinValue);
		}

		public static TimeSpan ToTimeSpan (this string input, TimeSpan error)
		{
			TimeSpan span;

			if (TimeSpan.TryParse(input, out span))
				return span;

			return error;
		}
	}
}
