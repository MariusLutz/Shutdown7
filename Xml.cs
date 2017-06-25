using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml;

namespace Shutdown7
{
	class Xml
	{
		public static string Path = System.IO.Path.GetDirectoryName(Data.EXE) + "\\Shutdown7.xml";
		public static string WOSBPath = System.IO.Path.GetDirectoryName(Data.EXE) + "\\wosb.xml";

		public static void Read()
		{
			try
			{
				XmlTextReader Reader = new XmlTextReader(Path);
				Reader.Read(); Reader.Read(); //xml Deklaration überspringen

				XmlNodeType curType;
				string curName = "", curValue = "";

				while (Reader.Read())
				{
					Reader.MoveToElement();
					curType = Reader.NodeType;
					curName = Reader.Name;

					switch (curType)
					{
						case XmlNodeType.Element:
							break;

						case XmlNodeType.Text:
							curValue = Reader.Value;
							break;

						case XmlNodeType.EndElement:
							//if (Data.debug_verbose)
                                //Message.Show(curName + "\n" + curValue);

							switch (curName)
							{
								case "Server":
									Array.Resize(ref Data.RemoteServers, Data.RemoteServers.Length + 1);
									Data.RemoteServers[Data.RemoteServers.Length - 1] = curValue;
									break;

								case "Mac":
									Array.Resize(ref Data.RemoteMacs, Data.RemoteMacs.Length + 1);
									Data.RemoteMacs[Data.RemoteMacs.Length - 1] = curValue;
									break;

								case "RemotePort":
									Data.RemotePort = Int32.Parse(curValue);
									break;

								case "RemotePassword":
									Data.RemotePassword = curValue;
									break;
								case "LastVersion":
									try { Data.LastVersion = new Version(curValue); }
									catch { Data.LastVersion = new Version(1, 0, 0, 0); }
									break;
								case "Shutdown7":
                                case "LastAction":
                                case "Parameters":
                                    Reader.Read();
									break;
                                case "Mode":
                                    Data.Mode = (Data.Modes) Enum.Parse(typeof(Data.Modes), curValue);
                                    break;
                                case "Condition":
                                    Data.Condition = (Data.Conditions) Enum.Parse(typeof(Data.Conditions), curValue);
                                    break;
                                case "t":
                                    Data.t = TimeSpan.FromSeconds(Int32.Parse(curValue));
                                    break;
                                default:
                                    if (Data.S.ContainsKey(curName))
										Data.S.Remove(curName);
									Data.S.Add(curName, Convert.ToBoolean(curValue));
									break;
							}

							curValue = "";
							break;
						default:
							Reader.Read();
							break;
					}
				}
				Reader.Close();
			}
			catch (Exception e)
			{
				Message.Show(Data.L["XmlReadError_1"] + String.Format(Data.L["XmlReadError_2"], e.Message), "Error", "Error");
				Environment.Exit(0);
			}
		}

		public static void Write()
		{
			XmlTextWriter Writer = new XmlTextWriter(Path, null);
			Writer.Formatting = Formatting.Indented;
			Writer.WriteStartDocument();
			Writer.WriteStartElement("Shutdown7");

			foreach (KeyValuePair<string, bool> kvp in Data.S)
			{
				//Message.Show(kvp.Key + kvp.Value.ToString());
				Writer.WriteStartElement(kvp.Key);
				Writer.WriteValue(kvp.Value);
				Writer.WriteEndElement();
			}

			Writer.WriteStartElement("RemoteServers");
			foreach (string RemoteServer in Data.RemoteServers)
			{
				Writer.WriteStartElement("Server");
				Writer.WriteValue(RemoteServer);
				Writer.WriteEndElement();
			}
			Writer.WriteEndElement();

			Writer.WriteStartElement("RemoteMacs");
			foreach (string RemoteMac in Data.RemoteMacs)
			{
				Writer.WriteStartElement("Mac");
				Writer.WriteValue(RemoteMac);
				Writer.WriteEndElement();
			}
			Writer.WriteEndElement();

			Writer.WriteStartElement("RemotePort");
			Writer.WriteValue(Data.RemotePort);
			Writer.WriteEndElement();

			Writer.WriteStartElement("RemotePassword");
			Writer.WriteValue(Data.RemotePassword);
			Writer.WriteEndElement();

			Writer.WriteStartElement("LastVersion");
			Writer.WriteValue(Data.CurVersion.ToString());
			Writer.WriteEndElement();

            // ***
            Writer.WriteStartElement("LastAction");

            if (Data.S["ResumeLastAction"])
            {
                Writer.WriteStartElement("Mode");
                Writer.WriteValue(Data.Mode.ToString());
                Writer.WriteEndElement();

                Writer.WriteStartElement("Condition");
                Writer.WriteValue(Data.Condition.ToString());
                Writer.WriteEndElement();

                Writer.WriteStartElement("Parameters");

                Writer.WriteStartElement("t");
                Writer.WriteValue(Data.t.TotalSeconds);
                Writer.WriteEndElement();

                Writer.WriteEndElement();

                Writer.WriteEndElement();
            }

            Writer.WriteEndElement();
            Writer.WriteEndDocument();
            Writer.Close();

        }

