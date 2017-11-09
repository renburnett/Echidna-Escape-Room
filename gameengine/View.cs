// Includes the View boxes and their design

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GameEngine
{
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
		private TextBox RoomContents;
		private TextBox StoryText;
		private ViewType type;

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
					areaToEdit = RoomContents;
					RoomContents = new TextBox(areaToEdit.width, areaToEdit.height, inputList, areaToEdit.textColor, areaToEdit.bgColor);
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
				var commandAndTitleHeight = 3;

		// This might be the spot to adjust the console window size
				var windowWidth = VisualRep.width + RoomContents.width;
				var windowHeight = RoomContents.height + commandAndTitleHeight;

				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					Console.WindowWidth = windowWidth;
					Console.WindowHeight = windowHeight;
				}

				//display.Show(Title.PadRight(windowWidth), ConsoleColor.DarkRed, ConsoleColor.White);

				for (var i = 0; i < VisualRep.height; i += 1)
				{
					display.Output(VisualRep.processedTextList[i], VisualRep.textColor, VisualRep.bgColor);
					display.Output(RoomContents.processedTextList[i], RoomContents.textColor, RoomContents.bgColor);
					Console.WriteLine();
				}

				for (var i = 0; i < StoryText.height; i += 1)
				{
					display.Output(StoryText.processedTextList[i], StoryText.textColor, StoryText.bgColor);
					display.Output(RoomContents.processedTextList[i + VisualRep.height], RoomContents.textColor, RoomContents.bgColor);
					Console.WriteLine();
				}

				return display.CommandPrompt();
			}

			display.Show("View Set Up not Supported");
			return "Quit";
		}

		public View(List<string> visRep, List<Item> roomContents, List<string> storyText, 
					ViewType viewType = ViewType.twoStackOneLong )
		{
			if (viewType == ViewType.twoStackOneLong)
			{
				//+--------Title---------------- +
				//+-----------------------------+
				//|               |             |
				//| VisualRep     |             |
				//|               |             |
				//|               | RoomContents|
				//+---------------+             |
				//|               |             |
				//| StoryText     |             |
				//|               |             |
				//+---------------+-------------+
				//+--------------Command-------- +


				VisualRep = new TextBox(60, 15, visRep, 
										textColor:ConsoleColor.Black, bgColor:ConsoleColor.White);
				RoomContents = new TextBox(40, 30, roomContents, 
										textColor:ConsoleColor.White, bgColor:ConsoleColor.DarkCyan);
				StoryText = new TextBox(60, 15, storyText, 
										textColor:ConsoleColor.White, bgColor:ConsoleColor.Black);
				type = viewType;
				//Title = title;
			}
		}
	}
}

public struct TextBox
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


