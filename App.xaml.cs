using Microsoft.Shell;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Shutdown7
{
	public partial class App : Application, ISingleInstanceApp
	{
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        public static MainWindow mainwindow;
		public static Stopwatch stopwatch;

        /// <summary>
        /// Interaktionslogik für "App.xaml"
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //Load Language Variables & Settings
            Data.Init(); //Crash Win XP

            #region Stopwatch
            if (Data.debug_stopwatch)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }
            #endregion

            //Single Instance
            if (SingleInstance<App>.InitializeAsFirstInstance(Assembly.GetExecutingAssembly().GetName().Name))
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();

                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            } else {
                IntPtr hWnd = FindWindow(null, "Shutdown7");
                if (!Data.S["SysIcon"])
                    ShowWindow(hWnd, 1);
                SetForegroundWindow(hWnd);
            }
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
		protected override void OnStartup(StartupEventArgs e)
		{
			//ErrorReporting
#if !DEBUG
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);					//From all threads in the AppDomain
			//Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(DispatcherUnhandledException);     //From the main UI dispatcher thread in your WPF application.
#endif

			//if (Data.debug_verbose)
			//	Message.Show("IsAdmin?\nAlt: " + new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator) + "\nNeu: " + isAdmin(), "Information");

			//Admin
			if (!Data.debug_debugging & !isAdmin())
			{
				if (Data.debug_verbose)
					Message.Show(Data.L["RequireAdmin"], "Start", "Warning");

				ProcessStartInfo p = new ProcessStartInfo();
				p.FileName = Data.EXE;
				p.WorkingDirectory = Directory.GetCurrentDirectory();
				p.Verb = "runas";
				p.Arguments = String.Join(" ", e.Args);
				Process.Start(p);
				Environment.Exit(0);
			}

			//Check dll's
			if (!File.Exists(Path.GetDirectoryName(Data.EXE) + "\\Hardcodet.Wpf.TaskbarNotification.dll") |
				!File.Exists(Path.GetDirectoryName(Data.EXE) + "\\Microsoft.WindowsAPICodePack.dll") |
				!File.Exists(Path.GetDirectoryName(Data.EXE) + "\\Microsoft.WindowsAPICodePack.Shell.dll") |
				!File.Exists(Path.GetDirectoryName(Data.EXE) + "\\taglib-sharp.dll") |
				!File.Exists(Path.GetDirectoryName(Data.EXE) + "\\WPFToolkit.Extended.dll") |
				!File.Exists(Path.GetDirectoryName(Data.EXE) + "\\AndroidLib.dll"))
			{
				MessageBox.Show(Data.L["DLLMissing"], "Shutdown7", MessageBoxButton.OK, MessageBoxImage.Error);
				Environment.Exit(0);
			}

			//Beta
			if (Data.debug_beta)
			{
				Data.Version += " Beta";
			}

			//Expiration
			if (Data.debug_expire)
			{
				if (DateTime.Now >= Data.Expiration)
				{
					Message.Show(Data.L["BetaExpired"], "Error");
					Environment.Exit(0);
				}
			}

			//Welcome
			if (Welcome.welcome)
				new Welcome().ShowDialog();
			
			if (Data.CurVersion.CompareTo(Data.LastVersion) > 0)
				Welcome.update = true;

			if (Welcome.update && !Welcome.welcome)
				new Welcome().ShowDialog();

			mainwindow = new MainWindow();

            // Change in 2.3.3
            if (Data.S["SysIcon"])
                mainwindow.CreateSystray();

            //Check Args
            if (e.Args.Length > 0)
			{
				if (!CheckArgs(e.Args))
					Environment.Exit(0);
			}
			else
			{
				/*if (Data.S["SysIcon"])
					mainwindow.CreateSystray();*/

				//Remote-Server
				if (Data.S["RemoteServer"])
					new Thread(new ThreadStart(mainwindow.StartRemoteServer)).Start();

				mainwindow.Show();
			}
            
            base.OnStartup(e);
		}

		public bool SignalExternalCommandLineArgs(IList<string> args)
		{
			if (Data.debug_verbose)
				Message.Show("Instanz läuft bereits.", "Start", "Information");
			
			string[] Args = new string[args.Count];
			args.CopyTo(Args, 0);
			CheckArgs(Args);

			return true;
		}

		#region ErrorReporting
		void DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs ex)
		{
			Exception e = ex.Exception as Exception;
#if DEBUG   // In debug mode do not custom-handle the exception, let Visual Studio handle it
				ex.Handled = false;
#else
			ErrorReporting(e);
			//ex.Handled = true;
			//Environment.Exit(1);
#endif
		}

		void UnhandledException(object sender, UnhandledExceptionEventArgs ex)
		{
			Exception e = ex.ExceptionObject as Exception;

			ErrorReporting(e);
			Environment.Exit(1);
		}

		static void ErrorReporting(Exception e)
		{
            if (e.TargetSite != null)
            {
                if (e.TargetSite.Name.ToString() == "GetPixelFormat" |
                    e.TargetSite.Name.ToString() == "StartWithShellExecuteEx" |
                    e.TargetSite.Name.ToString() == "GetPixelFormat" |
                    e.GetType().ToString() == "System.UnauthorizedAccessException"
                    )
                    return;
            }

			string log;
			log  = "==============================================================================\r\n";
			log += Assembly.GetEntryAssembly() + "\r\n";
			log += "------------------------------------------------------------------------------\r\n";
			log += "Application Information \r\n";
			log += "------------------------------------------------------------------------------\r\n";
			log += "Program      : " + Assembly.GetEntryAssembly().Location + "\r\n";
			log += "Time         : " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\r\n";
			//log += "User         : " + Environment.UserName + "\r\n";
			//log += "Computer     : " + Environment.MachineName + "\r\n";
			log += "OS           : " + Environment.OSVersion.ToString() + "\r\n";
			log += "Culture      : " + CultureInfo.CurrentCulture.Name + "\r\n";
			//log += "Processors   : " + Environment.ProcessorCount + "\r\n";
			//log += "Working Set  : " + Environment.WorkingSet + "\r\n";
			log += "Framework    : " + Environment.Version + "\r\n";
			log += "Run Time     : " + (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString() + "\r\n";
			log += "------------------------------------------------------------------------------\r\n";
			log += "Exception Information\r\n";
			log += "------------------------------------------------------------------------------\r\n";
			log += "Source       : " + e.Source.ToString().Trim() + "\r\n";
			log += "Method       : " + e.TargetSite.Name.ToString() + "\r\n";
			log += "Type         : " + e.GetType().ToString() + "\r\n";
			log += "Error        : " + GetExceptionStack(e) + "\r\n";
			log += "Stack Trace  : " + e.StackTrace.ToString().Trim() + "\r\n";
			log += "------------------------------------------------------------------------------\r\n";
			log += "\r\n";
			
			//Logfile
			string filename = AppDomain.CurrentDomain.BaseDirectory + "ErrorLog.txt";
            FileStream fs;
            FileMode fm;
            StringBuilder sb = new StringBuilder();
            Byte[] byt;

            if (!File.Exists(filename))
            {
                fm = FileMode.Create;
                fs = new FileStream(filename, fm);
                byt = Encoding.ASCII.GetBytes(Data.L["SendLogToDeveloper"] + "\r\n\r\n\r\n");
                fs.Write(byt, 0, byt.Length);
                fs.Close();
            }

            fm = FileMode.Append;
            fs = new FileStream(filename, fm);
			byt = Encoding.ASCII.GetBytes(log);
			fs.Write(byt, 0, byt.Length);
			fs.Close();
            
            MessageBox.Show(String.Format(Data.L["Crash"], filename) + Data.L["Error"] + ":\n" + e.Message/* + "\n" + e.InnerException*/, Data.L["Error"], MessageBoxButton.OK, MessageBoxImage.Error);
            
            //E-Mail
            if (MessageBox.Show(Data.L["ConfirmMail"], "Report Crash", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
			{
				var fromAddress = new MailAddress("crash@shutdown7.com", "Shutdown7");
				var toAddress = new MailAddress("crash@shutdown7.com", "Shutdown7");
				const string fromPsord = "Shutdown7";
				const string subject = "Crashreport Shutdown7";

				var smtp = new SmtpClient
				{
					//Host = "smtp.gmail.com",
					Host = "smtp.strato.de",
					Port = 587,
					EnableSsl = true,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(fromAddress.Address, fromPsord)
				};

				MailMessage message = new MailMessage(fromAddress, toAddress);
				message.Subject = subject;
				message.Body = log;
				smtp.Send(message);
				Message.Show(Data.L["ThankYou"], "Information");
			}
		}

		private static string GetExceptionStack(Exception e)
		{
			StringBuilder message = new StringBuilder();
			message.Append(e.Message);
			while (e.InnerException != null)
			{
				e = e.InnerException;
				message.Append(Environment.NewLine);
				message.Append(e.Message);
			}

			return message.ToString();
		}
		#endregion

		#region Args
		public bool CheckArgs(string[] Args)
		{
			if (Data.debug_verbose)
				Message.Show(string.Join(" ", Args).Trim(), "Argumente", "Information");
			//if (Data.debug_verbose) for (int i = 0; i < Args.Length; i++) Message.Show(Args[i], "Argumente", "Information");
			Debug.WriteLine(string.Join(" ", Args).Trim(), "Arguments");

			//if (mainwindow == null) mainwindow = new MainWindow();

			Arguments CommandLine = new Arguments(Args);
			
			if (CommandLine["updated"] != null)
			{
				mainwindow.Show();
				return true;
			}

			if (CommandLine["a"] != null)
			{
				mainwindow.StopShutdown();
				return true;
			}

			if (CommandLine["t"] != null)
			{
				Data.Condition = Data.Conditions.Time;
				try { Data.t = TimeSpan.FromSeconds(Convert.ToInt32(CommandLine["t"])); }
				catch { Data.t = TimeSpan.FromSeconds(0); };
			}

			if (CommandLine["server"] != null)
			{
				Data.ArgsServer = CommandLine["server"];
			}
			
			if (CommandLine["port"] != null)
			{
				Data.ArgsPort = Convert.ToInt32(CommandLine["port"]);
			}

			if (CommandLine["password"] != null)
			{
				Data.ArgsPassword = CommandLine["password"];
			}

			Data.orgMode = Data.Mode;
			Data.Mode = Data.Modes.None;

			if (CommandLine["s"] != null)
				Data.Mode = Data.Modes.Shutdown;
			else if (CommandLine["r"] != null)
				Data.Mode = Data.Modes.Restart;
			else if (CommandLine["l"] != null)
				Data.Mode = Data.Modes.Logoff;
			else if (CommandLine["lo"] != null)
				Data.Mode = Data.Modes.Lock;
			else if (CommandLine["sb"] != null)
			{
				 if (Data.S["WOSB"])
					Data.Mode = Data.Modes.StandbyWOSB;
				else
					Data.Mode = Data.Modes.Standby;
			}
			else if (CommandLine["h"] != null)
			{
				if (Data.S["WOSB"])
					Data.Mode = Data.Modes.HibernateWOSB;
				else
					Data.Mode = Data.Modes.Hibernate;
			}
			else if (CommandLine["ht"] != null && Data.S["WOSB"])
				Data.Mode = Data.Modes.HibernateWOSBTime;
			else if (CommandLine["hi"] != null && Data.S["WOSB"])
				Data.Mode = Data.Modes.HibernateWOSBIni;

			if (CommandLine["e"] != null)
			{
				if (Data.debug_verbose)
					Message.Show(CommandLine["e"] + "\nVorhanden: " + File.Exists(CommandLine["e"]), "Args", "Information");

				if (File.Exists(CommandLine["e"]))
				{
					Data.Mode =  Data.Modes.Launch;
					mainwindow.LaunchFile = CommandLine["e"];
				}
				else if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + CommandLine["e"]))
				{
					Data.Mode = Data.Modes.Launch;
					mainwindow.LaunchFile = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + CommandLine["e"];
				}
			}

			if ((Data.Mode != Data.Modes.None)/* & (Data.Mode != Data.orgMode)*/)
			{
				if (Data.ArgsServer != "" & Data.ArgsPort > 0 & Data.ArgsPassword != "")
				{
					Data.RemoteByArgs = true;

					ConsoleManager.Show();
					Console.WriteLine("Shutdown7");
					Console.WriteLine("");
					Console.WriteLine(Data.L["Condition"] + ": " + Data.L[Data.Mode.ToString()]);
					Console.WriteLine("Server: " + Data.ArgsServer);
					Console.WriteLine("Port: " + Data.ArgsPort);
					new MainWindow();
				}
				else
				{
					//Data.S["StayAfterShutdown"] = false; //TODO: workaround verbessern
					Data.LocalByArgs = true;
					mainwindow.Execute();
				}

				//Data.Mode = Data.orgMode;
				//Data.orgMode = Data.Modes.None;

				return true;
			}

			if (CommandLine["Run"] != null)
			{
				//Do the same as normal startup

				//Remote-Server
				if (Data.S["RemoteServer"])
					new Thread(new ThreadStart(mainwindow.StartRemoteServer)).Start();

				if (Data.S["SysIcon"])
				{
					mainwindow.CreateSystray();
					//MainWindow.WindowState = WindowState.Minimized;
					MainWindow.Visibility = Visibility.Hidden;
				}
				else
				{
					MainWindow.WindowState = WindowState.Minimized;
					mainwindow.Show();
				}

				return true;
			}

			//keine brauchbaren Args
			return false;
			//Application.Current.Shutdown();
			//Environment.Exit(0);
		}
		#endregion

		#region isAdmin
		[DllImport("advapi32.dll", SetLastError = true)]
		static extern bool GetTokenInformation(IntPtr tokenHandle, TokenInformationClass tokenInformationClass, IntPtr tokenInformation, int tokenInformationLength, out int returnLength);

		/// <summary>
		/// Passed to <see cref="GetTokenInformation"/> to specify what
		/// information about the token to return.
		/// </summary>
		enum TokenInformationClass
		{
			TokenUser = 1,
			TokenGroups,
			TokenPrivileges,
			TokenOwner,
			TokenPrimaryGroup,
			TokenDefaultDacl,
			TokenSource,
			TokenType,
			TokenImpersonationLevel,
			TokenStatistics,
			TokenRestrictedSids,
			TokenSessionId,
			TokenGroupsAndPrivileges,
			TokenSessionReference,
			TokenSandBoxInert,
			TokenAuditPolicy,
			TokenOrigin,
			TokenElevationType,
			TokenLinkedToken,
			TokenElevation,
			TokenHasRestrictions,
			TokenAccessInformation,
			TokenVirtualizationAllowed,
			TokenVirtualizationEnabled,
			TokenIntegrityLevel,
			TokenUiAccess,
			TokenMandatoryPolicy,
			TokenLogonSid,
			MaxTokenInfoClass
		}

		/// <summary>
		/// The elevation type for a user token.
		/// </summary>
		enum TokenElevationType
		{
			TokenElevationTypeDefault = 1,
			TokenElevationTypeFull,
			TokenElevationTypeLimited
		}

		bool isAdmin()
		{
			var identity = WindowsIdentity.GetCurrent();
			if (identity == null) throw new InvalidOperationException("Couldn't get the current user identity");
			var principal = new WindowsPrincipal(identity);

			// Check if this user has the Administrator role. If they do, return immediately.
			// If UAC is on, and the process is not elevated, then this will actually return false.
			if (principal.IsInRole(WindowsBuiltInRole.Administrator)) return true;

			// If we're not running in Vista onwards, we don't have to worry about checking for UAC.
			if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 6)
			{
				// Operating system does not support UAC; skipping elevation check.
				return false;
			}

			int tokenInfLength = Marshal.SizeOf(typeof(int));
			IntPtr tokenInformation = Marshal.AllocHGlobal(tokenInfLength);

			try
			{
				var token = identity.Token;
				var result = GetTokenInformation(token, TokenInformationClass.TokenElevationType, tokenInformation, tokenInfLength, out tokenInfLength);

				if (!result)
				{
					var exception = Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
					throw new InvalidOperationException("Couldn't get token information", exception);
				}

				var elevationType = (TokenElevationType)Marshal.ReadInt32(tokenInformation);

				switch (elevationType)
				{
					case TokenElevationType.TokenElevationTypeDefault:
						// TokenElevationTypeDefault - User is not using a split token, so they cannot elevate.
						return false;
					case TokenElevationType.TokenElevationTypeFull:
						// TokenElevationTypeFull - User has a split token, and the process is running elevated. Assuming they're an administrator.
						return true;
					case TokenElevationType.TokenElevationTypeLimited:
						// TokenElevationTypeLimited - User has a split token, but the process is not running elevated. Assuming they're an administrator.
						return true;
					default:
						// Unknown token elevation type.
						return false;
				}
			}
			finally
			{
				if (tokenInformation != IntPtr.Zero) Marshal.FreeHGlobal(tokenInformation);
			}
		}
		#endregion

	}
}
