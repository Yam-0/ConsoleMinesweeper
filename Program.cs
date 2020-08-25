using System;

namespace ConsoleMinesweeper
{
	public class Program
	{
		//The current map size
		public MapSize currentMap = new MapSize();

		//Gain extra tools for runtime
		public bool debugMode = true;

		string charOffset = "   ";

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
			bool[,] bombMap = Generate.GenerateBombMap(program.currentMap, program.debugMode, program.charOffset);
			string[,] map = Generate.GenerateMap(bombMap, program.currentMap, program.debugMode);
			bool[,] mask = Generate.GenerateMask(program.currentMap);
			program.Loop(map, bombMap, mask);

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

				//Break if any valid button pressed
				if (pressed)
					break;
			}

			return;
		}

		//The game loop
		void Loop(string[,] map, bool[,] bombMap, bool[,] mask)
		{
			Vector2 selectedPixel = new Vector2()
			{
				x = 0,
				y = 0
			};

			while (true)
			{
				Console.Clear();
				selectedPixel = SelectPixel(map, selectedPixel, mask);
				mask = UpdateMap(bombMap, mask, map, selectedPixel);
			}
		}

		//Updates the map - the viewable 2d array
		public bool[,] UpdateMap(bool[,] bombMap, bool[,] mask, string[,] map, Vector2 pos)
		{
			mask[pos.x, pos.y] = true;
			if (bombMap[pos.x, pos.y]) { GameOver(); }

			if (map[pos.x, pos.y] == "'")
			{
				Tools.NeighbourCall(bombMap, mask, map, pos);
			}

			return mask;
		}

		//Method to select a position in 2d array
		Vector2 SelectPixel(string[,] map, Vector2 pos, bool[,] mask)
		{
			//Loop until selected pixel
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
						if (mask[x, y])
						{
							message = map[x, y];
						}
						else
						{
							//message = "█";
							message = "X";
						}

						if (debugMode && map[x, y] == "*")
						{
							message = "*";
							writeColor = ConsoleColor.Red;
						}

						if (x == pos.x && y == pos.y) { writeColor = ConsoleColor.Green; }

						//Write the pixel
						Tools.WriteWithColor(message, writeColor);

						//Distance between chars
						Console.Write(charOffset);
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

					//Break if any valid button pressed
					if (pressed)
						break;
				}
			}
		}

		//Game over state
		void GameOver()
		{
			Console.Clear();
			Console.WriteLine("Game Over");
			Console.WriteLine();
			Console.WriteLine("Press any button to continue");
			Console.ReadKey(true);
			Main();
		}
	}
}