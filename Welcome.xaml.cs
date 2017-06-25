using System;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;

namespace Shutdown7
{
	/// <summary>
	/// Interaktionslogik für Welcome.xaml
	/// </summary>
	public partial class Welcome : Window
	{
		public static bool welcome, update;
		int curscreen;
		bool done, skip;
		string[] Titles, Instructions;
		TextBlock labelBugs = new TextBlock();

		public Welcome()
		{
			InitializeComponent();

			#region Lang
			switch (Data.Lang)
			{
				case "de":
					Titles = new string[]
					{
						"Willkommen bei Shutdown7",
						"Neues in Version " + Data.Version,
						"Autostart",
						"Einstellungen",
						"Windows 7-Features",
						"Remote-Funktionen",
						"Remote-Server Einstellungen",
						"WOSB",
						"WOSB Einstellungen",
						"User-Feedback",
						"Abgeschlossen",
					};

					Instructions = new string[]
					{
						"Dieser Assistent hilft Ihnen, Shutdown7 für den ersten Gebrauch zu konfigurieren.",
						"Neue Funktionen in dieser Version",
						"Wenn Sie möchten, lädt Shutdown7 jeden Windowsstart mit, um sofort bereit zu sein. Dies empfiehlt sich insbesondere, wenn Sie die Remote-Server-Option benutzen möchten.",
						"Hier können Sie das Verhalten von Shutdown7 festlegen.",
						"Diese Funktionen sind teilweise erst ab Windows 7 verfügbar.",
						"Indem Sie diese Funktionen aktivieren, können Sie Shutdown7 über das Internet steuern.",
						"Bitte konfigurieren Sie Shutdown7 für die Serverfunktion.",
						"Hier können Sie die Integration mit Wakeup from Standby and Hibernate (WOSB) aktivieren. Mit diesem Programm können Sie ihren PC zu beliebigen Zeiten aus dem Standby oder Ruhezustand automatisch aufwecken lassen.\n\nLaden Sie hierzu WOSB von der Website des Entwicklers herunter und legen Sie die wosb.exe in das gleiche Verzeichnis wie Shutdown7.",
						"Stellen Sie die Aufwach-Zeiten für WOSB im Format hh:mm:ss ein.\nSie können Sie auch leer lassen.",
						"Erlauben Sie Shutdown7, anonymer Nutzungsstatistiken an den Entwickler zu senden.",
						"Die Einrichtung ist hiermit abgeschlossen.\nIch wünsche Ihnen viel Spaß mit Shutdown7.\n\nBitte senden Sie mir eine E-Mail, falls Shutdown7 nicht so funktionieren sollte,\nwie es sollte und erwägen Sie, eine kleine Spende für dieses kostenlose Programm zu entrichten.",
					};
							
					checkAutostart.Content = "Benutze Autostart";
					//checkAsk.Content = "Abfrage vor Shutdown";
					checkForce.Content = "Shutdown erzwingen";
					checkPinTaskbar.Content = "An Taskleiste anheften";
					checkRemoteClient.Content = "Client-Funktionen";
					checkRemoteServer.Content = "Server-Funktionen";
					checkSendFeedback.Content = "Sende Nutzerstatistiken";
					checkStayAfterShutdown.Content = "Bleibe Aktiv nach Ausführung";
					checkWOSB.Content = "Benutze WOSB";
					labelWOSBAM.Content = "Vormittag";
					labelWOSBPM.Content = "Nachmittag";
					labelWOSBTime2.Content = "Montag";
					labelWOSBTime4.Content = "Dienstag";
					labelWOSBTime6.Content = "Mittwoch";
					labelWOSBTime8.Content = "Donnerstag";
					labelWOSBTime10.Content = "Freitag";
					labelWOSBTime12.Content = "Samstag";
					labelWOSBTime14.Content = "Sonntag";
					labelBugs.Text = "Behobene Bugs in dieser Version";
					labelRemotePassword.Content = "Passwort:";
					buttonPortCheck.Content = "Portweiterleitung testen";
					buttonFirewallException.Content = "Windows Firewallausnahme";
					buttonDownloadWOSB.Content = "Lade WOSB herunter";
					buttonCheckWOSB.Content = "Prüfe, ob verfügbar";
					buttonSendMail.Content = "Nachricht an den Entwickler";
					buttonHelp.Content = "Online-Hilfe";
					buttonDonate.Content = "Spenden";

					//TTAsk.Text = "Abfrage vor Herunterfahren, Neustart, Logoff, Sperren, Standby und Hibernate, wenn kein Timeout angegeben wurde.\nNur über GUI wenn kein Countdown angegeben wurde,\nnicht über Jumplist oder Parametern";
					TTForce.Text = "Erzwingt das Schließen aller offenen Programme.\nUngespeicherte Daten können verloren gehen.";
					//TTGlass.Text = "Alle Fenster sind semi-transparent.\nNur bei Windows Vista und Windows 7.";
					TTJumplist.Text = "Aktiviert die Jumplist (Rechtsklick auf Taskbar-Icon).\nVerfügbar ab Windows 7.";
					TTOverlay.Text = "Zeigt beim Countdown die letzten Zahlen im Taskbar-Icon an.\nVerfügbar ab Windows 7.";
					TTPinTaskbar.Text = "Heftet Shutdown7 für Schnellzugriffe an die Taskleiste an.\nVerfügbar ab Windows 7.";
					TTRemoteClient.Text = "Ermöglicht, andere Rechner mit Shutdown7 über das Internet zu steuern.";
					TTRemoteServer.Text = "Ermöglicht, über das Internet gesteuert zu werden.\nErfordert Ausnahme in der Firewall";
					TTRemotePort.Text = "Auf diesem Port wird Shutdown7 später erreichbar sein.";
					TTRemotePassword.Text = "Zu Ihrer Sicherheit muss man ein Passwort eingeben, um eine Aktion über das Internet auszuführen.";
					TTSendFeedback.Text = "Die Daten sind anonymisiert und dienen nur Statistik-Zwecken.\nEs handelt sich hierbei nur um Informationen wie Betriebssystem, Sprache und Nutzungsweise.";
					TTStayAfterShutdown.Text = "Shutdown7 beendet sich nicht nach Ausführen einer Aktion.";
					TTSysicon.Text = "Zeigt ein Icon im Systray (unten rechts in der Taskleiste) an.\nDas Hauptfenster wird ins Systray minimiert.";

					Data.L["Skip"] = "Überspringen";
					Data.L["Abort"] = "Abbrechen";
					Data.L["Next"] = "Weiter";
					Data.L["Complete"] = "Fertig stellen";
					Data.L["ConfirmAbortWelcomeScreen"] = "Möchten Sie diesen Assistenten wirklich abbrechen?";

					buttonAbort.Content = Data.L["Abort"];
					buttonNext.Content = Data.L["Next"];
					buttonBack.Content = "Zurück";
					break;
				default:
					Titles = new string[]
					{
						"Welcome to Shutdown7",
						"New in version " + Data.Version,
						"Autostart",
						"Settings",
						"Windows 7 features",
						"Remote functions",
						"Remote-server settings",
						"WOSB",
						"WOSB settings",
						"User feedback",
						"Completed",
					};

					Instructions = new string[]
					{
						"This wizard will help you to setup Shutdown7 for first use.",
						"New features in this version.",
						"If you like to, Shutdown7 starts everytime with Windows, so it's ready immediately. That's recommandable if you want to use the remote-server option.",
						"Here you can customize the behaviour of Shutdown7.",
						"Some of these features are shipped with Windows 7 only.",
						"If you activate these functions you can control Shutdown7 over the internet.",
						"Please configure the connection settings for the server-function.",
						"You can enable interaction with Wakeup from Standby and Hibernate (WOSB) here. This application allows you to automatically wake up your PC from standby or hibenation.\n\nTo do so, download WOSB from the developer's website and put the exe-file in the same directory as Shutdown7.",
						"Specify the wake-up times for WOSB in the format hh:mm:ss.\nYou can let them empty as well.",
						"Allow Shutdown7 to send anonymous user statistics to the developer.",
						"The setup is completed now.\nEnjoy Shutdown7.\n\nPlease send me an e-mail if Shutdown7 shouldn't work as expected\nand consider to donate a small amount for this free application.",
					};

					checkAutostart.Content = "Use Autostart";
					//checkAsk.Content = "Ask before shutdown";
					checkForce.Content = "Force shutdown";
					checkPinTaskbar.Content = "Pin to taskbar";
					checkRemoteClient.Content = "Client-functions";
					checkRemoteServer.Content = "Server-functions";
					checkSendFeedback.Content = "Send usage statistics";
					checkWOSB.Content = "Use WOSB";
					labelWOSBAM.Content = "A.M.";
					labelWOSBPM.Content = "P.M.";
					labelWOSBTime2.Content = "Monday";
					labelWOSBTime4.Content = "Tuesday";
					labelWOSBTime6.Content = "Wednesday";
					labelWOSBTime8.Content = "Thursday";
					labelWOSBTime10.Content = "Friday";
					labelWOSBTime12.Content = "Saturday";
					labelWOSBTime14.Content = "Sunday";
					labelBugs.Text = "Fixed bugs in this version";
					labelRemotePassword.Content = "Password:";
					buttonPortCheck.Content = "Test port-forwarding";
					buttonFirewallException.Content = "Windows firewall exception";
					buttonDownloadWOSB.Content = "Download WOSB";
					buttonCheckWOSB.Content = "Check again";
					buttonSendMail.Content = "Contact the developer";
					buttonHelp.Content = "Online help";
					buttonDonate.Content = "Donate";

					//TTAsk.Text = "Confirm before shutdown, reboot, log off, lock, standby and hibernate, if no timeout is specified.\nGUI only, not via jumplist or parameters";
					TTForce.Text = "Forces the closure of all open applications.\nUnsaved data may be lost.";
					//TTGlass.Text = "All windows are semi-transparent.\nWindows Vista and Windows 7 only.";
					TTJumplist.Text = "Activates the Jumplist (right click on the taskbar icon).\nAvailable from Windows 7.";
					TTOverlay.Text = "Shows the last countdown numbers in the taskbar icon.\nAvailable from Windows 7.";
					TTPinTaskbar.Text = "Pins Shutdown7 to your taskbar for quick access.\nAvailable from Windows 7.";
					TTRemoteClient.Text = "Control other computers with Shutdown7 via the internet.";
					TTRemoteServer.Text = "Enable control over the internet.\nRequires an exception in your firewall.";
					TTRemotePort.Text = "Shutdown7 will listen on this port.";
					TTRemotePassword.Text = "For your safety a password has to be entered to execute an action via the internet.";
					TTSendFeedback.Text = "The data will be anonymized and serves statistics only.\nIt will contain information like os version, language, usage type.";
					TTStayAfterShutdown.Text = "Shutdown does not close after execution.";
					TTSysicon.Text = "Shows an icon in the systray (on the right corner in the taskbar) .\nAlso the main window will be minimized into the systray.";
					
					Data.L["Next"] = "Next";
					Data.L["Complete"] = "Complete";
					Data.L["Skip"] = "Skip";
					Data.L["Abort"] = "Cancel";
					Data.L["ConfirmAbortWelcomeScreen"] = "Do you really want to cancel this wizard?";

					buttonAbort.Content = Data.L["Abort"];
					buttonNext.Content = Data.L["Next"];
					buttonBack.Content = "Back";
					break;
			}
			#endregion

			#region Changelog
			if (Data.News.Length > 0)
			{
				foreach (string curNews in Data.News)
				{
					TextBlock curTex = new TextBlock();
					curTex.Text = curNews;
					curTex.TextWrapping = TextWrapping.Wrap;
					curTex.Padding = new Thickness(5, 0, 0, 0);

					Label curLab = new Label();
					curLab.Content = curTex;

					Ellipse curEl = new Ellipse();
					curEl.Height = 6; curEl.Width = 6;
					curEl.Margin = new Thickness(4, 0, 0, 0);
					curEl.Fill = Brushes.Black; curEl.Stroke = Brushes.Black;

					BulletDecorator curBullet = new BulletDecorator();
					curBullet.Bullet = curEl;
					curBullet.Child = curLab;

					stackStep1.Children.Add(curBullet);
				}
			}

			if (Data.News.Length > 0 && Data.Bugs.Length > 0)
			{
				labelBugs.FontStyle = new FontStyle();
				labelBugs.FontSize = 14;
				labelBugs.TextDecorations = TextDecorations.Underline;
				stackStep1.Children.Add(labelBugs);
			}
			else if (Data.News.Length == 0 && Data.Bugs.Length > 0)
			{
				Instructions[1] = (string)labelBugs.Text;
			}

			if (Data.Bugs.Length > 0)
			{
				foreach (string curBug in Data.Bugs)
				{
					TextBlock curTex = new TextBlock();
					curTex.Text = curBug;
					curTex.TextWrapping = TextWrapping.Wrap;
					curTex.Padding = new Thickness(5, 0, 0, 0);

					Label curLab = new Label();
					curLab.Content = curTex;

					Ellipse curEl = new Ellipse();
					curEl.Height = 6; curEl.Width = 6;
					curEl.Margin = new Thickness(4, 0, 0, 0);
					curEl.Fill = Brushes.Black; curEl.Stroke = Brushes.Black;

					BulletDecorator curBullet = new BulletDecorator();
					curBullet.Bullet = curEl; curBullet.Child = curLab;

					stackStep1.Children.Add(curBullet);
				}
			}
			#endregion

			if (welcome)
				ChangeScreen(0);
			else if (update)
			{
				if (File.Exists("Shutdown7.zip")) File.Delete("Shutdown7.zip");
				if (File.Exists("UpdateHelper.exe")) File.Delete("UpdateHelper.exe");
				if (File.Exists("Ionic.Zip.dll")) File.Delete("Ionic.Zip.dll");
				ChangeScreen(1);
			}
			else
				Close();
		}

