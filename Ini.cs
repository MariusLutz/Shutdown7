using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Shutdown7
{
	class Ini
	{
		public static string Path = System.IO.Path.GetDirectoryName(Data.EXE) + "\\Settings.ini";
		public static string WOSBPath = System.IO.Path.GetDirectoryName(Data.EXE) + "\\wosb.ini";
		
		public static void Read()
		{
			try
			{
				StreamReader Fil = new StreamReader(Path);
				while (!Fil.EndOfStream)
				{
					string Txt = Fil.ReadLine();
					if (Txt.IndexOf("=") > 0)
					{
						string[] y = Txt.Split('=');
						if (y[0] == "RemoteServers" | y[0] == "RemoteMacs" | y[0] == "RemotePort" | y[0] == "RemotePassword" | y[0] == "LastVersion")
						{
							if (y[1].EndsWith(";")) y[1] = y[1].Substring(0, y[1].Length - 1);
							if (y[0] == "RemoteServers") Data.RemoteServers = y[1].Split(';');
							if (y[0] == "RemoteMacs") Data.RemoteMacs = y[1].Split(';');
							if (y[0] == "RemotePort") Data.RemotePort = Int32.Parse(y[1]);
							if (y[0] == "RemotePassword") Data.RemotePassword = y[1];
							if (y[0] == "LastVersion")
							{
								try { Data.LastVersion = new Version(y[1]); }
								catch { Data.LastVersion = new Version(1, 0, 0, 0); }
							}
						}
						else
						{
							if (Data.S.ContainsKey(y[0])) Data.S.Remove(y[0]);
							y[1] = y[1].Replace("1", "true").Replace("0", "false");
							Data.S.Add(y[0], Convert.ToBoolean(y[1]));
						}
					}
				}
				Fil.Close();
			}
			catch (Exception e)
			{
				Message.Show(Data.L["XmlReadError_1"] + Data.L["XmlReadError_2"] + "\n" + e.Message + e.StackTrace + e.GetType(), "Error");
				Application.Current.Shutdown();
			}
		}

		public static void Write()
		{
			StreamWriter Fil = new StreamWriter(Path);
			foreach (KeyValuePair<string, bool> kvp in Data.S)
			{
				Fil.WriteLine(kvp.Key + "=" + kvp.Value);
			}
			try
			{
				Fil.WriteLine("RemoteServers=" + String.Join(";", Data.RemoteServers));
			}
			catch
			{
				Fil.WriteLine("RemoteServers=");
			}
			try
			{
				Fil.WriteLine("RemoteMacs=" + String.Join(";", Data.RemoteMacs));
			}
			catch
			{
				Fil.WriteLine("RemoteMacs=");
			}
			Fil.WriteLine("RemotePort=" + Data.RemotePort);
			Fil.WriteLine("RemotePassword=" + Data.RemotePassword);
			Fil.WriteLine("LastVersion=" + Data.CurVersion);
			Fil.Close();
		}

		public static void ReadWOSB()
		{
			if (!File.Exists(WOSBPath))
			{
				WriteWOSB();
			}

			StreamReader Fil = new StreamReader(WOSBPath);
			try
			{
				while (!Fil.EndOfStream)
				{
					string Txt = Fil.ReadLine();
					if (Txt.IndexOf("=") > 0)
					{
						string[] y = Txt.Split('=');
						if (Data.W["Default"].ContainsKey(y[0])) Data.W["Default"].Remove(y[0]);
						Data.W["Default"].Add(y[0], y[1]);
					}
				}
			}
			catch (Exception e)
			{
				Message.Show(Data.L["XmlReadError_1"] + Data.L["XmlReadError_2"] + "\n" + e.Message, "Error");
				Environment.Exit(0);
			}
			Fil.Close();
		}

		public static void WriteWOSB()
		{
			try
			{
				StreamWriter Fil = new StreamWriter(WOSBPath);
				foreach (KeyValuePair<string, string> kvp in Data.W["Default"])
				{
					Fil.WriteLine(kvp.Key + "=" + kvp.Value);
				}
				Fil.Close();
			}
			catch (Exception e)
			{
				Message.Show(/*L["IniWriteError"] + "\n" +*/e.Message, "Error");
			}
		}
	}
}
