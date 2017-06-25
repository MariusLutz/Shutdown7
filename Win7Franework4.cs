using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;

namespace Shutdown7
{
	class Win7Framework4
	{
		public static JumpList jumpList;
		private static TaskbarItemInfo taskbar = new TaskbarItemInfo();

		public static bool IsWin7
		{
			get
			{
				//return TaskbarManager.IsPlatformSupported;
				return Environment.OSVersion.Version >= new Version(6, 1); 
			}
		}

		public static bool IsWin8
		{
			get
			{
				//return TaskbarManager.IsPlatformSupported;
				return Environment.OSVersion.Version >= new Version(6, 2);
			}
		}

		#region Taskbar
		public static void Progress(int cur)
		{
			if (!IsWin7)
				return;

			taskbar.ProgressValue = (double)(cur / 100);
		}

		public static void Progress(int cur, int max)
		{
			if (!IsWin7)
				return;

			taskbar.ProgressValue = (double)(cur / max);
		}

		public static void ProgressType(string Type)
		{
			if (!IsWin7) return;
			switch (Type)
			{
				case "Normal":
					taskbar.ProgressState = TaskbarItemProgressState.Normal;
					break;
				case "Paused":
					taskbar.ProgressState = TaskbarItemProgressState.Paused;
					break;
				case "Error":
					taskbar.ProgressState = TaskbarItemProgressState.Error;
					break;
				case "Indeterminate":
					taskbar.ProgressState = TaskbarItemProgressState.Indeterminate;
					break;
				default:
					taskbar.ProgressState = TaskbarItemProgressState.None;
					break;
			}

		}

		public static void Overlay(System.Drawing.Icon Icon, string Title)
		{
			if (!IsWin7)
				return;

			try
			{
				object resource = Application.Current.FindResource(Title);
				taskbar.Overlay = (DrawingImage)resource;
			}
			catch (ResourceReferenceKeyNotFoundException ex)
			{
				MessageBox.Show("Resource not found.");
				taskbar.Overlay = null;
			}

		}

		public static void ThumbnailToolbar(String Command, String Description, String Image)
		{
			if (!Win7.IsWin7)
				return;

			ThumbButtonInfo Button = new ThumbButtonInfo();

			//Button.Command = Command;
			Button.Description = Description;
			Button.ImageSource = (DrawingImage) Application.Current.FindResource(Image);

			taskbar.ThumbButtonInfos.Add(Button);
		}

		public static void ThumbnailToolbar(IntPtr handle, ThumbButtonInfo Button)
		{
			if (!Win7.IsWin7)
				return;

			//Button.ImageSource = ;

			taskbar.ThumbButtonInfos.Add(Button);
		}

		public static void Jumplist()
		{
			if (!Win7.IsWin7)
				return;

			try
			{
				//jumplist = JumpList.GetJumpList(App.Current);
				//jumplist.JumpItems.Clear();

				JumpTask jumpTask1 = new JumpTask();

				jumpTask1.ApplicationPath = Data.EXE;
				jumpTask1.IconResourcePath = Data.EXE;
				jumpTask1.IconResourceIndex = 1;
				jumpTask1.Arguments = "-s";

				jumpList.JumpItems.Add(jumpTask1);

				JumpList.SetJumpList(App.Current, jumpList);

				/*jumplist.AddUserTasks(new JumpListLink(Data.EXE, Data.L["Restart"]) { IconReference = new IconReference(Data.EXE, 2), Arguments = "-r" });
				jumplist.AddUserTasks(new JumpListLink(Data.EXE, Data.L["Logoff"]) { IconReference = new IconReference(Data.EXE, 3), Arguments = "-l" });
				jumplist.AddUserTasks(new JumpListLink(Data.EXE, Data.L["Lock"]) { IconReference = new IconReference(Data.EXE, 3), Arguments = "-lo" });
				jumplist.AddUserTasks(new JumpListLink(Data.EXE, Data.L["Standby"]) { IconReference = new IconReference(Data.EXE, 4), Arguments = "-sb" });
				jumplist.AddUserTasks(new JumpListLink(Data.EXE, Data.L["Hibernate"]) { IconReference = new IconReference(Data.EXE, 4), Arguments = "-h" });
				if (Data.S["WOSB"])
				{
					jumplist.AddUserTasks(new JumpListSeparator());
					jumplist.AddUserTasks(new JumpListLink(Data.EXE, Data.L["HibernateWOSBIni"]) { IconReference = new IconReference(Data.EXE, 5), Arguments = "-hi" });
					//jumplist.AddUserTasks(new JumpListLink(Data.EXE, Data.L["HibernateWOSBTime"]) { IconReference = new IconReference(Data.EXE, 5), Arguments = "-ht" });
				}
				//jumplist.AddUserTasks(new JumpListSeparator());
				//jumplist.AddUserTasks(new JumpListLink(Data.EXE, Data.L["Abort"]) { IconReference = new IconReference(EXE, 6), Arguments = "-a", WorkingDirectory = Path.GetDirectoryName(EXE) });

				jumplist.Refresh();*/
			}
			catch { }
		}

