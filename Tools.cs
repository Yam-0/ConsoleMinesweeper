using System;

//A collection of tools
class Tools
{
	//Console.WriteLine with color
	public static void WriteLineWithColor(string message, ConsoleColor color)
	{
		ConsoleColor temp = Console.ForegroundColor;
		Console.ForegroundColor = color;
		Console.WriteLine(message);
		Console.ForegroundColor = temp;
		return;
	}

	//Console.Write with color
	public static void WriteWithColor(string message, ConsoleColor color)
	{
		ConsoleColor temp = Console.ForegroundColor;
		Console.ForegroundColor = color;
		Console.Write(message);
		Console.ForegroundColor = temp;
		return;
	}

	//Count neighbouring bombs 0-8
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

	//Calls 0-8 neigbouring empty squares
	public static void NeighbourCall(bool[,] bombMap, bool[,] mask, string[,] map, Vector2 pos)
	{
		ConsoleMinesweeper.Program program = new ConsoleMinesweeper.Program();

		if (pos.x >= 0 && pos.x < bombMap.GetLength(0))
		{
			if (pos.y - 1 >= 0 && pos.y - 1 < bombMap.GetLength(1))
			{
				if (!mask[pos.x, pos.y - 1])
					program.UpdateMap(bombMap, mask, map, new Vector2 { x = pos.x, y = pos.y - 1 });
			}
		}

		if (pos.x + 1 >= 0 && pos.x + 1 < bombMap.GetLength(0))
		{
			if (pos.y - 1 >= 0 && pos.y - 1 < bombMap.GetLength(1))
			{
				if (!mask[pos.x + 1, pos.y - 1])
					program.UpdateMap(bombMap, mask, map, new Vector2 { x = pos.x + 1, y = pos.y - 1 });
			}
		}

		if (pos.x + 1 >= 0 && pos.x + 1 < bombMap.GetLength(0))
		{
			if (pos.y >= 0 && pos.y < bombMap.GetLength(1))
			{
				if (!mask[pos.x + 1, pos.y])
					program.UpdateMap(bombMap, mask, map, new Vector2 { x = pos.x + 1, y = pos.y });
			}
		}

		if (pos.x + 1 >= 0 && pos.x + 1 < bombMap.GetLength(0))
		{
			if (pos.y + 1 >= 0 && pos.y + 1 < bombMap.GetLength(1))
			{
				if (!mask[pos.x + 1, pos.y + 1])
					program.UpdateMap(bombMap, mask, map, new Vector2 { x = pos.x + 1, y = pos.y + 1 });
			}
		}

		if (pos.x >= 0 && pos.x < bombMap.GetLength(0))
		{
			if (pos.y + 1 >= 0 && pos.y + 1 < bombMap.GetLength(1))
			{
				if (!mask[pos.x, pos.y + 1])
					program.UpdateMap(bombMap, mask, map, new Vector2 { x = pos.x, y = pos.y + 1 });
			}
		}

		if (pos.x - 1 >= 0 && pos.x - 1 < bombMap.GetLength(0))
		{
			if (pos.y + 1 >= 0 && pos.y + 1 < bombMap.GetLength(1))
			{
				if (!mask[pos.x - 1, pos.y + 1])
					program.UpdateMap(bombMap, mask, map, new Vector2 { x = pos.x - 1, y = pos.y + 1 });
			}
		}

		if (pos.x - 1 >= 0 && pos.x - 1 < bombMap.GetLength(0))
		{
			if (pos.y >= 0 && pos.y < bombMap.GetLength(1))
			{
				if (!mask[pos.x - 1, pos.y])
					program.UpdateMap(bombMap, mask, map, new Vector2 { x = pos.x - 1, y = pos.y });
			}
		}

		if (pos.x - 1 >= 0 && pos.x - 1 < bombMap.GetLength(0))
		{
			if (pos.y - 1 >= 0 && pos.y - 1 < bombMap.GetLength(1))
			{
				if (!mask[pos.x - 1, pos.y - 1])
					program.UpdateMap(bombMap, mask, map, new Vector2 { x = pos.x - 1, y = pos.y - 1 });
			}
		}

		return;
	}
}