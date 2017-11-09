// Includes the View boxes and their design

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using Console = Colorful.Console;

namespace ReferenceGame
{
	// TODO: Remove this ViewType enum
	public enum ViewType
	{
		twoStackOneLong
	}

	public enum TextBoxArea
	{
		VisualRepresentation,
		RoomContents,
		StoryText,
		CommandText
	}

	public class View
	{
		private Display display = new Display();

		//private String Title;
		private TextBox VisualRep;
		private TextBox TBRoomContents;
		private TextBox StoryText;
		private ViewType type; // REMOVE

		//public void SetTitle(string input)
		//{
		//    Title = input;
		//}

		public void SetArea(string input, TextBoxArea area)
		{
			var wrappedText = Display.Wrap(input, VisualRep.width);
			SetArea(wrappedText, area);
		}

		public void SetArea(List<String> inputList, TextBoxArea area)
		{
			TextBox areaToEdit;

			switch (area) 
			{
				case TextBoxArea.VisualRepresentation:
					areaToEdit = VisualRep;
					VisualRep = new TextBox(areaToEdit.width, areaToEdit.height, inputList, areaToEdit.textColor, areaToEdit.bgColor);
					break;
				case TextBoxArea.RoomContents:
					areaToEdit = TBRoomContents;
					TBRoomContents = new TextBox(areaToEdit.width, areaToEdit.height, inputList, areaToEdit.textColor, areaToEdit.bgColor);
					break;
				case TextBoxArea.StoryText:
					areaToEdit = StoryText;
					StoryText = new TextBox(areaToEdit.width, areaToEdit.height, inputList, areaToEdit.textColor, areaToEdit.bgColor);
					break;
				default:
					areaToEdit = new TextBox(10, 10, new List<string>(), ConsoleColor.Red, ConsoleColor.White);
					break;
			}
		}
		
		public string UpdateScreenAndGetInput()
		{
			Console.Clear();
			if (type == ViewType.twoStackOneLong) {
				int commandAndTitleHeight = 3;

		// This might be the spot to adjust the console window size
				int windowWidth = VisualRep.width + TBRoomContents.width;
				int windowHeight = TBRoomContents.height + commandAndTitleHeight;

				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					Console.WindowWidth = windowWidth;
					Console.WindowHeight = windowHeight;
				}

				//display.Show(Title.PadRight(windowWidth), ConsoleColor.DarkRed, ConsoleColor.White);

				for (var i = 0; i < VisualRep.height; i += 1)
				{
					display.Output(VisualRep.processedTextList[i], VisualRep.textColor, VisualRep.bgColor);
					display.Output(TBRoomContents.processedTextList[i], TBRoomContents.textColor, TBRoomContents.bgColor);
					Console.WriteLine();
				}

				for (var i = 0; i < StoryText.height; i += 1)
				{
					display.Output(StoryText.processedTextList[i], StoryText.textColor, StoryText.bgColor);
					display.Output(TBRoomContents.processedTextList[i + VisualRep.height], TBRoomContents.textColor, TBRoomContents.bgColor);
					Console.WriteLine();
				}

				return display.CommandPrompt();
			}

			display.Show("View Set Up not Supported");
			return "Quit";
		}

		public View(List<string> visRep, List<string> roomContents, List<string> storyText, 
					ViewType viewType = ViewType.twoStackOneLong )
		{

			if (viewType == ViewType.twoStackOneLong)
			{
                //+--------Title----------------- +
                //+-------------------------------+
                //|               |               |
                //| VisualRep     |               |
                //|               |               |
                //|               | TBRoomContents|
                //+---------------+               |
                //|               |               |
                //| StoryText     |               |
                //|               |               |
                //+---------------+---------------+
                //+--------------Command-------- +

                int colOne = (int) Math.Round((Console.LargestWindowWidth - 10) * .75);
                int colTwo = (int)Math.Round((Console.LargestWindowWidth - 10) * .25);
                int largestHeight = Console.LargestWindowHeight - 10;
                int rowOne = (int)Math.Round(largestHeight * .65);
                int rowTwo = (int)Math.Round(largestHeight * .35);

				VisualRep = new TextBox(75, 20, visRep, 
										textColor:ConsoleColor.Black, bgColor:ConsoleColor.White);
				TBRoomContents = new TextBox(25, 32, roomContents, 
										textColor:ConsoleColor.White, bgColor:ConsoleColor.DarkCyan);
				StoryText = new TextBox(75, 12, storyText, 
										textColor:ConsoleColor.White, bgColor:ConsoleColor.Black);
				type = viewType;
				//Title = title;
			}
		}
	}
}

public class TextBox
{
	public int width;
	public int height;
	public List<string> processedTextList;
	public List<String> sourceText;
	public ConsoleColor textColor;
	public ConsoleColor bgColor;

	public TextBox(int width, int height, List<string> wrappedTextList, 
					ConsoleColor textColor, ConsoleColor bgColor)
	{
		var processedText = new List<String>();
		for (var i = 0; i < height; i += 1)
		{
			if(i > wrappedTextList.Count - 1)
			{
				processedText.Add("".PadRight(width));
			} else 
			{
				processedText.Add(wrappedTextList[i].PadRight(width));
			}
		}
		this.width = width;
		this.height = height;
		processedTextList = processedText;
		sourceText = wrappedTextList;
		this.textColor = textColor;
		this.bgColor = bgColor;
	}
}


