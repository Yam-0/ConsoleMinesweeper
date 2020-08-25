using System;

//A collection of debug tools
class Tools
{
	public static void WriteLineWithColor(string message, ConsoleColor color)
	{
		ConsoleColor temp = Console.ForegroundColor;
		Console.ForegroundColor = color;
		Console.WriteLine(message);
		Console.ForegroundColor = temp;
		return;
	}

	public static void WriteWithColor(string message, ConsoleColor color)
	{
		ConsoleColor temp = Console.ForegroundColor;
		Console.ForegroundColor = color;
		Console.Write(message);
		Console.ForegroundColor = temp;
		return;
	}

	public static int CountBombs(bool[,] bombMap, int x, int y)
	{
		int i = 0;

		if (x >= 0 && x < bombMap.GetLength(0))
		{
			if (y - 1 >= 0 && y - 1 < bombMap.GetLength(1))
			{
				if (bombMap[x, y - 1]) { i++; }
			}
		}

		if (x + 1 >= 0 && x + 1 < bombMap.GetLength(0))
		{
			if (y - 1 >= 0 && y - 1 < bombMap.GetLength(1))
			{
				if (bombMap[x + 1, y - 1]) { i++; }
			}
		}

		if (x + 1 >= 0 && x + 1 < bombMap.GetLength(0))
		{
			if (y >= 0 && y < bombMap.GetLength(1))
			{
				if (bombMap[x + 1, y]) { i++; }
			}
		}

		if (x + 1 >= 0 && x + 1 < bombMap.GetLength(0))
		{
			if (y + 1 >= 0 && y + 1 < bombMap.GetLength(1))
			{
				if (bombMap[x + 1, y + 1]) { i++; }
			}
		}

		if (x >= 0 && x < bombMap.GetLength(0))
		{
			if (y + 1 >= 0 && y + 1 < bombMap.GetLength(1))
			{
				if (bombMap[x, y + 1]) { i++; }
			}
		}

		if (x - 1 >= 0 && x - 1 < bombMap.GetLength(0))
		{
			if (y + 1 >= 0 && y + 1 < bombMap.GetLength(1))
			{
				if (bombMap[x - 1, y + 1]) { i++; }
			}
		}

		if (x - 1 >= 0 && x - 1 < bombMap.GetLength(0))
		{
			if (y >= 0 && y < bombMap.GetLength(1))
			{
				if (bombMap[x - 1, y]) { i++; }
			}
		}

		if (x - 1 >= 0 && x - 1 < bombMap.GetLength(0))
		{
			if (y - 1 >= 0 && y - 1 < bombMap.GetLength(1))
			{
				if (bombMap[x - 1, y - 1]) { i++; }
			}
		}

		return i;

	}
}