		#region ChangeScreen
		void ChangeScreen(int curscreen)
		{
			//Message.Show(curscreen + "");
			buttonNext.IsEnabled = true;
			buttonBack.IsEnabled = (curscreen != 0);

			labelTitle.Text = Titles[curscreen];
			labelInstruction.Text = Instructions[curscreen];

			if (curscreen == 0)
			{
				buttonAbort.Content = Data.L["Skip"];
				skip = true;
			}
			else
			{
				buttonAbort.Content = Data.L["Abort"];
				skip = false;
			}

			if (curscreen == 1)
			{
				if (Data.News.Length == 0 && Data.Bugs.Length == 0)
				{
					if (update)
						Close();
					else
						curscreen++;
				}
				else
				{
					labelInstruction.TextDecorations = TextDecorations.Underline;
					stackStep1.Visibility = Visibility.Visible;
				}
			}
			else
			{
				labelInstruction.TextDecorations = null;
				stackStep1.Visibility = Visibility.Collapsed;
			}

			if (curscreen == 2)
				stackStep2.Visibility = Visibility.Visible;
			else
				stackStep2.Visibility = Visibility.Collapsed;

			if (curscreen == 3)
				stackStep3.Visibility = Visibility.Visible;
			else
				stackStep3.Visibility = Visibility.Collapsed;

			if (curscreen == 4)
			{
				/*if (Environment.OSVersion.Version.Major < 6)
					checkGlass.IsEnabled = false;*/

				if (!Win7.IsWin7)
				{
					checkJumplist.IsEnabled = false;
					checkOverlay.IsEnabled = false;
					checkPinTaskbar.IsEnabled = false;
				}
				else
				{
					checkJumplist.IsEnabled = true;
					checkJumplist.IsChecked = true;
					checkOverlay.IsEnabled = true;
					checkOverlay.IsChecked = true;
					checkPinTaskbar.IsEnabled = true;

					if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar\Shutdown7.lnk"))
						checkPinTaskbar.IsChecked = true;
					else
					{
						checkPinTaskbar.IsEnabled = false;
						checkPinTaskbar.IsChecked = false;
					}
				}

				
				stackStep4.Visibility = Visibility.Visible;
			}
			else
				stackStep4.Visibility = Visibility.Collapsed;

			if (curscreen == 5)
				stackStep5.Visibility = Visibility.Visible;
			else
				stackStep5.Visibility = Visibility.Collapsed;

			if (curscreen == 6)
			{
				if (textRemotePort.Text.Length == 0 | textRemotePassword.Password.Length == 0)
					buttonNext.IsEnabled = false;
				stackStep6.Visibility = Visibility.Visible;
			}
			else
				stackStep6.Visibility = Visibility.Collapsed;

			if (curscreen == 7)
			{
				if (File.Exists(Data.WOSBPath))
				{
					checkWOSB.IsEnabled = true;
				}
				else
				{
					checkWOSB.IsChecked = false;
					checkWOSB.IsEnabled = false;
				}
				stackStep7.Visibility = Visibility.Visible;

			}
			else
				stackStep7.Visibility = Visibility.Collapsed;

			if (curscreen == 8)
				stackStep8.Visibility = Visibility.Visible;
			else
				stackStep8.Visibility = Visibility.Collapsed;

			if (curscreen == 9)
				stackStep9.Visibility = Visibility.Visible;
			else
				stackStep9.Visibility = Visibility.Collapsed;

			if (curscreen == 10)
				stackStep10.Visibility = Visibility.Visible;
			else
				stackStep10.Visibility = Visibility.Collapsed;

			if (curscreen == Titles.Length - 1)
			{
				buttonNext.Content = Data.L["Complete"];
				buttonAbort.IsEnabled = false;
				done = true;
			}
			else if (update & !welcome)
			{
				buttonNext.Content = Data.L["Complete"];
				buttonBack.IsEnabled = false;
				buttonAbort.IsEnabled = false;
				done = true;
			}
			else
			{
				buttonNext.Content = Data.L["Next"];
				buttonAbort.IsEnabled = true;
				done = false;
			}
		}

