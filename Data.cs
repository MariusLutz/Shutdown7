using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Shutdown7
{
	class Data
	{
		#region Deklarationen
		public static Dictionary<string, string> L = new Dictionary<string, string>();
		public static Dictionary<string, bool> S = new Dictionary<string, bool>();
		public static Dictionary<string, Dictionary<string, string>> W = new Dictionary<string, Dictionary<string, string>>();

		public static string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		public static Version CurVersion = Assembly.GetExecutingAssembly().GetName().Version;
		public static Version LastVersion = new Version(1, 0, 0, 0);
		public static string Lang = CultureInfo.CurrentCulture.Name.Substring(0, 2);
		public static TimeSpan t = new TimeSpan();
		public static string EXE = Assembly.GetExecutingAssembly().Location;
		public static string WOSBPath = Path.GetDirectoryName(EXE) + "\\wosb.exe";
		public static string NoisePath = Path.GetDirectoryName(EXE) + "\\White Noise.mp3";

		public static string[] RemoteServers = new string[0];
		public static string[] RemoteMacs = new string[0];
		public static string RemotePassword = "";
		public static int RemotePort = 0;
        public static string ProcessName = "", FileName = "", LaunchFile = "", NetworkAdapter = "";
        public static int Network = 0, CpuValue = 90;
        public static bool CpuMode;

        public static string curProfile;

		public static string ArgsServer = "";
		public static string ArgsPassword = "";
		public static int ArgsPort = 0;
		public static bool LocalByArgs = false;
		public static bool RemoteByArgs = false;
        
        public static string[] News, Bugs;

		public enum Modes
		{
			None,
			Shutdown,
			Restart,
			Logoff,
			Lock,
			Standby,
			StandbyWOSB,
			Hibernate,
			HibernateWOSB,
			HibernateWOSBIni,
			HibernateWOSBTime,
			WakeOnLan,
			Launch,
			RestartAndroid,
			Abort,
			
		}
		public enum Conditions
		{
			None,
			Now,
			Time,
			Process,
			File,
			Music,
			Idle,
			Cpu,
			Network
		}
		public static Modes Mode;
		public static Conditions Condition;
		public static Modes orgMode;
		#endregion

		#region Debug-Vars
		public static bool debug_beta = true;
		public static bool debug_expire = false;
		public static DateTime Expiration = new DateTime(2016, 06, 25);

		public static bool debug_verbose = false;
		public static bool debug_noexecute = true;
		public static bool debug_stopwatch = false;
		public static bool debug_mute = false;

		public static bool debug_debugging = true;
		#endregion

		public static void Init()
		{
			if (System.Diagnostics.Debugger.IsAttached)
				debug_debugging = true;

			#region Lang
			//CommandLabel.Content = rm.GetString("Message");

			if (!L.ContainsKey("Shutdown"))
			{
				switch (Lang)
				{
					case "de":
						//Updates
						News = new string[]
						{
                            "Neue Einstellung: Merke Fensterposition"
                        };

							Bugs = new string[]
						{
                            "Verbesserung der Zuverlässigkeit des Trayicons"
						};
						
						//App
						L.Add("DLLMissing", "Eine oder mehrere der erforderlichen dll-Dateien fehlen.\nBitte installieren Sie Shutdown7 neu oder kopieren Sie die dll-Dateien in den gleichen Ordner.");
						L.Add("BetaExpired", "Ihre Beta-Version ist abgelaufen. Bitte fordern Sie eine neue Version vom Entwickler an oder warten Sie auf die finale Version.");
						L.Add("Crash", "Ein schwerwiegender Fehler ist aufgetreten, der Shutdown7 abstürzen ließ. Dieser Fehler wurde in der Datei {0} protokolliert und sollte dem Entwickler gemeldet werden.\n\n");
						L.Add("Error", "Fehlermeldung");
						L.Add("ConfirmMail", "Den Fehlerbericht an den Entwickler zu senden?\nAlle Daten sind anonymisiert und werden nur zur Diagnose des Fehlers verwendet.\n");
						L.Add("ThankYou", "Vielen Dank für Ihre Unterstützung.");
                        L.Add("SendLogToDeveloper", "Bitte senden Sie dieses Log zusammen mit einer kurzen Fehlerbeschreibung an support@shutdown7.com.");

                        //MainWindow
                        L.Add("Shutdown", "Herunterfahren");
						L.Add("Restart", "Neustart");
						L.Add("Logoff", "Abmelden");
						L.Add("Lock", "Sperren");
						L.Add("Standby", "Standby");
						L.Add("Hibernate", "Ruhezustand");
						L.Add("WakeOnLan", "Wake on Lan");
						L.Add("LaunchFile", "Programm starten");
						L.Add("RestartAndroid", "Android neustarten");
						L.Add("Abort", "Abbrechen");
						L.Add("HibernateWOSBIni", "Plane WakeUp (Konfiguration)");
						L.Add("HibernateWOSBTime", "Plane WakeUp");

						L.Add("Condition", "Bedingung");
						L.Add("ModeNow", "Jetzt");
						L.Add("ModeTime", "Zeit abgelaufen");
						L.Add("ModeWindowClosed", "Fenster geschlossen");
						L.Add("ModeProcessClosed", "Prozess geschlossen");
						L.Add("ModeFileDeleted", "Datei gelöscht");
						L.Add("ModeMusicPlayed", "Musik abgespielt");
						L.Add("ModeIdle", "Keine Benutzeraktivität");
						L.Add("ModeCpu", "CPU Auslastung");
						L.Add("ModeNetwork", "Netzwerk Auslastung");
						L.Add("At", "Um");
						L.Add("In", "In");

						L.Add("AskShutdown", "Herunterfahren?");
						L.Add("AskReboot", "Neustarten?");
						L.Add("AskLogoff", "Abmelden?");
						L.Add("AskLock", "Sperren?");
						L.Add("AskStandby", "Computer in Standby versetzen?");
						L.Add("AskHibernate", "Computer in den Ruhezustand versetzen?");
						L.Add("CMD", "Kommandozeilenbefehle");
						L.Add("GO!", "GO!");
						L.Add("Restore", "Wiederherstellen");
						L.Add("Hide", "Verstecken");
						L.Add("Autostart", "Autostart");
						L.Add("Settings", "Einstellungen");
						L.Add("Exit", "Beenden");

						L.Add("BalloontipAbort", "Geplante Aktion abgebrochen.");
						L.Add("BalloontipTimeShutdown", "Der PC wird in {0} heruntergefahren.");
						L.Add("BalloontipTimeRestart", "Der PC wird in {0} neugestartet.");
						L.Add("BalloontipTimeLogoff", "Sie werden in {0} abgemeldet.");
						L.Add("BalloontipTimeLock", "Der PC wird in {0} gesperrt.");
						L.Add("BalloontipTimeStandby", "Der PC wird in {0} in den Standby versetzt.");
						L.Add("BalloontipTimeHibernate", "Der PC wird in {0} in den Ruhezustand versetzt.");
						L.Add("BalloontipTimeStandbyWOSB", L["BalloontipTimeStandby"]);
						L.Add("BalloontipTimeHibernateWOSB", L["BalloontipTimeHibernate"]);
						//HibernateWOSBTime,Ini nicht gebraucht
						L.Add("BalloontipTimeWakeOnLan", "Der PC wird in {0} aufgeweckt.");
						L.Add("BalloontipTimeLaunch", "Datei/Programm wird in {0} gestartet.");
						L.Add("BalloontipTimeRestartAndroid", "Smartphone wird in {0} neugestartet.");
						L.Add("BalloontipProcessShutdown", "Der PC wird heruntergefahren, sobald Prozess {0} geschlossen wird.");
						L.Add("BalloontipProcessRestart", "Der PC wird neugestartet, sobald Prozess {0} geschlossen wird.");
						L.Add("BalloontipProcessLogoff", "Der PC wird abgemeldet, sobald Prozess {0} geschlossen wird.");
						L.Add("BalloontipProcessLock", "Der PC wird gesperrt, sobald Prozess {0} geschlossen wird.");
						L.Add("BalloontipProcessStandby", "Der PC wird in den Standby versetzt, sobald Prozess {0} geschlossen wird.");
						L.Add("BalloontipProcessHibernate", "Der PC wird in den Ruhezustand versetzt, sobald Prozess {0} geschlossen wird.");
						L.Add("BalloontipProcessStandbyWOSB", L["BalloontipProcessStandby"]);
						L.Add("BalloontipProcessHibernateWOSB", L["BalloontipProcessHibernate"]);
						L.Add("BalloontipProcessWakeOnLan", "Der PC wird aufgeweckt, sobald Prozess {0} geschlossen wird.");
						L.Add("BalloontipProcessRestartAndroid", "Smartphone wird neugestartet, sobald Prozess {0} geschlossen wird.");
						L.Add("BalloontipProcessLaunch", "Datei/Programm wird gestartet, sobald Prozess {0} geschlossen wird.");
						L.Add("BalloontipWindowShutdown", "Der PC wird heruntergefahren, sobald Fenster {0} geschlossen wird.");
						L.Add("BalloontipWindowRestart", "Der PC wird neugestartet, sobald Fenster {0} geschlossen wird.");
						L.Add("BalloontipWindowLogoff", "Der PC wird abgemeldet, sobald Fenster {0} geschlossen wird.");
						L.Add("BalloontipWindowLock", "Der PC wird gesperrt, sobald Fenster {0} geschlossen wird.");
						L.Add("BalloontipWindowStandby", "Der PC wird in den Standby versetzt, sobald Fenster {0} geschlossen wird.");
						L.Add("BalloontipWindowHibernate", "Der PC wird in den Ruhezustand versetzt, sobald Fenster {0} geschlossen wird.");
						L.Add("BalloontipWindowStandbyWOSB", L["BalloontipWindowStandby"]);
						L.Add("BalloontipWindowHibernateWOSB", L["BalloontipWindowHibernate"]);
						L.Add("BalloontipWindowWakeOnLan", "Der PC wird aufgeweckt, sobald Fenster {0} geschlossen wird.");
						L.Add("BalloontipWindowLaunch", "Datei/Programm wird gestartet, sobald Fenster {0} geschlossen wird.");
						L.Add("BalloontipWindowRestartAndroid", "Smartphone wird neugestartet, sobald Fenster {0} geschlossen wird.");
						L.Add("BalloontipFileShutdown", "Der PC wird heruntergefahren, sobald die Datei {0} gelöscht wurde.");
						L.Add("BalloontipFileRestart", "Der PC wird neugestartet, sobald die Datei {0} gelöscht wurde.");
						L.Add("BalloontipFileLogoff", "Der PC wird abgemeldet, sobald die Datei {0} gelöscht wurde.");
						L.Add("BalloontipFileLock", "Der PC wird gesperrt, sobald die Datei {0} gelöscht wurde.");
						L.Add("BalloontipFileStandby", "Der PC wird in den Standby versetzt, sobald die Datei {0} gelöscht wurde.");
						L.Add("BalloontipFileHibernate", "Der PC wird in den Ruhezustand versetzt, sobald die Datei {0} gelöscht wurde.");
						L.Add("BalloontipFileStandbyWOSB", L["BalloontipFileStandby"]);
						L.Add("BalloontipFileHibernateWOSB", L["BalloontipFileHibernate"]);
						L.Add("BalloontipFileWakeOnLan", "Der PC wird aufgeweckt, sobald die Datei {0} gelöscht wurde.");
						L.Add("BalloontipFileLaunch", "Datei/Programm wird gestartet, sobald die Datei {0} gelöscht wurde.");
						L.Add("BalloontipFileRestartAndroid", "Smartphone wird neugestartet, sobald die Datei {0} gelöscht wurde.");
						L.Add("BalloontipMusicShutdown", "Der PC wird heruntergefahren, sobald die Datei(en) {0} abgespielt wurde(n).");
						L.Add("BalloontipMusicRestart", "Der PC wird neugestartet, sobald die Datei(en) {0} abgespielt wurde(n).");
						L.Add("BalloontipMusicLogoff", "Der PC wird abgemeldet, sobald die Datei(en) {0} abgespielt wurde(n).");
						L.Add("BalloontipMusicLock", "Der PC wird gesperrt, sobald die Datei(en) {0} abgespielt wurde(n).");
						L.Add("BalloontipMusicStandby", "Der PC wird in den Standby versetzt, sobald die Datei(en) {0} abgespielt wurde(n).");
						L.Add("BalloontipMusicHibernate", "Der PC wird in den Ruhezustand versetzt, sobald die Datei(en) {0} abgespielt wurde(n).");
						L.Add("BalloontipMusicStandbyWOSB", L["BalloontipMusicStandby"]);
						L.Add("BalloontipMusicHibernateWOSB", L["BalloontipMusicHibernate"]);
						L.Add("BalloontipMusicWakeOnLan", "Der PC wird aufgeweckt, sobald die Datei(en) {0} abgespielt wurde(n).");
						L.Add("BalloontipMusicLaunch", "Datei/Programm wird gestartet, sobald die Datei(en) {0} abgespielt wurde(n).");
						L.Add("BalloontipMusicRestartAndroid", "Smartphone wird neugestartet, sobald die Datei(en) {0} abgespielt wurde(n).");
						L.Add("BalloontipIdleShutdown", "Der PC wird bei {0} Inaktivität heruntergefahren.");
						L.Add("BalloontipIdleRestart", "Der PC wird bei {0} Inaktivität neugestartet.");
						L.Add("BalloontipIdleLogoff", "Sie werden bei {0} Inaktivität abgemeldet.");
						L.Add("BalloontipIdleLock", "Der PC wird bei {0} Inaktivität gesperrt.");
						L.Add("BalloontipIdleStandby", "Der PC wird bei {0} Inaktivität in den Standby versetzt.");
						L.Add("BalloontipIdleHibernate", "Der PC wird bei {0} Inaktivität in den Ruhezustand versetzt.");
						L.Add("BalloontipIdleStandbyWOSB", L["BalloontipIdleStandby"]);
						L.Add("BalloontipIdleHibernateWOSB", L["BalloontipIdleHibernate"]);
						L.Add("BalloontipIdleWakeOnLan", "Der PC wird bei {0} Inaktivität aufgeweckt.");
						L.Add("BalloontipIdleLaunch", "Datei/Programm wird bei {0} Inaktivität gestartet.");
						L.Add("BalloontipIdleRestartAndroid", "Smartphone wird wird bei {0} Inaktivität neugestartet.");
						L.Add("BalloontipCpuShutdown", "Der PC wird heruntergefahren, sobald die CPU Auslastung {0} {1} % für {2} ist.");
						L.Add("BalloontipCpuRestart", "Der PC wird neugestartet, sobald die CPU Auslastung {0} {1} % für {2} ist.");
						L.Add("BalloontipCpuLogoff", "Sie werden abgemeldet, sobald die CPU Auslastung {0} {1} % für {2} ist.");
						L.Add("BalloontipCpuLock", "Der PC wird gesperrt, sobald die CPU Auslastung {0} {1} % für {2} ist.");
						L.Add("BalloontipCpuStandby", "Der PC wird in den Standby versetzt, sobald die CPU Auslastung {0} {1} % für {2} ist.");
						L.Add("BalloontipCpuHibernate", "Der PC wird in den Ruhezustand versetzt, sobald die CPU Auslastung {0} {1} % für {2} ist.");
						L.Add("BalloontipCpuStandbyWOSB", L["BalloontipTimeStandby"]);
						L.Add("BalloontipCpuHibernateWOSB", L["BalloontipTimeHibernate"]);
						L.Add("BalloontipCpuWakeOnLan", "Der PC wird aufgeweckt, sobald die CPU Auslastung {0} {1} % für {2} ist.");
						L.Add("BalloontipCpuLaunch", "Datei/Programm wird gestartet, sobald die CPU Auslastung {0} {1} % für {2} ist.");
						L.Add("BalloontipCpuRestartAndroid", "Smartphone wird wird neugestartet, sobald die CPU Auslastung {0} {1} % für {2} ist.");
						L.Add("BalloontipNetworkShutdown", "Der PC wird heruntergefahren, sobald die Netzwerk Auslastung {0} {1} kbps für {2} ist.");
						L.Add("BalloontipNetworkRestart", "Der PC wird neugestartet, sobald die Network Auslastung {0} {1} kbps für {2} ist.");
						L.Add("BalloontipNetworkLogoff", "Sie werden abgemeldet, sobald die Network Auslastung {0} {1} kbps für {2} ist.");
						L.Add("BalloontipNetworkLock", "Der PC wird gesperrt, sobald die Network Auslastung {0} {1} kbps für {2} ist.");
						L.Add("BalloontipNetworkStandby", "Der PC wird in den Standby versetzt, sobald die Network Auslastung {0} {1} kbps für {2} ist.");
						L.Add("BalloontipNetworkHibernate", "Der PC wird in den Ruhezustand versetzt, sobald die Network Auslastung {0} {1} kbps für {2} ist.");
						L.Add("BalloontipNetworkStandbyWOSB", L["BalloontipTimeStandby"]);
						L.Add("BalloontipNetworkHibernateWOSB", L["BalloontipTimeHibernate"]);
						L.Add("BalloontipNetworkWakeOnLan", "Der PC wird aufgeweckt, sobald die Network Auslastung {0} {1} kbps für {2} ist.");
						L.Add("BalloontipNetworkLaunch", "Datei/Programm wird gestartet, sobald die Network Auslastung {0} {1} kbps für {2} ist.");
						L.Add("BalloontipNetworkRestartAndroid", "Smartphone wird wird neugestartet, sobald die Network Auslastung {0} {1} kbps für {2} ist.");
						
						L.Add("FiveSecs", "5 Sekunden");
						L.Add("XmlReadError_1", "Fehler beim Lesen der Konfigurationsdatei, achten Sie auf Systaxfehler.");
						L.Add("XmlReadError_2", "\n\nGenauer Fehler:\n\n{0}\n\nBitte Anwendung neustarten.");
						L.Add("WOSBIniFileDone", "WOSB-Inidatei erfolgreich geschrieben.Bitte Anwendung neustarten.");
						L.Add("NoWakeUpSheduled", "Kein Wakeup eingetragen.");
						//L.Add("RequireAdmin", "Shutdown7 funktioniert nur mit Adminrechten.\nBitte mit Adminrechten neustarten.");
						L.Add("RequireAdmin", "Shutdown7 funktioniert nur mit Adminrechten.\nEs wird mit Adminrechten neugestartet.");
						L.Add("Updated", "Erfolgreich aktualisiert.");
						L.Add("SelectFile", "Wähle eine Datei");
						L.Add("SelectFiles", "Wähle Datei(en)");
						L.Add("AllFiles", "Alle Dateien");
						L.Add("MusicFiles", "Musikstücke");
						L.Add("ExeFiles", "Anwendungen");
						L.Add("PlayLists", "Playlisten");
						L.Add("Browse", "Durchsuchen");
						L.Add("Add", "Hinzufügen");
						L.Add("Remove", "Entfernen");
						L.Add("Above", "Über");
						L.Add("Below", "Unter");
						L.Add("Down", "Downstream");
						L.Add("Up", "Upstream");
						L.Add("ScreenOff", "Monitor ausschalten");
						L.Add("MusicFadeout", "Musiklautstärke absenken");
						L.Add("MusicOrgVolume", "Anfangslautstärke");
						L.Add("FadeStart", "Senke ab nach % der Gesamtlänge");
						L.Add("FadeEndVolume", "Endlautstärke (0 = Stille)");
						L.Add("PlayNoise", "Spiele weißes Rauschen");
						L.Add("Password", "Passwort");
                        L.Add("ResumeLastAction", "Letzten Auftrag fortsetzen");
                        L.Add("StartShutdownError", "Fehler aufgetreten beim Starten des Shutdowns.");
						L.Add("NoAndroidFound", "Kein angeschlossenes Android-Gerät gefunden.");

						L.Add("RemoteConnect", "Verbinde");
						L.Add("RemoteSend", "Sende");
						L.Add("RemoteErrorRequest", "Ungültiger Befehl gesendet");
						L.Add("RemoteErrorBrowser", "Sie haben Shutdown7 im Webbrowser aufgerufen.<br />\nUm Ihren PC auszuschalten, besuchen Sie bitte die <span id=\"WebUI\"><a href=\"{0}\">WebUI</a></span> oder benutzen Sie Shutdown7 im Client-Mode.<br />\n");
						L.Add("RemoteWrongPass", "Jemand versuchte Ihren PC herunterzufahren, benutzte aber das falsche Passwort.\nWenn Sie das nicht waren, ändern Sie aus Sicherheitsgründen den Port und/oder das Passwort.");
						L.Add("RemoteWrongPassShort", "Falsches Passwort");
						L.Add("RemoteBusy", "Shutdown7 führt bereits eine Aktion auf diesem PC aus.\nWenn Sie einen weiteren Auftrag durchführen möchten, müssen Sie den vorherigen abbrechen.");
						L.Add("RemoteServerTimeout", "Server nicht erreichbar");
						L.Add("RemoteBusyShort", "Shutdown läuft bereits");
						L.Add("RemoteError", "Unbekannter Fehler");
						L.Add("RemotePortMissing", "Bitte wählen Sie einen freie Portnummer zwischen 1024 und 65535.");
						L.Add("WebUIStatus", "<br /><b>Status:</b><br />\n");
						L.Add("WebUIProcssClosed", ", wenn der Prozess '{0}.exe' geschlossen wurde.");
						L.Add("WebUIWindowClosed", ", wenn das Fenster '{0}' geschlossen wurde.");
						L.Add("WebUIFileDeleted", ", wenn die Datei '{0}' gel&ouml;scht wird.");
						L.Add("WebUIMusicPlayed_1", ", wenn ");
						L.Add("WebUIMusicPlayed_2a", " abgespielt wurde.");
						L.Add("WebUIMusicPlayed_2b", " abgespielt wurden.");
						L.Add("WebUIIdle", ", wenn der PC f&uuml;r {0} nicht benutzt wird.");
						L.Add("Ready", "Bereit");
						L.Add("Receive", "Empfange");
						L.Add("Sucessful", "Erfolgreich");
						L.Add("Aborted", "Abgebrochen");
						L.Add("and", "und");

						L.Add("UpdateisAvaiable", "Update verf&uuml;gbar");

						//Settings
						L.Add("Monday", "Montag");
						L.Add("Tuesday", "Dienstag");
						L.Add("Wednesday", "Mittwoch");
						L.Add("Thursday", "Donnerstag");
						L.Add("Friday", "Freitag");
						L.Add("Saturday", "Samstag");
						L.Add("Sunday", "Sonntag");
						L.Add("RemoteServerHelp", "Benötigen Sie Hilfe bei Einrichtung der Portweiterleitung?");
						L.Add("SystrayNotSupported", "Die Systray-Funktion ist zur Zeit sehr fehleranfällig.\nMöchten Sie diese Funktion wirklich aktivieren?");
						L.Add("PortCheckErrPort", "Port ist bereits belegt, bitte anderen Port wählen.");
						L.Add("PortCheckErrLocal", "Kann nicht lokal verbinden\nÜberprüfen Sie die Firewalleinstellungen.\nIP-Adresse: ");
						L.Add("PortCheckErrNetwork", "Kann nicht lokal übers Netzwerk verbinden\nÜberprüfen Sie die Firewalleinstellungen.\nIP-Adresse: ");
						L.Add("PortCheckErrRemote", "Kann nicht über das Internet verbinden\nÜberprüfen Sie die Firewalleinstellungen.\nIP-Adresse: ");
						L.Add("PortCheckOK", "Portweiterleitung funktioniert.");
						L.Add("RemotePasswordMissing", "Bitte Passwort eingeben.");
						L.Add("FirewallOK", "Firewallausnahme eingetragen.");
						L.Add("FirewallError", "Fehler beim Eintragen der Firewallausnahme.\nBitte schauen Sie in der Web-Hilfe nach, wie sie die Ausnahme manuell einrichten.\nFehler: ");
						L.Add("LastProfile", "Dies ist Ihr letztes Profil. Es kann nicht gelöscht werden, bis Sie ein neues erstellen.");

						//About
						L.Add("Help", "Hilfe");

						//Welcome
						L.Add("PinText", "An Taskleiste anheften");
						break;
					default:
                        //Updates
                        News = new string[]
                        {
                            "New Setting: Remember window position"
                        };

                        Bugs = new string[]
                        {
                            "Systray stability improvements"
                        };

						//App
						L.Add("DDLMissing", "One of the required ddl-files are missing.\nPlease reinstall Shutdown7 or copy the dll-files in the same folder.");
						L.Add("BetaExpired", "Your beta-version is expired. Please demand a newer built from the developer or wait for the final version.");
						L.Add("Crash", "The application encountered a fatal error and must exit. This error has been logged in the file {0} and should be reported to the developer.");
						L.Add("Error", "Error");
						L.Add("ConfirmMail", "Do you allow to send a crash report to the developer?\nAll data are anonymized and will only be used for diagnosis of the error.\n");
						L.Add("ThankYou", "Thank you for your support.");
                        L.Add("SendLogToDeveloper", "Please send this log with a short error description to support@shutdown7.com.");

						//MainWindow
						L.Add("Shutdown", "Shutdown");
						L.Add("Restart", "Restart");
						L.Add("Logoff", "Logoff");
						L.Add("Lock", "Lock");
						L.Add("Standby", "Standby");
						L.Add("Hibernate", "Hibernate");
						L.Add("WakeOnLan", "Wake on Lan");
						L.Add("LaunchFile", "Start application");
						L.Add("RestartAndroid", "Reboot Android device");
						L.Add("Abort", "Abort");
						L.Add("HibernateWOSBIni", "Schedule wake up (Configuration)");
						L.Add("HibernateWOSBTime", "Shedule wake up");

						L.Add("Condition", "Condition");
						L.Add("ModeNow", "Now");
						L.Add("ModeTime", "Time is up");
						L.Add("ModeWindowClosed", "Window closed");
						L.Add("ModeProcessClosed", "Process closed");
						L.Add("ModeFileDeleted", "File deleted");
						L.Add("ModeMusicPlayed", "Music played");
						L.Add("ModeIdle", "No user input");
						L.Add("ModeCpu", "CPU usage");
						L.Add("ModeNetwork", "Network usage");
						L.Add("At", "At");
						L.Add("In", "In");

						L.Add("AskShutdown", "Shutdown?");
						L.Add("AskReboot", "Restart?");
						L.Add("AskLogoff", "Log off?");
						L.Add("AskLock", "Lock?");
						L.Add("AskHibernate", "Hibernate?");
						L.Add("CMD", "Commandline arguments");
						L.Add("GO!", "GO!");
						L.Add("Restore", "Restore");
						L.Add("Hide", "Hide");
						L.Add("Autostart", "Autostart");
						L.Add("Settings", "Settings");
						L.Add("Exit", "Exit");

						L.Add("BalloontipAbort", "Planned action cancelled.");
						//TODO: Translate Android
						L.Add("BalloontipTimeShutdown", "The PC will shutdown in {0}.");
						L.Add("BalloontipTimeRestart", "The PC will restart in {0}.");
						L.Add("BalloontipTimeLogoff", "You will be logged off in {0}.");
						L.Add("BalloontipTimeLock", "The PC will be locked in {0}.");
						L.Add("BalloontipTimeStandby", "The PC will go to standby in {0}.");
						L.Add("BalloontipTimeHibernate", "The PC will hibernate in {0}.");
						L.Add("BalloontipTimeStandbyWOSB", L["BalloontipTimeStandby"]);
						L.Add("BalloontipTimeHibernateWOSB", L["BalloontipTimeHibernate"]);
						L.Add("BalloontipTimeWakeOnLan", "The PC is waked up in {0}.");
						L.Add("BalloontipTimeLaunch", "File/program is run in {0}.");
						L.Add("BalloontipProcessShutdown", "The PC will shutdown when process {0} is closed.");
						L.Add("BalloontipProcessRestart", "The PC will restart when process {0} is closed");
						L.Add("BalloontipProcessLogoff", "You will be logged off when process {0} is closed.");
						L.Add("BalloontipProcessLock", "The PC will be locked when process {0} is closed.");
						L.Add("BalloontipProcessStandby", "The PC will go to standby when process {0} is closed.");
						L.Add("BalloontipProcessHibernate", "The PC will hibernate when process {0} is closed.");
						L.Add("BalloontipProcessStandbyWOSB", L["BalloontipProcessStandby"]);
						L.Add("BalloontipProcessHibernateWOSB", L["BalloontipProcessHibernate"]);
						L.Add("BalloontipProcessWakeOnLan", "The PC is waked up when process {0} is closed.");
						L.Add("BalloontipProcessLaunch", "File/program is run when process {0} is closed.");
						L.Add("BalloontipWindowShutdown", "The PC will shutdown when window {0} is closed.");
						L.Add("BalloontipWindowRestart", "The PC will restart when window {0} is closed.");
						L.Add("BalloontipWindowLogoff", "You will be logged off when window {0} is closed.");
						L.Add("BalloontipWindowLock", "The PC will be locked when window {0} is closed.");
						L.Add("BalloontipWindowStandby", "The PC will go to standby when window {0} is closed.");
						L.Add("BalloontipWindowHibernate", "The PC will hibernate when window {0} is closed.");
						L.Add("BalloontipWindowStandbyWOSB", L["BalloontipWindowStandby"]);
						L.Add("BalloontipWindowHibernateWOSB", L["BalloontipWindowHibernate"]);
						L.Add("BalloontipWindowWakeOnLan", "The PC is waked up when window {0} is closed.");
						L.Add("BalloontipWindowLaunch", "File/program is run when window {0} is closed.");
						L.Add("BalloontipFileShutdown", "The PC will shutdown when file {0} is deleted.");
						L.Add("BalloontipFileRestart", "The PC will restart when file {0} is deleted.");
						L.Add("BalloontipFileLogoff", "You will be logged off when file {0} is deleted.");
						L.Add("BalloontipFileLock", "The PC will be locked when file {0} is deleted.");
						L.Add("BalloontipFileStandby", "The PC will go to standby when file {0} is deleted.");
						L.Add("BalloontipFileHibernate", "The PC will hibernate when file {0} is deleted.");
						L.Add("BalloontipFileStandbyWOSB", L["BalloontipFileStandby"]);
						L.Add("BalloontipFileHibernateWOSB", L["BalloontipFileHibernate"]);
						L.Add("BalloontipFileWakeOnLan", "The PC is waked up when file {0} is deleted.");
						L.Add("BalloontipFileLaunch", "File/program is run when file {0} is deleted.");
						L.Add("BalloontipMusicShutdown", "The PC will shutdown when file(s) {0} was/were played.");
						L.Add("BalloontipMusicRestart", "The PC will restart when file(s) {0} was/were played.");
						L.Add("BalloontipMusicLogoff", "You will be logged off when file(s) {0} was/were played.");
						L.Add("BalloontipMusicLock", "The PC will be locked when file(s) {0} was/were played.");
						L.Add("BalloontipMusicStandby", "The PC will go to standby when file(s) {0} was/were played.");
						L.Add("BalloontipMusicHibernate", "The PC will hibernate when file(s) {0} was/were played.");
						L.Add("BalloontipMusicStandbyWOSB", L["BalloontipMusicStandby"]);
						L.Add("BalloontipMusicHibernateWOSB", L["BalloontipMusicHibernate"]);
						L.Add("BalloontipMusicWakeOnLan", "The PC is waked up when file(s) {0} was/were played.");
						L.Add("BalloontipMusicLaunch", "File/program is run when file(s) {0} was/were played.");
						L.Add("BalloontipIdleShutdown", "The PC will shutdown at {0} idle time.");
						L.Add("BalloontipIdleRestart", "The PC will restart at {0} idle time.");
						L.Add("BalloontipIdleLogoff", "You will be logged off at {0} idle time.");
						L.Add("BalloontipIdleLock", "The PC will be locked at {0} idle time.");
						L.Add("BalloontipIdleStandby", "The PC will go to standby at {0} idle time.");
						L.Add("BalloontipIdleHibernate", "The PC will hibernate at {0} idle time.");
						L.Add("BalloontipIdleStandbyWOSB", L["BalloontipIdleStandby"]);
						L.Add("BalloontipIdleHibernateWOSB", L["BalloontipIdleHibernate"]);
						L.Add("BalloontipIdleWakeOnLan", "The PC is waked up at {0} idle time.");
						L.Add("BalloontipIdleLaunch", "File/program is run at {0} idle time.");
						L.Add("BalloontipIdleRestartAndroid", "Smartphone is restarted at {0} idle time.");				 //
						L.Add("BalloontipCpuShutdown", "The PC will shutdown when cpu usage is {0} {1} % for {2}.");
						L.Add("BalloontipCpuRestart", "The PC will restart when cpu usage is {0} {1} % for {2}.");
						L.Add("BalloontipCpuLogoff", "You will be logged off when cpu usage is {0} {1} % for {2}.");
						L.Add("BalloontipCpuLock", "The PC will be locked when cpu usage is {0} {1} % for {2}.");
						L.Add("BalloontipCpuStandby", "he PC will go to standby when cpu usage is {0} {1} % for {2}.");
						L.Add("BalloontipCpuHibernate", "he PC will hibernate when cpu usage is {0} {1} % for {2}.");
						L.Add("BalloontipCpuStandbyWOSB", L["BalloontipTimeStandby"]);
						L.Add("BalloontipCpuHibernateWOSB", L["BalloontipTimeHibernate"]);
						L.Add("BalloontipCpuWakeOnLan", "The PC is waked up when cpu usage is {0} {1} % for {2}.");
						L.Add("BalloontipCpuLaunch", "File/program is run when cpu usage is {0} {1} % for {2}.");
						L.Add("BalloontipCpuRestartAndroid", "Smartphone is restarted when cpu usage is {0} {1} % for {2}.");
						L.Add("BalloontipNetworkShutdown", "The PC will shutdown when network usage is {0} {1} kbps for {2}.");
						L.Add("BalloontipNetworkRestart", "The PC will restart when network usage is {0} {1} kbps for {2}.");
						L.Add("BalloontipNetworkLogoff", "You will be logged off when network usage is {0} {1} kbps for {2}.");
						L.Add("BalloontipNetworkLock", "The PC will be locked when network usage is {0} {1} kbps for {2}.");
						L.Add("BalloontipNetworkStandby", "he PC will go to standby when network usage is {0} {1} kbps for {2}.");
						L.Add("BalloontipNetworkHibernate", "he PC will hibernate when network usage is {0} {1} kbps for {2}.");
						L.Add("BalloontipNetworkStandbyWOSB", L["BalloontipTimeStandby"]);
						L.Add("BalloontipNetworkHibernateWOSB", L["BalloontipTimeHibernate"]);
						L.Add("BalloontipNetworkWakeOnLan", "The PC is waked up when network usage is {0} {1} kbps for {2}.");
						L.Add("BalloontipNetworkLaunch", "File/program is run when network usage is {0} {1} kbps for {2}.");
						L.Add("BalloontipNetworkRestartAndroid", "Smartphone is restarted when network usage is {0} {1} kbps for {2}.");

						L.Add("FiveSecs", "5 seconds");
						L.Add("XmlReadError_1", "Couldn't read configuration file.");
						L.Add("XmlReadError_2", "\n\nPlease check syntax errors. Error:\n\n{0}\n\nPlease restart this program.");
						L.Add("WOSBIniFileDone", "WOSB-inifile sucessfull written.\nPlease restart this program.");
						L.Add("NoWakeUpSheduled", "No wakeup sheduled.");
						L.Add("RequireAdmin", "Shutdown7 works with admin rights only.\nPlease restart with admin rights.");
						L.Add("Updated", "Update complete.");
						L.Add("SelectFile", "Select a file");
						L.Add("SelectFiles", "Select file(s)");
						L.Add("AllFiles", "All files");
						L.Add("MusicFiles", "Music files");
						L.Add("ExeFiles", "Executables");
						L.Add("PlayLists", "Playlists");
						L.Add("Browse", "Browse");
						L.Add("Add", "Add");
						L.Add("Remove", "Remove");
						L.Add("Above", "Above");
						L.Add("Below", "Below");
						L.Add("Down", "Downstream");
						L.Add("Up", "Upstream");
						L.Add("Password", "Password");
						L.Add("ScreenOff", "Turn screen off");
						L.Add("MusicFadeout", "Fade out music volume");
						L.Add("MusicOrgVolume", "Initial volume");
						L.Add("FadeStart", "Start fading after % of total duration");
						L.Add("FadeEndVolume", "End volume (0 = silent)");
						L.Add("PlayNoise", "Play white noise");
                        L.Add("ResumeLastAction", "Resume last action");
						L.Add("StartShutdownError", "Error occured while shutting down your computer.");
						L.Add("NoAndroidFound", "No connected Android device found.");

						L.Add("RemoteConnect", "Connecting");
						L.Add("RemoteSend", "Sending");
						L.Add("RemoteErrorRequest", "Invalid command sent");
						L.Add("RemoteErrorBrowser", "You visited Shutdown7 via the webbrowser.<br />\nTo shutdown your PC over the internet you have to visit the <span id=\"WebUI\"><a href=\"{0}\">WebUI</a></span> or use Shutdown7 in client-mode.<br />\n");
						L.Add("RemoteWrongPass", "Someone tried to shutdown this PC, but used the wrong password.\nIf it wasn't you, change because of security reasons your port number and/or yout password.");
						L.Add("RemoteWrongPassShort", "Wrong password");
						L.Add("RemoteServerTimeout", "Server not reachable (timeout).");
						L.Add("RemoteBusy", "Shutdown7 is executing already. If you want to execute another command, you have to abort the previous one.");
						L.Add("RemoteBusyShort", "Shutdown already running");
						L.Add("RemoteError", "Unknown Error");
						L.Add("WebUIStatus", "<br /><b>Status:</b><br />\n");
						L.Add("WebUIProcssClosed", " when process '{0}.exe' is closed. ");
						L.Add("WebUIWindowClosed", " when window '{0}' is closed. ");
						L.Add("WebUIFileDeleted", " when file '{0}' is deleted. ");
						L.Add("WebUIMusicPlayed_1", " when ");
						L.Add("WebUIMusicPlayed_2a", " has been played.");
						L.Add("WebUIMusicPlayed_2b", " have been played.");
						L.Add("WebUIIdle", ", when the computer isn't used for {0}.");
						L.Add("Ready", "Ready");
						L.Add("Receive", "Receive");
						L.Add("Sucessful", "Sucessful");
						L.Add("Aborted", "Aborted");
						L.Add("and", "and");

						L.Add("UpdateisAvaiable", "Update available");

						//Settings
						L.Add("Monday", "Monday");
						L.Add("Tuesday", "Tuesday");
						L.Add("Wednesday", "Wednesday");
						L.Add("Thursday", "Thursday");
						L.Add("Friday", "Friday");
						L.Add("Saturday", "Saturday");
						L.Add("Sunday", "Sunday");
						L.Add("RemotePortMissing", "Please specifiy a free port number between 1024 and 65535.");
						L.Add("RemotePasswordMissing", "Please enter password");
						L.Add("RemoteServerHelp", "Do you need help setting up port forwarding?");
						L.Add("PortCheckErrPort", "Port is already in use. Please choose another one.");
						L.Add("PortCheckErrLocal", "Can't connect over localhost\nPlease check firewall settings.\nIP adress: ");
						L.Add("PortCheckErrNetwork", "Can't connect over local network\nPlease check firewall settings.\nIP adress: ");
						L.Add("PortCheckErrRemote", "Can't connect over internet\nPlease check firewall settings.\nIP adress: ");
						L.Add("PortCheckOK", "Port forwarding works.");
						L.Add("FirewallOK", "Firewall exception added.");
						L.Add("FirewallError", "Couldn't add firewall exception.\nPlease refer to the web-help to add the exception manually.\nError: ");
						L.Add("LastProfile", "This is your last profile. You can't delete this one until you created a new one.");

						//About
						L.Add("Help", "Help");

						//Welcome
						L.Add("PinText", "Pin to Taskbar");
						break;
				}
			}
			#endregion

			#region Settings
			if (!S.ContainsKey("Force")) //Default Values
			{
				S.Add("AllProcesses", false);
				S.Add("Ask", false);
				S.Add("Autostart", false);
				S.Add("Force", false);
				S.Add("Glass", false);
				S.Add("IPv4", true);
				S.Add("Jumplist", true);
				S.Add("ModusIcons", true);
				S.Add("Overlay", true);
				S.Add("RemoteClient", false);
				S.Add("RemoteServer", false);
                S.Add("ResumeLastAction", false);
                S.Add("SaveWindowState", false);
                S.Add("SendFeedback", false);
				S.Add("StayAfterShutdown", false);
                S.Add("SysIcon", false);
				S.Add("ThumbnailToolbar", false);
				S.Add("WakeOnLan", false);
				S.Add("Win8Hybrid", Win7.IsWin8);
				S.Add("WOSB", false);
            }

			if (!File.Exists(Ini.Path) & !File.Exists(Xml.Path))
			{
				Welcome.welcome = true;
				//new Welcome().ShowDialog();
			}
			else
			{
				if (File.Exists(Xml.Path))
					Xml.Read();
				else
					Xml.MigrateIni();

				if (Data.S["WOSB"] & !File.Exists(Xml.WOSBPath))
				{
					Xml.MigrateWOSBIni();
				}
			}

			if (!File.Exists(WOSBPath))
				S["WOSB"] = false;

			/*if (S["WOSB"])
			{
				if (!W.ContainsKey("File"))
				{
					W.Add("File", "");
					W.Add("Params", "");
					W.Add("AwFile", "");
					W.Add("AwParams", "");
					W.Add("Extra", "/psbh /ptowu");
					Xml.ReadWOSB();
				}
			}*/
			#endregion
		}
	}
}
