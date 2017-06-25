/// <summary>
/// Updater
/// (c) Marius Lutz
/// Individueller Programmupdater (WPF)
/// Version 1.3
/// 1.0 Initial Release
/// 1.1 Win7-Progress
/// 1.2 Multi-Lang
///		Aero
///	1.3 Fix: GUI hängt bei Prüfung der Version
/// Windows XP, Windows Vista, Windows 7
/// 2010-2011
/// www.shutdown7.com
/// </summary>

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Shutdown7
{
	public partial class Updater : Window
	{
		Version OldVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
		Version NewVersion;

		System.Collections.Generic.Dictionary<string, string> L = new System.Collections.Generic.Dictionary<string, string>();

		Uri VersionURL = new Uri("http://www.shutdown7.com/download.php?cmd=version&file=Shutdown");
		Uri ChangelogURL = new Uri("http://www.shutdown7.com/download.php?cmd=changelog&file=Shutdown&mode=text");
		Uri DownloadPortableURL = new Uri("http://www.shutdown7.com/download.php?cmd=download&file=Shutdown7Zip");
		Uri DownloadSetupURL = new Uri("http://www.shutdown7.com/download.php?cmd=download&file=Shutdown7Setup");
		Uri UnzipURL = new Uri("http://www.shutdown7.com/download.php?cmd=download&file=Unzip");
		Uri HelperURL = new Uri("http://www.shutdown7.com/download.php?cmd=download&file=UpdateHelper");
		Uri DownloadURL; 
		string PortableFileName = "Shutdown7.zip";
		string SetupFileName = "Shutdown7_Setup.exe";
		string HelperFileName = "UpdateHelper.exe";
		string ZipDllFileName = "Ionic.Zip.dll";
		string FileName = "";
		bool IsPortable = true;
		bool IsInstaller = true;
		bool Aero = Data.S["Glass"];

		#region Suche
		void CheckInternet()
		{
			label1.Content = L["CheckConnection"];
			button1.IsEnabled = false;
			progressBar1.IsIndeterminate = true;
			Win7Progress("Indeterminate", 0);
			new Thread(new ThreadStart(CheckInternetCon)).Start();
			return;
		}

		void CheckInternetCon()
		{
			try
			{
				Dns.GetHostEntry("www.google.com");
				/*Dispatcher.BeginInvoke((Action)delegate { */SearchUpdate();/* });*/
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message.ToString(), "Updater", MessageBoxButton.OK, MessageBoxImage.Error);
				Dispatcher.BeginInvoke((Action)delegate
				{
					label1.Content = L["NoConnection"];
					button1.IsEnabled = false;
					progressBar1.IsIndeterminate = false;
				});
				Win7Progress("None", 0);
			}

		}

		void SearchUpdate()
		{
			Dispatcher.Invoke((Action)delegate { 
				label1.Content = L["SearchUpdates"];
				progressBar1.IsIndeterminate = true;
				Win7Progress("Indeterminate", 0);
			});

			WebClient w = new WebClient();
			w.DownloadStringCompleted += new DownloadStringCompletedEventHandler(CheckUpdate);
			w.DownloadStringAsync(VersionURL);
		}

		void CheckUpdate(object sender, DownloadStringCompletedEventArgs e)
		{
			Dispatcher.Invoke((Action)delegate { 
				progressBar1.IsIndeterminate = false;
				Win7Progress("None", 0);
			});

			NewVersion = new Version(e.Result);
			if (OldVersion.CompareTo(NewVersion) < 0)
			{
				//Update verfügbar
				Dispatcher.Invoke((Action)delegate
				{
					label1.Content = L["UpdateAvaiable"] + ": Version " + NewVersion.ToString();
					button1.Content = "Download";
					button1.IsEnabled = false;
					expanderChangelog.Visibility = Visibility.Visible;
					radioInstaller.Visibility = Visibility.Visible;
					radioPortable.Visibility = Visibility.Visible;
				});
			}
			else
			{
				//Kein Update
				Dispatcher.Invoke((Action)delegate
				{
					label1.Content = L["NoUpdateAvaiable"];
				});
			}
		}
		#endregion

		#region Download
		void DownloadUpdate()
		{
			label1.Content = L["DownloadUpdate"];
			button1.IsEnabled = false;
			radioPortable.IsEnabled = false;
			radioInstaller.IsEnabled = false;

			WebClient w = new WebClient();
			w.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Update_DownloadProgressChanged);
			w.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Update_DownloadComplete);
			w.DownloadFileAsync(DownloadURL, FileName);
		}

		void Update_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
			Win7Progress("Normal", e.ProgressPercentage);
		}

		void Update_DownloadComplete(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			if ((bool)radioPortable.IsChecked)
				DownloadHelper();
			else
			{
				label1.Content = L["DownloadComplete"];
				button1.Content = L["Install"];
				button1.IsEnabled = true;
			}
		}

		#region Helper
		void DownloadHelper()
		{
			label1.Content = L["DownloadHelper"] + " 1";
			progressBar1.Value = 0;
			WebClient w = new WebClient();
			w.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Helper_DownloadProgressChanged);
			w.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Helper_DownloadComplete);
			w.DownloadFileAsync(UnzipURL, ZipDllFileName);
		}

		void Helper_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
		}

		void Helper_DownloadComplete(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			DownloadHelper_2();
		}

		void DownloadHelper_2()
		{
			label1.Content = L["DownloadHelper"] + " 2";
			progressBar1.Value = 0;
			WebClient w = new WebClient();
			w.DownloadFileAsync(HelperURL, HelperFileName);
			w.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Helper_2_DownloadProgressChanged);
			w.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Helper_2_DownloadComplete);
		}

		void Helper_2_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
		}

		void Helper_2_DownloadComplete(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			label1.Content = L["DownloadComplete"];
			button1.Content = L["Install"];
			button1.IsEnabled = true;
		}
		#endregion
		#endregion

		#region Install
		void InstallUpdate()
		{
			progressBar1.IsIndeterminate = true;
			Win7Progress("Indeterminate", 0);
			button1.IsEnabled = false;

			if (File.Exists(FileName))
			{
				if ((bool)radioPortable.IsChecked)
				{
					//Portable
					label1.Content = L["Install"];
					Process.Start(HelperFileName, "-update -Zip " + FileName);
					Application.Current.Shutdown();
				}
				else
				{
					//Installer
					label1.Content = L["Install"];
					Process.Start(FileName);
					Application.Current.Shutdown();
				}
			}
			else
			{
				label1.Content = L["ErrFileMissing"];
			}

		}
		#endregion

		#region Changelog
		private void expanderChangelog_Expanded(object sender, RoutedEventArgs e)
		{
			if (Changelog.Text == "" | Changelog.Text == L["LoadChangelog"])
			{
				WebClient w = new WebClient();
				w.DownloadStringAsync(ChangelogURL);
				w.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Changelog_Completed);
			}
		}

		void Changelog_Completed(object sender, DownloadStringCompletedEventArgs e)
		{
			Changelog.Text = e.Result.Replace("&nbsp;", " ");
		}
		#endregion

		#region Form
		public Updater()
		{
			InitializeComponent();

			if (!IsPortable) radioPortable.Visibility = Visibility.Collapsed;
			if (!IsInstaller) radioInstaller.Visibility = Visibility.Collapsed;

			#region Lang
			switch (Data.Lang)
			{
				case "de":
					L.Add("Ready", "Bereit");
					L.Add("LoadChangelog", "Lade Changelog");
					L.Add("SearchUpdates", "Suche Updates");
					L.Add("CheckConnection", "Überprüfe Internetverbindung");
					L.Add("NoConnection", "Keine Internetverbindung");
					L.Add("UpdateAvaiable", "Update verfügbar");
					L.Add("NoUpdateAvaiable", "Kein Update verfügbar");
					L.Add("DownloadUpdate", "Lade Update herunter");
					L.Add("DownloadHelper", "Lade Helper");
					L.Add("DownloadComplete", "Erfolgreich heruntergeladen");
					L.Add("Install", "Installieren");
					L.Add("ErrFileMissing", "Fehler: Datei wurde nicht gefunden");
					break;
				default:
					L.Add("Ready", "Ready");
					L.Add("LoadChangelog", "Load changelog");
					L.Add("SearchUpdates", "Search Updates");
					L.Add("CheckConnection", "Check internet-connection");
					L.Add("NoConnection", "No internet-connection");
					L.Add("UpdateAvaiable", "Update avaiable");
					L.Add("NoUpdateAvaiable", "No update avaiable");
					L.Add("DownloadUpdate", "Download update");
					L.Add("DownloadHelper", "Download helper");
					L.Add("DownloadComplete", "Download complete");
					L.Add("Install", "Install");
					L.Add("ErrFileMissing", "Error: File not found");
					break;
			}

			label1.Content = L["Ready"];
			button1.Content = L["SearchUpdates"];

			#endregion
		}

		#region Aero
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			// This can't be done any earlier than the SourceInitialized event:

			if ((int)Environment.OSVersion.Version.Major >= 6)
			{
				if (Aero && DwmIsCompositionEnabled())
				{
					ExtendGlassFrame(this, new Thickness(-1));
				}
			}
		}

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern bool DwmIsCompositionEnabled();

		[DllImport("dwmapi.dll", PreserveSig = false)]
		static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

		public static bool ExtendGlassFrame(Window window, Thickness margin)
		{
			try
			{
				if (!DwmIsCompositionEnabled())
					return false;

				IntPtr hwnd = new WindowInteropHelper(window).Handle;
				if (hwnd == IntPtr.Zero)
					throw new InvalidOperationException("The Window must be shown before extending glass.");

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

		void button1_Click(object sender, RoutedEventArgs e)
		{
			switch ((string)button1.Content)
			{
				case "Suche Updates":
					CheckInternet();
					break;
				case "Search Updates":
					CheckInternet();
					break;
				case "Download":
					DownloadUpdate();
					break;
				case "Installieren":
					InstallUpdate();
					break;
				case "Install":
					InstallUpdate();
					break;
			}

		}

		private void radioInstaller_Click(object sender, RoutedEventArgs e)
		{
			DownloadURL = DownloadSetupURL;
			FileName = SetupFileName;
			button1.IsEnabled = true;
		}

		private void radioPortable_Click(object sender, RoutedEventArgs e)
		{
			DownloadURL = DownloadPortableURL;
			FileName = PortableFileName;
			button1.IsEnabled = true;
		}
		#endregion

		#region Win7
		void Win7Progress(string Mode, int Value)
		{
			if (!File.Exists("Microsoft.WindowsAPICodePack.Shell.dll")) return;
			if (!Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.IsPlatformSupported) return;
			switch (Mode)
			{
				case "Indeterminate":
					Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Indeterminate);
					break;
				case "Normal":
					Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressValue(Value, 100);
					Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Normal);
					break;
				case "Error":
					Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressValue(Value, 100);
					Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Error);
					break;
				case "None":
					Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressValue(Value, 100);
					Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.NoProgress);
					break;
			}

		}
		#endregion
	}
}
