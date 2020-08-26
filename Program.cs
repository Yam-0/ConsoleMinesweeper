using System;

namespace ConsoleMinesweeper
{
	public class Program
	{
		//Some settings
		public MapSize currentMap = new MapSize();
		public int bombs;
		public bool debugMode = true;

		string charOffset = "   ";

		//Entry point
		static void Main()
		{
			Console.Title = "MINESWEEPER";
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();

			Console.WriteLine("Press any button to start");
			Console.ReadKey(true);

			//Instance of class for non-static fields
			Program program = new Program();
			program.Start();
			bool[,] bombMap = Generate.GenerateBombMap(program.currentMap, program.bombs, program.charOffset);
			string[,] map = Generate.GenerateMap(bombMap, program.currentMap, program.debugMode);
			bool[,] viewMask = Generate.GenerateViewMask(program.currentMap);
			bool[,] selectionMask = Generate.GenerateSelectMask(program.currentMap);
			program.Loop(map, bombMap, viewMask, selectionMask);

			Main();
		}

		//Start of game menues and options
		void Start()
		{
			//Different map sizes - class object instances
			MapSize map0 = new MapSize() { x = 10, y = 7 };
			MapSize map1 = new MapSize() { x = 14, y = 8 };
			MapSize map2 = new MapSize() { x = 18, y = 9 };
			MapSize map3 = new MapSize() { x = 27, y = 14 };

			//Build array with objects
			MapSize[] maps = new MapSize[]{
				map0, map1, map2, map3
			};

			//Different bomb settings - integers
			int[] bombAmounts = new int[]{
				7, 15, 35, 55
			};

			//Map selection menu
			Console.Clear();
			Console.WriteLine("Available map sizes");
			Console.WriteLine("------------------");
			for (int i = 0; i < maps.Length; i++)
			{
				Console.WriteLine("Size " + (i + 1) + "  (" + maps[i].x + ", " + maps[i].y + ")");
			}
			Console.WriteLine("------------------");
			Console.WriteLine("Choose a map size");
			Console.WriteLine();

			//Apply selection
			currentMap = maps[Select()];

			//Bomb number selection menu
			Console.WriteLine("Available bomb amounts");
			Console.WriteLine("------------------");
			for (int i = 0; i < bombAmounts.Length; i++)
			{
				Console.WriteLine("Amount " + (i + 1) + "  (" + bombAmounts[i] + ")");
			}
			Console.WriteLine("------------------");
			Console.WriteLine("Choose a map size");
			Console.WriteLine();

			//Apply selection
			bombs = bombAmounts[Select()];

			Console.Clear();
			return;
		}

		private int Select()
		{
			int index = 0;

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
						index = 0;
						break;

					case ConsoleKey.D2:
						index = 1;
						break;

					case ConsoleKey.D3:
						index = 2;
						break;

					case ConsoleKey.D4:
						index = 3;
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

			return index;
		}

		//The game loop
		void Loop(string[,] map, bool[,] bombMap, bool[,] viewMask, bool[,] selectionMask)
		{
			bool[,] a = new bool[1000, 1000];
			Vector2 zero = new Vector2() { x = 0, y = 0 };

			PlaceData selectedPixel = new PlaceData();
			selectedPixel.pos = zero;
			selectedPixel.place = false;

			while (true)
			{
				Console.Clear();
				selectedPixel = SelectPixel(map, selectedPixel.pos, viewMask, selectionMask);

				if (selectedPixel.place)
				{
					if (selectedPixel.place && !selectionMask[selectedPixel.pos.x, selectedPixel.pos.y])
					{
						viewMask = UpdateMap(bombMap, viewMask, map, selectedPixel.pos);
					}
				}
				else
				{
					if (selectionMask[selectedPixel.pos.x, selectedPixel.pos.y])
					{
						selectionMask[selectedPixel.pos.x, selectedPixel.pos.y] = false;
					}
					else
					{
						selectionMask[selectedPixel.pos.x, selectedPixel.pos.y] = true;
					}
				}

				int squares = currentMap.x * currentMap.y;
				int count = 0;

				for (int y = 0; y < currentMap.y; y++)
				{
					for (int x = 0; x < currentMap.x; x++)
					{
						if (viewMask[x, y]) { count++; }
					}
				}

				if (squares - count == bombs)
				{
					Win();
				}
			}
		}

		//Updates the map - the viewable 2d array
		public bool[,] UpdateMap(bool[,] bombMap, bool[,] viewMask, string[,] map, Vector2 pos)
		{
			viewMask[pos.x, pos.y] = true;
			if (bombMap[pos.x, pos.y]) { GameOver(); }


			if (map[pos.x, pos.y] == "'")
			{
				Tools.NeighbourCall(bombMap, viewMask, map, pos);
			}

			return viewMask;
		}

		//Method to select a position in 2d array
		PlaceData SelectPixel(string[,] map, Vector2 pos, bool[,] viewMask, bool[,] selectionMask)
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

						if (selectionMask[x, y] && map[x, y] != "■")
						{
							writeColor = ConsoleColor.Red;
						}
						else
						{
							selectionMask[x, y] = false;
						}

						if (viewMask[x, y])
						{
							message = map[x, y];
							if (message != "'")
							{
								writeColor = ConsoleColor.Blue;
							}
						}
						else
						{
							message = "■";
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
							PlaceData placeData0 = new PlaceData();
							placeData0.pos = pos;
							placeData0.place = true;
							return placeData0;

						case ConsoleKey.Tab:
							PlaceData placeData1 = new PlaceData();
							placeData1.pos = pos;
							placeData1.place = false;
							return placeData1;

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

		//Game over lose state
		void GameOver()
		{
			Console.Clear();
			Console.WriteLine("Game Over");
			Console.WriteLine();
			Console.WriteLine("Press any button to continue");
			Console.ReadKey(true);
			Main();
		}

		//Game over win state
		void Win()
		{
			Console.Clear();
			Console.WriteLine("You Won!");
			Console.WriteLine();
			Console.WriteLine("Map size 	: (" + currentMap.x + ", " + currentMap.y + ")");
			Console.WriteLine("Bombs 		: (" + bombs + ")");
			Console.WriteLine();
			Console.WriteLine("Press any button to continue");
			Console.ReadKey(true);
			Main();
		}
	}
}