using System;

class Generate
{
	public static string[,] GenerateMap(bool[,] bombMap, MapSize currentMap, bool debugMode)
	{
		/*
		ConsoleMinesweeper.Program program = new ConsoleMinesweeper.Program();

		MapSize currentMap = program.currentMap;
		bool debugMode = program.debugMode;
		*/

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
}