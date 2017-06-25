using Microsoft.Win32;

namespace Shutdown7
{
	class Autostart
	{
		public static void SetAutoStart(string keyName, string assemblyLocation)
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
			key.SetValue(keyName, assemblyLocation);
		}

		public static bool IsAutoStartEnabled(string keyName, string assemblyLocation)
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
			if (key == null)
				return false;

			string value = (string)key.GetValue(keyName);
			if (value == null)
				return false;

			return (value == assemblyLocation);
		}

		public static void UnSetAutoStart(string keyName)
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
			key.DeleteValue(keyName);
		}
	}
}
