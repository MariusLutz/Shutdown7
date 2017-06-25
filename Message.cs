using System;
using System.Threading;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Shutdown7
{
	class Message
	{
		public static void Show(double Text)
		{
			Show(Text.ToString());
		}

		public static void Show(bool Text)
		{
			Show(Text.ToString());
		}
		
		public static void Show(string Text)
		{
			_Show(Text, "Shutdown7", "");
		}

		public static void Show(string Text, string Icon)
		{
			_Show(Text, "Shutdown7", Icon);
		}

		public static void Show(string Text, string Title, string Icon)
		{
			_Show(Text, Title, Icon);
		}


		static void _Show(string Text, string Title, string Icon)
		{
			try
			{
				TaskDialog td = new TaskDialog();
				td.Caption = "Shutdown7";
				td.InstructionText = Title;
				td.Text = Text;
				switch (Icon)
				{
					case "Information":
						td.Icon = TaskDialogStandardIcon.Information;
						break;
					case "Warning":
						td.Icon = TaskDialogStandardIcon.Warning;
						break;
					case "Error":
						td.Icon = TaskDialogStandardIcon.Error;
						break;
					case "Shield":
						td.Icon = TaskDialogStandardIcon.Shield;
						break;
                    default:
                        td.Icon = TaskDialogStandardIcon.None;
                        break;
				}
				td.Show();
			}
			catch// (NotSupportedException)
			{
				MessageBoxImage icon;
				switch (Icon)
				{
					case "Information":
						icon = MessageBoxImage.Information;
						break;
					case "Warning":
						icon = MessageBoxImage.Warning;
						break;
					case "Error":
						icon = MessageBoxImage.Error;
						break;
					default:
						icon = MessageBoxImage.None;
						break;
				}
				MessageBox.Show(Text, Title, MessageBoxButton.OK, icon);
			}
		}
	}
}
