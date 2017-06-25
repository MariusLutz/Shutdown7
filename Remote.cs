using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Shutdown7
{
	class Remote
	{
		public static bool ExitRemoteServer;
		
		//Host to IP
		public static string GetIP(string Host)
		{
			string IP = null;

			if (Data.S["IPv4"])
				IP = Dns.GetHostByName(Host).AddressList[0].ToString();
			else
				IP = Dns.GetHostEntry(Host).AddressList[0].ToString();

			if (Data.debug_verbose)
				Message.Show(IP, "Portcheck", "Information");

			return IP;
		}

		//IP to Mac
		[DllImport("iphlpapi.dll")]
		private static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);

		public static string IpToMacAddress(IPAddress ipAddress)
		{
			byte[] mac = new byte[6];
			int len = mac.Length;
			int res = SendARP(BitConverter.ToInt32(ipAddress.GetAddressBytes(), 0), 0, mac, ref len);
			if (res != 0)
				return null;
			else
				return BitConverter.ToString(mac);
		}

		public static string GetWebUI(DispatcherTimer Timer, string ProcessName, string FileName, ItemCollection listMusicFiles, bool Reload)
		{
			string WebUI;

            string CSS = "body{ background-color: #e9e9e9; color: #1e2329; font-size: 12pt; font-family:verdana, sans-serif; }\na { text-decoration: none; color: #0072E5; }\nh2 { display:inline-block; font-variant: small-caps; color: #333333; margin-top: 0; margin-bottom: 10px; border-bottom: 2px solid #0066FF; }\n";
            CSS += ".logo{font-family: Tahoma, Geneva, sans-serif; font-size: 26pt; float: left;}\n.logo:before {content: url(http://www.shutdown7.com/images/Shutdown7.png); position: relative;top: 2px;}\n.logo2 { color: #1e2329; text-shadow: 1px 2px 3px rgba(30, 35, 41, 0.3) }\n";
            CSS += "nav { height: 34px; margin: -11px 0 5px; border-radius: 3px; background: #fff; filter: background: -moz-linear-gradient(#ffffff, #dddddd); background: -webkit-linear-gradient(#ffffff, #dddddd); background: -o-linear-gradient(top, #ffffff 0%,#dddddd 100%); background: -ms-linear-gradient(top, #ffffff 0%,#dddddd 100%);progid:DXImageTransform.Microsoft.gradient( startColorstr='#ffffff', endColorstr='#dddddd',GradientType=0 ); background: linear-gradient(top, #ffffff 0%,#dddddd 100%);}\nnav ul{padding: 0 !important; position: relative;}\nnav ul li{ list-style-type: none; float: left; margin-right: 5px;}\nnav ul li a {color: #1e2329;}\n.menu-link{display: inline-block;font-weight: bold;font-size: 14px;padding: 3px 15px;line-height: 2em;text-transform: uppercase;border-radius: 2px;-moz-border-radius: 2px;-webkit-border-radius: 2px;-o-border-radius: 2px;-ms-border-radius: 2px;-khtml-border-radius: 2px;}\n.menu-link:hover{color: #fff; text-shadow: 1px 2px 3px rgba(255, 255, 255, 0.3); background: #f82814; background: -moz-linear-gradient(#f82814, #940a00); background: -webkit-linear-gradient(#f82814, #940a00); background: -o-linear-gradient(top, #f82814 0%,#940a00 100%); background: -ms-linear-gradient(top, #f82814 0%,#940a00 100%); filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#f82814', endColorstr='#940a00',GradientType=0 ); background: linear-gradient(top, #f82814 0%,#940a00 100%);}\n";
            CSS += "#main { background-color: #efefef; padding: 20px 20px 0; line-height: 1.5; min-height: 400px; -moz-border-radius: 10px; -webkit-border-radius: 10px; -o-border-radius: 10px; -ms-border-radius: 10px; -khtml-border-radius: 10px;}\n";
			CSS += "progress { height: 1.5em; width: 200px; }\n";
            CSS += "#ok {color: green; }\n#error {color: red; }\n#page { width: 95%; margin: 5px auto }\n.clearfix {clear: both; height: 0;}\n";
            CSS += "footer p{ color: #888888; font-size: 10pt; margin: 0; float: right; clear: both; position: relative; bottom: 15px;}\n#version {color: #383d43;}";

			string jQuery = "<script src=\"//ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js\"></script>\n";
			string JS_Countdown = "<script language='JavaScript'>\nvar h={1}, m={2}, s={3}\nfunction countdown() <|\nif (h == 0 & m == 0 & s == 0) <|\ndocument.getElementById('status').innerHTML = '{0}';\n|> else <|\nif (s < 1)\n<|\ns = 59;\nif (m == 0)\n<|\nm = 59;\nif (h != 0)\nh -= 1;\n|>\nelse\nm -= 1;\n|>\nelse\ns -= 1;\nvar hx = h, mx = m, sx = s;\nif(h<10) hx='0'+h; if(mx<10) mx='0'+m; if(s<10) sx='0'+s;\ndocument.getElementById('Countdown').innerHTML = hx+':'+mx+':'+sx;\nsetTimeout('countdown()',1000);\n|>\n|>\n</script>\n";
			string JS_Update = "<script language='JavaScript'>var versionCompare = function(minimum, current) { var parseVersion = function(version) { version = /(\\d+)\\.?(\\d+)?\\.?(\\d+)?\\.?(\\d+)?/.exec(version); return { major: parseInt(version[1]) || 0, minor: parseInt(version[2]) || 0, build: parseInt(version[3]) || 0, revision: parseInt(version[4]) || 0 } }; minimum = parseVersion(minimum); current = parseVersion(current); if (minimum.major != current.major) return (current.major > minimum.major); else { if (minimum.minor != current.minor) return (current.minor > minimum.minor); else { if (minimum.build != current.build) { return (current.build > minimum.build); } else { if (minimum.revision != current.revision) { return (current.revision > minimum.revision); } else { return true; } } } } };\nif (window.jQuery) { $.get('http://www.shutdown7.com/download.php?cmd=version&file=Shutdown', function(version) { if (versionCompare('" + Data.CurVersion + "', version)) { $('#update').html('<a href=\"#\" target=\"_blank\">" + Data.L["UpdateisAvaiable"] + ": ' + version + '</a>'); } }); }</script>\n";
			string JS_Reload = "<script language='JavaScript'>setTimeout(\"$('body').load(location.href);\",10000)</script>\n";
			string JS_ExtIP = "<script language='JavaScript'>if (window.jQuery) { $('#WebUI').html('<a href=\"http://www.shutdown7.com/webui.php?server=' + window.location.hostname + '&port=" + Data.RemotePort + "\" target=\"_blank\">WebUI</a>'); $.get('http://www.shutdown7.com/getip.php', function(data) { $('#WebUI').html('<a href=\"http://www.shutdown7.com/webui.php?server=' + data + '&port=" + Data.RemotePort + "\" target=\"_blank\">WebUI</a>'); } ); }</script>\n";

			WebUI = "<!DOCTYPE HTML>\n";
			WebUI += "<html>\n";
			WebUI += "<head>\n";
			WebUI += "<meta http-equiv=\"X-UA-Compatible\" content=\"chrome=1\">\n";
			WebUI += "<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\" />\n";
			WebUI += "<title>Shutdown7 Server</title>\n";
			WebUI += "<link rel=\"shortcut icon\" href=\"http://www.shutdown7.com/images/shutdown7.ico\" type=\"image/x-icon\" />\n";
			WebUI += "<style>\n" + CSS + "\n</style>\n";
			if (Reload)
			{
				WebUI += "<script language='JavaScript'>window.location = window.location.pathname;</script>\n";
				WebUI += "</header>";
			}
			else
			{
				WebUI += jQuery;
				WebUI += "</head>\n";

				WebUI += "<body" + ((Timer.IsEnabled & Data.Condition == Data.Conditions.Time) ? " onload='countdown()'" : "") + ">\n";
				WebUI += "<div id=\"page\">";
				WebUI += "<header>";
				WebUI += "<div class=\"logo\">";
				WebUI += "<span class=\"logo2\"> Shutdown<sub>7</sub> Server - <em>Shutdown smarter!</em></span>";
				WebUI += "</div>";
				WebUI += "<div class=\"clearfix\"></div>";

				WebUI += "<nav>\n<ul>";
				WebUI += "<li><a class=\"menu-link\" href=\"http://www.shutdown7.com\">Shutdown7.com</a></li>";
				WebUI += "<li><a class=\"menu-link\" href=\"http://www.shutdown7.com/android.php\">Android</a></li>";
				WebUI += "<li><a class=\"menu-link\" href=\"http://www.shutdown7.com/webui.php?server=&port=" + Data.RemotePort + "\">WebUI</a></li>";
				WebUI += "<li><a class=\"menu-link\" href=\"http://www.shutdown7.com/help.php\">Hilfe</a></li>";
				WebUI += "";
				WebUI += "";
				WebUI += "</ul></nav>";
				WebUI += "</header>";

				WebUI += "<div class=\"clearfix\"></div>";
				WebUI += "<div id=\"main\">";

				WebUI += String.Format(Data.L["RemoteErrorBrowser"], "http://www.shutdown7.com/webui.php" + "?server=&port=" + Data.RemotePort);

				if (Timer.IsEnabled & Data.Condition == Data.Conditions.Time)
					WebUI += String.Format(JS_Countdown, Data.L[Data.Mode.ToString()] + "...", Data.t.Hours.ToString(), Data.t.Minutes.ToString(), Data.t.Seconds.ToString()).Replace("<|", "{").Replace("|>", "}");

				WebUI += Data.L["WebUIStatus"] + "<span id='status'>" + RemoteStatus(Timer, ProcessName, FileName, listMusicFiles) + "</span><br />\n";

				WebUI += "<progress id=\"ProgressBar\" value=\"0\" max=\"1\">\n<!--<div id=\"ProgressBar-fallback\">\n<div id=\"Progress\">&nbsp;</div>\n</div>-->\n</progress><br />\n";

				if (Timer.IsEnabled)
					WebUI += "<a href ='?abort'>" + Data.L["Abort"] + "</a><br />\n";

				WebUI += "</div>";

				WebUI += "<footer id=\"footer\">";
				WebUI += "<p id=\"version\">Shutdown7 Version " + Data.Version + "<br />\n<div id='update'></div>\n";
				WebUI += "<p>&copy; " + DateTime.Now.Year + " <a href=\"http://www.mariuslutz.com\">Marius Lutz</a>, Alle Rechte vorbehalten</p>";
				WebUI += "</footer>";

				WebUI += JS_ExtIP + JS_Update + JS_Reload;
			}

			WebUI += "</body>\n";
			WebUI += "</html>";

			return WebUI;
		}

		static string RemoteStatus(DispatcherTimer Timer, string ProcessName, string FileName, ItemCollection listMusicFiles)
		{
			string _RemoteStatus;

			if (!Timer.IsEnabled)
				_RemoteStatus = Data.L["Ready"];
			else
			{
				switch (Data.Condition)
				{
					case Data.Conditions.Process:
						if (Data.S["AllProcesses"])
							_RemoteStatus = Data.L[Data.Mode.ToString()] + String.Format(Data.L["WebUIProcssClosed"], HtmlEncode(ProcessName));
						else
							_RemoteStatus = Data.L[Data.Mode.ToString()] + String.Format(Data.L["WebUIWindowClosed"], HtmlEncode(ProcessName));
						break;
					case Data.Conditions.File:
						_RemoteStatus = Data.L[Data.Mode.ToString()] + String.Format(Data.L["WebUIFileDeleted"], HtmlEncode(FileName.Replace("\\\\", "\\")));
						break;
					case Data.Conditions.Music:
						_RemoteStatus = Data.L[Data.Mode.ToString()] + Data.L["WebUIMusicPlayed_1"];

						foreach (string curfile in listMusicFiles)
							_RemoteStatus += "'" + HtmlEncode(curfile.Substring(curfile.LastIndexOf("\\") + 1) + "', ");

						_RemoteStatus = _RemoteStatus.Remove(_RemoteStatus.Length - 2, 2);

						if (_RemoteStatus.Remove(_RemoteStatus.IndexOf(", "), 2).IndexOf(", ") > -1)
						{
							int pos = _RemoteStatus.LastIndexOf(", ");
							_RemoteStatus = _RemoteStatus.Remove(pos, 2).Insert(pos, " " + Data.L["and"] + " ");
						}

						if (_RemoteStatus.IndexOf(" " + Data.L["and"] + " ") > -1)
							_RemoteStatus += Data.L["WebUIMusicPlayed_2b"];
						else
							_RemoteStatus += Data.L["WebUIMusicPlayed_2a"];

						break;
					case Data.Conditions.Idle:
						_RemoteStatus = Data.L[Data.Mode.ToString()] + String.Format(Data.L["WebUIIdle"], Data.t.Hours + ":" + Data.t.Minutes + ":" + Data.t.Seconds);
						break;
					case Data.Conditions.Time:
						_RemoteStatus = Data.L[Data.Mode.ToString()] + " in <span id='Countdown'>" + Data.t.Hours + ":" + Data.t.Minutes + ":" + Data.t.Seconds + "</span>";
						break;
					default: //Now
						_RemoteStatus = Data.L[Data.Mode.ToString()] + "...";
						break;
				}
			}

			return _RemoteStatus;
		}

		public static string HtmlEncode(string text)
		{
			char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();
			StringBuilder result = new StringBuilder(text.Length + (int)(text.Length * 0.1));

			foreach (char c in chars)
			{
				int value = Convert.ToInt32(c);
				if (value > 127)
					result.AppendFormat("&#{0};", value);
				else
					result.Append(c);
			}

			return result.ToString();
		}

		//MD5
		public static string md5(string txt)
		{
			return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(txt))).Replace("-", "");
		}

	}
}
