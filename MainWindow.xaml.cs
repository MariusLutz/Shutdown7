/// <summary>
/// Shutdown7
/// (c) Marius Lutz
/// Fährt Computer mit Unterstütztung der Windows 7 Features herunter.
/// Version 2.3.2
/// Windows XP, Windows Vista, Windows 7, Windows 8, Windows 10
/// Oct 2009 - Jun 206
/// www.shutdown7.com
/// 
/// Changelog:
/// 1.0 10.2009
/// 	Initial Release
/// 	Taskbarprogress
/// 	Jumplist
/// 	Taskbaroverlay
/// 	Timeout
/// 	Parameter
/// 	Shana Easteregg
/// 1.1 2009
/// 	Inimanagment
/// 	Settings
/// 	Globale Variablen
/// 	Spenden
/// 	Autostart
/// 	Systray
/// 	Balloontips
/// 	Installer
/// 1.2 2010
/// 	Verbessertes Kontextmenu
/// 	UAC Verbesserungen
/// 	Einstellungen Tooltips
/// 	Aero Glass
/// 	WOSB Integration
/// 	Pictureboxen Transparent
///		Progressbar Updatecheck Indeterminate
/// 	Rika Easteregg
/// 1.3 2010
/// 	Umbenennung in Shutdown7
///		Complete Rewrite WPF
///		Shutdown zu bestimmter Zeit
///		ThumbnailToolbar (Win7)
///		Jumplist Option
///		Overlayicon transparentr (Win7)
///		Settings Layout Update
///		Single Instance
///		Pacman Easteregg
///		Systray Verbesserungen
///	1.4 2010
/// 	Neuer Updater
///		-WPF
///		-Async
///		-Autoupdate
///		-Portable oder Installer
///		Remote-Shutdown
///		-Settings
///		-WebUI (http://shutdown7.com/webui.php?lang=xx)
///		-Portforward-Test
///		-PW MD5-Crypt
///		-Hilfe (s.u.)
///		-Serverauswahl aus Ini
///		Systray Verbesserungen
///		About Layout Update
///		F1: Web-Hilfe (http://shutdown7.com/help.php?lang=xx)
///		Aero Glow
///		Settings Tooltips Translate
///		Wait Background Thread
///		Settings Stackpanel
///		Shutdown Lock intern statt externer
///		Language Detection Change Localized (de, en)
///		DLL-Check
///		Auto Firewall-Exception (XP, Vista/7)
///		ShutdownStart/Stop getrennt von GoButton_Click
///		Komprimierung der Eastereggs->Filesize um 0,7 MB geschrumpft
///		UAC-Manifest included in Resource-File
///		Hibernate Timeout Fix
///		HibernateWOSB Monat/Tag Fix
///		hh, mm, ss Fix größer als 23, 59
///	1.4.1 2010
/// 	  Fix Windows XP Settings Overlayicon Checkbox
///	      Fix Windows XP Settings Save Crash
///	      Fix Crash Updater Falscher Lang-String
///	      Fix Start Shutdown runas, Crash
///	1.4.2 2010
/// 	  Fix Remote Client Password Textfeld
///		  Fix Autostart Systray Minimieren
///		  Fix Double Instance Parameter
///	1.4.3 01.01.2011
/// 	  Settings Layoutupdate
///		  Fix Updater Crash NoConnection
///		  Fix GoButton Remote
///		  WakeOnLan Beta
///	1.5 23.02.2011
/// 	Neue Shutdown-Optionen
///		-Prozess geschlossen
///		-Datei gelöscht
///		-Musik abgespielt
///		 -Playlist
///		 -Fadeout
///		Bildschirm ausschalten Option
///		Main & Settings Layout Update
///		-Expander
///		Single Instance
///		-Parameter-Weiterleitung
///		Windows 7: Neuer FileOpenDialog und TaskDialoge (Win7API)
///		-Fallback
///		Neuer Timer (DispatchTimer)
///		WebUI Abbrechen (Get)
///		Aero-Glow heruntergesetzt
///		Systray nicht mehr unterstützt
///	1.5.1 23.02.2011
/// 	  Fix Crash TaskDialog
///	1.6 23.03.2011
/// 	Neuer Modus: IdleTime
///		Play Music Playlists (m3u, wpl, xspf)
///		Integer t, h, m, s ersetzt durch TimeSpan t
///		Fix: Play Music Restzeit falsch kalkuliert wenn Gesamtlänge der Stücke über 24h
///		Fix: Argumente
///	1.6.1 23.03.2011
/// 	  Fix: Time: At Falsche Kalkulation
///		  Fix: Time: Progress Falsche Kalkulation
///	1.7 20.08.2011
/// 	Neue Icons
///		Erster Start (keine Ini): WelcomeScreen
///		-Neue Version: Changelog (WelcomeScreen)
/// 	Modus-Auswahl mit Icons (CustomControl, Textbox als Workaround für SelectedItem)
/// 	Neuer Modus: Programm starten
/// 	Neue Bedingung: Sofort
/// 	NumericUpDown-Control
/// 	Music:
/// 	-Start Volume
/// 	-Fadeout End Volume
///		Systray Icon verbessert
///		-jetzt in Code satt XAML; nur geladen, wenn nötig
///		-Tooltips verbessert
///		WOSB Settings
///		WakeOnLan mit IP-Adresse
///		ThumbnailToolbar & Systray Abbrechen hinzugefügt; aktiviert, wenn nötig
///		Jumplist WOSBTime entfent
///		SendFeedback Option
///		-Sende RemoteClient-Daten an WebUI
///		IPv4 Kompatibilität-Option
///		PacMan Easteregg: Autostart
///		Funktionen aus MainWindow ausgelagert in Klassen
///		SingleInstance verbessert (neue Methode, keine DLL mehr)
///		Argumente Handling vereinfacht und ausgelagert
///		TaskDialoge + Fallback MessageBox
///		Debug-Vars
///		Fix: Jumplist wird nicht angezeigt
///		Fix: Wenn Modus WOSBIni, GoButton deaktiviert
///		Fix: Falsche Anzeige der Restzeit
///		Fix: WOL-Bug
///		Fix: Shutdown7 wird nicht beendet, wenn RemoteServer an
///		Fix: Remote-Server Bug
///		Fix: MusicUpDown Buttons werden unter XP nicht angezeigt (fehlende Zeichen in Tahoma Font)
///	1.8	24.11.11
///		Unterstützung für neue WebUI
///		-Status Abfrage
///		Neue lokale WebUI
///		-STATUS Abfrage, Codeänderungen
///		-jQuery dynamischer Updatecheck
///		XML-Format statt INI
///		WOSB-Profile
///		Beliebig viele WOSB-Zeiten pro Tag
///		-Bis zu 4 in UI
///		WOSB-Shutdown ausführbar wenn Countdown läuft
///		Modus-Box Update ohne Neustart
///		File Textbox Drag n' Drop
///		Process Liste alphabetisch (Linq)
///		Umbenannt Fenster/Prozess
///		Drop NyanCat Easteregg
/// 1.8.1 06.12.11
///		Fix Welcomescreen EN: fehlende Übersetzung
///		Fix WOSB Time
///	1.9 21.02.12
///		Neues Icon
///		Fadeout Start und Endvolume Sliderskalierung angepasst
///		Musik Fadeout Polygon zur Veranschaulichung
///	2.0 24.03.13
///	    Neue WebUI (Beta)
///	    Initialization verbessert
///	    -UAC Check/Admin verbessert
///	    UAC Manifest eingebaut
///	    IPv4 Standard
///	    UI: At: Sekunde bei 0
///	    UI: Musik-Fadeout Polygon Layoutupdates
///	    UI: About: Linkupdates
///	    Fix: Miscalculation Time At
///	    Fix: Crash at WOSBIni
///	2.1 19.01.14
///		Option: Aktiv bleiben nach Aktion
///		Option: Support Hybrid-Shutdown (Win8)
///		Crash-Reporting
///		Minimiert bei Autostart
///		Fix: Wakeup Skip Sunday (vorläufig)
///	    Fix: Autostart
///	 2.1.1 28.09.14
///		Systray Verbesserungen
///		-Tooltips für alle Bedingungen
///		-Kontextmenü umorganisiert
///		Fix: Timer läuft weiter wenn Stop gedrückt wurde
///	 2.1.2 28.09.14
///		Fix: ?
///	 2.2 17.01.15
///		Neuer Modus: Android Reboot (Beta)
///		Neue Bedingung: Cpu Auslastung
///		Neue Bedingung: Netzwerk Auslastung (deaktiviert)
///		Fix: Wait function counts to -1
///		Fix: Crash bei Systray + Launch (fehlender Eintrag)
///		Fix: Modus/Conditions UI
///	2.3 27.07.15
///		Remote Shutdown über Argumente möglich
///		Remote: Status übermittelt Mode Launch, Android; Conditions Cpu
///		Error Reporting Filter
///		Fix: Shutdown7 bleibt an bei gesetzter Option auch bei Argumenten
///		Fix: Server startete nicht wenn Start über Autostart
///		Fix: Systray brauchte zwei Doppelklicks beim ersten Start zum öffnen des Fensters
///		Fix: URLs angepasst
///	2.3.1 10.04.16
///		Fix: UAC (Manifest eingebunden)
///     Test Build: Debug Message for shutdown
///		Wakeup (Ini) umbenannt
///	2.3.2 18.06.16
///     Remember Window Position (optional)
///     Focus existing window if Second Instance called
///     WPF NotifyIcon updated to 1.0.8
///     -resolved tooltip bug
///     -systray disposed after execute
///	X.Y xx.yy.zz
///	
///	TODO:
/// -revisit shutdown procudure, is closing enough?
/// 
///	Bugs:
///	-Args + laufender Shutdown: Timer wurde schon überschrieben, also wird neuer Countdown gesetzt und die Warnung kommt
///	-Music Time Calculation ~5-10s zu viel <überprüfen>
///	-Noise Volume + Fadeout
///	-Bei Status:Music wird immer Gesamtdauer angezeigt, korrigiere mit aktuellem Countdown
///	-Status Launch: Programm ähnlich wie Music angeben
///	
/// Features:
///	-Task Scheduling (PoC implemented in Settings) http://support.microsoft.com/kb/814596/de
///	 -Scheduling wie bei WOSB das automatisiert Start/Shutdown
/// -Screensaver option http://www.codeproject.com/Questions/116918/Turn-on-screensaver-programmatically-in-C
///	-Autostart ohne admin?
///	-Framework 4.0 ohne APIPack (geht nicht sehr gut)
///	-Single Instance überarbeiten? https://code.msdn.microsoft.com/Windows-7-Taskbar-Single-4120eafd/sourcecode?fileId=18659&pathId=1728260600

/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using RegawMOD.Android;
using System.Net.NetworkInformation;

namespace Shutdown7
{
	public partial class MainWindow : Window
	{
		#region Deklarationen
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool LockWorkStation();

		[DllImport("powrprof.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool SetSuspendState(bool Hibernate, bool ForceCritical, bool DisableWakeEvent);

		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AbortSystemShutdown(string lpMachineName);

		[DllImport("user32.dll")]
		private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);

		[DllImport("User32.dll")] //IdleTime
		private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

		string WOSBZeit;
		int FadeTotalRunTime, startfade, total;
		bool AtChecked, CpuMode, NetworkMode, ScreenOffChecked, PlayNoiseChecked;
		double orgVolume, endVolume, length, percent;

		public static string curRemoteServer, curRemoteIP, curRemotePassword, curRemoteMac;
		public string ProcessName, FileName, LaunchFile, NetworkAdapter;
		public static int curRemotePort, Cpu, Network, cpuHits, networkHits;
		public static bool remote;

		TcpListener tcpListener;
		TcpClient client;
		ArrayList MusicFiles = new ArrayList();
		DispatcherTimer Timer = new DispatcherTimer();
		Thread MusicThread, NoiseThread;
		MediaPlayer mp3, noise;
		TaskbarIcon sysicon;
		MenuItem contextRestore, contextAbort, contextShutdown, contextShutdown1, contextShutdown2, contextShutdown3, contextShutdown4, contextShutdown5, contextShutdown6, contextShutdown7, contextExit;
		public static List<CodeItem> list;
		static ThumbnailToolBarButton tt1, tt2, tt3, tt4, tt5, tt6, /*tt7, */tt8;
		PerformanceCounter cpuCounter;
        #endregion
        
        #region Form	
        #region Events
        public MainWindow()
		{
			InitializeComponent();
		}

		private void Main_Initialized(object sender, EventArgs e)
		{
			#region Layout
			expanderMode.Header = Data.L["Condition"];
			expanderSettings.Header = Data.L["Settings"];
			buttonBrowseLaunchFile.Content = Data.L["Add"];
			buttonDeleteLaunchFile.Content = Data.L["Remove"];
			radioNow.Content = Data.L["ModeNow"];
			radioTime.Content = Data.L["ModeTime"];
			radioProcessClose.Content = (Data.S["AllProcesses"]) ? Data.L["ModeProcessClosed"] : Data.L["ModeWindowClosed"];
			radioFileDelete.Content = Data.L["ModeFileDeleted"];
			radioPlayMusic.Content = Data.L["ModeMusicPlayed"];
			radioIdle.Content = Data.L["ModeIdle"];
			radioCpu.Content = Data.L["ModeCpu"];
			radioNetwork.Content = Data.L["ModeNetwork"];
			At.Content = Data.L["At"];
			In.Content = Data.L["In"];
			buttonBrowseDeleteFile.Content = Data.L["Add"];
			buttonDeleteDeleteFile.Content = Data.L["Remove"];
			buttonBrowseMusicFile.Content = Data.L["Browse"];
			buttonDeleteMusicFile.Content = Data.L["Remove"];
			comboCpu.Items.Add(Data.L["Above"]);
			comboCpu.Items.Add(Data.L["Below"]);
			comboNetwork.Items.Add(Data.L["Down"]);
			comboNetwork.Items.Add(Data.L["Up"]);
			//labelNetworkBelow.Content = Data.L["Below"];
			labelRemotePassword.Content = Data.L["Password"] + ": ";
            //checkResumeLastAction.Content = Data.L[""]; TODO
            checkScreenOff.Content = Data.L["ScreenOff"];
			checkMusicFadeout.Content = Data.L["MusicFadeout"];
			labelFade.Content = Data.L["FadeStart"];
			labelOrgVol.Content = Data.L["MusicOrgVolume"];
			labelEndVol.Content = Data.L["FadeEndVolume"];
			checkPlayNoise.Content = Data.L["PlayNoise"];
			SettingsLabel.Text = Data.L["Settings"];

			UpdateModus();

			//MusicUpDown Buttons Fix für XP
			buttonUpMusicFile.FontFamily = new FontFamily("Arial");
			buttonDownMusicFile.FontFamily = new FontFamily("Arial");

			//Music Scroll
			scrollMusicFiles.MaxHeight = SystemParameters.VirtualScreenHeight / 2;

			//Network Adapters
			comboNetworkAdapters.ItemsSource = GetNetworkAdapters();

			if (Data.S["RemoteClient"])
			{
				expanderRemote.Visibility = Visibility.Visible;
				foreach (string CurServer in Data.RemoteServers)
					textRemoteServer.Items.Add(CurServer);

				if (Data.S["WakeOnLan"])
				{
					foreach (string CurMac in Data.RemoteMacs)
						textRemoteMac.Items.Add(CurMac);
				}
			}

			//Noise
			if (File.Exists(Data.NoisePath))
				checkPlayNoise.IsEnabled = true;
			else
				checkPlayNoise.IsEnabled = false;

			#endregion

			if (Data.RemoteByArgs)
			{
				Hide();
				curRemoteServer = Data.ArgsServer;
				curRemotePort = Data.ArgsPort;
				curRemotePassword = Data.ArgsPassword;
				RemoteClientStart();
			}
            
            if (Data.S["ResumeLastAction"])
                ResumeLastAction();
        }

        void ResumeLastAction()
        {
            //Read last settings
            //Xml.ReadLastAction();

            if (LastActionisValid())
            {
                ApplyActiontoUI();
                StartShutdown();
            }
        }

        bool LastActionisValid()
        {
            switch (Data.Condition)
            {
                case Data.Conditions.Time:
                    if (Data.t.TotalSeconds < 5)
                        return false;
                    break;
                case Data.Conditions.Idle:
                    if (Data.t.TotalSeconds < 1)
                        return false;
                    break;
                case Data.Conditions.Process:
                    //sure process is running
                case Data.Conditions.File:
                    //make sure file exists
                default:
                    return false;
            }

            return true;
        }

        void ApplyActiontoUI()
        {
            //TODO: test robustness
            comboModus.SelectedIndex = Convert.ToInt32(Data.Mode) - 1;
            /*switch (Data.Mode)
            {
                case Data.Modes.Shutdown:
                    comboModus.SelectedIndex = 0;
                    break;
                case Data.Modes.Restart:
                    comboModus.SelectedIndex = 1;
                    break;
                case Data.Modes.Logoff:
                    comboModus.SelectedIndex = 2;
                    break;
                case Data.Modes.Lock:
                    comboModus.SelectedIndex = 3;
                    break;
                case Data.Modes.Standby:
                case Data.Modes.StandbyWOSB:
                    comboModus.SelectedIndex = 4;
                    break;
                case Data.Modes.Hibernate:
                case Data.Modes.HibernateWOSB:
                    comboModus.SelectedIndex = 5;
                    break;
                case Data.Modes.HibernateWOSBIni:
                case Data.Modes.HibernateWOSBTime:
                case Data.Modes.WakeOnLan:
                case Data.Modes.Launch:
                case Data.Modes.RestartAndroid:
                default:
                    break;
            }*/

            switch (Data.Condition)
            {
                case Data.Conditions.Time:
                    radioTime.IsChecked = true;
                    In.IsChecked = true;
                    hh.Value = Data.t.Hours; mm.Value = Data.t.Minutes; ss.Value = Data.t.Seconds;
                    break;
                case Data.Conditions.Process:
                    radioProcessClose.IsChecked = true;
                    //if (ProcessExists(ProcessName)) comboProcesses.SelectedValue = ProcessName;
                    Message.Show("Not supported (yet).");
                    break;
                case Data.Conditions.File:
                    radioFileDelete.IsChecked = true;
                    Message.Show("Not supported (yet).");
                    break;
                case Data.Conditions.Music:
                    radioPlayMusic.IsChecked = true;
                    Message.Show("Not supported (yet).");
                    break;
                case Data.Conditions.Idle:
                    radioIdle.IsChecked = true;
                    hh.Value = Data.t.Hours; mm.Value = Data.t.Minutes; ss.Value = Data.t.Seconds;
                    break;
                case Data.Conditions.Cpu:
                    radioCpu.IsChecked = true;
                    Message.Show("Not supported (yet).");
                    break;
                //case Data.Conditions.None:
                //case Data.Conditions.Now:
                //case Data.Conditions.Network:
                default:
                    Message.Show("Nothing to do.");
                    return;
            }
        }

        #region Aero
        protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			Win7.Glass(this);
		}
		#endregion

