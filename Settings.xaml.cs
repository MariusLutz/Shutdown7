using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using System.Collections.Generic;
using Microsoft.Win32.TaskScheduler;

namespace Shutdown7
{
	public partial class Settings : Window
	{
		public Settings()
		{
			InitializeComponent();

			#region Settings
			checkAllProcesses.IsChecked = Data.S["AllProcesses"];
			checkAsk.IsChecked = Data.S["Ask"];
			checkAutostart.IsChecked = Autostart.IsAutoStartEnabled("Shutdown", Assembly.GetExecutingAssembly().Location + " /Run");
			checkForce.IsChecked = Data.S["Force"];
			checkGlass.IsChecked = Data.S["Glass"];
			checkHybrid.IsChecked = Data.S["Win8Hybrid"];
			checkIPv4.IsChecked = Data.S["IPv4"];
			checkJumplist.IsChecked = Data.S["Jumplist"];
			checkOverlay.IsChecked = Data.S["Overlay"];
			checkModusIcons.IsChecked = Data.S["ModusIcons"];
			checkRemoteClient.IsChecked = Data.S["RemoteClient"];
			checkRemoteServer.IsChecked = Data.S["RemoteServer"];
            checkSaveWindowState.IsChecked = Data.S["SaveWindowState"];
            checkSendFeedback.IsChecked = Data.S["SendFeedback"];
			checkStay.IsChecked = Data.S["StayAfterShutdown"];
			checkSystray.IsChecked = Data.S["SysIcon"];
			checkThumbnailToolbar.IsChecked = Data.S["ThumbnailToolbar"];
			checkWakeOnLan.IsChecked = Data.S["WakeOnLan"];
			checkWOSB.IsChecked = Data.S["WOSB"];

			if (Data.RemotePort > 1024 && Data.RemotePort < 65535)
				textRemotePort.Items.Add(Data.RemotePort.ToString());
			if (Data.RemotePort != 5556) textRemotePort.Items.Add("5556");
			if (Data.RemotePort !=   80) textRemotePort.Items.Add("80");
			if (Data.RemotePort != 8080) textRemotePort.Items.Add("8080");
			textRemotePort.SelectedItem = textRemotePort.Items[0];

			textRemotePassword.Password = Data.RemotePassword;

			#endregion
			
			#region Checkboxen
			if (Environment.OSVersion.Version.Major >= 6 & Environment.OSVersion.Version.Minor >= 2)
				checkHybrid.IsEnabled = true;
			else
			{
				checkHybrid.IsEnabled = false;
				checkHybrid.IsChecked = false;
			}

			if (File.Exists(Data.WOSBPath))
			{
				checkWOSB.IsEnabled = true;
			}
			else
			{
				checkWOSB.IsEnabled = false;
				checkWOSB.IsChecked = false;
			}

			if ((bool)checkWOSB.IsChecked)
				ActivateWOSBUI();
			else
				DeactivateWOSBUI();

			if (Win7.IsWin7)
			{
				checkJumplist.IsEnabled = true;
				checkOverlay.IsEnabled = true;
				checkThumbnailToolbar.IsEnabled = true;
			}
			else
			{
				checkJumplist.IsChecked = false;
				checkJumplist.IsEnabled = false;
				checkOverlay.IsChecked = false;
				checkOverlay.IsEnabled = false;
				checkThumbnailToolbar.IsChecked = false;
				checkThumbnailToolbar.IsEnabled = false;
			}

			if (Environment.OSVersion.Version.Major >= 6)
				checkGlass.IsEnabled = true;
			else
				checkGlass.IsEnabled = false;

			if (Data.S["RemoteClient"])
			{
				checkWakeOnLan.IsEnabled = true;
				checkIPv4.IsEnabled = true;
			}
			else
			{
				checkWakeOnLan.IsChecked = false;
				checkWakeOnLan.IsEnabled = false;
				checkIPv4.IsChecked = false;
				checkIPv4.IsEnabled = false;
			}

			if (Data.S["RemoteServer"])
			{
				textRemotePort.IsEnabled = true;
				textRemotePassword.IsEnabled = true;
				buttonPortCheck.IsEnabled = true;
				buttonFirewallException.IsEnabled = true;
			}
			else
			{
				textRemotePort.IsEnabled = false;
				textRemotePassword.IsEnabled = false;
				buttonPortCheck.IsEnabled = false;
				buttonFirewallException.IsEnabled = false;
			}
			#endregion

			#region WOSB
			if (File.Exists(Xml.WOSBPath))
			{
				Xml.ReadWOSB();

				foreach (KeyValuePair<string, Dictionary<string, string>> kvp in Data.W)
				{
					comboProfiles.Items.Add(kvp.Key);
				}

				comboProfiles.SelectedItem = Data.curProfile;
			}
			#endregion

			#region Lang
			this.Title = Data.L["Settings"];

			switch (Data.Lang)
			{
				case "de":
					TabAppearance.Header = "Aussehen";
					TabWOSB.Header = "WOSB";
					TabMiscellaneous.Header = "Sonstiges";

					checkAllProcesses.Content = "Alle Prozesse anzeigen";
					checkAsk.Content = "Abfrage vor Shutdown";
					checkForce.Content = "Shutdown erzwingen";
					checkHybrid.Content = "Hybrid-Shutdown";
					checkStay.Content = "Bleibe nach Ausführen aktiv";
					checkIPv4.Content = "IPv4 Kompatibilität";
					checkModusIcons.Content = "Icons in Modus-Auswahl";
					checkSendFeedback.Content = "Sende Nutzerstatistiken";
                    checkSaveWindowState.Content = "Speichere Fensterposition";
					checkWOSB.Content = "Benutze WOSB";
					labelRestart.Content = "Ein Neustart ist erforderlich, damit alle Einstellungen wirksam werden.";
					labelProfiles.Content = "Profile";
					labelWOSBTimes.Header = "Weckzeiten";
					labelWOSBDay1.Content = "Montag";
					labelWOSBDay2.Content = "Dienstag";
					labelWOSBDay3.Content = "Mittwoch";
					labelWOSBDay4.Content = "Donnerstag";
					labelWOSBDay5.Content = "Freitag";
					labelWOSBDay6.Content = "Samstag";
					labelWOSBDay7.Content = "Sonntag";
					labelWOSBProg1.Content = "Programm1";
					labelWOSBProg2.Content = "Programm2";
					labelWOSBArgs1.Content = "Argumente";
					labelWOSBArgs2.Content = "Argumente";
					labelWOSBExtra.Content = "Extra-Args";
					labelRemotePassword.Content = "Passwort:";
					buttonPortCheck.Content = "Portweiterleitung testen";
					buttonFirewallException.Content = "Windows Firewallausnahme";
					buttonAddProfile.Content = "Hinzufügen";
					buttonDeleteProfile.Content = "Löschen";
					buttonWOSBProg1.Content = Data.L["Browse"];
					buttonWOSBProg2.Content = Data.L["Browse"];

					TTHForce.Text = (string)checkForce.Content;
					TTForce.Text = "Erzwingt das Schließen aller offenen Programme. Ungespeicherte Daten können verloren gehen.";
					TTHAsk.Text = "Abfrage vor Shutdown";
					TTAsk.Text = "Abfrage vor Herunterfahren, Neustart, Logoff, Sperren, Standby und Hibernate, wenn kein Timeout angegeben wurde.\nNur über GUI, nicht über Jumplist oder Parametern";
					TTHHybrid.Text = (string)checkHybrid.Content;
					TTHybrid.Text = "Benutze Hybridshutdown. Erst ab Windows 8.";
					TTHStay.Text = (string)checkStay.Content;
					TTStay.Text = "Shutdown7 beendet sich nicht nach Ausführen einer Aktion";
					TTHAllProcesses.Text = (string)checkAllProcesses.Content;
					TTAllProcesses.Text = "Es werden alle Prozesse aufgelistet, auch die nicht sichtbaren.";
					TTHWOSB.Text = "Benutze WOSB";
					TTWOSB.Text = "Schaltet Integrationsfeatures mit WOSB (Wakeup from Standby and Hibernate) frei.\nwosb.exe muss im gleichen Verzeichnis liegen.";
					TTHSystray.Text = "Systrayicon";
					TTSystray.Text = "Zeigt ein Icon im Systray (unten rechts in der Taskleiste) an.";
                    TTHSaveWindowState.Text = (string)checkSaveWindowState.Content;
                    TTSaveWindowState.Text = "Speichert die Fensterposition zwischen Sessionen.";
                    TTHModusIcons.Text = "Modus Icons";
					TTModusIcons.Text = "Zeigt Icons in Modus-Auswahlbox an.";
					TTHGlass.Text = "Aero Glass";
					TTGlass.Text = "Alle Fenster sind transparent.\nVerfügbar ab Windows Vista.";
					TTHOverlay.Text = "Overlayicon";
					TTOverlay.Text = "Zeigt beim Countdown die letzten Zahlen im Taskbaricon an.\nVerfügbar ab Windows 7.";
					TTHJumplist.Text = "Jumplist";
					TTJumplist.Text = "Aktiviert die Jumplist (Rechtsklick auf Taskbar-Icon).\nVerfügbar ab Windows 7.";
					TTHThumbnailToolbar.Text = "Thumbnail Toolbar";
					TTThumbnailToolbar.Text = "Zeigt in der Fenstervorschau in der Taskleiste Icons zum schnellen Ausführen der Aktionen.\nVerfügbar ab Windows 7.";
					TTHRemoteClient.Text = "Client";
					TTRemoteClient.Text = "Client-Funktionen";
					TTHRemoteServer.Text = "Server";
					TTRemoteServer.Text = "Server-Funktionen";
					TTHWakeOnLan.Text = (string)checkWakeOnLan.Content;
					TTWakeOnLan.Text = "Aktiviert die Wake on Lan-Funktion.";
					TTHIPv4.Text = (string)checkIPv4.Content;
					TTIPv4.Text = "Aktivieren Sie diese Option, wenn Remoteshutdown nicht funktioniert oder Ihr PC IPv6 nicht unterstützt.";
					TTHAutostart.Text = (string)checkAutostart.Content;
					TTAutostart.Text = "Lädt dieses Programm bei jedem Start von Windows.";
					TTHSendFeedback.Text = "Sende Nutzungsstatistiken";
					TTSendFeedback.Text = "Sende anonyme Nutzungsstatistiken an den Entwickler.\nDie Daten sind anonymisiert und dienen nur Statistik-Zwecken.\nEs handelt sich hierbei nur um Informationen wie Betriebssystem, Sprache und Nutzungsdauer und -art.";
					
					AbortButton.Content = "Abbrechen";
					ApplyButton.Content = "Übernehmen";
					break;
				default:
					TabAppearance.Header = "Appearance";
					TabWOSB.Header = "WOSB";
					TabMiscellaneous.Header = "Miscellaneous";

					checkAllProcesses.Content = "Show all processes";
					checkAsk.Content = "Ask before shutdown";
					checkForce.Content = "Force shutdown";
					checkHybrid.Content = "Use hybrid shutdown. Only for Windows 8.";
					checkStay.Content = "Stay active after execution";
					checkIPv4.Content = "IPv4 Compatibility";
					checkModusIcons.Content = "Icons in modus selector";
					checkSendFeedback.Content = "Send usage statistics";
                    checkSaveWindowState.Content = "Save window position";
                    checkWOSB.Content = "Use WOSB";
					labelRestart.Content = "A restart is required to apply all settings.";
					labelProfiles.Content = "Profiles";
					labelWOSBTimes.Header = "Wakeup Times";
					labelWOSBDay1.Content = "Monday";
					labelWOSBDay2.Content = "Tuesday";
					labelWOSBDay3.Content = "Wednesday";
					labelWOSBDay4.Content = "Thursday";
					labelWOSBDay5.Content = "Friday";
					labelWOSBDay6.Content = "Saturday";
					labelWOSBDay7.Content = "Sunday";
					labelWOSBProg1.Content = "Application1";
					labelWOSBProg2.Content = "Application2";
					labelWOSBArgs1.Content = "Arguments";
					labelWOSBArgs2.Content = "Arguments";
					labelWOSBExtra.Content = "Additional Arguments";
					labelRemotePassword.Content = "Password:";
					buttonPortCheck.Content = "Test port-forwarding";
					buttonFirewallException.Content = "Windows firewall exception";
					buttonAddProfile.Content = "Add";
					buttonDeleteProfile.Content = "Delete";
					buttonWOSBProg1.Content = Data.L["Browse"];
					buttonWOSBProg2.Content = Data.L["Browse"];

					TTHForce.Text = (string)checkForce.Content;
					TTForce.Text = "Forces closure of all open programs. Unsaved data may be lost.";
					TTHAsk.Text = "Ask before shutdown";
					TTAsk.Text = "Confirm before shutdown, reboot, log off, lock, standby and hibernate, if no timeout is specified.\nGUI only, not via jumplist or parameters.";
					TTHHybrid.Text = (string)checkHybrid.Content;
					TTHybrid.Text = "Hybrid shutdown";
					TTHStay.Text = (string)checkStay.Content;
					TTStay.Text = "Shutdown does not close after execution.";
					TTHAllProcesses.Text = (string)checkAllProcesses.Content;
					TTAllProcesses.Text = "List all processes, invisible too.";
					TTHWOSB.Text = "Use WOSB";
					TTWOSB.Text = "Activate intigration with WOSB (Wakeup from Standby and Hibernate).\nwosb.exe has to be in same directory.";
					TTHSystray.Text = "Systrayicon";
					TTSystray.Text = "Shows a systray icon (bottom right in the taskbar).";
                    TTHSaveWindowState.Text = (string) checkSaveWindowState.Content;
                    TTSaveWindowState.Text = "Save window position between sessions.";
					TTHGlass.Text = "Aero Glass";
					TTGlass.Text = "All windows are semi-transparent.\nAvailbale from Windows Vistay.";
					TTHModusIcons.Text = "Mode icons";
					TTModusIcons.Text = "Display icons in the modus selector.";
					TTHOverlay.Text = "Overlayicon";
					TTOverlay.Text = "Shows the last 10 seconds the countdown in the taskbar while counting down.\nAvailbale from Windows 7.";
					TTHJumplist.Text = "Jumplist";
					TTJumplist.Text = "Activates jumplist (Right click on taskbar icon).\nAvailbale from Windows 7.";
					TTHThumbnailToolbar.Text = "Thumbnail Toolbar";
					TTThumbnailToolbar.Text = "Shows shutdown icons in window preview to access functions faster.\nAvailbale from Windows 7.";
					TTHRemoteClient.Text = "Client";
					TTRemoteClient.Text = "Client-functions";
					TTHRemoteServer.Text = "Server";
					TTRemoteServer.Text = "Server-functions";
					TTHWakeOnLan.Text = (string)checkWakeOnLan.Content;
					TTWakeOnLan.Text = "Activates Wake on Lan function.";
					TTHIPv4.Text = (string)checkIPv4.Content;
					TTIPv4.Text = "Activate if Remote Shutdown doesn't work or your PC doesn't support IPv6.";
					TTHAutostart.Text = (string)checkAutostart.Content;
					TTAutostart.Text = "Loads this program every time Windows boots.";
					TTHSendFeedback.Text = "Send usage statistics";
					TTSendFeedback.Text = "Allow Shutdown7 to send anonymous user statistics to the developer.\nThe data will be anonymized and serves statistics only.\nIt will contain information like os version, language, usage time and -type.";
					
					AbortButton.Content = "Abort";
					ApplyButton.Content = "Apply";
					break;
			}
			#endregion
		}