		private void buttonBack_Click(object sender, RoutedEventArgs e)
		{
			if (curscreen == 2 && Data.News.Length == 0 && Data.Bugs.Length == 0)
				curscreen--;

			if (curscreen == 7 && !(bool)checkRemoteServer.IsChecked)
				curscreen--;

			if (curscreen == 9 && !(bool)checkWOSB.IsChecked)
				curscreen--;

			curscreen--;
			ChangeScreen(curscreen);
		}

		private void buttonNext_Click(object sender, RoutedEventArgs e)
		{
			if (update)
				Close();
			
			switch (curscreen)
			{
				case 2:
					Data.S["Autostart"] = (bool)checkAutostart.IsChecked;
					break;
				case 3:
					//Data.S["Ask"] = (bool)checkAsk.IsChecked;
					Data.S["Force"] = (bool)checkForce.IsChecked;
					Data.S["StayAfterShutdown"] = (bool)checkStayAfterShutdown.IsChecked;
					Data.S["SysIcon"] = (bool)checkSysicon.IsChecked;
					break;
				case 4:
					Data.S["Jumplist"] = (bool)checkJumplist.IsChecked;
					Data.S["Overlay"] = (bool)checkOverlay.IsChecked;
					//Data.S["Glass"] = (bool)checkGlass.IsChecked;

					if ((bool)checkPinTaskbar.IsChecked)
					{
						new Thread(Win7.PinToTaskbar).Start();
					}
					break;
				case 5:
					Data.S["RemoteClient"] = (bool)checkRemoteClient.IsChecked;
					Data.S["RemoteServer"] = (bool)checkRemoteServer.IsChecked;
					if (!(bool)checkRemoteServer.IsChecked)
						curscreen++;
					break;
				case 6:
					if (!Int32.TryParse(textRemotePort.Text, out Data.RemotePort) | (Data.RemotePort < 1024 | Data.RemotePort > 65535))
					{
						MessageBox.Show(Data.L["RemotePortMissing"], "Shutdown7", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						return;
					}

					if (textRemotePassword.Password.Length == 0)
					{
						MessageBox.Show(Data.L["RemotePasswordMissing"], "Shutdown7", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						return;
					}

					Data.RemotePassword = Remote.md5(textRemotePassword.Password);
					break;
				case 7:
					Data.S["WOSB"] = (bool)checkWOSB.IsChecked;
					if (!(bool)checkWOSB.IsChecked)
						curscreen++;
					else
					{
						Data.W.Add("Default", new Dictionary<string, string>());
						Data.curProfile = "Default";
					}
					break;
				case 8:
					Data.W["Default"]["Mo1"] = textWOSBTime1.Text;
					Data.W["Default"]["Mo2"] = textWOSBTime2.Text;
					Data.W["Default"]["Th1"] = textWOSBTime3.Text;
					Data.W["Default"]["Th2"] = textWOSBTime4.Text;
					Data.W["Default"]["We1"] = textWOSBTime5.Text;
					Data.W["Default"]["We2"] = textWOSBTime6.Text;
					Data.W["Default"]["Th1"] = textWOSBTime7.Text;
					Data.W["Default"]["Th2"] = textWOSBTime8.Text;
					Data.W["Default"]["Fr1"] = textWOSBTime9.Text;
					Data.W["Default"]["Fr2"] = textWOSBTime10.Text;
					Data.W["Default"]["Sa1"] = textWOSBTime11.Text;
					Data.W["Default"]["Sa2"] = textWOSBTime12.Text;
					Data.W["Default"]["Su1"] = textWOSBTime13.Text;
					Data.W["Default"]["Su2"] = textWOSBTime14.Text;
					Data.W["Default"]["File"] = "";
					Data.W["Default"]["Params"] = "";
					Data.W["Default"]["AwFile"] = "";
					Data.W["Default"]["AwParams"] = "";
					Data.W["Default"]["Extra"] = "";
					break;
				case 9:
					Data.S["SendFeedback"] = (bool)checkSendFeedback.IsChecked;
					break;
				default:
					break;
			}

			if (curscreen == Titles.Length - 1)
			{
				Data.RemoteServers = new string[] { "127.0.0.1" };

				Data.RemoteMacs = new string[] { };
				foreach (NetworkInterface CurMac in NetworkInterface.GetAllNetworkInterfaces())
					if (CurMac.OperationalStatus == OperationalStatus.Up)
						if (CurMac.GetPhysicalAddress().ToString().Length > 0)
						{
							Array.Resize(ref Data.RemoteMacs, Data.RemoteMacs.Length + 1);
							Data.RemoteMacs[Data.RemoteMacs.Length - 1] = CurMac.GetPhysicalAddress().ToString();
						}

				Xml.Write();
				if ((bool)checkWOSB.IsChecked)
					Xml.WriteWOSB();

				Close();
			}
			else
			{
				curscreen++;
				ChangeScreen(curscreen);
			}
		}
		#endregion

		#region Close
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!done & !skip)
			{
				try
				{
					TaskDialog td = new TaskDialog();
					td.Caption = "Shutdown7";
					td.InstructionText = Data.L["Cancel"];
					td.Text = Data.L["ConfirmAbortWelcomeScreen"];
					td.Icon = TaskDialogStandardIcon.Warning;
					td.StandardButtons = TaskDialogStandardButtons.No | TaskDialogStandardButtons.Cancel;
					td.Cancelable = true;
					if (td.Show() == TaskDialogResult.No)
					{
						e.Cancel = true;
						return;
					}
				}
				catch
				{
					if (MessageBox.Show(Data.L["ConfirmAbortWelcomeScreen"], "Shutdown7", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
					{
						e.Cancel = true;
						return;
					}
				}
			}

			Xml.Write();

			ProcessStartInfo p = new ProcessStartInfo();
			p.FileName = Data.EXE;
			p.WorkingDirectory = Directory.GetCurrentDirectory();
			p.Verb = "runas";
			Process.Start(p);
			//Message.Show(L["RequireAdmin"], "Error");
			Environment.Exit(0);
		}
		#endregion

		#region Events
		private void buttonAbort_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		#region Remote
		private void textRemotePort_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			e.Handled = !Char.IsDigit(Convert.ToChar(e.Text));
		}

		private void textRemotePort_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (stackStep6.Visibility != Visibility.Visible) return;
			int y; if (!Int32.TryParse(textRemotePort.Text, out y) | (y < 1024 | y > 65535) | textRemotePassword.Password.Length == 0)
				buttonNext.IsEnabled = false;
			else
				buttonNext.IsEnabled = true;
		}

