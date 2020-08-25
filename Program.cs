using System;

namespace ConsoleMinesweeper
{
	class Program
	{
		//The current map size
		public MapSize currentMap = new MapSize();

		//Gain extra tools for runtime
		bool debugMode = true;

		//Entry point
		static void Main()
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();

			Console.WriteLine("Press any button to start");
			Console.ReadKey(true);

			//Instance of class for non-static fields
			Program program = new Program();
			program.Start();
			bool[,] bombMap = program.GenerateBombMap();
			string[,] map = program.GenerateMap(bombMap);
			program.Loop(map, bombMap);

			Main();
		}

		//Start of game menues and options
		void Start()
		{
			//Different map sizes - class object instances
			MapSize Map1 = new MapSize() { x = 10, y = 7 };
			MapSize Map2 = new MapSize() { x = 14, y = 8 };
			MapSize Map3 = new MapSize() { x = 18, y = 9 };

			//Map selection menu
			Console.Clear();
			Console.WriteLine("Available map sizes");
			Console.WriteLine("------------------");
			Console.WriteLine("1 : " + Map1.x + "x" + Map1.y);
			Console.WriteLine("2 : " + Map2.x + "x" + Map2.y);
			Console.WriteLine("3 : " + Map3.x + "x" + Map3.y);
			Console.WriteLine("------------------");
			Console.WriteLine("Choose a map size");
			Console.WriteLine();
			Console.Write("Map : ");

			//Loop until valid input
			while (true)
			{
				bool pressed = true;

				//Wait until console key input is available
				while (!Console.KeyAvailable) { }

				//Get current key info
				ConsoleKeyInfo keyInfo = Console.ReadKey(true);

				//Compare and apply input
				switch (keyInfo.Key)
				{
					case ConsoleKey.D1:
						currentMap = Map1;
						break;

					case ConsoleKey.D2:
						currentMap = Map2;
						break;

					case ConsoleKey.D3:
						currentMap = Map3;
						break;

					default:
						//Keep looping if invalid input
						pressed = false;
						break;
				}

				//Break if map selection complete
				if (pressed)
					break;
			}

			return;
		}

		//Generate a field of mines
		bool[,] GenerateBombMap()
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
						Console.Write("   ");
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

		void Loop(string[,] map, bool[,] bombMap)
		{
			Vector2 selectedPixel = new Vector2()
			{
				x = 0,
				y = 0
			};

			while (true)
			{
				Console.Clear();
				selectedPixel = SelectPixel(map, selectedPixel);
				UpdateMap(map, bombMap, selectedPixel);
			}
		}

		string[,] GenerateMap(bool[,] bombMap)
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

		string[,] UpdateMap(string[,] map, bool[,] bombMap, Vector2 pos)
		{

			return map;
		}

		Vector2 SelectPixel(string[,] map, Vector2 pos)
		{
			while (true)
			{
				Console.Clear();
				//Loop over every pixel
				for (int y = 0; y < currentMap.y; y++)
				{
					Console.WriteLine();
					for (int x = 0; x < currentMap.x; x++)
					{
						string message = "";
						ConsoleColor writeColor = ConsoleColor.White;


						if (debugMode && map[x, y] == "*")
						{
							message = "*";
							writeColor = ConsoleColor.Red;
						}
						else
						{
							message = map[x, y];
						}

						if (x == pos.x && y == pos.y)
						{
							writeColor = ConsoleColor.Green;
						}

						//Write the pixel
						Tools.WriteWithColor(message, writeColor);

						//Distance between chars
						Console.Write("   ");
					}
				}

				//Loop until valid input
				while (true)
				{
					bool pressed = true;

					//Wait until console key input is available
					while (!Console.KeyAvailable) { }

					//Get current key info
					ConsoleKeyInfo keyInfo = Console.ReadKey(true);

					//Compare and apply input
					switch (keyInfo.Key)
					{
						case ConsoleKey.UpArrow:
						case ConsoleKey.W:
							if (pos.y > 0) { pos.y--; }
							break;

						case ConsoleKey.RightArrow:
						case ConsoleKey.D:
							if (pos.x <= currentMap.x - 2) { pos.x++; }
							break;

						case ConsoleKey.DownArrow:
						case ConsoleKey.S:
							if (pos.y <= currentMap.y - 2) { pos.y++; }
							break;

						case ConsoleKey.LeftArrow:
						case ConsoleKey.A:
							if (pos.x > 0) { pos.x--; }
							break;

						case ConsoleKey.Spacebar:
						case ConsoleKey.Enter:
							return pos;

						default:
							//Keep looping if invalid input
							pressed = false;
							break;
					}

					//Break if map selection complete
					if (pressed)
						break;
				}
			}
		}
	}

	//Map size class object
	class MapSize
	{
		public int x;
		public int y;
	}

	//Vector2 class object
	class Vector2
	{
		public int x;
		public int y;
	}
}