		void LoadWakeupTimes(string curProfile)
		{
			textWOSBTimeMo1.Value = null;
			textWOSBTimeMo2.Value = null;
			textWOSBTimeMo3.Value = null;
			textWOSBTimeMo4.Value = null;
			textWOSBTimeTu1.Value = null;
			textWOSBTimeTu2.Value = null;
			textWOSBTimeTu3.Value = null;
			textWOSBTimeTu4.Value = null;
			textWOSBTimeWe1.Value = null;
			textWOSBTimeWe2.Value = null;
			textWOSBTimeWe3.Value = null;
			textWOSBTimeWe4.Value = null;
			textWOSBTimeTh1.Value = null;
			textWOSBTimeTh2.Value = null;
			textWOSBTimeTh3.Value = null;
			textWOSBTimeTh4.Value = null;
			textWOSBTimeFr1.Value = null;
			textWOSBTimeFr2.Value = null;
			textWOSBTimeFr3.Value = null;
			textWOSBTimeFr4.Value = null;
			textWOSBTimeSa1.Value = null;
			textWOSBTimeSa2.Value = null;
			textWOSBTimeSa3.Value = null;
			textWOSBTimeSa4.Value = null;
			textWOSBTimeSu1.Value = null;
			textWOSBTimeSu2.Value = null;
			textWOSBTimeSu3.Value = null;
			textWOSBTimeSu4.Value = null;
			textWOSBProg1.Text = "";
			textWOSBArgs1.Text = "";
			textWOSBProg2.Text = "";
			textWOSBArgs2.Text = "";
			textWOSBExtra.Text = "";

			if (Data.W[curProfile].ContainsKey("Mo1")) textWOSBTimeMo1.Value = DateTime.Parse(Data.W[curProfile]["Mo1"]);
			if (Data.W[curProfile].ContainsKey("Mo2")) textWOSBTimeMo2.Value = DateTime.Parse(Data.W[curProfile]["Mo2"]);
			if (Data.W[curProfile].ContainsKey("Mo3")) textWOSBTimeMo3.Value = DateTime.Parse(Data.W[curProfile]["Mo3"]);
			if (Data.W[curProfile].ContainsKey("Mo4")) textWOSBTimeMo4.Value = DateTime.Parse(Data.W[curProfile]["Mo4"]);
			if (Data.W[curProfile].ContainsKey("Tu1")) textWOSBTimeTu1.Value = DateTime.Parse(Data.W[curProfile]["Tu1"]);
			if (Data.W[curProfile].ContainsKey("Tu2")) textWOSBTimeTu2.Value = DateTime.Parse(Data.W[curProfile]["Tu2"]);
			if (Data.W[curProfile].ContainsKey("Tu3")) textWOSBTimeTu3.Value = DateTime.Parse(Data.W[curProfile]["Tu3"]);
			if (Data.W[curProfile].ContainsKey("Tu4")) textWOSBTimeTu4.Value = DateTime.Parse(Data.W[curProfile]["Tu4"]);
			if (Data.W[curProfile].ContainsKey("We1")) textWOSBTimeWe1.Value = DateTime.Parse(Data.W[curProfile]["We1"]);
			if (Data.W[curProfile].ContainsKey("We2")) textWOSBTimeWe2.Value = DateTime.Parse(Data.W[curProfile]["We2"]);
			if (Data.W[curProfile].ContainsKey("We3")) textWOSBTimeWe3.Value = DateTime.Parse(Data.W[curProfile]["We3"]);
			if (Data.W[curProfile].ContainsKey("We4")) textWOSBTimeWe4.Value = DateTime.Parse(Data.W[curProfile]["We4"]);
			if (Data.W[curProfile].ContainsKey("Th1")) textWOSBTimeTh1.Value = DateTime.Parse(Data.W[curProfile]["Th1"]);
			if (Data.W[curProfile].ContainsKey("Th2")) textWOSBTimeTh2.Value = DateTime.Parse(Data.W[curProfile]["Th2"]);
			if (Data.W[curProfile].ContainsKey("Th3")) textWOSBTimeTh3.Value = DateTime.Parse(Data.W[curProfile]["Th3"]);
			if (Data.W[curProfile].ContainsKey("Th4")) textWOSBTimeTh4.Value = DateTime.Parse(Data.W[curProfile]["Th4"]);
			if (Data.W[curProfile].ContainsKey("Fr1")) textWOSBTimeFr1.Value = DateTime.Parse(Data.W[curProfile]["Fr1"]);
			if (Data.W[curProfile].ContainsKey("Fr2")) textWOSBTimeFr2.Value = DateTime.Parse(Data.W[curProfile]["Fr2"]);
			if (Data.W[curProfile].ContainsKey("Fr3")) textWOSBTimeFr3.Value = DateTime.Parse(Data.W[curProfile]["Fr3"]);
			if (Data.W[curProfile].ContainsKey("Fr4")) textWOSBTimeFr4.Value = DateTime.Parse(Data.W[curProfile]["Fr4"]);
			if (Data.W[curProfile].ContainsKey("Sa1")) textWOSBTimeSa1.Value = DateTime.Parse(Data.W[curProfile]["Sa1"]);
			if (Data.W[curProfile].ContainsKey("Sa2")) textWOSBTimeSa2.Value = DateTime.Parse(Data.W[curProfile]["Sa2"]);
			if (Data.W[curProfile].ContainsKey("Sa3")) textWOSBTimeSa3.Value = DateTime.Parse(Data.W[curProfile]["Sa3"]);
			if (Data.W[curProfile].ContainsKey("Sa4")) textWOSBTimeSu4.Value = DateTime.Parse(Data.W[curProfile]["Su4"]);
			if (Data.W[curProfile].ContainsKey("Su1")) textWOSBTimeSu1.Value = DateTime.Parse(Data.W[curProfile]["Su1"]);
			if (Data.W[curProfile].ContainsKey("Su2")) textWOSBTimeSu2.Value = DateTime.Parse(Data.W[curProfile]["Su2"]);
			if (Data.W[curProfile].ContainsKey("Su3")) textWOSBTimeSu3.Value = DateTime.Parse(Data.W[curProfile]["Su3"]);
			if (Data.W[curProfile].ContainsKey("Su4")) textWOSBTimeSu4.Value = DateTime.Parse(Data.W[curProfile]["Su4"]);

			if (Data.W[curProfile].ContainsKey("File")) textWOSBProg1.Text = Data.W[curProfile]["File"];
			if (Data.W[curProfile].ContainsKey("Params")) textWOSBArgs1.Text = Data.W[curProfile]["Params"];
			if (Data.W[curProfile].ContainsKey("AwFile")) textWOSBProg2.Text = Data.W[curProfile]["AwFile"];
			if (Data.W[curProfile].ContainsKey("AwParams")) textWOSBArgs2.Text = Data.W[curProfile]["AwParams"];
			if (Data.W[curProfile].ContainsKey("Extra")) textWOSBExtra.Text = Data.W[curProfile]["Extra"];
		}