		#region Checkboxen
		private void radioNow_Checked(object sender, RoutedEventArgs e)
		{
			Data.Condition = Data.Conditions.Now;

			if (Visibility != Visibility.Visible) return;

			stackTime.Visibility = Visibility.Collapsed;
			stackProcess.Visibility = Visibility.Collapsed;
			stackFile.Visibility = Visibility.Collapsed;
			stackMusic.Visibility = Visibility.Collapsed;
			stackCpu.Visibility = Visibility.Collapsed;
			stackNetwork.Visibility = Visibility.Collapsed;

			stackMusicVol.IsEnabled = false;
			checkMusicFadeout.IsEnabled = false;
			checkMusicFadeout.IsChecked = false;
			checkPlayNoise.IsEnabled = false;
			checkPlayNoise.IsChecked = false;

			GoButton_Enabled();
		}
		
		private void radioTime_Checked(object sender, RoutedEventArgs e)
		{
			Data.Condition = Data.Conditions.Time;

			stackTime.Visibility = Visibility.Visible;
			stackProcess.Visibility = Visibility.Collapsed;
			stackFile.Visibility = Visibility.Collapsed;
			stackMusic.Visibility = Visibility.Collapsed;
			stackCpu.Visibility = Visibility.Collapsed;
			stackNetwork.Visibility = Visibility.Collapsed;

			At.IsEnabled = true;
			In.IsEnabled = true;
			stackMusicVol.IsEnabled = false;
			checkMusicFadeout.IsEnabled = false;
			checkMusicFadeout.IsChecked = false;
			if (!expanderRemote.IsExpanded && File.Exists(Data.NoisePath))
				checkPlayNoise.IsEnabled = true;

			GoButton_Enabled();
		}

		private void radioProcess_Checked(object sender, RoutedEventArgs e)
		{
			Data.Condition = Data.Conditions.Process;
			
			stackTime.Visibility = Visibility.Collapsed;
			stackProcess.Visibility = Visibility.Visible;
			stackFile.Visibility = Visibility.Collapsed;
			stackMusic.Visibility = Visibility.Collapsed;
			stackCpu.Visibility = Visibility.Collapsed;
			stackNetwork.Visibility = Visibility.Collapsed;

			if (Data.S["AllProcesses"])
			{
				//comboProcesses.DisplayMemberPath = "ProcessName";
				comboProcesses.ItemsSource =
				from x in Process.GetProcesses()
				where (x.ProcessName.Length > 0)
				orderby x.ProcessName
				select x.ProcessName + ".exe";
			}
			else
			{
				//comboProcesses.DisplayMemberPath = "MainWindowTitle";
				comboProcesses.ItemsSource =
				from x in Process.GetProcesses()
				where (x.MainWindowTitle.Length > 0)
				orderby x.MainWindowTitle
				select x.MainWindowTitle;
			}

			stackMusicVol.IsEnabled = false;
			checkMusicFadeout.IsEnabled = false;
			checkMusicFadeout.IsChecked = false;
			if (File.Exists(Data.NoisePath)) checkPlayNoise.IsEnabled = true;

			GoButton_Enabled();
		}

		private void radioFile_Checked(object sender, RoutedEventArgs e)
		{
			Data.Condition = Data.Conditions.File;

			stackTime.Visibility = Visibility.Collapsed;
			stackProcess.Visibility = Visibility.Collapsed;
			stackFile.Visibility = Visibility.Visible;
			stackMusic.Visibility = Visibility.Collapsed;
			stackCpu.Visibility = Visibility.Collapsed;
			stackNetwork.Visibility = Visibility.Collapsed;

			stackMusicVol.IsEnabled = false;
			checkMusicFadeout.IsEnabled = false;
			checkMusicFadeout.IsChecked = false;
			if (File.Exists(Data.NoisePath)) checkPlayNoise.IsEnabled = true;

			GoButton_Enabled();
		}

		private void radioMusic_Checked(object sender, RoutedEventArgs e)
		{
			Data.Condition = Data.Conditions.Music;
			
			stackTime.Visibility = Visibility.Collapsed;
			stackProcess.Visibility = Visibility.Collapsed;
			stackFile.Visibility = Visibility.Collapsed;
			stackMusic.Visibility = Visibility.Visible;
			stackCpu.Visibility = Visibility.Collapsed;
			stackNetwork.Visibility = Visibility.Collapsed;

			stackMusicVol.IsEnabled = true;
			checkMusicFadeout.IsEnabled = true;
			checkPlayNoise.IsEnabled = false;
			checkPlayNoise.IsChecked = false;
			
			GoButton_Enabled();
		}

		private void radioIdle_Checked(object sender, RoutedEventArgs e)
		{
			Data.Condition = Data.Conditions.Idle;

			stackTime.Visibility = Visibility.Visible;
			stackProcess.Visibility = Visibility.Collapsed;
			stackFile.Visibility = Visibility.Collapsed;
			stackMusic.Visibility = Visibility.Collapsed;
			stackCpu.Visibility = Visibility.Collapsed;
			stackNetwork.Visibility = Visibility.Collapsed;

			In.IsChecked = true;
			At.IsEnabled = false;
			In.IsEnabled = false;

			stackMusicVol.IsEnabled = false;
			checkMusicFadeout.IsEnabled = false;
			checkMusicFadeout.IsChecked = false;
			checkPlayNoise.IsEnabled = true;

			GoButton_Enabled();
		}

		private void radioCpu_Checked(object sender, RoutedEventArgs e)
		{
			Data.Condition = Data.Conditions.Cpu;

			stackTime.Visibility = Visibility.Visible;
			stackProcess.Visibility = Visibility.Collapsed;
			stackFile.Visibility = Visibility.Collapsed;
			stackMusic.Visibility = Visibility.Collapsed;
			stackCpu.Visibility = Visibility.Visible;
			stackNetwork.Visibility = Visibility.Collapsed;

			In.IsChecked = true;
			At.IsEnabled = false;
			In.IsEnabled = false;

			stackMusicVol.IsEnabled = false;
			checkMusicFadeout.IsEnabled = false;
			checkMusicFadeout.IsChecked = false;
			if (File.Exists(Data.NoisePath)) checkPlayNoise.IsEnabled = true;

			GoButton_Enabled();
		}

		private void radioNetwork_Checked(object sender, RoutedEventArgs e)
		{
			Data.Condition = Data.Conditions.Network;

			stackTime.Visibility = Visibility.Visible;
			stackProcess.Visibility = Visibility.Collapsed;
			stackFile.Visibility = Visibility.Collapsed;
			stackMusic.Visibility = Visibility.Collapsed;
			stackCpu.Visibility = Visibility.Collapsed;
			stackNetwork.Visibility = Visibility.Visible;

			In.IsChecked = true;
			At.IsEnabled = false;
			In.IsEnabled = false;

			stackMusicVol.IsEnabled = false;
			checkMusicFadeout.IsEnabled = false;
			checkMusicFadeout.IsChecked = false;
			if (File.Exists(Data.NoisePath)) checkPlayNoise.IsEnabled = true;

			GoButton_Enabled();
		}

		private void In_Checked(object sender, RoutedEventArgs e)
		{
			hh.Value = Data.t.Hours;
			mm.Value = Data.t.Minutes;
			ss.Value = Data.t.Seconds;
		}

		private void At_Checked(object sender, RoutedEventArgs e)
		{
			Data.t = TimeSpan.Parse(hh.Value + ":" + mm.Value + ":" + ss.Value);
			hh.Value = DateTime.Now.Hour;
			mm.Value = DateTime.Now.Minute;
			ss.Value = 0/*DateTime.Now.Second*/;
		}

		private void checkPlayNoise_Checked(object sender, RoutedEventArgs e)
		{
			radioPlayMusic.IsEnabled = false;
			if (Data.Condition == Data.Conditions.Music)
			{
				radioTime.IsChecked = true;
				Data.Condition = Data.Conditions.Time;
			}
		}

		private void checkPlayNoise_Unchecked(object sender, RoutedEventArgs e)
		{
			radioPlayMusic.IsEnabled = true;
		}

		private void checkMusicFadeout_Checked(object sender, RoutedEventArgs e)
		{
			updateCanvas();
			stackMusicFadeout.Visibility = Visibility.Visible;
		}

		private void checkMusicFadeout_Unchecked(object sender, RoutedEventArgs e)
		{
			stackMusicFadeout.Visibility = Visibility.Collapsed;
		}

        private void checkResumeLastAction_Checked(object sender, RoutedEventArgs e)
        {
            Data.t = TimeSpan.Parse(hh.Value + ":" + mm.Value + ":" + ss.Value);
            if (!LastActionisValid())
            {
                checkResumeLastAction.Checked -= checkResumeLastAction_Checked;
                checkResumeLastAction.IsChecked = false;
                Message.Show("Not valid");
                checkResumeLastAction.Checked += checkResumeLastAction_Checked;
            }
        }
        #endregion

            #region Modus
        public void UpdateModus()
		{
			list = new List<CodeItem>();
			list.Clear();

			if (Data.S["ModusIcons"])
			{
				list.Add(new CodeItem(Data.L["Shutdown"], "pack://application:,,,/Shutdown7;component/Resources/Shutdown.ico"));
				list.Add(new CodeItem(Data.L["Restart"], "pack://application:,,,/Shutdown7;component/Resources/Reboot.ico"));
				list.Add(new CodeItem(Data.L["Logoff"], "pack://application:,,,/Shutdown7;component/Resources/Logoff.ico"));
				list.Add(new CodeItem(Data.L["Lock"], "pack://application:,,,/Shutdown7;component/Resources/Logoff.ico"));
				list.Add(new CodeItem(Data.L["Standby"], "pack://application:,,,/Shutdown7;component/Resources/Standby.ico"));
				list.Add(new CodeItem(Data.L["Hibernate"], "pack://application:,,,/Shutdown7;component/Resources/Standby.ico"));

				if (Data.S["WOSB"])
				{
					list.Add(new CodeItem(Data.L["HibernateWOSBTime"], "pack://application:,,,/Shutdown7;component/Resources/WOSB.ico"));
					list.Add(new CodeItem(Data.L["HibernateWOSBIni"], "pack://application:,,,/Shutdown7;component/Resources/WOSB.ico"));
				}

				if (Data.S["WakeOnLan"])
					list.Add(new CodeItem(Data.L["WakeOnLan"], "pack://application:,,,/Shutdown7;component/Resources/WakeOnLan.ico"));

				list.Add(new CodeItem(Data.L["LaunchFile"], "pack://application:,,,/Shutdown7;component/Resources/EXE.ico"));
				if (Data.debug_beta)
					list.Add(new CodeItem(Data.L["RestartAndroid"], "pack://application:,,,/Shutdown7;component/Resources/Android.ico"));
			}
			else
			{
				list.Add(new CodeItem(Data.L["Shutdown"], ""));
				list.Add(new CodeItem(Data.L["Restart"], ""));
				list.Add(new CodeItem(Data.L["Logoff"], ""));
				list.Add(new CodeItem(Data.L["Lock"], ""));
				list.Add(new CodeItem(Data.L["Standby"], ""));
				list.Add(new CodeItem(Data.L["Hibernate"], ""));

				if (Data.S["WakeOnLan"])
					list.Add(new CodeItem(Data.L["WakeOnLan"], ""));

				if (Data.S["WOSB"])
				{
					list.Add(new CodeItem(Data.L["HibernateWOSBTime"], ""));
					list.Add(new CodeItem(Data.L["HibernateWOSBIni"], ""));
				}

				list.Add(new CodeItem(Data.L["LaunchFile"], ""));
				if (Data.debug_beta)
					list.Add(new CodeItem(Data.L["RestartAndroid"], ""));
			}

			int curSelectionI = comboModus.SelectedIndex;

			comboModus.ItemsSource = list;

			if (curSelectionI < 6)
				comboModus.SelectedIndex = curSelectionI;
		}

		private void Modus_TextChanged(object sender, TextChangedEventArgs e)
		{
			GoButton_Enabled();

			if (Modus.Text == Data.L["WakeOnLan"])
				Data.Mode = Data.Modes.WakeOnLan;
			else if (Modus.Text == Data.L["HibernateWOSBIni"])
				Data.Mode = Data.Modes.HibernateWOSBIni;
			else if (Modus.Text == Data.L["HibernateWOSBTime"])
				Data.Mode = Data.Modes.HibernateWOSBTime;
			else if (Modus.Text == Data.L["LaunchFile"])
				Data.Mode = Data.Modes.Launch;
			else if (Modus.Text == Data.L["RestartAndroid"])
				Data.Mode = Data.Modes.RestartAndroid;
			else
			{
				switch (comboModus.SelectedIndex)
				{
					case 0: Data.Mode = Data.Modes.Shutdown; break;
					case 1: Data.Mode = Data.Modes.Restart; break;
					case 2: Data.Mode = Data.Modes.Logoff; break;
					case 3: Data.Mode = Data.Modes.Lock; break;
					case 4:
						if (Data.S["WOSB"])
							Data.Mode = Data.Modes.StandbyWOSB;
						else
							Data.Mode = Data.Modes.Standby;
						break;
					case 5:
						if (Data.S["WOSB"])
							Data.Mode = Data.Modes.HibernateWOSB;
						else
							Data.Mode = Data.Modes.Hibernate;
						break;
				}
			}

			if (Modus.Text == Data.L["HibernateWOSBIni"] | Modus.Text == Data.L["HibernateWOSBTime"] | Modus.Text == Data.L["LaunchFile"])
			{
				expanderRemote.IsEnabled = false;
				expanderRemote.IsExpanded = false;
			}
			else
			{
				expanderRemote.IsEnabled = true;
			}

			if (Modus.Text != Data.L["WakeOnLan"])
			{
				labelRemoteMac.Visibility = Visibility.Collapsed;
				textRemoteMac.Visibility = Visibility.Collapsed;
				labelRemotePassword.Visibility = Visibility.Visible;
				textRemotePassword.Visibility = Visibility.Visible;
			}
			else
			{
				expanderRemote.IsExpanded = true;
				labelRemoteMac.Visibility = Visibility.Visible;
				textRemoteMac.Visibility = Visibility.Visible;
				textRemoteServer.Text = IPAddress.Broadcast.ToString();
				labelRemotePassword.Visibility = Visibility.Collapsed;
				textRemotePassword.Visibility = Visibility.Collapsed;
			}

			if (Modus.Text == Data.L["HibernateWOSBTime"])
			{
				radioNow.IsEnabled = false;
				radioProcessClose.IsEnabled = false;
				radioFileDelete.IsEnabled = false;
				radioPlayMusic.IsEnabled = false;
				radioIdle.IsEnabled = false;
				radioTime.IsChecked = true;
				radioCpu.IsEnabled = false;
				At.IsChecked = true;
				In.IsEnabled = false;
			}
			else
			{
				radioNow.IsEnabled = true;
				radioProcessClose.IsEnabled = true;
				radioFileDelete.IsEnabled = true;
				radioPlayMusic.IsEnabled = true;
				radioIdle.IsEnabled = true;
				radioCpu.IsEnabled = true;
				In.IsEnabled = true;
			}

			if (Modus.Text == Data.L["HibernateWOSBIni"])
			{
				radioNow.IsChecked = true;
				expanderMode.IsEnabled = false;
				expanderMode.IsExpanded = false;
			}
			else
			{
				expanderMode.IsEnabled = true;
				expanderMode.IsExpanded = true;
			}

			if (Modus.Text == Data.L["LaunchFile"])
			{
				stackLaunchFile.Visibility = Visibility.Visible;
			}
			else
			{
				stackLaunchFile.Visibility = Visibility.Collapsed;
			}

		}

