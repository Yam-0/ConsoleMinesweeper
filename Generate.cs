using System;

class Generate
{
	//Generate underlaying map layer
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
	public static bool[,] GenerateBombMap(MapSize currentMap, int bombCount, string charOffset)
	{
		//Declare 2d array of booleans
		bool[,] bombMap = new bool[currentMap.x, currentMap.y];
		int placedBombs = 0;

		while (placedBombs < bombCount)
		{
			Random random = new Random();

			int x = random.Next(0, currentMap.x);
			int y = random.Next(0, currentMap.y);

			if (!bombMap[x, y])
			{
				placedBombs++;
				bombMap[x, y] = true;
			}
		}

		//Clear and continue
		return bombMap;
	}

	//Generate empty view mask
	public static bool[,] GenerateViewMask(MapSize currentMap)
	{
		bool[,] mask = new bool[currentMap.x, currentMap.y];

		//Loop over every pixel and set it to false
		for (int y = 0; y < currentMap.y; y++)
		{
			for (int x = 0; x < currentMap.x; x++)
			{
				mask[x, y] = false;
			}
		}

		return mask;
	}

	//Generate empty select mask
	public static bool[,] GenerateSelectMask(MapSize currentMap)
	{
		bool[,] slectionMask = new bool[currentMap.x, currentMap.y];

		//Loop over every pixel and set it to false
		for (int y = 0; y < currentMap.y; y++)
		{
			for (int x = 0; x < currentMap.x; x++)
			{
				slectionMask[x, y] = false;
			}
		}

		return slectionMask;
	}
}