		#region Apply
		void Apply()
		{
			Data.S["AllProcesses"] = (bool)checkAllProcesses.IsChecked;
			Data.S["Ask"] = (bool)checkAsk.IsChecked;
			Data.S["Autostart"] = (bool)checkAutostart.IsChecked && checkAutostart.IsEnabled;
			Data.S["Force"] = (bool)checkForce.IsChecked;
			Data.S["Glass"] = (bool)checkGlass.IsChecked && checkGlass.IsEnabled;
			Data.S["Win8Hybrid"] = (bool)checkHybrid.IsChecked && checkHybrid.IsEnabled;
			Data.S["IPv4"] = (bool)checkIPv4.IsChecked;
			Data.S["Jumplist"] = (bool)checkJumplist.IsChecked && checkJumplist.IsEnabled;
			Data.S["ModusIcons"] = (bool)checkModusIcons.IsChecked;
			Data.S["Overlay"] = (bool)checkOverlay.IsChecked && checkOverlay.IsEnabled;
			Data.S["RemoteClient"] = (bool)checkRemoteClient.IsChecked && checkRemoteClient.IsEnabled;
			Data.S["RemoteServer"] = (bool)checkRemoteServer.IsChecked && checkRemoteServer.IsEnabled;
            Data.S["SaveWindowState"] = (bool)checkSaveWindowState.IsChecked;
            Data.S["SendFeedback"] = (bool)checkSendFeedback.IsChecked;
			Data.S["StayAfterShutdown"] = (bool)checkStay.IsChecked;
			Data.S["SysIcon"] = (bool)checkSystray.IsChecked;
			Data.S["ThumbnailToolbar"] = (bool)checkThumbnailToolbar.IsChecked && checkThumbnailToolbar.IsEnabled;
			Data.S["WakeOnLan"] = (bool)checkWakeOnLan.IsChecked && checkWakeOnLan.IsEnabled;
			Data.S["WOSB"] = (bool)checkWOSB.IsChecked && checkWOSB.IsEnabled;
	
			int y; if (Int32.TryParse(textRemotePort.Text, out y) & (y > 1024 & y < 65535))
				Data.RemotePort = Int32.Parse(textRemotePort.Text);

			if (textRemotePassword.Password != "")
				Data.RemotePassword = textRemotePassword.Password;

			Xml.Write();

			if (Data.S["WOSB"])
			{
				string curProfile = (string)comboProfiles.SelectedItem;

				if (textWOSBTimeMo1.Value != null) Data.W[curProfile]["Mo1"] = textWOSBTimeMo1.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Mo1"] = "";
				if (textWOSBTimeMo2.Value != null) Data.W[curProfile]["Mo2"] = textWOSBTimeMo2.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Mo2"] = "";
				if (textWOSBTimeMo3.Value != null) Data.W[curProfile]["Mo3"] = textWOSBTimeMo3.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Mo3"] = "";
				if (textWOSBTimeMo4.Value != null) Data.W[curProfile]["Mo4"] = textWOSBTimeMo4.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Mo4"] = "";
				if (textWOSBTimeTu1.Value != null) Data.W[curProfile]["Tu1"] = textWOSBTimeTu1.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Tu1"] = "";
				if (textWOSBTimeTu2.Value != null) Data.W[curProfile]["Tu2"] = textWOSBTimeTu2.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Tu2"] = "";
				if (textWOSBTimeTu3.Value != null) Data.W[curProfile]["Tu3"] = textWOSBTimeTu3.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Tu3"] = "";
				if (textWOSBTimeTu4.Value != null) Data.W[curProfile]["Tu4"] = textWOSBTimeTu4.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Tu4"] = "";
				if (textWOSBTimeWe1.Value != null) Data.W[curProfile]["We1"] = textWOSBTimeWe1.Value.Value.ToLongTimeString(); else Data.W[curProfile]["We1"] = "";
				if (textWOSBTimeWe2.Value != null) Data.W[curProfile]["We2"] = textWOSBTimeWe2.Value.Value.ToLongTimeString(); else Data.W[curProfile]["We2"] = "";
				if (textWOSBTimeWe3.Value != null) Data.W[curProfile]["We3"] = textWOSBTimeWe3.Value.Value.ToLongTimeString(); else Data.W[curProfile]["We3"] = "";
				if (textWOSBTimeWe4.Value != null) Data.W[curProfile]["We4"] = textWOSBTimeWe4.Value.Value.ToLongTimeString(); else Data.W[curProfile]["We4"] = "";
				if (textWOSBTimeTh1.Value != null) Data.W[curProfile]["Th1"] = textWOSBTimeTh1.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Th1"] = "";
				if (textWOSBTimeTh2.Value != null) Data.W[curProfile]["Th2"] = textWOSBTimeTh2.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Th2"] = "";
				if (textWOSBTimeTh3.Value != null) Data.W[curProfile]["Th3"] = textWOSBTimeTh3.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Th3"] = "";
				if (textWOSBTimeTh4.Value != null) Data.W[curProfile]["Th4"] = textWOSBTimeTh4.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Th4"] = "";
				if (textWOSBTimeFr1.Value != null) Data.W[curProfile]["Fr1"] = textWOSBTimeFr1.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Fr1"] = "";
				if (textWOSBTimeFr2.Value != null) Data.W[curProfile]["Fr2"] = textWOSBTimeFr2.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Fr2"] = "";
				if (textWOSBTimeFr3.Value != null) Data.W[curProfile]["Fr3"] = textWOSBTimeFr3.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Fr3"] = "";
				if (textWOSBTimeFr4.Value != null) Data.W[curProfile]["Fr4"] = textWOSBTimeFr4.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Fr4"] = "";
				if (textWOSBTimeSa1.Value != null) Data.W[curProfile]["Sa1"] = textWOSBTimeSa1.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Sa1"] = "";
				if (textWOSBTimeSa2.Value != null) Data.W[curProfile]["Sa2"] = textWOSBTimeSa2.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Sa2"] = "";
				if (textWOSBTimeSa3.Value != null) Data.W[curProfile]["Sa3"] = textWOSBTimeSa3.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Sa3"] = "";
				if (textWOSBTimeSa4.Value != null) Data.W[curProfile]["Sa4"] = textWOSBTimeSa4.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Sa4"] = "";
				if (textWOSBTimeSu1.Value != null) Data.W[curProfile]["Su1"] = textWOSBTimeSu1.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Su1"] = "";
				if (textWOSBTimeSu2.Value != null) Data.W[curProfile]["Su2"] = textWOSBTimeSu2.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Su2"] = "";
				if (textWOSBTimeSu3.Value != null) Data.W[curProfile]["Su3"] = textWOSBTimeSu3.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Su3"] = "";
				if (textWOSBTimeSu4.Value != null) Data.W[curProfile]["Su4"] = textWOSBTimeSu4.Value.Value.ToLongTimeString(); else Data.W[curProfile]["Su4"] = "";
				Data.W[curProfile]["File"] = textWOSBProg1.Text;
				Data.W[curProfile]["Params"] = textWOSBArgs1.Text;
				Data.W[curProfile]["AwFile"] = textWOSBProg2.Text;
				Data.W[curProfile]["AwParams"] = textWOSBArgs2.Text;
				Data.W[curProfile]["Extra"] = textWOSBExtra.Text;

				Xml.WriteWOSB();
			}

            if (Data.S["Autostart"]) {
				if (!Autostart.IsAutoStartEnabled("Shutdown", Assembly.GetExecutingAssembly().Location + " /Run"))
					Autostart.SetAutoStart("Shutdown", Assembly.GetExecutingAssembly().Location + " /Run");
			}
			else
			{
			if (Autostart.IsAutoStartEnabled("Shutdown", Assembly.GetExecutingAssembly().Location + " /Run"))
				Autostart.UnSetAutoStart("Shutdown");
			}
            
			if (Data.S["Jumplist"])
				Win7.Jumplist();
			else
				Win7.ClearJumplist();

			//MainWindow mainwindow = new MainWindow();

			if (Data.S["SysIcon"])
				App.mainwindow.CreateSystray();
			else
				App.mainwindow.DisposeSystray();

			if (Data.S["ThumbnailToolbar"])
				App.mainwindow.ShowThumbnailToolbar();
			else
				App.mainwindow.HideThumbnailToolbar();

			if (Data.S["RemoteServer"])
				new Thread(new ThreadStart(App.mainwindow.StartRemoteServer)).Start();
			else
				App.mainwindow.StopRemoteServer(); //doesn't work

			App.mainwindow.UpdateModus();

		}
		#endregion