		public static void ClearJumplist()
		{
			if (!Win7.IsWin7)
				return;

			JumpList jumpList = JumpList.GetJumpList(App.Current);
			jumpList.JumpItems.Clear();
			jumpList.Apply();
		}
		#region Pin
		public static void PinToTaskbar()
		{
			if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar\Shutdown7.lnk"))
			{
				try
				{
					StreamWriter file = new StreamWriter(Environment.ExpandEnvironmentVariables("%Temp%") + "\\pin.vbs");
					file.WriteLine("strlPath = \"" + Data.EXE + "\"");
					file.Write("On Error Resume Next\r\nSet objFSO = CreateObject(\"Scripting.FileSystemObject\")\r\nSet objShell = CreateObject(\"Shell.Application\")\r\nIf Not objFSO.FileExists(strlPath) Then Wscript.Quit\r\nstrFolder = objFSO.GetParentFolderName(strlPath)\r\nstrFile = objFSO.GetFileName(strlPath)\r\nSet objFolder = objShell.Namespace(strFolder)\r\nSet objFolderItem = objFolder.ParseName(strFile)\r\nSet colVerbs = objFolderItem.Verbs\r\nFor each itemverb in objFolderItem.verbs\r\n");
					file.WriteLine("If Replace(itemverb.name, \"&\", \"\") = \"" + Data.L["PinText"] + "\" Then itemverb.DoIt");
					file.WriteLine("Next");
					file.Close();
					Process p = new Process();
					p.StartInfo.FileName = Environment.ExpandEnvironmentVariables("%Temp%") + "\\pin.vbs";
					p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					p.Start();
					p.WaitForExit();
					File.Delete(Environment.ExpandEnvironmentVariables("%Temp%") + "\\pin.vbs");
				}
				catch (Exception ex)
				{
					if (Data.debug_verbose)
						Message.Show(ex.Message, "Shutdown7", "Error");
				}
			}
		}
		#endregion
		#endregion

		#region Aero
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern bool DwmIsCompositionEnabled();

		[DllImport("dwmapi.dll", PreserveSig = false)]

		static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);
		
		public static bool Glass(Window window)
		{
			if (Environment.OSVersion.Version.Major < 6) return false;
			if (Data.S["Glass"] & DwmIsCompositionEnabled())
				return ExtendGlassFrame(window, new Thickness(-1));
			else
				return false;
		}

		public static bool Glass(Window window, Thickness thickness)
		{
			if (Environment.OSVersion.Version.Major < 6) return false;
			if (Data.S["Glass"] & DwmIsCompositionEnabled())
				return ExtendGlassFrame(window, thickness);
			else
				return false;
		}

		static bool ExtendGlassFrame(Window window, Thickness margin)
		{
			try
			{
				if (!DwmIsCompositionEnabled())
					return false;

				IntPtr hwnd = new WindowInteropHelper(window).Handle;
				if (hwnd == IntPtr.Zero)
					return false;
				//The Window must be shown before extending glass

				// Set the background to transparent from both the WPF and Win32 perspectives
				window.Background = Brushes.Transparent;
				HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;

				MARGINS margins = new MARGINS(margin);
				DwmExtendFrameIntoClientArea(hwnd, ref margins);
				return true;
			}
			catch (DllNotFoundException)
			{
				return false;
			}
		}

		struct MARGINS
		{
			public MARGINS(Thickness t)
			{
				Left = (int)t.Left;
				Right = (int)t.Right;
				Top = (int)t.Top;
				Bottom = (int)t.Bottom;
			}
			public int Left;
			public int Right;
			public int Top;
			public int Bottom;
		}
		#endregion
	}
}
