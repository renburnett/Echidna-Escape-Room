﻿using System;
using System.Collections.Generic;

namespace EchidnaEscape_01
{
	public class Display
	{
		private void WriteLine(string text)
		{
			Console.WriteLine(text);
		}

		private void Write(string text)
		{
			Console.Write(text);
		}

		// Is this just for showing the room name? If so, let's delete it
		public void Show(string text, 
						ConsoleColor txColor = ConsoleColor.White, 
						ConsoleColor bg = ConsoleColor.Black)
		{
			Console.BackgroundColor = bg;
			Console.ForegroundColor = txColor;
			WriteLine(text);
			Console.ResetColor();
		}

		public void Show(List<string> textList, 
						 ConsoleColor txColor = ConsoleColor.Green, 
						 ConsoleColor bg = ConsoleColor.Black)
		{
			string temp = "";
			foreach (string key in textList)
			{
				temp += key;
				temp += Environment.NewLine;
			}
			Show(temp, txColor, bg);
		}

		public void Output(string text, 
							ConsoleColor fg = ConsoleColor.White, 
							ConsoleColor bg = ConsoleColor.Black)
		{
			Console.BackgroundColor = bg;
			Console.ForegroundColor = fg;
			Write(text);
			Console.ResetColor();
		}
		
		public string CommandPrompt(string promptText = "~:// ")
		{
			Output(promptText);
			return Console.ReadLine();

		}

		public static List<string> Wrap(string str, int maxWidth, int upperMargin = 1, int lowerMargin = 0)
		{
			List<string> output = new List<string>();
			str = new string(' ', upperMargin) + str;
			string lower = new string(' ', lowerMargin);
			while (str.Substring(str.Length - 1) == "\n") str = str.Substring(0, str.Length - 1);
			while (str.Length > maxWidth || str.IndexOf("\n") != -1 || str[0] == '\n')
			{
				int lineBreak = str.IndexOf("\n");
                int lastSpace = str.Substring(0, maxWidth).LastIndexOf(" ");
				int space;
				if (lineBreak == 0)
				{
					output.Add("");
					str = new string(' ', upperMargin) + str.Substring(1);
					lastSpace = str.Substring(0, maxWidth).LastIndexOf(" ");
				}
				if (lineBreak > 0 && lineBreak < maxWidth) space = lineBreak;
				else if (lastSpace > upperMargin && lastSpace < maxWidth) space = lastSpace;
				else space = maxWidth - 1;
				output.Add(str.Substring(0, space).PadRight(maxWidth));
				str = str.Substring(space);
				if (space < str.Length && str[0] != '\n') str = new string(' ', lowerMargin) + str;
			}
			output.Add(str.PadRight(maxWidth));
			return output;
		}

		public static List<string> VisualWrap(string str, int maxWidth, int upperMargin = 1, int lowerMargin = 0)
		{
			List<string> output = new List<string>();
			str = new string(' ', upperMargin) + str;
			string lower = new string(' ', lowerMargin);
			//while (str.Substring(str.Length - 1) == "\n") str = str.Substring(0, str.Length - 1);
			while (str.Length > maxWidth || str.IndexOf("\n") != -1 || str[0] == '\n')
			{
				int lineBreak = str.IndexOf("\n");

				int lastSpace = str.Substring(0, maxWidth).LastIndexOf(" ");
				int space;
				if (lineBreak == 0)
				{
					output.Add("");
					str = new string(' ', upperMargin) + str.Substring(1);
					lastSpace = str.Substring(0, maxWidth).LastIndexOf(" ");
				}
				if (lineBreak > 0 && lineBreak < maxWidth) space = lineBreak;
				else if (lastSpace > upperMargin && lastSpace < maxWidth) space = lastSpace;
				else space = maxWidth - 1;
				output.Add(str.Substring(0, space).PadRight(maxWidth));
				str = str.Substring(space);
				if (space < str.Length && str[0] != '\n') str = new string(' ', lowerMargin) + str;
			}
			output.Add(str.PadRight(maxWidth));
			return output;
		}

	}
}

