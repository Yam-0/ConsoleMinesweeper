using System;

//A collection of debug tools
class Debug
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
}