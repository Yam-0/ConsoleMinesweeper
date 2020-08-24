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

			Console.WriteLine("Press any button to continue");
			Console.ReadKey(true);

			//Instance of class for non-static fields
			Program program = new Program();
			program.Start();
			bool[,] bombMap = program.Generate();
			program.Loop();

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
		bool[,] Generate()
		{
			//Declare 2d array of booleans
			bool[,] bombMap = new bool[currentMap.x, currentMap.y];

			//Loop over every pixel
			for (int x = 0; x < currentMap.x; x++)
			{
				for (int y = 0; y < currentMap.y; y++)
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

				Debug.WriteLineWithColor("Debug window", ConsoleColor.Red);

				//Loop through squares and draw bombs
				for (int x = 0; x < currentMap.x; x++)
				{
					Console.WriteLine();
					for (int y = 0; y < currentMap.y; y++)
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
				Debug.WriteLineWithColor("Press any button to continue", ConsoleColor.Red);
				Console.ReadKey(true);
			}

			//Clear and continue
			Console.Clear();
			return bombMap;
		}

		void Loop()
		{

		}

		string[,] UpdateMap(string[,] map, int x, int y)
		{
			return map;
		}
	}

	//Map size class object
	class MapSize
	{
		public int x;
		public int y;
	}
}