		public static void ReadWOSB()
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(WOSBPath);

				XmlNode Profiles = xmlDoc.DocumentElement.ChildNodes[0];

				foreach (XmlNode _curProfile in Profiles.ChildNodes)
				{
					string curProfile = _curProfile.Attributes[0].Value;
					bool isActive = Convert.ToBoolean(_curProfile.Attributes[1].Value);

					if (Data.W.ContainsKey(curProfile))
						Data.W.Remove(curProfile);
					Data.W.Add(curProfile, new Dictionary<string, string>());

					foreach (XmlElement curDay in _curProfile.ChildNodes[0].ChildNodes)
					{
						ReadWakeupTimes(curProfile, curDay.ChildNodes);
					}

					foreach (XmlElement curSetting in _curProfile.ChildNodes)
					{
						//Times-Element ignorieren
						if (Data.W[curProfile].ContainsKey(curSetting.Name))
							Data.W[curProfile].Remove(curSetting.Name);
						Data.W[curProfile].Add(curSetting.Name, curSetting.InnerText);
					}

					if (isActive)
						Data.curProfile = curProfile;

					//sichert, dass ein Profil aktiviert wird, wenn alle auf inaktiv stehen
					if (String.IsNullOrEmpty(Data.curProfile))
						Data.curProfile = curProfile;
				}

				//es existiert keine wosb.xml
				if (String.IsNullOrEmpty(Data.curProfile))
				{
					Data.W.Add("Default", new Dictionary<string, string>());
					Data.curProfile = "Default";
				}


