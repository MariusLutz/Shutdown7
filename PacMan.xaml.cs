using System;
using System.Windows;

namespace Shutdown7
{
	/// <summary>
	/// Interaktionslogik für PacMan.xaml
	/// </summary>
	public partial class PacMan : Window
	{
		public PacMan()
		{
			InitializeComponent();

			webbrowser.Navigate(new Uri("http://old.mariuslutz.de/pacman/"));
		}

		#region Aero
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			Win7.Glass(this, new Thickness(2));
		}
		#endregion

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			webbrowser.Dispose();
		}
	}
}
