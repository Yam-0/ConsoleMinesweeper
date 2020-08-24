using System;

namespace ConsoleMinesweeper
{
	class Program
	{
		int gameWidth;
		int gameHeight;

		int mapSizeIndex;

		bool debugMode = true;

		static void Main()
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();

			Console.WriteLine("Press enter to continue");
			Console.ReadLine();

			Program program = new Program();
			program.Start();
			bool[,] bombMap = program.Generate();
			program.Loop();
		}

		void Start()
		{
			Console.Clear();
			Console.WriteLine("Choose game size");
			Console.WriteLine();
			Console.WriteLine("1 : 9x7");
			Console.WriteLine("2 : 12x8");
			Console.WriteLine("3 : 16x9");
			Console.WriteLine();
			Console.WriteLine("Write a number");

			string input = Console.ReadLine();
			switch (input)
			{
				case "1":
					gameWidth = 9;
					gameHeight = 7;
					mapSizeIndex = 1;
					break;
				case "2":
					gameWidth = 12;
					gameHeight = 8;
					mapSizeIndex = 2;
					break;
				case "3":
					gameWidth = 16;
					gameHeight = 9;
					mapSizeIndex = 3;
					break;

				default:
					Start();
					break;
			}
			return;
		}

		bool[,] Generate()
		{
			bool[,] bombMap = new bool[gameWidth, gameHeight];
			for (int i = 0; i < gameWidth; i++)
			{
				for (int j = 0; j < gameHeight; j++)
				{
					Random random = new Random();
					Boolean bomb = false;

					if (random.Next(0, 5) == 0)
					{
						bomb = true;
					}
					bombMap[i, j] = bomb;
				}
			}

			if (debugMode)
			{
				Console.Clear();
				Console.WriteLine("Debug window");
				for (int i = 0; i < gameWidth; i++)
				{
					Console.WriteLine();
					for (int j = 0; j < gameHeight; j++)
					{
						if (bombMap[i, j] == true)
						{
							Console.Write("*");
						}
						else
						{
							Console.Write("0");
						}
						Console.Write("   ");
					}
				}

				Console.WriteLine();
				Console.ReadLine();
			}

			Console.Clear();
			Console.WriteLine("Press enter to continue");
			Console.ReadLine();
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
}