		private void textRemotePassword_PasswordChanged(object sender, RoutedEventArgs e)
		{
			int y; if (!Int32.TryParse(textRemotePort.Text, out y) | (y < 1024 | y > 65535) | textRemotePassword.Password.Length == 0)
				buttonNext.IsEnabled = false;
			else
				buttonNext.IsEnabled = true;
		}

		private void textRemotePassword_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
		{
			textRemotePassword.Password = "";
		}

		private void buttonPortCheck_Click(object sender, RoutedEventArgs e)
		{
			buttonPortCheck.IsEnabled = false;
			Thread t = new Thread(new ParameterizedThreadStart(new Settings().Portcheck));
			t.Start(textRemotePort.Text);
			new Thread(new ParameterizedThreadStart(WaiterHelper)).Start(t);
		}

		void WaiterHelper(object _t)
		{
			Thread t = (Thread)_t;

			while (t.IsAlive)
			{
				Thread.Sleep(100);
			}

			Dispatcher.Invoke((Action)delegate { buttonPortCheck.IsEnabled = true; });
		}

		private void buttonFirewallException_Click(object sender, RoutedEventArgs e)
		{
			buttonFirewallException.IsEnabled = false;
			new Settings().FirewallException();
			buttonFirewallException.IsEnabled = true;
		}
		#endregion

		#region WOSB
		private void buttonDownloadWOSB_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("http://www.dennisbabkin.com/php/download.php?what=WOSB");
		}

		private void buttonCheckWOSB_Click(object sender, RoutedEventArgs e)
		{
			if (File.Exists(Data.WOSBPath))
			{
				checkWOSB.IsEnabled = true;
			}
			else
			{
				checkWOSB.IsChecked = false;
				checkWOSB.IsEnabled = false;
			}
		}
		#endregion

		#region Websites
		private void buttonSendMail_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("http://www.shutdown7.com/kontakt.php?lang=" + Data.Lang);
		}

		private void buttonHelp_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("http://www.shutdown7.com/help.php?lang=" + Data.Lang);
		}

		private void buttonDonate_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=10883669");
		}
		#endregion
		#endregion

		#region Aero
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			Win7.Glass(this);
		}
		#endregion

	}
}
