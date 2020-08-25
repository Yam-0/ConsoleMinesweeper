using System;

class Generate
{
	public static string[,] GenerateMap(bool[,] bombMap, MapSize currentMap, bool debugMode)
	{
		//Instantiate new map instance
		string[,] map = new string[currentMap.x, currentMap.y];

		//Loop over every pixel
		for (int y = 0; y < currentMap.y; y++)
		{
			for (int x = 0; x < currentMap.x; x++)
			{
				//Counts bombs around pixel
				int bombNumber = Tools.CountBombs(bombMap, x, y);

				//Never draw zero
				if (bombNumber == 0)
				{
					map[x, y] = "'";
				}
				else
				{
					map[x, y] = bombNumber.ToString();
				}

				//Show bombs if in debug mode
				if (debugMode)
				{
					if (bombMap[x, y])
					{
						map[x, y] = "*";
					}
				}
			}
		}

		return map;
	}

	//Generate a field of mines
	public static bool[,] GenerateBombMap(MapSize currentMap, bool debugMode, string charOffset)
	{
		//Declare 2d array of booleans
		bool[,] bombMap = new bool[currentMap.x, currentMap.y];

		//Loop over every pixel
		for (int y = 0; y < currentMap.y; y++)
		{
			for (int x = 0; x < currentMap.x; x++)
			{
				//50% chance for every square to contain a bomb
				Random random = new Random();
				Boolean bomb = false;

				if (random.Next(0, 5) == 0)
				{
					bomb = true;
				}
				bombMap[x, y] = bomb;
			}
		}

		//Show the bombMap to the player if debug mode is on
		if (debugMode)
		{
			Console.Clear();

			Tools.WriteLineWithColor("Debug window", ConsoleColor.Red);

			//Loop through squares and draw bombs
			for (int y = 0; y < currentMap.y; y++)
			{
				Console.WriteLine();
				for (int x = 0; x < currentMap.x; x++)
				{
					//Draw bombs and empty squares
					if (bombMap[x, y] == true)
					{
						Console.Write("*");
					}
					else
					{
						Console.Write("'");
					}

					//distance between chars
					Console.Write(charOffset);
				}
			}

			Console.WriteLine();
			Console.WriteLine();
			Tools.WriteLineWithColor("Press any button to continue", ConsoleColor.Red);
			Console.ReadKey(true);
		}

		//Clear and continue
		Console.Clear();
		return bombMap;
	}
}