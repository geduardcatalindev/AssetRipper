﻿namespace AssetRipper.Core.Extensions
{
	public static class ArrayExtensions
	{
		public static bool IsNullOrEmpty<T>(this T[]? array) => array is null || array.Length == 0;
	}
}
