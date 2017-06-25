using System;
using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Windows.Input;

namespace Shutdown7
{
	public partial class About : Window
	{
		public About()
		{
			InitializeComponent();

			#region Lang
			HelpLabel.Text = Data.L["Help"];
			#endregion

			Versionx.Content = "Version " + Data.Version;
		}

		#region Aero
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			Win7.Glass(this);
		}
		#endregion

		#region Websites
		private void WebsiteLabel_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("http://shutdown7.com/?lang=" + Data.Lang.ToLower());
		}

		private void FeedbackLabel_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("http://www.shutdown7.com/kontakt.php?lang=" + Data.Lang.ToLower());
		}

		private void HelpLabel_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("http://www.shutdown7.com/faq.php?lang=" + Data.Lang.ToLower());
		}
		#endregion

		#region Eastereggs
		PacMan pacman = new PacMan();

		private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key.ToString().ToUpper() == "S" && !Rika.IsVisible)
			{
				if (!Shana.IsVisible)
				{
					this.Title = "Shana";
					this.Top = 0;
					Shana.Visibility = Visibility.Visible;
					SoundPlayer Sound = new SoundPlayer(Shutdown7.Properties.Resources.Urusai);
					Sound.Play();
				}
				else
				{
					this.Title = "About";
					Shana.Visibility = Visibility.Collapsed;
				}
			}
			else if (e.Key.ToString().ToUpper() == "N" && !Shana.IsVisible)
			{
				if (!Rika.IsVisible)
				{
					this.Title = "Nipah~";
					this.Top = 0;
					Rika.Visibility = Visibility.Visible;
					SoundPlayer Sound = new SoundPlayer(Shutdown7.Properties.Resources.Nipah);
					Sound.Play();
				}
				else
				{
					this.Title = "About";
					Rika.Visibility = Visibility.Collapsed;
				}
			}
			else if (e.Key.ToString().ToUpper() == "P" && !Shana.IsVisible && !Rika.IsVisible)
			{
				new PacMan().Show();
			}
		}
		#endregion

		#region Buttons
		private void Donate_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=10883669");
		}

		private void Like_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Process.Start("http://www.facebook.com/Shutdown7/");
		}

		private void Tweet_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Process.Start("http://twitter.com/share?_=1285003415520&count=none&original_referer=http%3A%2F%2Fwww.shutdown7.com&text=Shutdown7&url=http%3A%2F%2Fwww.shutdown7.com&via=ShutdownSeven");
		}
		#endregion
	}
}