		#region LaunchFile
		private void buttonBrowseLaunchFile_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (ShellLibrary.IsPlatformSupported)
				{
					CommonOpenFileDialog fd = new CommonOpenFileDialog(Data.L["SelectFile"]);
					fd.Filters.Add(new CommonFileDialogFilter(Data.L["AllFiles"], "*.*"));
					fd.EnsureFileExists = true;

					if (fd.ShowDialog() == CommonFileDialogResult.Ok)
					{
						textLaunchFile.Text = fd.FileName;
						LaunchFile = fd.FileName;
						buttonBrowseLaunchFile.IsEnabled = false;
						buttonDeleteLaunchFile.IsEnabled = true;
					}
				}
				else
				{
					OpenFileDialog fd = new OpenFileDialog();
					fd.Filter = Data.L["AllFiles"] + "|*.*";
					fd.Title = Data.L["SelectFile"];
					fd.Multiselect = true;
					fd.CheckFileExists = true;

					if ((bool)fd.ShowDialog())
					{
						textLaunchFile.Text = fd.FileName;
						LaunchFile = fd.FileName;
						buttonBrowseLaunchFile.IsEnabled = false;
						buttonDeleteLaunchFile.IsEnabled = true;
					}
				}
			}
			catch (Exception ex)
			{
				Message.Show(ex.Message, "Shutdown7", "Error");
			}
		}

		private void buttonDeleteLaunchFile_Click(object sender, RoutedEventArgs e)
		{
			textLaunchFile.Text = "";
			buttonBrowseLaunchFile.IsEnabled = true;
			buttonDeleteLaunchFile.IsEnabled = false;
		}

		void textLaunchFile_TextChanged(object sender, TextChangedEventArgs e)
		{
			GoButton_Enabled();
		}
		#endregion

		#endregion

		#region Conditions
		#region Time
		/*private void hh_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !Char.IsDigit(Convert.ToChar(e.Text));
		}

		private void mm_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !Char.IsDigit(Convert.ToChar(e.Text));

			if (Int32.Parse(mm.Text) > 59)
			{
				mm.Text = (Int32.Parse(e.Text) - 60).ToString();
				hh.Text = (Int32.Parse(hh.Text) + 1).ToString();
				if (hh.Text.Length == 1) hh.Text = "0" + hh.Text;
				if (mm.Text.Length == 1) mm.Text = "0" + mm.Text;
				if (ss.Text.Length == 1) ss.Text = "0" + ss.Text;
			}
		}

		private void ss_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !Char.IsDigit(Convert.ToChar(e.Text));
		}

		private void hh_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				if (Int32.Parse(hh.Text) > 23)
				{
					hh.Text = "23"; if (hh.Text.Length == 1) hh.Text = "0" + hh.Text;
					if (mm.Text.Length == 1) mm.Text = "0" + mm.Text;
					if (ss.Text.Length == 1) ss.Text = "0" + ss.Text;
				}
			}
			catch { }
		}

		private void mm_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				if (Int32.Parse(mm.Text) > 59)
				{
					mm.Text = (Int32.Parse(mm.Text) - 60).ToString();
					hh.Text = (Int32.Parse(hh.Text) + 1).ToString();
					if (hh.Text.Length == 1) hh.Text = "0" + hh.Text;
					if (mm.Text.Length == 1) mm.Text = "0" + mm.Text;
					if (ss.Text.Length == 1) ss.Text = "0" + ss.Text;
				}
			}
			catch { }
		}

		private void ss_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				if (Int32.Parse(ss.Text) > 59)
				{
					ss.Text = (Int32.Parse(ss.Text) - 60).ToString();
					mm.Text = (Int32.Parse(mm.Text) + 1).ToString(); if (hh.Text.Length == 1) hh.Text = "0" + hh.Text;
					if (mm.Text.Length == 1) mm.Text = "0" + mm.Text;
					if (ss.Text.Length == 1) ss.Text = "0" + ss.Text;
				}
			}
			catch { }
		}*/
		#endregion

		#region Process
		void comboProcesses_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Dispatcher.Thread != Thread.CurrentThread)
			{
				Dispatcher.Invoke((Action)delegate { comboProcesses_SelectionChanged(sender, e); });
				return;
			}

			GoButton_Enabled();

			try
			{
				if ((string)comboProcesses.SelectedItem != "")
					ProcessName = e.AddedItems[0].ToString().Replace(".exe", "");
			}
			catch { }
		}
		#endregion

		#region File
		private void buttonBrowseDeleteFile_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (ShellLibrary.IsPlatformSupported)
				{
					CommonOpenFileDialog fd = new CommonOpenFileDialog(Data.L["SelectFile"]);
					fd.Filters.Add(new CommonFileDialogFilter(Data.L["AllFiles"], "*.*"));
					fd.EnsureFileExists = true;

					if (fd.ShowDialog() == CommonFileDialogResult.Ok)
					{
						textDeleteFile.Text = fd.FileName;
						FileName = fd.FileName;
						buttonBrowseDeleteFile.IsEnabled = false;
						buttonDeleteDeleteFile.IsEnabled = true;
					}
				}
				else
				{
					OpenFileDialog fd = new OpenFileDialog();
					fd.Filter = Data.L["AllFiles"] + "|*.*";
					fd.Title = Data.L["SelectFile"];
					fd.Multiselect = true;
					fd.CheckFileExists = true;

					if ((bool)fd.ShowDialog())
					{
						textDeleteFile.Text = fd.FileName;
						FileName = fd.FileName;
						buttonBrowseDeleteFile.IsEnabled = false;
						buttonDeleteDeleteFile.IsEnabled = true;
					}
				}
			}
			catch (Exception ex)
			{
				Message.Show(ex.Message, "Error");
			}
		}

		private void buttonDeleteDeleteFile_Click(object sender, RoutedEventArgs e)
		{
			textDeleteFile.Text = "";
			buttonBrowseDeleteFile.IsEnabled = true;
			buttonDeleteDeleteFile.IsEnabled = false;
		}

		void textDeleteFile_TextChanged(object sender, TextChangedEventArgs e)
		{
			GoButton_Enabled();
		}

		private void textDeleteFile_PreviewDragEnter(object sender, DragEventArgs e)
		{
			string f = null;
			object text = e.Data.GetData(DataFormats.FileDrop);
			TextBox tb = sender as TextBox;
			if (tb != null)
			{
				if (text != null)
					f = string.Format("{0}", ((string[])text)[0]);

				if (File.Exists(f) | Directory.Exists(f))
				{
					e.Effects = DragDropEffects.Copy;
					e.Handled = true;
				}
			}
		}

		private void textDeleteFile_PreviewDrop(object sender, DragEventArgs e)
		{
			string f = null;
			object text = e.Data.GetData(DataFormats.FileDrop);
			TextBox tb = sender as TextBox;
			if (tb != null)
			{
				if (text != null)
					f = string.Format("{0}", ((string[])text)[0]);

				if (File.Exists(f) | Directory.Exists(f))
				{
					tb.Text = f;
					buttonBrowseDeleteFile.IsEnabled = false;
					buttonDeleteDeleteFile.IsEnabled = true;
				}
			}
		}
		#endregion

		#region Music
		private void buttonBrowseMusicFile_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (ShellLibrary.IsPlatformSupported)
				{
					CommonOpenFileDialog fd = new CommonOpenFileDialog(Data.L["SelectFiles"]);
					fd.Filters.Add(new CommonFileDialogFilter(Data.L["MusicFiles"], "*.mp3,*.wma,*.wav,*.m4a,*.aac"));
					fd.Filters.Add(new CommonFileDialogFilter(Data.L["PlayLists"], "*.m3u,*.wpl,*.xspf"));
					fd.Multiselect = true;
					fd.EnsureFileExists = true;
					fd.InitialDirectory = KnownFolders.Music.Path;

					if (fd.ShowDialog() == CommonFileDialogResult.Ok)
					{
						foreach (string filename in fd.FileNames)
						{
							MusicFilesToList(filename);
						}
					}
				}
				else
				{
					OpenFileDialog fd = new OpenFileDialog();
					fd.Filter = Data.L["MusicFiles"] + "|*.mp3;*.wma;*.wav;*.m4a;*.aac;" + "|" + Data.L["PlayLists"] + "|*.m3u,*.wpl,*.xspf";
					fd.Title = Data.L["SelectFiles"];
					fd.Multiselect = true;
					fd.CheckFileExists = true;

					if ((bool)fd.ShowDialog())
					{
						foreach (string filename in fd.FileNames)
						{
							MusicFilesToList(filename);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Message.Show(ex.Message, "Error");
			}

			if (listMusicFiles.Items.Count > 0)
				listMusicFiles.Visibility = Visibility.Visible;

			GoButton_Enabled();
		}

		void MusicFilesToList(string filename)
		{
			if (Path.GetExtension(filename) == ".m3u")
			{
				string[] m3u = File.ReadAllLines(filename);

				foreach (string f in m3u)
				{
					try
					{
						string _filename;
						if (f.Substring(1, 1) == ":") //Anderes Laufwerk
							_filename = Path.GetFullPath(f);
						else
							_filename = Path.GetFullPath(Path.GetDirectoryName(filename) + "\\" + f);

						if (Data.debug_verbose) Message.Show(_filename, "Information");
						MusicFiles.Add(_filename);
						listMusicFiles.Items.Add(Path.GetFileName(f).Replace(Path.GetExtension(_filename), ""));
					}
					catch
					{
					}
				}
			}
			else if (Path.GetExtension(filename) == ".wpl")
			{
				XmlTextReader readList = new XmlTextReader(filename);
				while (readList.Read())
				{
					if (readList.NodeType == XmlNodeType.Element)
					{
						if (readList.LocalName.Equals("media"))
						{
							try
							{
								string _filename;
								if (readList.GetAttribute(0).ToString().Substring(1, 1) == ":") //Anderes Laufwerk
									_filename = Path.GetFullPath(readList.GetAttribute(0).ToString());
								else
									_filename = Path.GetFullPath(Path.GetDirectoryName(filename) + "\\" + readList.GetAttribute(0).ToString());

								if (Data.debug_verbose) Message.Show(_filename, "Information");
								MusicFiles.Add(_filename);
								listMusicFiles.Items.Add(Path.GetFileName(readList.GetAttribute(0).ToString()).Replace(Path.GetExtension(_filename), ""));
							}
							catch
							{
							}
						}
					}
				}
			}
			else if (Path.GetExtension(filename) == ".xspf")
			{
				XmlTextReader readList = new XmlTextReader(filename);
				while (readList.Read())
				{
					if (readList.LocalName.Equals("location"))
					{
						readList.Read(); //Springe zu nächstem Element
						try
						{
							if (!String.IsNullOrEmpty(readList.Value.Trim()))
							{
								string _filename = new Uri(readList.Value.Trim()).LocalPath;
								if (Data.debug_verbose) Message.Show(_filename, "Information");
								MusicFiles.Add(_filename);
								listMusicFiles.Items.Add(Path.GetFileName(_filename).Replace(Path.GetExtension(_filename), ""));
							}
						}
						catch
						{
						}
					}
				}
			}
			else
			{
				MusicFiles.Add(filename);
				listMusicFiles.Items.Add(Path.GetFileName(filename).Replace(Path.GetExtension(filename), ""));
			}
		}

		private void buttonDeleteMusicFile_Click(object sender, RoutedEventArgs e)
		{
			if (listMusicFiles.SelectedIndex == -1) return;
			MusicFiles.RemoveAt(listMusicFiles.SelectedIndex);
			listMusicFiles.Items.RemoveAt(listMusicFiles.SelectedIndex);

			if (listMusicFiles.Items.Count == 0)
			{
				buttonDeleteMusicFile.IsEnabled = false;
				buttonUpMusicFile.IsEnabled = false;
				buttonDownMusicFile.IsEnabled = false;
			}

			GoButton_Enabled();
		}

		private void listMusicFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			buttonDeleteMusicFile.IsEnabled = true;
			buttonUpMusicFile.IsEnabled = true;
			buttonDownMusicFile.IsEnabled = true;
			GoButton_Enabled();
		}

		private void buttonUpMusicFile_Click(object sender, RoutedEventArgs e)
		{
			if (listMusicFiles.SelectedIndex == -1) return;
			int iIndexSel = listMusicFiles.SelectedIndex;
			int iIndexFirst = 0;
			if ((iIndexSel != -1) && (iIndexSel - 1 >= iIndexFirst))
			{
				Object oItem = MusicFiles[iIndexSel];
				MusicFiles.RemoveAt(iIndexSel);
				MusicFiles.Insert(iIndexSel - 1, oItem);

				oItem = listMusicFiles.Items.GetItemAt(iIndexSel);
				listMusicFiles.Items.RemoveAt(iIndexSel);
				listMusicFiles.Items.Insert(iIndexSel - 1, oItem);
				listMusicFiles.SelectedIndex = listMusicFiles.Items.IndexOf(oItem);
				listMusicFiles.ScrollIntoView(listMusicFiles.SelectedItem);
			}
		}

		private void buttonDownMusicFile_Click(object sender, RoutedEventArgs e)
		{
			if (listMusicFiles.SelectedIndex == -1) return;
			int iIndexSel = listMusicFiles.SelectedIndex;
			int iIndexLast = listMusicFiles.Items.Count - 1;
			if ((iIndexSel != -1) && (iIndexSel + 1 <= iIndexLast))
			{
				Object oItem = MusicFiles[iIndexSel];
				MusicFiles.RemoveAt(iIndexSel);
				MusicFiles.Insert(iIndexSel + 1, oItem);

				oItem = listMusicFiles.Items.GetItemAt(iIndexSel);
				listMusicFiles.Items.RemoveAt(iIndexSel);
				listMusicFiles.Items.Insert(iIndexSel + 1, oItem);
				listMusicFiles.SelectedIndex = listMusicFiles.Items.IndexOf(oItem);
				listMusicFiles.ScrollIntoView(listMusicFiles.SelectedItem);
			}
		}

		private void sliderFade_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			labelFadeSlider.Content = ((e.NewValue < 10) ? "  " : "") + e.NewValue + " %";
			if (canvasMusicFadeout != null) updateCanvas();
		}

		private void sliderOrgVol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			labelOrgVolSlider.Content = ((e.NewValue < 100) ? "  " : "") + e.NewValue + " %";
			if (canvasMusicFadeout != null) updateCanvas();
		}

		private void sliderEndVol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			labelEndVolSlider.Content = ((e.NewValue < 10) ? "  " : "") + e.NewValue + " %";
			if (canvasMusicFadeout != null) updateCanvas();
		}

		void updateCanvas()
		{
			PointCollection p = new PointCollection();
			p.Add(new Point(0, canvasMusicFadeout.ActualHeight - canvasMusicFadeout.ActualHeight * (sliderOrgVol.Value / 100)));
			p.Add(new Point(0, canvasMusicFadeout.ActualHeight));
			p.Add(new Point(canvasMusicFadeout.ActualWidth, canvasMusicFadeout.ActualHeight));
			p.Add(new Point(canvasMusicFadeout.ActualWidth, canvasMusicFadeout.ActualHeight - canvasMusicFadeout.ActualHeight * (sliderOrgVol.Value / 100) * (sliderEndVol.Value / 100)));
			p.Add(new Point(canvasMusicFadeout.ActualWidth * (sliderFade.Value / 100), canvasMusicFadeout.ActualHeight - canvasMusicFadeout.ActualHeight * (sliderOrgVol.Value / 100)));
			polygonMusicFadeout.Points = p;
		}

		#endregion
		#endregion

		#region Remote
		private void expanderRemote_Expanded(object sender, RoutedEventArgs e)
		{
			if (Data.Mode == Data.Modes.WakeOnLan)
				return;

			//radioTime.IsEnabled = true;
			if (!(bool)radioNow.IsChecked) radioTime.IsChecked = true;
			radioProcessClose.IsEnabled = false;
			radioFileDelete.IsEnabled = false;
			radioPlayMusic.IsEnabled = false;
			radioIdle.IsEnabled = false;
			radioCpu.IsEnabled = false;
			radioNetwork.IsEnabled = false;
			checkPlayNoise.IsEnabled = false;
			checkPlayNoise.IsChecked = false;
			GoButton_Enabled();
		}

		private void expanderRemote_Collapsed(object sender, RoutedEventArgs e)
		{
			radioProcessClose.IsEnabled = true;
			radioFileDelete.IsEnabled = true;
			radioPlayMusic.IsEnabled = true;
			radioIdle.IsEnabled = true;
			radioCpu.IsEnabled = true;
			radioNetwork.IsEnabled = true;
			checkPlayNoise.IsEnabled = true;
			GoButton_Enabled();
		}

		private void textRemotePassword_PasswordChanged(object sender, RoutedEventArgs e)
		{
			GoButton_Enabled();
		}

		private void textRemoteServerPortMac_TextChanged(object sender, TextChangedEventArgs e)
		{
			GoButton_Enabled();
		}

		private void textRemotePort_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !Char.IsDigit(Convert.ToChar(e.Text));
		}
		#endregion

		#region GoButton
		void GoButton_Enabled()
		{
			if (this.Visibility != Visibility.Visible) return;

			GoButton.IsEnabled = true;

			#region Modus
			if (Modus.Text == "")
				GoButton.IsEnabled = false;

			if (((bool)!radioNow.IsChecked && (bool)!radioTime.IsChecked && (bool)!radioProcessClose.IsChecked && (bool)!radioFileDelete.IsChecked && (bool)!radioPlayMusic.IsChecked && (bool)!radioIdle.IsChecked && (bool)!radioCpu.IsChecked && (bool)!radioNetwork.IsChecked) && Modus.Text != Data.L["HibernateWOSBIni"])
				GoButton.IsEnabled = false;

			if (Modus.Text == Data.L["LaunchFile"])
			{
				if (textLaunchFile.Text == "")
					GoButton.IsEnabled = false;
			}
			#endregion

			#region Processs
			if ((bool)radioProcessClose.IsChecked)
			{
				if ((string)comboProcesses.SelectedItem == null)
					GoButton.IsEnabled = false;
			}

			#endregion

			#region File
			if ((bool)radioFileDelete.IsChecked)
			{
				if (textDeleteFile.Text == "")
					GoButton.IsEnabled = false;
			}
			#endregion

			#region Music
			if ((bool)radioPlayMusic.IsChecked)
			{
				if (listMusicFiles.Items.Count == 0)
					GoButton.IsEnabled = false;
			}
			#endregion

			#region Remote
			if (expanderRemote.IsExpanded | Data.Mode == Data.Modes.WakeOnLan)
			{
				if (Modus.Text == Data.L["WakeOnLan"])
				{
					if (textRemoteServer.Text == "" | textRemotePort.Text == "" | textRemoteMac.Text == "")
						GoButton.IsEnabled = false;
				}
				else
				{
					if (textRemoteServer.Text == "" | textRemotePort.Text == "" | textRemotePassword.Password == "")
						GoButton.IsEnabled = false;
				}
			}
			#endregion
		}

		private void GoButton_Click(object sender, RoutedEventArgs e)
		{
            if ((string)GoButton.Content == Data.L["Abort"])
				StopShutdown();
			else
				StartShutdown();
		}
        #endregion

        #region Keydown
        private void Main_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.F1:
					Process.Start("http://www.shutdown7.com/faq.php?lang=" + Data.Lang.ToLower());
					break;
			}
		}
		#endregion

		#region OpenWindows
		private void Updates_Click(object sender, RoutedEventArgs e)
		{
			new Updater().Show();

            //if (Data.debug_debugging)
            //    throw new ArgumentException("The parameter was invalid.");
        }

		private void About_Click(object sender, RoutedEventArgs e)
		{
			new About().Show();
		}

		private void Settings_Click(object sender, RoutedEventArgs e)
		{
			new Settings().Show();
		}
		#endregion

		#region Close
		protected override void OnClosing(CancelEventArgs e)
		{
			Remote.ExitRemoteServer = true;
            if (Data.S["SaveWindowState"])
                Properties.Settings.Default.Save();
            else
               Properties.Settings.Default.Reset();

            DisposeSystray();
            Xml.Write();

			base.OnClosing(e);
			Environment.Exit(0);
		}

        /// <summary>
        /// Called when the window is closed
        /// </summary>
        /*private void Main_Closed(object sender, EventArgs e)
        {
            // save the property settings
            Properties.Settings.Default.Save();
        }*/
        #endregion

        #region Win7
        private void Main_ContentRendered(object sender, EventArgs e)
		{
			if (Data.S["Jumplist"])
				Win7.Jumplist();

			if (Data.S["ThumbnailToolbar"])
				ThumbnailToolbar();

			if (Data.debug_stopwatch)
			{
				if (Environment.GetCommandLineArgs().Length <= 1)
				{
					App.stopwatch.Stop();
					Message.Show("Startzeit: " + App.stopwatch.Elapsed.TotalMilliseconds, "Start", "Information");
				}
			}
		}

		#region ThumbnailToolbar
		void ThumbnailToolbar()
		{
			tt1 = new ThumbnailToolBarButton(Properties.Resources.Shutdown, Data.L["Shutdown"]);
			tt2 = new ThumbnailToolBarButton(Properties.Resources.Reboot, Data.L["Restart"]);
			tt3 = new ThumbnailToolBarButton(Properties.Resources.Logoff, Data.L["Logoff"]);
			tt4 = new ThumbnailToolBarButton(Properties.Resources.Logoff, Data.L["Lock"]);
			tt5 = new ThumbnailToolBarButton(Properties.Resources.Standby, Data.L["Standby"]);
			tt6 = new ThumbnailToolBarButton(Properties.Resources.Standby, Data.L["Hibernate"]);
			//tt7 = new ThumbnailToolBarButton(Properties.Resources.WOSB, Data.L["HibernateWOSBIni"]);
			tt8 = new ThumbnailToolBarButton(Properties.Resources.Abort, Data.L["Abort"]);
			tt1.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(tt1_Click);
			tt2.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(tt2_Click);
			tt3.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(tt3_Click);
			tt4.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(tt4_Click);
			tt5.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(tt5_Click);
			tt6.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(tt6_Click);
			//tt7.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(tt7_Click);
			tt8.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(tt8_Click);

			tt8.Enabled = false;

			//Win7.ThumbnailToolbar(null , Data.L["Shutdown"], "Shutdown");

			Win7.ThumbnailToolbar(new WindowInteropHelper(Application.Current.MainWindow).Handle, new ThumbnailToolBarButton[] { tt1, tt2, tt3, tt4, tt5, tt6, /*tt7, */tt8 }, this);
		}

		public void ShowThumbnailToolbar()
		{
			if (tt1 == null)
			{
				ThumbnailToolbar();
				return;
			}
			tt1.Visible = true;
			tt2.Visible = true;
			tt3.Visible = true;
			tt4.Visible = true;
			tt5.Visible = true;
			tt6.Visible = true;
			//tt7.Visible = true;
			tt8.Visible = true;
		}

		public void HideThumbnailToolbar()
		{
			if (tt1 == null)
			{
				return;
			}
			tt1.Visible = false;
			tt2.Visible = false;
			tt3.Visible = false;
			tt4.Visible = false;
			tt5.Visible = false;
			tt6.Visible = false;
			//tt7.Visible = false;
			tt8.Visible = false;
		}

		void tt1_Click(object sender, ThumbnailButtonClickedEventArgs e)
		{
			Data.t = TimeSpan.FromSeconds(5);
			Data.Mode = Data.Modes.Shutdown;
			Execute();
		}

		void tt2_Click(object sender, ThumbnailButtonClickedEventArgs e)
		{
			Data.t = TimeSpan.FromSeconds(5);
			Data.Mode = Data.Modes.Restart;
			Execute();
		}

		void tt3_Click(object sender, ThumbnailButtonClickedEventArgs e)
		{
			Data.t = TimeSpan.FromSeconds(5);
			Data.Mode = Data.Modes.Logoff;
			Execute();
		}

		void tt4_Click(object sender, ThumbnailButtonClickedEventArgs e)
		{
			Data.t = TimeSpan.FromSeconds(5);
			Data.Mode = Data.Modes.Lock;
			Execute();
		}

		void tt5_Click(object sender, ThumbnailButtonClickedEventArgs e)
		{
			Data.t = TimeSpan.FromSeconds(5);
			Data.Mode = Data.Modes.Standby;
			Execute();
		}

		void tt6_Click(object sender, ThumbnailButtonClickedEventArgs e)
		{
			Data.t = TimeSpan.FromSeconds(5);
			Data.Mode = Data.Modes.Hibernate;
			Execute();
		}

		/*void tt7_Click(object sender, ThumbnailButtonClickedEventArgs e)
		{
			Data.t = TimeSpan.FromSeconds(0);
			Data.Mode = Data.Modes.HibernateWOSBIni;
			Execute();
		}*/

		void tt8_Click(object sender, ThumbnailButtonClickedEventArgs e)
		{
			StopShutdown();
		}

		#endregion
		#endregion
		#endregion

		#region Fade
		void Fadein(double seconds, string objname)
		{
			if (Dispatcher.Thread != Thread.CurrentThread)
				Dispatcher.Invoke((Action)delegate { Fade(true, seconds, objname); });
			else
				Fade(true, seconds, objname);
		}

		void Fadeout(double seconds, string objname)
		{
			if (Dispatcher.Thread != Thread.CurrentThread)
				Dispatcher.Invoke((Action)delegate { Fade(false, seconds, objname); });
			else
				Fade(false, seconds, objname);
		}

		void Fade(bool state, double seconds, string objname)
		{
			Storyboard storyboard = new Storyboard();
			TimeSpan duration = TimeSpan.FromSeconds(seconds);
			DoubleAnimation animation = new DoubleAnimation();

			if (state) //Fadein
			{
				animation.From = 0.0;
				animation.To = 1.0;
			}
			else //Fadeout
			{
				animation.From = 1.0;
				animation.To = 0.0;
			}

			animation.Duration = new Duration(duration);
			Storyboard.SetTargetName(animation, objname);
			Storyboard.SetTargetProperty(animation, new PropertyPath(Control.OpacityProperty));
			storyboard.Children.Add(animation);

			storyboard.Begin(this);
		}
		#endregion
		#endregion

		#region Shutdown
		#region StartShutdown
		/// <summary>
		/// Initiates the shutdown process.
		/// </summary>
		void StartShutdown()
		{
			if (Dispatcher.Thread != Thread.CurrentThread)
				{ Dispatcher.Invoke((Action)delegate { StartShutdown(); }); return; }

			if (Timer != null)
			{
				Timer.Stop();
				Timer = null;
				Timer = new DispatcherTimer();
			}

			LockUI();

            //TODO: systray hide?

			AtChecked = (bool)At.IsChecked;
            Data.S["ResumeLastAction"] = (bool)checkResumeLastAction.IsChecked;
            ScreenOffChecked = (bool)checkScreenOff.IsChecked;
			PlayNoiseChecked = (bool)checkPlayNoise.IsChecked;

			if (remote)
			{
				curRemoteServer = textRemoteServer.Text;
				curRemoteMac = textRemoteMac.Text;
				curRemotePort = Int32.Parse(textRemotePort.Text);
				curRemotePassword = textRemotePassword.Password;
			}

			#region Ask
			if (Data.S["Ask"]/*not remote*/)
			{
				string msg = "";

				switch (comboModus.SelectedIndex)
				{
					case 0:
						msg = Data.L["AskShutdown"];
						break;
					case 1:
						msg = Data.L["AskReboot"];
						break;
					case 2:
						msg = Data.L["AskLogoff"];
						break;
					case 3:
						msg = Data.L["AskLock"];
						break;
					case 4:
						msg = Data.L["AskStandby"];
						break;
					case 5:
						msg = Data.L["AskHibernate"];
						break;
				}

				if (msg != "")
				{
					if (MessageBox.Show(msg, "Shutdown7", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
					{
						StopShutdown();
						return;
					}
				}
			}
			#endregion

			if ((Data.Condition == Data.Conditions.Time || Data.Condition == Data.Conditions.Idle || Data.Condition == Data.Conditions.Cpu || Data.Condition == Data.Conditions.Network) & Visibility == Visibility.Visible & !remote)
			{
				if (AtChecked && Data.Mode != Data.Modes.HibernateWOSBTime)
				{
					double ts = (int)(DateTime.Parse(hh.Value + ":" + mm.Value + ":" + ss.Value) - DateTime.Now).TotalSeconds;
					if (ts < 0)
						ts += TimeSpan.FromDays(1).TotalSeconds;
					Data.t = TimeSpan.FromSeconds(ts);

					//Data.t = DateTime.Parse(hh.Text + ":" + mm.Text + ":" + ss.Text) - DateTime.Now;
					In.IsChecked = true;
				}
				else
					Data.t = TimeSpan.Parse(hh.Value + ":" + mm.Value + ":" + ss.Value);
					//Data.t = TimeSpan.Parse(hh.Text + ":" + mm.Text + ":" + ss.Text);

				if (Data.Condition == Data.Conditions.Cpu)
				{
					CpuMode = (comboCpu.SelectedIndex == 0);	//True: Above, False: Below
					Cpu = (int)cpu.Value;
				}
				else if (Data.Condition == Data.Conditions.Network)
				{
					NetworkMode = (comboNetwork.SelectedIndex == 0);	//True: Down, False: Up
					Network = (int)network.Value;
					NetworkAdapter = (string)comboNetworkAdapters.SelectedItem;
				}
			}

			#region ThumbnailToolbar
			if (Data.S["ThumbnailToolbar"])
			{
				tt1.Enabled = false;
				tt2.Enabled = false;
				tt3.Enabled = false;
				tt4.Enabled = false;
				tt5.Enabled = false;
				tt6.Enabled = false;
				//tt7.Enabled = false;
				tt8.Enabled = true;
			}
			#endregion

			Execute();
		}

		void LockUI()
		{
			if (Dispatcher.Thread != Thread.CurrentThread) { Dispatcher.Invoke((Action)delegate { LockUI(); }); return; }
			
			remote = (expanderRemote.IsExpanded && Data.Mode != Data.Modes.WakeOnLan);

			GoButton.IsEnabled = true; //Workaround RemoteServer Request eingehend
			GoButton.Content = Data.L["Abort"];
			Modus.IsEnabled = false;
			comboModus.IsEnabled = false;

			expanderMode.IsExpanded = false;
			expanderMode.IsEnabled = false;
			stackLaunchFile.Visibility = Visibility.Collapsed;
			expanderRemote.IsExpanded = false;
			expanderRemote.IsEnabled = false;
			expanderSettings.IsExpanded = false;
			expanderSettings.IsEnabled = false;

			#region Systray
			if (Data.S["SysIcon"] && sysicon != null)
			{
				Dispatcher.BeginInvoke((Action)delegate
				{
					sysicon.Icon = Shutdown7.Properties.Resources.ShutdownWait;
					contextRestore.Header = Data.L["Restore"];
					contextAbort.IsEnabled = true;
					contextShutdown.IsEnabled = false;
				});
			}
			#endregion

		}
		#endregion

		#region StopShutdown
		/// <summary>
		/// Stops the shutdown process
		/// </summary>
		public void StopShutdown()
		{
			if (Dispatcher.Thread != Thread.CurrentThread) { Dispatcher.Invoke((Action)delegate { StopShutdown(); }); return; }
			
			bool pok = AbortSystemShutdown(null);
			/*if (!pok)
				Message.Show(Data.L["StartShutdownError"], "Error");*/
		
			//Moved to StartShutdown() /WIESO??!!!! Rückgängig gemacht 2.1.1
			if (Timer != null)
			{
				Timer.Stop();
				Timer = null;
				Timer = new DispatcherTimer();
			}

			//Beende Musik
			if (mp3 != null)
			{
				mp3.Stop();
				mp3 = null;
			}
			if (noise != null)
			{
				noise.Stop();
				noise = null;
			}
			if (MusicThread != null/* & MusicThread.IsAlive*/)
				MusicThread.Abort();
			if (NoiseThread != null/* & MusicThread.IsAlive*/)
				NoiseThread.Abort();

			#region Systray
			if (Data.S["SysIcon"])
				sysicon.ShowBalloonTip("Shutdown7", Data.L["BalloontipAbort"], BalloonIcon.Info);
			#endregion

			UnlockUI();

			#region ThumbnailToolbar
			if (Data.S["ThumbnailToolbar"])
			{
				tt1.Enabled = true;
				tt2.Enabled = true;
				tt3.Enabled = true;
				tt4.Enabled = true;
				tt5.Enabled = true;
				tt6.Enabled = true;
				//tt7.Enabled = true;
				tt8.Enabled = false;
			}
			#endregion

			Win7.Progress(0, this);
			Win7.ProgressType("No", this);
			Win7.Overlay(null, null, this);
		}

		void UnlockUI()
		{
			//if (Dispatcher.Thread != Thread.CurrentThread) { Dispatcher.Invoke((Action)delegate { UnlockUI(); }); return; }
			
			Title = "Shutdown7";

			GoButton.Content = Data.L["GO!"];
			Modus.IsEnabled = true;

			comboModus.IsEnabled = true;
			expanderMode.IsEnabled = true;
			expanderMode.IsExpanded = true;
			if (Data.Mode == Data.Modes.Launch)
				stackLaunchFile.Visibility = Visibility.Visible;
			expanderRemote.IsEnabled = true;
			if (remote) expanderRemote.IsExpanded = true;
			expanderSettings.IsEnabled = true;
			progressBar.Value = 0;
			progressBar.IsIndeterminate = false;
			labelStatus.Content = "";

			#region Systray
			if (Data.S["SysIcon"] & sysicon != null)
			{
				sysicon.Icon = Shutdown7.Properties.Resources.Shutdown7;
				sysicon.ToolTipText = Title;
				contextAbort.IsEnabled = false;
				contextShutdown.IsEnabled = true;
			}
			#endregion

		}
		#endregion

		Stopwatch w1 = new Stopwatch();
		Stopwatch w2 = new Stopwatch();
		Stopwatch w3 = new Stopwatch();
		Stopwatch w5 = new Stopwatch();

		#region Execute
		public void Execute()
		{
			if (Data.LocalByArgs)
			{
				#region Form
				Dispatcher.Invoke((Action)delegate
				{
					LockUI();
					switch (Data.Mode)
					{
						case Data.Modes.Shutdown:
							comboModus.SelectedIndex = 0;
							break;
						case Data.Modes.Restart:
							comboModus.SelectedIndex = 1;
							break;
						case Data.Modes.Logoff:
							comboModus.SelectedIndex = 2;
							break;
						case Data.Modes.Lock:
							comboModus.SelectedIndex = 3;
							break;
						case Data.Modes.Standby:
						case Data.Modes.StandbyWOSB:
							comboModus.SelectedIndex = 4;
							break;
						case Data.Modes.Hibernate:
						case Data.Modes.HibernateWOSB:
							comboModus.SelectedIndex = 5;
							break;
						case Data.Modes.HibernateWOSBIni:
						case Data.Modes.HibernateWOSBTime:
						case Data.Modes.WakeOnLan:
						case Data.Modes.Launch:
						case Data.Modes.RestartAndroid:
						default:
							break;
					}

					//Workaround
					expanderMode.IsExpanded = false;
					expanderMode.IsEnabled = false;
					expanderRemote.IsExpanded = false;
					expanderRemote.IsEnabled = false;
				});

				#endregion
			}
			
			if (!remote | Data.Mode == Data.Modes.WakeOnLan)
			{
				#region Lokal

                //Prevent crash
                if (Timer != null)
                {
                    if (Timer.IsEnabled)
                    {
                        //Führe WOSB-Aktionen trotzdem aus, da unabhängig
                        if (Data.Mode == Data.Modes.HibernateWOSBIni | Data.Mode == Data.Modes.HibernateWOSBTime)
                        {
                            Shutdown();
                            Data.Mode = Data.orgMode;
                            return;
                        }

                        //Aktuellen beenden
                        Message.Show(Data.L["RemoteBusy"], "Warning");
                        return;
                        /// TODO: Timer wurde schon überschrieben, also wird neuer Countdown gesetzt und die Warnung kommt

                        //Diesen anhalten
                        /*Timer.Stop();
                        Timer = null;*/
                    }
                }

                if (Timer != null)
                    Timer.Stop();
				Timer = new DispatcherTimer();
				Timer.Interval = TimeSpan.FromSeconds(1);

				switch (Data.Condition)
				{
					case Data.Conditions.Process:
						progressBar.IsIndeterminate = true;
						Win7.ProgressType("Indeterminate", this);

						Timer.Tick += new EventHandler(WaitProcessClose);

						#region Systray
						if (Data.S["SysIcon"])
						{
							Dispatcher.BeginInvoke((Action)delegate
							{
								string Baloontiptext = "";
								if (Data.S["AllProcesses"])
									Baloontiptext = Data.L["BalloontipProcess" + Data.Mode.ToString()];
								else
									Baloontiptext = Data.L["BalloontipWindow" + Data.Mode.ToString()];

								sysicon.ShowBalloonTip("Shutdown7", String.Format(Baloontiptext, ProcessName + (Data.S["AllProcesses"] ? ".exe" : "")), BalloonIcon.Info);
							});
						}
						#endregion
						break;
					case Data.Conditions.File:
						progressBar.IsIndeterminate = true;
						Win7.ProgressType("Indeterminate", this);

						Timer.Tick += new EventHandler(WaitFileDelete);

						#region Systray
						if (Data.S["SysIcon"])
						{
							Dispatcher.BeginInvoke((Action)delegate
							{
								string Baloontiptext = Data.L["BalloontipFile" + Data.Mode.ToString()];
								sysicon.ShowBalloonTip("Shutdown7", String.Format(Baloontiptext, FileName), BalloonIcon.Info);
							});
						}
						#endregion
						break;
					case Data.Conditions.Music:
						Dispatcher.Invoke((Action)delegate
						{
							orgVolume = sliderOrgVol.Value / 100;
							if ((bool)checkMusicFadeout.IsChecked)
							{
								startfade = (int)sliderFade.Value;
								endVolume = sliderEndVol.Value / 100;
							}
							else
							{
								startfade = 100;
								endVolume = orgVolume;
							}
						});

						mp3 = new MediaPlayer();
						mp3.Volume = orgVolume;

						length = 0;
						foreach (string fn in MusicFiles.ToArray(typeof(string)))
						{
							try
							{
								length += TagLib.File.Create(fn).Properties.Duration.TotalSeconds;
							}
							catch (Exception ex)
							{
								Message.Show(ex.Message + "\n" + fn, "Music", "Error");
							}
						}
						Data.t = TimeSpan.FromSeconds(length);

						Timer.Tick += new EventHandler(WaitMusicPlaying);

						MusicThread = new Thread(new ParameterizedThreadStart(WaitMusicPlay));
						MusicThread.Start(MusicFiles.ToArray(typeof(string)));

						w2.Start();

						#region Systray
						if (Data.S["SysIcon"])
						{
							Dispatcher.BeginInvoke((Action)delegate
							{
								string Baloontiptext = Data.L["BalloontipMusic" + Data.Mode.ToString()];
								//String.Format(Baloontiptext, "test.mp3");
								//Message.Show(String.Format(Baloontiptext, "test.mp3"));

								String musicfiles = "";
								foreach (string fn in MusicFiles.ToArray(typeof(string)))
								{ musicfiles += Path.GetFileName(fn) + ", "; }
								musicfiles = musicfiles.Substring(0, musicfiles.Length - 2);
								
								sysicon.ShowBalloonTip("Shutdown7", String.Format(Baloontiptext, musicfiles), BalloonIcon.Info);
							});
						}
						#endregion
						break;
					case Data.Conditions.Idle:
						progressBar.IsIndeterminate = true;
						Win7.ProgressType("Indeterminate", this);

						Timer.Tick += new EventHandler(WaitIdle);
						GetIdleTime();

						#region Systray
						if (Data.S["SysIcon"])
						{
							Dispatcher.BeginInvoke((Action)delegate
							{
								string Baloontiptext = Data.L["BalloontipIdle" + Data.Mode.ToString()];
								sysicon.ShowBalloonTip("Shutdown7", String.Format(Baloontiptext, ((Data.t.Hours < 10) ? "0" : "") + Data.t.Hours + ":" + ((Data.t.Minutes < 10) ? "0" : "") + Data.t.Minutes + ":" + ((Data.t.Seconds < 10) ? "0" : "") + Data.t.Seconds), BalloonIcon.Info);
							});
						}
						#endregion
						break;
					case Data.Conditions.Cpu:
						if (/*total*/Data.t.TotalSeconds > 0 && Data.Mode != Data.Modes.HibernateWOSBTime)
						{
							progressBar.IsIndeterminate = true;
							Win7.ProgressType("Indeterminate", this);

							cpuCounter = new PerformanceCounter();
							cpuCounter.CategoryName = "Processor";
							cpuCounter.CounterName = "% Processor Time";
							cpuCounter.InstanceName = "_Total";
							cpuCounter.NextValue();		//Initialize
							cpuHits = 0;
							Timer.Tick += new EventHandler(WaitCpu);
							
							#region Systray
							if (Data.S["SysIcon"])
							{
								Dispatcher.BeginInvoke((Action)delegate
								{
									string Baloontiptext = Data.L["BalloontipCpu" + Data.Mode.ToString()];
									sysicon.ShowBalloonTip("Shutdown7", String.Format(Baloontiptext, (CpuMode ? Data.L["Above"] : Data.L["Below"]).ToLower(), Cpu, ((Data.t.Hours < 10) ? "0" : "") + Data.t.Hours + ":" + ((Data.t.Minutes < 10) ? "0" : "") + Data.t.Minutes + ":" + ((Data.t.Seconds < 10) ? "0" : "") + Data.t.Seconds), BalloonIcon.Info);
								});
							}
							#endregion
						}
						else
							Shutdown();
						break;
					case Data.Conditions.Network:
						progressBar.IsIndeterminate = true;
						Win7.ProgressType("Indeterminate", this);
						networkHits = 0;
						Timer.Tick += new EventHandler(WaitNetwork);

						#region Systray
						if (Data.S["SysIcon"])
						{
							Dispatcher.BeginInvoke((Action)delegate
							{
								string Baloontiptext = Data.L["BalloontipNetwork" + Data.Mode.ToString()];
								sysicon.ShowBalloonTip("Shutdown7", String.Format(Baloontiptext, (NetworkMode ? Data.L["Down"] : Data.L["Up"]).ToLower(), Network, ((Data.t.Hours < 10) ? "0" : "") + Data.t.Hours + ":" + ((Data.t.Minutes < 10) ? "0" : "") + Data.t.Minutes + ":" + ((Data.t.Seconds < 10) ? "0" : "") + Data.t.Seconds), BalloonIcon.Info);
							});
						}
						#endregion
						break;
					case Data.Conditions.Time:
						total = (int)Data.t.TotalSeconds;		//Wird gebraucht für Prozent-Berechnung in WaitTime
						if (/*total*/Data.t.TotalSeconds > 0 && Data.Mode != Data.Modes.HibernateWOSBTime)
						{
							Timer.Tick += new EventHandler(WaitTime);

							#region Systray
							if (Data.S["SysIcon"] && sysicon != null)
							{
								Dispatcher.BeginInvoke((Action)delegate
								{
									string Baloontiptext = Data.L["BalloontipTime" + Data.Mode.ToString()];
									sysicon.ShowBalloonTip("Shutdown7", String.Format(Baloontiptext, ((Data.t.Hours < 10) ? "0" : "") + Data.t.Hours + ":" + ((Data.t.Minutes < 10) ? "0" : "") + Data.t.Minutes + ":" + ((Data.t.Seconds < 10) ? "0" : "") + Data.t.Seconds), BalloonIcon.Info);
								});
							}
							#endregion
						}
						else
							Shutdown();
						break;
					default: //ModeNow
						Data.t = TimeSpan.FromSeconds(0);
						Timer = null;
						Shutdown();
						return;
				}
	
				#region Systray
				if (Data.S["SysIcon"])
                {
                    //WindowState = WindowState.Minimized;
                    //Visibility = Visibility.Hidden;
                    Hide();
                    //contextRestore.Header = Data.L["Restore"];
                    /*Dispatcher.Invoke((Action)delegate {
						WindowState = WindowState.Minimized;
                        Hide();
					});*/
                }
				#endregion

				if (Data.debug_verbose)
					new Thread(new ThreadStart(delegate {
						Message.Show("Wartezeit:\n" + Data.t.Hours + ":" + Data.t.Minutes + ":" + Data.t.Seconds, "Wait", "Information");
					})).Start();
				
				w3.Start();
				Timer.Start();

				if (PlayNoiseChecked)
				{
					NoiseThread = new Thread(new ThreadStart(PlayNoise));
					NoiseThread.Start();
				}

				if (ScreenOffChecked)
				{
					Thread.Sleep(500);
					Dispatcher.BeginInvoke((Action)delegate { SendMessage((int)new WindowInteropHelper(this).Handle, 0x0112, 0xF170, 2); });
				}
				#endregion
			}
			else
			{
				#region Remote
				progressBar.IsIndeterminate = true;
				Win7.ProgressType("Indeterminate", this);

				if (AtChecked && Data.Mode != Data.Modes.HibernateWOSBTime)
				{
					Data.t = DateTime.Parse(hh.Value + ":" + mm.Value + ":" + ss.Value) - DateTime.Now;
					//Data.t = DateTime.Parse(hh.Text + ":" + mm.Text + ":" + ss.Text) - DateTime.Now;
					In.IsChecked = true;
				}
				else
					Data.t = TimeSpan.Parse(hh.Value + ":" + mm.Value + ":" + ss.Value);
					//Data.t = TimeSpan.Parse(hh.Text + ":" + mm.Text + ":" + ss.Text);
				
				new Thread(RemoteClientStart).Start();
				#endregion
			}

			//For Arguments
			if (Data.orgMode != Data.Modes.None)
			{
				Data.Mode = Data.orgMode;
				Data.orgMode = Data.Modes.None;
			}
		}

		/// <summary>
		/// Starts the shutdown process.
		/// </summary>
		///
		public void Shutdown()
		{
			if (Data.debug_noexecute)
            {
                if (Data.debug_debugging)
                    Message.Show("Would start execution here.");

				if (!Data.S["StayAfterShutdown"])
				{
                    if (!Timer.IsEnabled)
                        Close();//Environment.Exit(0); //killt nur aktuellen Prozess
					//Application.Current.Shutdown(); //killt alle Prozesse
					return;
				} else {
					Timer.Stop();
					StopShutdown();
					return;
                }
            }

            Process Shutdown = new Process();
			Shutdown.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			Shutdown.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\shutdown.exe";
			Shutdown.StartInfo.Verb = "runas";

			switch (Data.Mode)
			{
				case Data.Modes.Shutdown:
					if (Data.S["Force"])
					{
						//ExitWindowsEx(5, 0);
						if (Data.S["Win8Hybrid"])
							Shutdown.StartInfo.Arguments = "-s -hybrid -f -t 0";
						else
							Shutdown.StartInfo.Arguments = "-s -f -t 0";
					}
					else
					{
						//ExitWindowsEx(1, 0);
						if (Data.S["Win8Hybrid"])
							Shutdown.StartInfo.Arguments = "-s -hybrid -t 0";
						else
							Shutdown.StartInfo.Arguments = "-s -t 0";
					}

					try
					{
						bool pok = Shutdown.Start();
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
                    }
                    /*catch (FileNotFoundException ex)
                    {
                        Message.Show(Data.L["StartShutdownError"] + ex.Message, "Error");
                    }*/
                    catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ((Data.debug_verbose) ? "\n" + ex.Message : ""), "Error");
					}
					break;
				case Data.Modes.Restart:
					if (Data.S["Force"])
					{
						//bool pok = ExitWindowsEx(6, 0);
						Shutdown.StartInfo.Arguments = "-r -f -t 0";
					}
					else
					{
						//bool pok = ExitWindowsEx(2, 0);
						Shutdown.StartInfo.Arguments = "-r -t 0";
					}

					try
					{
						bool pok = Shutdown.Start();
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
					}
					catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}
					break;
				case Data.Modes.Logoff:
					if (Data.S["Force"])
					{
						//bool pok = ExitWindowsEx(4, 0);
						Shutdown.StartInfo.Arguments = "-l -f";
					}
					else
					{
						//bool pok = ExitWindowsEx(0, 0);
						Shutdown.StartInfo.Arguments = "-l";
					}

					try
					{
						bool pok = Shutdown.Start();
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
					}
					catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}
					break;
				case Data.Modes.Lock:
					try
					{
						bool pok = LockWorkStation();
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
					}
					catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}
					break;
				case Data.Modes.Standby:
					try
					{
						bool pok = SetSuspendState(false, Data.S["Force"], false);
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
					}
					catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}
					break;
				case Data.Modes.StandbyWOSB:
					Shutdown.StartInfo.FileName = Data.WOSBPath;
					Shutdown.StartInfo.Verb = "";

					Shutdown.StartInfo.Arguments = "/run /ami /standby";
					if (Data.S["Force"])
						Shutdown.StartInfo.Arguments += "/force";

					try
					{
						bool pok = Shutdown.Start();
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
					}
					catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}
					break;
				case Data.Modes.Hibernate:
					try
					{
						bool pok = SetSuspendState(true, Data.S["Force"], false);
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
					}
					catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}
					break;
				case Data.Modes.HibernateWOSB:
					Shutdown.StartInfo.FileName = Data.WOSBPath;
					Shutdown.StartInfo.Verb = "";
					
					Shutdown.StartInfo.Arguments = "/run /ami /hibernate";
					if (Data.S["Force"])
						Shutdown.StartInfo.Arguments += "/force";

					try
					{
						bool pok = Shutdown.Start();
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
					}
					catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}
					break;
				case Data.Modes.HibernateWOSBIni:
					Shutdown.StartInfo.FileName = Data.WOSBPath;
					Shutdown.StartInfo.Verb = "";

					WOSBZeit = GetWZeit().ToLongTimeString();

					Shutdown.StartInfo.Arguments = "/run /ami /systray " + "tm=\"" + WOSBZeit + "\" file=\"" + Data.W[Data.curProfile]["File"] + "\" params=\"" + Data.W[Data.curProfile]["Params"] + "\" awfile=\"" + Data.W[Data.curProfile]["AwFile"] + "\" awparams=\"" + Data.W[Data.curProfile]["AwParams"] + "\" " + Data.W[Data.curProfile]["Extra"];
					if (Data.S["Force"])
						Shutdown.StartInfo.Arguments += " /force";

					try
					{
						bool pok = Shutdown.Start();
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
					}
                    catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}
					break;
				case Data.Modes.HibernateWOSBTime:
					Xml.ReadWOSB();

					if (this.Visibility == Visibility.Visible)
					{
						try { WOSBZeit = DateTime.Parse(Data.t.Hours + ":" + Data.t.Minutes + ":" + Data.t.Seconds).ToLongTimeString(); }
						catch { DateTime Time = DateTime.Now.AddHours(1); WOSBZeit = Time.Hour + ":" + Time.Minute + ":" + Time.Second; }
					}
					else
						WOSBZeit = DateTime.Now.AddHours(1).ToLongTimeString();
					//	WOSBZeit = DateTime.Parse(Microsoft.VisualBasic.Interaction.InputBox("Zeit eingeben")).ToLongTimeString();

					Shutdown.StartInfo.FileName = Data.WOSBPath;
					Shutdown.StartInfo.Arguments = "/run /ami /systray " + "tm=\"" + WOSBZeit + "\" file=\"" + Data.W[Data.curProfile]["File"] + "\" params=\"" + Data.W[Data.curProfile]["Params"] + "\" awfile=\"" + Data.W[Data.curProfile]["AwFile"] + "\" awparams=\"" + Data.W[Data.curProfile]["AwParams"] + "\" " + Data.W[Data.curProfile]["Extra"];
					if (Data.S["Force"])
						Shutdown.StartInfo.Arguments += " /force";

					try
					{
						bool pok = Shutdown.Start();
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
					}
					catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}
					break;
				case Data.Modes.WakeOnLan:
					try
					{
						if (curRemoteMac.Contains(".")) //IP-Adresse
							curRemoteMac = Remote.IpToMacAddress(IPAddress.Parse(curRemoteMac));
						if (curRemoteMac == null)
						{
							RemoteDisplayMsg(Data.L["RemoteServerTimeout"]);
							StopShutdown();
							return;
						}
						curRemoteMac = curRemoteMac.Replace("-", "").Replace(":", "");

						int counter = 0;
						byte[] packet = new byte[17 * 6];
						for (int y = 0; y < 6; y++)
							packet[counter++] = 0xFF;

						for (int y = 0; y < 16; y++)
						{
							int i = 0;
							for (int z = 0; z < 6; z++)
							{
								packet[counter++] = byte.Parse(curRemoteMac.Substring(i, 2), NumberStyles.HexNumber);
								i += 2;
							}
						}

						if (Data.debug_verbose)
							Message.Show("Mac-Adresse: " + curRemoteMac + "\nBroadcast-IP: " + IPAddress.Broadcast + "\nPort: " + curRemotePort + "\nPacket: " + BitConverter.ToString(packet), "Shutdown7", "Information");
						
						UdpClient client = new UdpClient();
						client.EnableBroadcast = true;
						client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 0);
						client.Send(packet, packet.Length, new IPEndPoint(Dns.GetHostEntry(curRemoteServer).AddressList[0],curRemotePort));
					}
					catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}

					break;
				case Data.Modes.Launch:
					Shutdown.StartInfo.FileName = LaunchFile;
					Shutdown.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
					Shutdown.StartInfo.Verb = "";

					try
					{
						bool pok = Shutdown.Start();
						if (!pok)
							Message.Show(Data.L["StartShutdownError"], "Error");
					}
					catch (Exception ex)
					{
						Message.Show(Data.L["StartShutdownError"] + ex.Message /* ((Data.debug_verbose) ? "\n" + ex.Message : "") */, "Error");
					}
					break;
				case Data.Modes.RestartAndroid:
					new Thread(new ThreadStart(RestartAndroid)).Start();
					//RestartAndroid();
					break;
				default:
					Message.Show(Data.L["StartShutdownError"], "Error");
					break;
			}

			if (Data.debug_stopwatch & Environment.GetCommandLineArgs().Length > 1)
				Message.Show("Gesamtzeit: " + App.stopwatch.Elapsed.TotalMilliseconds, "Start", "Information");

			if (!Data.S["StayAfterShutdown"]) //TODO: Workaround App.cs verbessern
			{
                // 2.3.1: Obsolete
                /*if (Timer == null) //TODO: Workaround App.cs verbessern
					Timer = new DispatcherTimer();*/

                //Crash if WOSBTime
                if (Timer != null)
                {
                    if (!Timer.IsEnabled)
                    {
                        DisposeSystray();
                        Environment.Exit(0); //killt nur aktuellen Prozess
                                             //Application.Current.Shutdown(); //killt alle Prozesse

                    }
                }
                else
                {
                    DisposeSystray();
                    Environment.Exit(0); //killt nur aktuellen Prozess
                                         //Application.Current.Shutdown(); //killt alle Prozesse
                }
            }
			else
			{
                if (Timer != null)
                    Timer.Stop();
                else
                    Timer = new DispatcherTimer();
                //StopShutdown();
                UnlockUI();
			}
		}

		void RestartAndroid()
		{
			
			//try
			//{
			Dispatcher.Invoke((Action)delegate
			{
				LockUI();
				progressBar.IsIndeterminate = true;
				labelStatus.Content = "Waiting for device"; //Data.L["WaitingForAndroid"]
			});

			AndroidController android = AndroidController.Instance;
			Device device;
			string serialNumber;

			if (!android.HasConnectedDevices)
			{
				Dispatcher.Invoke((Action)delegate
				{
					UnlockUI();
					//labelStatus.Content = Data.L["NoAndroidFound"];
				});
				
				Message.Show(Data.L["NoAndroidFound"], "Error");
				
				android.Dispose();
				return;
			}
			else
				if (Data.debug_verbose) Message.Show(String.Join(";", android.ConnectedDevices.ToArray()));

			Dispatcher.BeginInvoke((Action)delegate
			{
				progressBar.IsIndeterminate = true;
				labelStatus.Content = "Rebooting";
			});

			android.WaitForDevice();
			serialNumber = android.ConnectedDevices[0];
			device = android.GetConnectedDevice(serialNumber);
			
			if (Data.debug_verbose)
				Message.Show("Connected Device - " + device.SerialNumber + "\nBattery: " + device.Battery.Level + " Root: " + device.HasRoot, "Information");
			
			device.Reboot();
			android.Dispose();
		
			Dispatcher.Invoke((Action)delegate
			{
				UnlockUI();
			});

			/*}
			catch (Exception ex)
			{
				Message.Show(Data.L["StartShutdownError"] + ((Data.debug_verbose) ? "\n" + ex.Message : ""), "Error");
			}*/
		}
		#endregion

		#region Wait
		void WaitTime(object sender, EventArgs e)
		{
			if ((int)Data.t.TotalSeconds == 0)
			{
				Timer.Stop();
				if (Visibility == Visibility.Visible)
				{
					Win7.Progress(100, this);
					Dispatcher.BeginInvoke((Action)delegate { progressBar.Value = 100; });
				}
				Shutdown();
				return;
			}

			Data.t = Data.t.Subtract(TimeSpan.FromSeconds(1));
			percent = 100 - (Data.t.TotalSeconds / total) * 100;

			#region Form
			Dispatcher.Invoke((Action)delegate
			{
				Title = Data.t.ToString();
				progressBar.Value = percent;
				ss.Value = Data.t.Seconds;
				mm.Value = Data.t.Minutes;
				hh.Value = Data.t.Hours;
				
				/*ss.Text = t.Seconds.ToString();
				mm.Text = t.Minutes.ToString();
				hh.Text = t.Hours.ToString();
				if (hh.Text.Length == 1) hh.Text = "0" + hh.Text;
				if (mm.Text.Length == 1) mm.Text = "0" + mm.Text;
				if (ss.Text.Length == 1) ss.Text = "0" + ss.Text;*/
			});
			#endregion
			
			if (Visibility == Visibility.Visible)
			{
				#region Win7

				#region Taskbar
				Win7.Progress((int)percent, this);

				if (percent >= 90)
					Win7.ProgressType("Error", this);
				else if (percent >= 80)
					Win7.ProgressType("Paused", this);
				else
					Win7.ProgressType("Normal", this);
				#endregion

				#region Overlay
				if (Data.S["Overlay"])
				{
					switch ((int)Data.t.TotalSeconds)
					{
						case 1:
							Win7.Overlay(Properties.Resources._1, "1", this);
							break;
						case 2:
							Win7.Overlay(Properties.Resources._2, "2", this);
							break;
						case 3:
							Win7.Overlay(Properties.Resources._3, "3", this);
							break;
						case 4:
							Win7.Overlay(Properties.Resources._4, "4", this);
							break;
						case 5:
							Win7.Overlay(Properties.Resources._5, "5", this);
							break;
						case 6:
							Win7.Overlay(Properties.Resources._6, "6", this);
							break;
						case 7:
							Win7.Overlay(Properties.Resources._7, "7", this);
							break;
						case 8:
							Win7.Overlay(Properties.Resources._8, "8", this);
							break;
						case 9:
							Win7.Overlay(Properties.Resources._9, "9", this);
							break;
						case 0:
							Win7.Overlay(Properties.Resources._0, "0", this);
							break;
						default:
							Win7.Overlay(null, null, this);
							break;
					}
				}
				#endregion
				#endregion
			}

			#region Systray
			if (Data.S["SysIcon"] && sysicon != null)
				sysicon.ToolTipText = ((Data.t.Hours < 10) ? "0" : "") + Data.t.Hours + ":" + ((Data.t.Minutes < 10) ? "0" : "") + Data.t.Minutes + ":" + ((Data.t.Seconds < 10) ? "0" : "") + Data.t.Seconds;
			#endregion
		}

		void WaitProcessClose(object sender, EventArgs e)
		{
			bool stop = true;
			if (Data.S["AllProcesses"])
			{
				foreach (Process p in Process.GetProcessesByName(ProcessName))
				{
					if (p.ProcessName == ProcessName)
						stop = false;
				}
			}
			else
			{
				foreach (Process p in Process.GetProcesses())
				{
					if (p.MainWindowTitle == ProcessName)
						stop = false;
				}
			}

			if (stop)
			{
				Timer.Stop();
				if (Visibility == Visibility.Visible)
				{
					Win7.Progress(100, this);
					Dispatcher.BeginInvoke((Action)delegate { progressBar.Value = 100; });
				}
				Shutdown();
			}
		}

		void WaitFileDelete(object sender, EventArgs e)
		{
			if (!File.Exists(FileName))
			{
				Timer.Stop();
				if (Visibility == Visibility.Visible)
				{
					Win7.Progress(100, this);
					Dispatcher.BeginInvoke((Action)delegate { progressBar.Value = 100; });
				}
				Shutdown();
			}
		}

		void WaitMusicPlaying(object sender, EventArgs e)
		{
			if ((int)Data.t.TotalSeconds == 0)
			{
				w3.Stop(); w5.Stop();
				Timer.Stop();
				if (Visibility == Visibility.Visible)
				{
					Win7.Progress(100, this);
					Dispatcher.BeginInvoke((Action)delegate { progressBar.Value = 100; });
				}

				if (Data.debug_verbose)
					Message.Show("Songlänge: " + (int)length + "\nPlaythread:" + (int)w2.Elapsed.TotalSeconds + "\nWaitthread:" + (int)w3.Elapsed.TotalSeconds + "\nUnterschied:" + (int)w5.Elapsed.TotalSeconds);
				
				Shutdown();
			}

			Data.t = Data.t.Subtract(TimeSpan.FromSeconds(1));
			double _t = length - Data.t.TotalSeconds;
			percent = (_t / length) * 100;

			if (Visibility == Visibility.Visible)
			{
				#region Form
				Dispatcher.BeginInvoke((Action)delegate
				{
					Title = TimeSpan.FromSeconds((int)length - (int)_t).ToString();
					progressBar.Value = percent;
					hh.Value = Data.t.Hours;
					ss.Value = Data.t.Seconds;
					mm.Value = Data.t.Minutes;
				});
				#endregion

				#region Win7
				#region Taskbar
				Win7.Progress((int)percent, this);

				if (percent >= 90)
					Win7.ProgressType("Error", this);
				else if (percent >= 80)
					Win7.ProgressType("Paused", this);
				else
					Win7.ProgressType("Normal", this);
				#endregion

				#region Overlay
				if (Data.S["Overlay"])
				{
					switch ((int)Data.t.TotalSeconds)
					{
						case 1:
							Win7.Overlay(Properties.Resources._1, "1", this);
							break;
						case 2:
							Win7.Overlay(Properties.Resources._2, "2", this);
							break;
						case 3:
							Win7.Overlay(Properties.Resources._3, "3", this);
							break;
						case 4:
							Win7.Overlay(Properties.Resources._4, "4", this);
							break;
						case 5:
							Win7.Overlay(Properties.Resources._5, "5", this);
							break;
						case 6:
							Win7.Overlay(Properties.Resources._6, "6", this);
							break;
						case 7:
							Win7.Overlay(Properties.Resources._7, "7", this);
							break;
						case 8:
							Win7.Overlay(Properties.Resources._8, "8", this);
							break;
						case 9:
							Win7.Overlay(Properties.Resources._9, "9", this);
							break;
						case 0:
							Win7.Overlay(Properties.Resources._0, "0", this);
							break;
						default:
							Win7.Overlay(null, null, this);
							break;
					}
				}
				#endregion
				#endregion
			}

			#region Systray
			if (Data.S["SysIcon"] && sysicon != null)
				sysicon.ToolTipText = ((Data.t.Hours < 10) ? "0" : "") + Data.t.Hours + ":" + ((Data.t.Minutes < 10) ? "0" : "") + Data.t.Minutes + ":" + ((Data.t.Seconds < 10) ? "0" : "") + Data.t.Seconds;
			#endregion

			#region Musik-Fadeout
			if ((bool)checkMusicFadeout.IsChecked)
			{
				if (percent >= startfade & !Data.debug_mute)
				{
					FadeTotalRunTime++;
					double FadeTime = length - length * startfade / 100;
					double FadeVolumeAdj = (orgVolume - endVolume) / FadeTime;
					double NewVol = orgVolume - (FadeVolumeAdj * FadeTotalRunTime);

					/*if (Data.debug_verbose & percent >= 95)
						Message.Show("Stück-Länge: " + length + "\nSeit Begin-Fade: " + FadeTotalRunTime + "\nVoladj: " + FadeVolumeAdj + "\nFadetime: " + FadeTime + "\nOld-Volume: " + orgVolume + "\nEnd-Volume: " + endVolume + "\nVolume: " + NewVol, "Shutdown7", "Information");
					*/

					if (Data.debug_verbose & percent >= 95)
						Debug.WriteLine("\nVoladj: " + FadeVolumeAdj + "\nFadetime: " + FadeTime + "\nOld-Volume: " + orgVolume + "\nEnd-Volume: " + endVolume + "\nVolume: " + NewVol + "\n");

					Dispatcher.Invoke((Action)delegate { mp3.Volume = NewVol; });

					//MMDevice defaultDevice = new MMDeviceEnumerator().GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia).AudioEndpointVolume.MasterVolumeLevel = NewVol;
				}
			}
			#endregion
		}

		#region Musik-Hilfsfunktion
		void WaitMusicPlay(object items)
		{
			//w2.Start();
			foreach (string fn in (string[])items)
			{
				if (!Timer.IsEnabled) return;
				
				Dispatcher.Invoke((Action)delegate
				{
					if (Data.debug_mute) mp3.Volume = 0;
					mp3.Open(new Uri(fn));
					mp3.Play();
				});

				Thread.Sleep((int)TagLib.File.Create(fn).Properties.Duration.TotalMilliseconds);
				
				if (!Timer.IsEnabled) return;

				Dispatcher.Invoke((Action)delegate
				{
					mp3.Stop();
					mp3.Close();
				});
			}
			w2.Stop();
			w5.Start();

			//Shutdown();
		}

		void PlayNoise()
		{
			do
			{
				//if (!Timer.IsEnabled) return;

				Dispatcher.Invoke((Action)delegate
				{
					noise = new MediaPlayer();
					if (Data.debug_mute) noise.Volume = 0;
					noise.Open(new Uri(Data.NoisePath));
					noise.Play();
				});

				Thread.Sleep((int)TagLib.File.Create(Data.NoisePath).Properties.Duration.TotalMilliseconds);

				//if (!Timer.IsEnabled) return;

				Dispatcher.Invoke((Action)delegate
				{
					if (noise != null)
					{
						noise.Stop();
						noise.Close();
					}
				});
			} while (Timer.IsEnabled);
		}
		#endregion

		void WaitIdle(object sender, EventArgs e)
		{
			if ((GetIdleTime() / 1000) > Data.t.TotalSeconds)
			{
				if (Data.debug_verbose)
					Message.Show(GetIdleTime() / 1000 + " " + Data.t.TotalSeconds, "Wait - Idle", "Information");

				Timer.Stop();
				if (Visibility == Visibility.Visible)
				{
					Win7.Progress(100, this);
					Dispatcher.Invoke((Action)delegate { progressBar.Value = 100; });
				}
				Shutdown();
			}
		}

		#region Idle-Hilfsfunktionen
		public static uint GetIdleTime()
		{
			LASTINPUTINFO lastInPut = new LASTINPUTINFO();
			lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
			GetLastInputInfo(ref lastInPut);

			return ((uint)Environment.TickCount - lastInPut.dwTime);
		}

		internal struct LASTINPUTINFO
		{
			public uint cbSize;
			public uint dwTime;
		}
		#endregion

		void WaitCpu(object sender, EventArgs e)
		{
			int cpuusage = GetCpu();
			bool conditionCpu;
			if (CpuMode)
				conditionCpu = cpuusage > Cpu;
			else
				conditionCpu = cpuusage < Cpu;
			//bool conditionCpu = CpuMode ? cpuusage > Cpu : cpuusage < Cpu;	//Geht nicht :(

			if (conditionCpu)
			{
				cpuHits++;

				if (cpuHits == Data.t.TotalSeconds)
				{
					if (Data.debug_verbose)
						Message.Show(cpuusage + "% Erfüllt: " + conditionCpu + " " + cpuHits + " " + Data.t.TotalSeconds, "Wait - Cpu", "Information");

					Timer.Stop();
					cpuHits = 0;
					if (Visibility == Visibility.Visible)
					{
						Win7.Progress(100, this);
						Dispatcher.Invoke((Action)delegate { progressBar.Value = 100; });
					}

					Shutdown();

				}
			}
			else
			{
				cpuHits = 0;
			}
		}

		#region Cpu-Hilfsfunktionen
		//https://stackoverflow.com/questions/278071/how-to-get-the-cpu-usage-in-c
		int GetCpu()
		{
			int secondValue = (int)cpuCounter.NextValue();
			return secondValue;
		}
		#endregion

		void WaitNetwork(object sender, EventArgs e)
		{
			int networkusage = GetNetwork(NetworkMode);

			if (networkusage < Network)
			{
				networkHits++;

				if (networkHits == Data.t.TotalSeconds)
				{
					if (Data.debug_verbose)
						Message.Show(networkusage + " kbps " + " Mode:" + NetworkMode + " Hits: " + networkHits + " " + Data.t.TotalSeconds, "Wait - Network", "Information");

					Timer.Stop();
					cpuHits = 0;
					if (Visibility == Visibility.Visible)
					{
						Win7.Progress(100, this);
						Dispatcher.Invoke((Action)delegate { progressBar.Value = 100; });
					}

					Shutdown();

				}
			}
			else
			{
				cpuHits = 0;
			}
		}

		#region Network-Hilfsfunktionen
		int GetNetwork(bool mode)
		{
			if (!NetworkInterface.GetIsNetworkAvailable())
				return 0;

			NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
			long b = 0;

			foreach (NetworkInterface ni in interfaces)
			{
				if (ni.Name != NetworkAdapter)
					continue;

				IPv4InterfaceStatistics s = ni.GetIPv4Statistics();

				if (mode)
					b += s.BytesReceived / 1000;
				else
					b += s.BytesSent / 1000;
			}

			return (int)b;
		}

		String[] GetNetworkAdapters()
		{
			//TODO: Check counting

			if (!NetworkInterface.GetIsNetworkAvailable())
				return null;

			NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
			List<string> adapters = new List<string>();

			foreach (NetworkInterface ni in interfaces)
			{
				adapters.Add(ni.Name);
			}

			return adapters.ToArray();
		}
		#endregion

		#endregion

		#region WOSB
		DateTime GetWZeit()
		{//Bug: Sonntag selektiert Zeit von Montag
			Xml.ReadWOSB();

			string WZeit = null;
			DayOfWeek curWeekday;
			int diff;

			if (Data.debug_verbose) Message.Show("Aktuelles Profil: " + Data.curProfile, "WOSB Ini", "Information");

			foreach (KeyValuePair<string, string> kvp in Data.W[Data.curProfile])
			{
				if (kvp.Key.Length > 3) continue;

				DateTime dtime;
				curWeekday = GetDayOfWeek(kvp.Key.Substring(0, 2));

				diff = curWeekday - DateTime.Now.DayOfWeek;
				if (diff == -6) diff += 7; //Workaround - test!
				//Message.Show(DateTime.Now.AddDays(diff).Day + "." + DateTime.Now.AddDays(diff).Month + "." + DateTime.Now.AddDays(diff).Year + " " + kvp.Value);
				if (!DateTime.TryParse(DateTime.Now.AddDays(diff).Day + "." + DateTime.Now.AddDays(diff).Month + "." + DateTime.Now.AddDays(diff).Year + " " + kvp.Value, out dtime))
				{
					Message.Show(Data.L["XmlReadError_1"], "Error");
					Application.Current.Shutdown();
				}

				if (Data.debug_verbose)
					Message.Show(kvp.Key + "\nDifferenz: " + (dtime - DateTime.Now).Days + "d " + (dtime - DateTime.Now).Hours + "h ", "WOSB Ini", "Information");
				
				if (dtime < DateTime.Now)
					continue;

				//if (Data.debug_verbose) Message.Show(kvp.Key + " " + kvp.Value, "WOSB Ini", "Information");
				if (Data.debug_verbose) Message.Show("Treffer!\nDann: " + curWeekday.ToString() + "\nHeute: " + DateTime.Now.DayOfWeek + "\nDifferenz: " + diff, "WOSB Ini", "Information");

				if (dtime > DateTime.Now)
				{
					WZeit = kvp.Value;
					if (Data.debug_verbose) Message.Show("Wakeup-Time: " + curWeekday + " " + kvp.Value, "WOSB Ini", "Information");
					break;
				}
			}

			if (!String.IsNullOrEmpty(WZeit))
			{
				return DateTime.Parse(WZeit);
			}
			else
			{
				Message.Show(Data.L["NoWakeUpSheduled"], "Warning");
				Environment.Exit(0);
				return DateTime.Now;
			}
		}

		DayOfWeek GetDayOfWeek(string Day)
		{
			switch(Day)
			{
				case "Mo":
					return DayOfWeek.Monday;
				case "Tu":
					return DayOfWeek.Tuesday;
				case "We":
					return DayOfWeek.Wednesday;
				case "Th":
					return DayOfWeek.Thursday;
				case "Fr":
					return DayOfWeek.Friday;
				case "Sa":
					return DayOfWeek.Saturday;
				case "So":
					return DayOfWeek.Sunday;
				case "Su":
					return DayOfWeek.Sunday;
				default:
					return DayOfWeek.Monday;
			}
		}
		#endregion

		#region Remote
		#region Server
		public void StartRemoteServer()
		{
			if (tcpListener != null) return;

			try
			{
				tcpListener = new TcpListener(IPAddress.Any, Data.RemotePort);
				tcpListener.Start();
			}
			catch// (Exception ex)
			{
				//if (Data.debug_verbose)
				//	Message.Show(ex.Message, "RemoteServer", "Error");
				return;
			}

			Thread.Sleep(1000);

			while (!Remote.ExitRemoteServer)
			{
				if (tcpListener.Pending())
				{
					client = tcpListener.AcceptTcpClient();
					Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
					clientThread.Start(client);
				}
				Thread.Sleep(100);
			}
			tcpListener.Stop();
		}

		public void StopRemoteServer()
		{
			Remote.ExitRemoteServer = true;
		}

		public void HandleClientComm(object client)
		{
			TcpClient tcpClient = (TcpClient)client;
			NetworkStream clientStream = tcpClient.GetStream();
			byte[] message = new byte[4096];
			int bytesRead;

			RemoteDisplayMsg("");
			while (!Remote.ExitRemoteServer)
			{
				//Lese Nachricht
				bytesRead = 0; try { bytesRead = clientStream.Read(message, 0, 1024); } catch { break; } if (bytesRead == 0) break;
				string Input = new ASCIIEncoding().GetString(message, 0, bytesRead);

				//if (Data.debug_verbose)
				//	Message.Show("Input: " + Input, "Remote Server", "Information");
				
				if (Input.StartsWith("GET")) //Webbrowser
				{
					StopShutdown();
					byte[] buffer = null;
					if (Input.StartsWith("GET /?abort"))
					{
						StopShutdown();
						buffer = new ASCIIEncoding().GetBytes(Remote.GetWebUI(Timer, ProcessName, FileName, listMusicFiles.Items, true));
					}
					else
					{
						buffer = new ASCIIEncoding().GetBytes(Remote.GetWebUI(Timer, ProcessName, FileName, listMusicFiles.Items, false));
					}
					clientStream.Write(buffer, 0, buffer.Length);
					clientStream.Flush();
					clientStream.Close();
					tcpClient.Close();

					return;
				}

				if (Input.ToUpper() == "PING") //Porttest
				{
					RemoteDisplayMsg("");
					byte[] buffer = new ASCIIEncoding().GetBytes("Pong");
					clientStream.Write(buffer, 0, buffer.Length);
					clientStream.Flush();
					clientStream.Close();
					tcpClient.Close();

					return;
				}

				if (Input.ToUpper() == "STATUS") //Porttest
				{
					RemoteDisplayMsg("");

					byte[] buffer = null;

					if (Timer.IsEnabled)
						Dispatcher.Invoke((Action)delegate { buffer = new ASCIIEncoding().GetBytes(Data.Mode + "|" + Data.Condition + "|" + GetParamFromCondition(Data.Condition)); });
					else
						buffer = new ASCIIEncoding().GetBytes("IDLE");

					clientStream.Write(buffer, 0, buffer.Length);
					clientStream.Flush();
					clientStream.Close();
					tcpClient.Close();

					return;
				}

				RemoteDisplayMsg(Data.L["Receive"]);

				//Shutdown7 Client / WebUI
				string[] ShutdownString = Input.Split('|'); //Modi|Time|PW
				if (ShutdownString.Length != 3)
				{
					RemoteDisplayMsg(Data.L["RemoteErrorRequest"]);
					byte[] buffer = new ASCIIEncoding().GetBytes("ERRORREQUEST");
					clientStream.Write(buffer, 0, buffer.Length);
					clientStream.Flush();
					clientStream.Close();
					tcpClient.Close();

					Message.Show(Data.L["RemoteErrorRequest"], "Error");
					Thread.Sleep(2000);
					RemoteDisplayMsg("");
					return;
				}

				//Falsches Passwort
				if (ShutdownString[2] != Data.RemotePassword)
				{
					RemoteDisplayMsg(Data.L["RemoteWrongPassShort"]);
					byte[] buffer = new ASCIIEncoding().GetBytes("WRONGPW");
					clientStream.Write(buffer, 0, buffer.Length);
					clientStream.Flush();
					clientStream.Close();
					tcpClient.Close();

					if (Data.S["SysIcon"])
					{
						sysicon.ShowBalloonTip("Shutdown7", Data.L["RemoteWrongPass"], BalloonIcon.Warning);
					}
					else
					{
						Message.Show(Data.L["RemoteWrongPass"], "Warning");
					}

					Thread.Sleep(2000);
					RemoteDisplayMsg("");
					return;
				}

				//Abbrechen
				if (ShutdownString[0] == Data.Modes.Abort.ToString())
				{
					byte[] buffer;
					if (Timer.IsEnabled)
					{
						RemoteDisplayMsg(Data.L["Aborted"]);
						buffer = new ASCIIEncoding().GetBytes("OK");
						clientStream.Write(buffer, 0, buffer.Length);
						StopShutdown();
					}
					else
					{
						buffer = new ASCIIEncoding().GetBytes("IDLE");
						clientStream.Write(buffer, 0, buffer.Length);
					}
					clientStream.Flush();
					clientStream.Close();
					tcpClient.Close();

					Thread.Sleep(2000);
					RemoteDisplayMsg("");
					return;
				}

				//Shutdown läuft bereits
				if (Timer.IsEnabled)
				{
					RemoteDisplayMsg(Data.L["RemoteBusyShort"]);
					byte[] buffer = new ASCIIEncoding().GetBytes("BUSY");
					clientStream.Write(buffer, 0, buffer.Length);
					clientStream.Flush();
					clientStream.Close();
					tcpClient.Close();
					
					Thread.Sleep(2000);
					RemoteDisplayMsg("");
					return;
				}

				//OK
				byte[] bufferOK = new ASCIIEncoding().GetBytes("OK");
				clientStream.Write(bufferOK, 0, bufferOK.Length);
				clientStream.Flush();
				clientStream.Close();
				tcpClient.Close();
			
				RemoteDisplayMsg(Data.L["Sucessful"]);

				StopShutdown();

				Data.Condition = Data.Conditions.Time;
				Data.t = TimeSpan.FromSeconds(Double.Parse(ShutdownString[1]));
				remote = false;

				#region Form
				Dispatcher.Invoke((Action)delegate
				{
					hh.Value = Data.t.Hours; mm.Value = Data.t.Minutes; ss.Value = Data.t.Seconds;

					switch (ShutdownString[0])
					{
						case "Shutdown":
							comboModus.SelectedIndex = 0;
							break;
						case "Restart":
							comboModus.SelectedIndex = 1;
							break;
						case "Logoff":
							comboModus.SelectedIndex = 2;
							break;
						case "Lock":
							comboModus.SelectedIndex = 3;
							break;
						case "Standby":
							comboModus.SelectedIndex = 4;
							break;
						case "Hibernate":
							comboModus.SelectedIndex = 5;
							break;
					}
				});
				#endregion

				bool Askold = Data.S["Ask"];
				Data.S["Ask"] = false;
				StartShutdown();
				Data.S["Ask"] = Askold;
			}

			Thread.Sleep(3000);
			RemoteDisplayMsg("");
		}

		private string GetParamFromMode(Data.Modes mode)
		{
			switch (mode)
			{
				case Data.Modes.Launch:
					return LaunchFile;
				default:
					return null;
			}
		}

		private string GetParamFromCondition(Data.Conditions condition)
		{
			switch (condition)
			{
				case Data.Conditions.Now:
				case Data.Conditions.None:
					return "0";
				case Data.Conditions.Time:
				case Data.Conditions.Idle:
					return Data.t.TotalSeconds.ToString();
				case Data.Conditions.File:
					return FileName;
				case Data.Conditions.Process:
					return ProcessName + (Data.S["AllProcesses"] ? ".exe" : "");
				case Data.Conditions.Music:
					string files = "";
					foreach (string curfile in MusicFiles)
						files += curfile.Substring(curfile.LastIndexOf("\\") + 1) + "?";
					return (int)length + "*" + files;
				case Data.Conditions.Cpu:
					return Data.t.TotalSeconds.ToString() + "?" + Cpu.ToString() + "?" + CpuMode;
				default:
					return null;
			}
		}

		#endregion

		#region Client
		void RemoteClientStart()
		{
			string statusmsg = "";

			try
			{
				RemoteDisplayMsg(Data.L["RemoteConnect"]);
				curRemoteIP = Remote.GetIP(curRemoteServer);

				TcpClient client = new TcpClient();
				client.Connect(new IPEndPoint(IPAddress.Parse(curRemoteIP), curRemotePort));
				NetworkStream clientStream = client.GetStream();
				RemoteDisplayMsg(Data.L["RemoteSend"]);
				ASCIIEncoding encoder = new ASCIIEncoding();

				byte[] buffer = encoder.GetBytes(Data.Mode + "|" + Data.t.TotalSeconds + "|" + Remote.md5(curRemotePassword));
				clientStream.Write(buffer, 0, buffer.Length);
				clientStream.Flush();

				byte[] message = new byte[32];
				statusmsg = new ASCIIEncoding().GetString(message, 0, clientStream.Read(message, 0, message.Length));
			}
			catch (SocketException ex)
			{
				Debug.WriteLine(ex.Message);
				if (Data.debug_verbose)
					Message.Show(ex.Message, "Remote Server", "Error");
				if (ex.SocketErrorCode == SocketError.TimedOut)
					statusmsg = "TIMEOUT";
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				if (Data.debug_verbose)
					Message.Show(ex.Message, "Remote Server", "Error");
			}

			if (Data.debug_verbose)
				Message.Show("Antwort von Server: " + statusmsg, "Remote Server", "Information");

			if (Data.S["SendFeedback"])
			{
				//Send Data to WebUI
				new Thread(new ParameterizedThreadStart(SendFeedback)).Start(statusmsg);
			}

			if (statusmsg == "OK")
				RemoteDisplayMsg(Data.L["Sucessful"]);
			else if (statusmsg == "TIMEOUT")
				RemoteDisplayMsg(Data.L["RemoteServerTimeout"]);
			else if (statusmsg == "WRONGPW")
				RemoteDisplayMsg(Data.L["RemoteWrongPassShort"]);
			else if (statusmsg == "BUSY")
			{
				RemoteDisplayMsg(Data.L["RemoteBusyShort"]);
				if (!Data.RemoteByArgs)
					Message.Show(Data.L["RemoteBusy"], "Warning");
			}
			else
				RemoteDisplayMsg(Data.L["RemoteError"]);

			if (Data.RemoteByArgs)
			{
				Thread.Sleep(1000);
				Environment.Exit((statusmsg == "OK") ? 1 : 0);
				return;
			}

			StopShutdown();

			Thread.Sleep(2000);
			RemoteDisplayMsg("");
		}

		#region SendFeedback
		void SendFeedback(object statusmsg)
		{
			try
			{
				string postData = "logonly=true&url=" + curRemoteServer + "&mac=" + curRemoteMac + "&port=" + curRemotePort + "&modi=" + Data.Mode + "&client=Shutdown7 " + Data.Version + "&msg=" + (string)statusmsg + "&ok=" + ((string)statusmsg == "OK");
				if (Data.debug_verbose)
					Message.Show(postData, "RemoteClient - SendFeedback", "Information");
				byte[] byteArray = Encoding.UTF8.GetBytes(postData);

				WebRequest request = WebRequest.Create("http://www.shutdown7.com/webui.php");
				request.Method = "POST";
				request.ContentLength = byteArray.Length;
				request.ContentType = "application/x-www-form-urlencoded";
				Stream dataStream = request.GetRequestStream();
				dataStream.Write(byteArray, 0, byteArray.Length);
				dataStream.Close();
			}
			catch (Exception ex)
			{
				if (Data.debug_verbose)
					Message.Show(ex.Message, "RemoteClient - SendFeedback", "Error");
			}
		}
		#endregion

		void RemoteDisplayMsg(string txt)
		{
			//if (this.Visibility != Visibility.Visible) return;
			if (Data.RemoteByArgs)
			{
				Console.WriteLine(txt);
				return;
			}

			if (Dispatcher.Thread != Thread.CurrentThread)
			{
				Dispatcher.BeginInvoke((Action)delegate { RemoteDisplayMsg(txt); });
				return;
			}

			Fadein(0.5, labelStatus.Name);

			if (txt != "")
			{
				labelStatus.Content = txt;
			}
			else
			{
				Fadeout(0.5, labelStatus.Name);
				//labelStatus.Content = "";
			}
		}
		#endregion
		#endregion
		#endregion

		#region Systray
		static bool hiding = false;
		public void CreateSystray()
		{
			if (sysicon != null) return;
			sysicon = new TaskbarIcon();
			sysicon.TrayMouseDoubleClick += new RoutedEventHandler(sysicon_Click);
			StateChanged += new EventHandler(MainWindow_StateChanged);
			sysicon.Icon = Shutdown7.Properties.Resources.Shutdown7;
			sysicon.ToolTipText = Title;

			ContextMenu menu = new ContextMenu();

			contextRestore = new MenuItem();
			contextRestore.Header = Data.L["Hide"];
			contextRestore.Click += new RoutedEventHandler(contextRestore_Click);
			contextRestore.FontWeight = FontWeights.Bold;
			menu.Items.Add(contextRestore);

			contextAbort = new MenuItem();
			contextAbort.Header = Data.L["Abort"];
			contextAbort.Click += new RoutedEventHandler(contextAbort_Click);
			contextAbort.Icon = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Shutdown7;component/Resources/Abort.ico")), Width = 16, Height = 16 };
			menu.Items.Add(contextAbort);

			/*contextAutostart = new MenuItem();
			contextAutostart.Header = Data.L["Autostart"];
			contextAutostart.IsCheckable = true;
			contextAutostart.IsChecked = Data.S["Autostart"];
			contextAutostart.Click += new RoutedEventHandler(contextAutostart_Click);
			menu.Items.Add(contextAutostart);*/

			contextShutdown = new MenuItem();
			contextShutdown.Header = Data.L["Shutdown"];
			menu.Items.Add(contextShutdown);

			contextShutdown1 = new MenuItem();
			contextShutdown1.Header = Data.L["Shutdown"];
			contextShutdown1.Click += new RoutedEventHandler(contextShutdown1_Click);
			contextShutdown1.Icon = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Shutdown7;component/Resources/Shutdown.ico")), Width = 16, Height = 16 };
			contextShutdown.Items.Add(contextShutdown1);
						
			contextShutdown2 = new MenuItem();
			contextShutdown2.Header = Data.L["Restart"];
			contextShutdown2.Click += new RoutedEventHandler(contextShutdown2_Click);
			contextShutdown2.Icon = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Shutdown7;component/Resources/Reboot.ico")), Width = 16, Height = 16 };
			contextShutdown.Items.Add(contextShutdown2);

			contextShutdown3 = new MenuItem();
			contextShutdown3.Header = Data.L["Logoff"];
			contextShutdown3.Click += new RoutedEventHandler(contextShutdown3_Click);
			contextShutdown3.Icon = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Shutdown7;component/Resources/Logoff.ico")), Width = 16, Height = 16 };
			contextShutdown.Items.Add(contextShutdown3);

			contextShutdown4 = new MenuItem();
			contextShutdown4.Header = Data.L["Lock"];
			contextShutdown4.Click += new RoutedEventHandler(contextShutdown4_Click);
			contextShutdown4.Icon = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Shutdown7;component/Resources/Logoff.ico")), Width = 16, Height = 16 };
			contextShutdown.Items.Add(contextShutdown4);

			contextShutdown5 = new MenuItem();
			contextShutdown5.Header = Data.L["Standby"];
			contextShutdown5.Click += new RoutedEventHandler(contextShutdown5_Click);
			contextShutdown5.Icon = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Shutdown7;component/Resources/Standby.ico")), Width = 16, Height = 16 };
			contextShutdown.Items.Add(contextShutdown5);

			contextShutdown6 = new MenuItem();
			contextShutdown6.Header = Data.L["Hibernate"];
			contextShutdown6.Click += new RoutedEventHandler(contextShutdown6_Click);
			contextShutdown6.Icon = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Shutdown7;component/Resources/Standby.ico")), Width = 16, Height = 16 };
			contextShutdown.Items.Add(contextShutdown6);

			contextShutdown7 = new MenuItem();
			contextShutdown7.Header = Data.L["HibernateWOSBIni"];
			contextShutdown7.Click += new RoutedEventHandler(contextShutdown7_Click);
			contextShutdown7.Icon = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Shutdown7;component/Resources/WOSB.ico")), Width = 16, Height = 16 };
			if (!Data.S["WOSB"]) contextShutdown7.Visibility = Visibility.Collapsed;
			contextShutdown.Items.Add(contextShutdown7);

			/*contextShutdown8 = new MenuItem();
			contextShutdown8.Header = Data.L["Abort"];
			contextShutdown8.Click += new RoutedEventHandler(contextShutdown8_Click);
			contextShutdown8.Icon = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Shutdown7;component/Resources/Abort.ico")), Width = 16, Height = 16 };
			contextShutdown.Items.Add(contextShutdown8);*/

			if (Timer.IsEnabled)
			{
				contextAbort.IsEnabled = true;
				contextShutdown.IsEnabled = false;
				/*contextShutdown1.IsEnabled = false;
				contextShutdown2.IsEnabled = false;
				contextShutdown3.IsEnabled = false;
				contextShutdown4.IsEnabled = false;
				contextShutdown5.IsEnabled = false;
				contextShutdown6.IsEnabled = false;
				contextShutdown7.IsEnabled = false;
				contextShutdown8.IsEnabled = true;*/
			}
			else
			{
				contextAbort.IsEnabled = false;
				contextShutdown.IsEnabled = true;
				/*contextShutdown1.IsEnabled = true;
				contextShutdown2.IsEnabled = true;
				contextShutdown3.IsEnabled = true;
				contextShutdown4.IsEnabled = true;
				contextShutdown5.IsEnabled = true;
				contextShutdown6.IsEnabled = true;
				contextShutdown7.IsEnabled = true;
				contextShutdown8.IsEnabled = false;*/
			}
			
			menu.Items.Add(new Separator());

			contextExit = new MenuItem();
			contextExit.Header = Data.L["Exit"];
			contextExit.Click += new RoutedEventHandler(contextExit_Click);
			menu.Items.Add(contextExit);

			sysicon.ContextMenu = menu;
			sysicon.Visibility = Visibility.Visible;
		}

		public void DisposeSystray()
		{
			if (sysicon != null)
			{
				sysicon.Visibility = Visibility.Collapsed;
				sysicon.Dispose();
			}
		}

		#region Events
		void MainWindow_StateChanged(object sender, EventArgs e)
		{
			if (Data.S["SysIcon"] & !hiding)
			{
				switch (WindowState)
				{
					case WindowState.Minimized:
						Visibility = Visibility.Hidden;
						Hide();
						contextRestore.Header = Data.L["Restore"];
						break;
					case WindowState.Normal:
                        Activate();
                        Focus();
                        contextRestore.Header = Data.L["Hide"];
						break;
				}
			}
		}

		public void sysicon_Click(object sender, RoutedEventArgs e)
		{
			if (Visibility == Visibility.Hidden)
			{
				Show();
				WindowState = WindowState.Normal;
				Activate();
				Focus();
				contextRestore.Header = Data.L["Hide"];
			}
			else
			{
				hiding = true;
				WindowState = WindowState.Minimized;
				hiding = false;
				Hide();
				contextRestore.Header = Data.L["Restore"];
			}
		}
		#endregion

		#region Contextmenu
		public void contextRestore_Click(object sender, EventArgs e)
		{
			if (WindowState == WindowState.Minimized)
			{
				contextRestore.Header = Data.L["Hide"];
				this.Visibility = Visibility.Visible;
				this.Show();
				this.WindowState = WindowState.Normal;
				this.Focus();
			}
			else
			{
				contextRestore.Header = Data.L["Restore"];
				hiding = true;
				this.WindowState = WindowState.Minimized;
				this.Visibility = Visibility.Hidden;
				this.Hide();
				hiding = false;
			}

		}

		/*public void contextAutostart_Click(object sender, EventArgs e)
		{
			if (contextAutostart.IsChecked)
			{
				Autostart.SetAutoStart("Shutdown", Data.EXE + " /Run");
				Data.S["Autostart"] = true;
				contextAutostart.IsChecked = true;
			}
			else
			{
				Autostart.UnSetAutoStart("Shutdown");
				Data.S["Autostart"] = false;
				contextAutostart.IsChecked = false;
			}
			Xml.Write();
		}*/

		public void contextExit_Click(object sender, EventArgs e)
		{
			sysicon.Dispose();
			Application.Current.Shutdown();
		}

		public void contextShutdown1_Click(object sender, EventArgs e)
		{
			sysicon.ShowBalloonTip("Shutdown7", String.Format(Data.L["BalloontipTimeShutdown"], Data.L["FiveSecs"]), BalloonIcon.Info);
			Data.Mode = Data.Modes.Lock;
			Data.Condition = Data.Conditions.Time;
			Data.t = TimeSpan.FromSeconds(5);
			comboModus.SelectedIndex = 0;
			LockUI();
			Execute();
		}

		public void contextShutdown2_Click(object sender, EventArgs e)
		{
			sysicon.ShowBalloonTip("Shutdown7", String.Format(Data.L["BalloontipTimeRestart"], Data.L["FiveSecs"]), BalloonIcon.Info);
			Data.Mode = Data.Modes.Lock;
			Data.Condition = Data.Conditions.Time;
			Data.t = TimeSpan.FromSeconds(5);
			comboModus.SelectedIndex = 1;
			LockUI();
			Execute();
		}

		public void contextShutdown3_Click(object sender, EventArgs e)
		{
			sysicon.ShowBalloonTip("Shutdown7", String.Format(Data.L["BalloontipTimeLogoff"], Data.L["FiveSecs"]), BalloonIcon.Info);
			Data.Mode = Data.Modes.Lock;
			Data.Condition = Data.Conditions.Time;
			Data.t = TimeSpan.FromSeconds(5);
			comboModus.SelectedIndex = 2;
			LockUI();
			Execute();
		}

		public void contextShutdown4_Click(object sender, EventArgs e)
		{
			sysicon.ShowBalloonTip("Shutdown7", String.Format(Data.L["BalloontipTimeLock"], Data.L["FiveSecs"]), BalloonIcon.Info);
			Data.Mode = Data.Modes.Lock;
			Data.Condition = Data.Conditions.Time;
			Data.t = TimeSpan.FromSeconds(5);
			comboModus.SelectedIndex = 3;
			LockUI();
			Execute();
		}

		public void contextShutdown5_Click(object sender, EventArgs e)
		{
			sysicon.ShowBalloonTip("Shutdown7", String.Format(Data.L["BalloontipTimeStandby"], Data.L["FiveSecs"]), BalloonIcon.Info);
			Data.Mode = Data.Modes.Lock;
			Data.Condition = Data.Conditions.Time;
			Data.t = TimeSpan.FromSeconds(5);
			comboModus.SelectedIndex = 4;
			LockUI();
			Execute();
		}

		public void contextShutdown6_Click(object sender, EventArgs e)
		{
			sysicon.ShowBalloonTip("Shutdown7", String.Format(Data.L["BalloontipTimeHibernate"], Data.L["FiveSecs"]), BalloonIcon.Info);
			Data.Mode = Data.Modes.Lock;
			Data.Condition = Data.Conditions.Time;
			Data.t = TimeSpan.FromSeconds(5);
			comboModus.SelectedIndex = 5;
			LockUI();
			Execute();
		}

		public void contextShutdown7_Click(object sender, EventArgs e)
		{
			Data.Mode = Data.Modes.HibernateWOSBIni;
			expanderMode.IsEnabled = false;
			expanderMode.IsExpanded = false;
			expanderRemote.IsEnabled = false;
			expanderRemote.IsExpanded = false;
			Execute();
		}

		public void contextAbort_Click(object sender, EventArgs e)
		{
			sysicon.ShowBalloonTip("Shutdown7", Data.L["BalloontipAbort"], BalloonIcon.Info);
			StopShutdown();
		}
		#endregion
		#endregion
	}
}