				/*if (Data.debug_verbose)
				{
					foreach (KeyValuePair<Dictionary, string> kvp in Data.W)
					{
						Message.Show(kvp.Key + " " + kvp.Value);
					}
				}*/
			}
			catch (Exception e)
			{
				Message.Show(Data.L["XmlReadError_1"] + String.Format(Data.L["XmlReadError_2"], e.Message + e.StackTrace + e.GetType()), "Error");
				Application.Current.Shutdown();
			}
            
        }

		static void ReadWakeupTimes(string curProfile, XmlNodeList curDay)
		{
			try
			{
				string curWeekday = "", curTime = "";

				for (int i = 0; i < curDay.Count; i++)
				{
					curWeekday = curDay[i].ParentNode.Name.Substring(0, 2) + (i + 1);
					curTime = curDay[i].InnerText;

					//if (Data.debug_verbose) Message.Show(curWeekday + " " + curTime);
					if (Data.W[curProfile].ContainsKey(curWeekday))
						Data.W[curProfile].Remove(curWeekday);
					Data.W[curProfile].Add(curWeekday, curTime);
				}
			}
			catch (Exception e)
			{
				Message.Show(Data.L["XmlReadError_1"] + String.Format(Data.L["XmlReadError_2"], e.Message + e.StackTrace + e.GetType()), "Error");
				Application.Current.Shutdown();
			}
		}
        
        public static void WriteWOSB()
		{
			XmlTextWriter Writer = new XmlTextWriter(WOSBPath, null);
			Writer.Formatting = Formatting.Indented;
			Writer.WriteStartDocument();
			Writer.WriteStartElement("Shutdown7");

			Writer.WriteStartElement("Profiles");

			foreach (KeyValuePair<string, Dictionary<string, string>> curProfile in Data.W)
			{
				Writer.WriteStartElement("Profile");
				Writer.WriteAttributeString("Name", curProfile.Key);
				Writer.WriteAttributeString("Active", (Data.curProfile == curProfile.Key).ToString());

				/*foreach (DayOfWeek DayofWeek in Enum.GetValues(typeof(DayOfWeek)))
				{ //Beginnt mit Sonntag!
					Message.Show(DayofWeek.ToString());
					WriteWakeupTimes(Writer, DayofWeek);
				}*/

				Writer.WriteStartElement("Times");
				WriteWakeupTimes(Writer, curProfile.Key, "Monday");
				WriteWakeupTimes(Writer, curProfile.Key, "Tuesday");
				WriteWakeupTimes(Writer, curProfile.Key, "Wednesday");
				WriteWakeupTimes(Writer, curProfile.Key, "Thursday");
				WriteWakeupTimes(Writer, curProfile.Key, "Friday");
				WriteWakeupTimes(Writer, curProfile.Key, "Saturday");
				WriteWakeupTimes(Writer, curProfile.Key, "Sunday");
				Writer.WriteEndElement();

				Writer.WriteStartElement("File");
				Writer.WriteValue(Data.W[curProfile.Key]["File"]);
				Writer.WriteEndElement();

				Writer.WriteStartElement("Params");
				Writer.WriteValue(Data.W[curProfile.Key]["Params"]);
				Writer.WriteEndElement();

				Writer.WriteStartElement("AwFile");
				Writer.WriteValue(Data.W[curProfile.Key]["AwFile"]);
				Writer.WriteEndElement();

				Writer.WriteStartElement("AwParams");
				Writer.WriteValue(Data.W[curProfile.Key]["AwParams"]);
				Writer.WriteEndElement();

				Writer.WriteStartElement("Extra");
				Writer.WriteValue(Data.W[curProfile.Key]["Extra"]);
				Writer.WriteEndElement();

				Writer.WriteEndElement();
			}
			Writer.WriteEndElement();

			Writer.WriteEndElement();
			Writer.WriteEndDocument();
			Writer.Close();
		}

		static void WriteWakeupTimes(XmlWriter Writer, string curProfile, string Day)
		{
			Writer.WriteStartElement(Day);
			foreach (KeyValuePair<string, string> kvp in Data.W[curProfile])
			{
				if (kvp.Key.StartsWith(Day.Substring(0, 2)) & kvp.Value != "")
				{
					Writer.WriteStartElement("Time");
					Writer.WriteValue(kvp.Value);
					Writer.WriteEndElement();
				}
			}
			Writer.WriteEndElement();
		}

		public static void MigrateIni()
		{
			Ini.Read();
			Xml.Write();
			System.IO.File.Delete(Ini.Path);
		}

		public static void MigrateWOSBIni()
		{
			Data.W.Add("Default", new Dictionary<string, string>());
			Data.W["Default"]["Mo1"] = "";
			Data.W["Default"]["Mo2"] = "";
			Data.W["Default"]["Mo3"] = "";
			Data.W["Default"]["Mo4"] = "";
			Data.W["Default"]["Tu1"] = "";
			Data.W["Default"]["Tu2"] = "";
			Data.W["Default"]["Tu3"] = "";
			Data.W["Default"]["Tu4"] = "";
			Data.W["Default"]["We1"] = "";
			Data.W["Default"]["We2"] = "";
			Data.W["Default"]["We3"] = "";
			Data.W["Default"]["We4"] = "";
			Data.W["Default"]["Th1"] = "";
			Data.W["Default"]["Th2"] = "";
			Data.W["Default"]["Th3"] = "";
			Data.W["Default"]["Th4"] = "";
			Data.W["Default"]["Fr1"] = "";
			Data.W["Default"]["Fr2"] = "";
			Data.W["Default"]["Fr3"] = "";
			Data.W["Default"]["Fr4"] = "";
			Data.W["Default"]["Sa1"] = "";
			Data.W["Default"]["Sa2"] = "";
			Data.W["Default"]["Sa3"] = "";
			Data.W["Default"]["Sa4"] = "";
			Data.W["Default"]["Su1"] = "";
			Data.W["Default"]["Su2"] = "";
			Data.W["Default"]["Su3"] = "";
			Data.W["Default"]["Su4"] = "";
			Data.W["Default"]["File"] = "";
			Data.W["Default"]["Params"] = "";
			Data.W["Default"]["AwFile"] = "";
			Data.W["Default"]["AwParams"] = "";
			Data.W["Default"]["Extra"] = "";
			Data.curProfile = "Default";

			Ini.ReadWOSB();
			Xml.WriteWOSB();
			System.IO.File.Delete(Ini.WOSBPath);
		}

	}
}