		#region Aero
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			Win7.Glass(this);
		}
		#endregion

		#region Events
		private void comboProfiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (String.IsNullOrEmpty((string)comboProfiles.SelectedItem)) return;
			LoadWakeupTimes((string)comboProfiles.SelectedItem);
			Data.curProfile = (string)comboProfiles.SelectedItem;
		}

		#region Buttons
		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			Apply();
			Close();
		}

		private void ApplyButton_Click(object sender, RoutedEventArgs e)
		{
			Apply();
		}

		private void AbortButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void WOSB_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("http://www.dennisbabkin.com/php/download.php?what=WOSB");
		}

		private void buttonWOSBProg1_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (ShellLibrary.IsPlatformSupported)
				{
					CommonOpenFileDialog fd = new CommonOpenFileDialog(Data.L["SelectFile"]);
					fd.Filters.Add(new CommonFileDialogFilter(Data.L["ExeFiles"], "*.exe"));
					fd.EnsureFileExists = true;

					if (fd.ShowDialog() == CommonFileDialogResult.Ok)
						textWOSBProg1.Text = fd.FileName;
				}
				else
				{
					OpenFileDialog fd = new OpenFileDialog();
					fd.Filter = Data.L["ExeFiles"] + "|*.exe";
					fd.Title = Data.L["SelectFile"];
					fd.Multiselect = true;
					fd.CheckFileExists = true;

					if ((bool)fd.ShowDialog())
					{
						textWOSBProg1.Text = fd.FileName;
					}
				}
			}
			catch (Exception ex)
			{
				Message.Show(ex.Message, "Shutdown7", "Error");
			}
		}

		private void buttonWOSBProg2_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (ShellLibrary.IsPlatformSupported)
				{
					CommonOpenFileDialog fd = new CommonOpenFileDialog(Data.L["SelectFile"]);
					fd.Filters.Add(new CommonFileDialogFilter(Data.L["ExeFiles"], "*.exe"));
					fd.EnsureFileExists = true;

					if (fd.ShowDialog() == CommonFileDialogResult.Ok)
						textWOSBProg2.Text = fd.FileName;
				}
				else
				{
					OpenFileDialog fd = new OpenFileDialog();
					fd.Filter = Data.L["ExeFiles"] + "|*.exe";
					fd.Title = Data.L["SelectFile"];
					fd.Multiselect = true;
					fd.CheckFileExists = true;

					if ((bool)fd.ShowDialog())
					{
						textWOSBProg2.Text = fd.FileName;
					}
				}
			}
			catch (Exception ex)
			{
				Message.Show(ex.Message, "Shutdown7", "Error");
			}
		}

		private void buttonAddProfile_Click(object sender, RoutedEventArgs e)
		{
			if (Data.W.ContainsKey(textNewProfile.Text) | String.IsNullOrEmpty(textNewProfile.Text)) return;

			Data.W.Add(textNewProfile.Text, new Dictionary<string, string>());
			comboProfiles.Items.Add(textNewProfile.Text);
			comboProfiles.SelectedItem = textNewProfile.Text;
			textNewProfile.Text = "";
		}

		private void buttonDeleteProfile_Click(object sender, RoutedEventArgs e)
		{
			if (comboProfiles.Items.Count == 1)
			{
				Message.Show(Data.L["LastProfile"], "Warning");
				return;
			}

			Data.W.Remove((string)comboProfiles.SelectedItem);
			comboProfiles.Items.Remove(comboProfiles.SelectedItem);
			comboProfiles.SelectedIndex = 0;
		}

		#endregion

		#region CheckBoxen
		private void checkWOSB_Click(object sender, RoutedEventArgs e)
		{
			if ((bool)checkWOSB.IsChecked)
				ActivateWOSBUI();
			else
				DeactivateWOSBUI();
		}

		void ActivateWOSBUI()
		{
			panelProfiles.IsEnabled = true;
			labelWOSBTimes.IsEnabled = true;
		}

		void DeactivateWOSBUI()
		{
			panelProfiles.IsEnabled = false;
			labelWOSBTimes.IsEnabled = false;
		}
		#endregion
		#endregion

		#region Remote
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (Data.S["RemoteServer"] | (bool)checkRemoteServer.IsChecked)
			{
				int y; if (!Int32.TryParse(textRemotePort.Text, out y) | (y < 1024 | y > 65535))
				{
					Message.Show(Data.L["RemotePortMissing"], "Error");
					e.Cancel = true;
				}

				if (textRemotePassword.Password == "")
				{
					Message.Show(Data.L["RemotePasswordMissing"], "Error");
					e.Cancel = true;
				}
			}
		}

		private void checkRemoteClient_Checked(object sender, RoutedEventArgs e)
		{
			checkWakeOnLan.IsEnabled = true;
			checkIPv4.IsEnabled = true;
		}

		private void checkRemoteClient_Unchecked(object sender, RoutedEventArgs e)
		{
			checkWakeOnLan.IsChecked = false;
			checkWakeOnLan.IsEnabled = false;
			checkIPv4.IsEnabled = false;
		}
		
		private void checkRemoteServer_Checked(object sender, RoutedEventArgs e)
		{
			textRemotePort.IsEnabled = true;
			textRemotePassword.IsEnabled = true;
			buttonPortCheck.IsEnabled = true;
			buttonFirewallException.IsEnabled = true;

			if (Visibility != Visibility.Collapsed)
			{
				try
				{
					TaskDialog td = new TaskDialog();
					td.Caption = "Shutdown7";
					td.InstructionText = "Remote-Server";
					td.Text = Data.L["RemoteServerHelp"];
					td.Icon = TaskDialogStandardIcon.Information;
					td.StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No;
					if (td.Show() == TaskDialogResult.Yes)
						Process.Start("http://www.shutdown7.com/faq.php?lang=" + Data.Lang);
				}
				catch
				{
					if (MessageBox.Show(Data.L["RemoteServerHelp"], "Shutdown7", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
						Process.Start("http://www.shutdown7.com/faq.php?lang=" + Data.Lang);
				}
			}
		}

		private void checkRemoteServer_Unchecked(object sender, RoutedEventArgs e)
		{
			textRemotePort.IsEnabled = false;
			textRemotePassword.IsEnabled = false;
			buttonPortCheck.IsEnabled = false;
			buttonFirewallException.IsEnabled = false;
		}
		
		#region RemotePW
		private void textRemotePassword_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
		{
			textRemotePassword.Password = Remote.md5(textRemotePassword.Password);
		}

		private void textRemotePassword_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
		{
			textRemotePassword.Password = "";
		}
		#endregion

		#region Portcheck
		private void buttonPortCheck_Click(object sender, RoutedEventArgs e)
		{
			buttonPortCheck.IsEnabled = false;
			Thread t = new Thread(new ParameterizedThreadStart(Portcheck));
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

			Dispatcher.Invoke((System.Action)delegate { buttonPortCheck.IsEnabled = true; });
		}

		int _Port;
		public void Portcheck(object _param)
		{
			_Port = Convert.ToInt32(_param);

			//Port
			try
			{
				TcpListener tcpListener = new TcpListener(Dns.GetHostEntry("127.0.0.1").AddressList[0], _Port);
				tcpListener.Start();
				tcpListener.Stop();
			}
			catch (Exception ex)
			{
				if (Data.debug_verbose)
					Message.Show(ex.Message, "Shutdown7", "Error");

				Message.Show(Data.L["PortCheckErrPort"], "Shutdown7", "Error");
				return;
			}

			//Lokal
			string answer; 
			try
			{
				TcpClient tcpClient = new TcpClient(Remote.GetIP("127.0.0.1"), _Port);
				NetworkStream clientStream = tcpClient.GetStream();
				byte[] buffer = new ASCIIEncoding().GetBytes("Ping");
				clientStream.Write(buffer, 0, buffer.Length);
				clientStream.Flush();

				byte[] message = new byte[32];
				answer = new ASCIIEncoding().GetString(message, 0, clientStream.Read(message, 0, 32));
				
				tcpClient.Close();
			}
			catch (Exception ex)
			{
				if (Data.debug_verbose)
					Message.Show(ex.Message, "Shutdown7", "Error");
				answer = "";
			}

			if (answer != "Pong")
			{
				Message.Show(Data.L["PortCheckErrLocal"] + Remote.GetIP(Dns.GetHostName()), "Shutdown7", "Error");
				return;
			}

			//Network
			answer = "";
			try
			{
				TcpClient tcpClient = new TcpClient(Remote.GetIP(Dns.GetHostName()), _Port);
				NetworkStream clientStream = tcpClient.GetStream();
				byte[] buffer = new ASCIIEncoding().GetBytes("Ping");
				clientStream.Write(buffer, 0, buffer.Length);
				clientStream.Flush();

				byte[] message = new byte[32];
				answer = new ASCIIEncoding().GetString(message, 0, clientStream.Read(message, 0, 32));
				if (Data.debug_verbose)
					Message.Show(answer, "Shutdown7", "Information");

				tcpClient.Close();
			}
			catch (Exception ex)
			{
				if (Data.debug_verbose)
					Message.Show(ex.Message, "Shutdown7", "Information");
				answer = "";
			}

			if (answer != "Pong")
			{
				Message.Show(Data.L["PortCheckErrNetwork"] + Remote.GetIP(Dns.GetHostName()), "Shutdown7", "Error");
				return;
			}

			//Remote
			answer = ""; 
			string RemoteIP = new WebClient().DownloadString("http://automation.whatismyip.com/n09230945.asp").Trim();
			try
			{
				TcpClient tcpClient = new TcpClient(RemoteIP, _Port);
				NetworkStream clientStream = tcpClient.GetStream();
				byte[] buffer = new ASCIIEncoding().GetBytes("Ping");
				clientStream.Write(buffer, 0, buffer.Length);
				clientStream.Flush();

				byte[] message = new byte[32];
				answer = new ASCIIEncoding().GetString(message, 0, clientStream.Read(message, 0, 32));
				if (Data.debug_verbose)
					Message.Show(answer, "Shutdown7", "Information");

				if (answer != "Pong")
				{
					Message.Show(Data.L["PortCheckErrRemote"] + Remote.GetIP("127.0.0.1"), "Shutdown7", "Error");
					return;
				}
				tcpClient.Close();
			}
			catch (Exception ex)
			{
				if (Data.debug_verbose)
					Message.Show(ex.Message, "Shutdown7", "Information");
				answer = "";
			}

			if (answer != "Pong")
			{
				Message.Show(Data.L["PortCheckErrNetwork"] + Remote.GetIP(Dns.GetHostName()), "Shutdown7", "Error");
				return;
			}

			Message.Show(Data.L["PortCheckOK"], "Shutdown7", "Information");
		}

		#endregion

		#region Firewall
		private void buttonFirewallException_Click(object sender, RoutedEventArgs e)
		{
			buttonFirewallException.IsEnabled = false;
			FirewallException();
			buttonFirewallException.IsEnabled = true;
		}
		
		public void FirewallException()
		{
			Process p = new Process();
			if (Environment.OSVersion.Version.Major >= 6)
			{
				p.StartInfo.FileName = "netsh";
				p.StartInfo.Arguments = "advfirewall firewall add rule name=Shutdown7 dir=in action=allow profile=any protocol=tcp localport=" + textRemotePort.Text;
			}
			else
			{
				p.StartInfo.FileName = "netsh";
				p.StartInfo.Arguments = "firewall add portopening protocol=tcp port=" + textRemotePort.Text + " name=Shutdown7 profile=all";
			}
			p.StartInfo.Verb = "runas";
			p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.UseShellExecute = false;
			p.Start();

			string ok = p.StandardOutput.ReadLine();
			p.WaitForExit();
			
			if (ok == "OK.")
				Message.Show(Data.L["FirewallOK"], "Shutdown7", "Information");
			else
				Message.Show(Data.L["FirewallError"] + ok, "Shutdown7", "Error");
		}
		#endregion

		private void _Click(object sender, RoutedEventArgs e)
		{

		}
		#endregion

		#region Taskschedule

		void CreateTask()
		{
			/// Documentation: https://taskscheduler.codeplex.com/documentation
			/// Trigger: https://taskscheduler.codeplex.com/wikipage?title=TriggerSamples&referringTitle=Documentation
			
			// Get the service on the local machine
			using (TaskService ts = new TaskService())
			{
				// Create a new task definition and assign properties
				TaskDefinition td = ts.NewTask();
				td.RegistrationInfo.Description = "Launch Shutdown7";

				// Create a trigger that will fire the task at this time every other day
				td.Triggers.Add(new DailyTrigger { DaysInterval = 1 });

				// Create an action that will launch Shutdown7 whenever the trigger fires
				td.Actions.Add(new ExecAction(Data.EXE, "-lo", null));

				// Register the task in the root folder
				ts.RootFolder.RegisterTaskDefinition(@"Shutdown7", td);

				// Remove the task we just created
				ts.RootFolder.DeleteTask("Shutdown7");
			}
		}

		#endregion

	}
}
