#region Using Includes
using System.Windows;
#endregion

namespace Information
{
	/// <summary>
	/// Interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow : Window
	{
		#region Variables/Windows
		private const int GWL_STYLE = -16;
		private const int WS_SYSMENU = 0x80000;

		private bool allowclose = false;

		public OverlaySettings overlaySettings = new OverlaySettings();
		#endregion

		#region Constructor
		public SettingsWindow()
		{
			InitializeComponent();

			initialWindowSize();
		}
		#endregion

		#region Initialize Window
		private void initialWindowSize()
		{
			double width = System.Windows.SystemParameters.PrimaryScreenWidth;
			double height = System.Windows.SystemParameters.PrimaryScreenHeight;
			double widthscale = width / 7.5;
			double heightscale = height / 10;

			if (width > 1920)
			{
				settings.Width += widthscale;
			}
			else if (width < 1920)
			{
				settings.Width -= widthscale;
			}
			else if (width == 1920)
				return;

			if (height > 1080)
			{
				settings.Height += heightscale;
			}
			else if (height < 1080)
			{
				settings.Height -= heightscale;
			}
		}
		#endregion

		#region Window Closing
		public void overideclose(bool reset)
		{
			allowclose = reset;
		}

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			if (allowclose == false)
			{
				base.OnClosing(e);
				e.Cancel = true;
				((MainWindow)this.Owner).disownSettings();
				this.Visibility = Visibility.Hidden;
			}
			else if (allowclose == true)
				overlaySettings.allowClose(true);
		}
		#endregion

		#region Text Color
		private void text_color_Click(object sender, RoutedEventArgs e)
		{
			ColorSelector selector = new ColorSelector(1);

			selector.Owner = this;

			selector.Show();
		}
		#endregion

		#region Button Color
		private void button_color_Click(object sender, RoutedEventArgs e)
		{
			ColorSelector selector = new ColorSelector(2);

			selector.Owner = this;

			selector.Show();
		}
		#endregion

		#region Meter Foreground Color
		private void meter_fore_color_Click(object sender, RoutedEventArgs e)
		{
			ColorSelector selector = new ColorSelector(3);

			selector.Owner = this;

			selector.Show();
		}
		#endregion

		#region Main Background Color
		private void background_color_Click(object sender, RoutedEventArgs e)
		{
			ColorSelector selector = new ColorSelector(4);

			selector.Owner = this;

			selector.Show();
		}
		#endregion

		#region Meter Background Color
		private void meter_back_color_Click(object sender, RoutedEventArgs e)
		{
			ColorSelector selector = new ColorSelector(5);

			selector.Owner = this;

			selector.Show();
		}
		#endregion

		#region Overlay Color
		private void overlay_color_Click(object sender, RoutedEventArgs e)
		{
			ColorSelector selector = new ColorSelector(6);

			selector.Owner = this;

			selector.Show();
		}
		#endregion

		#region Overlay Options
		private void overlay_options_Click(object sender, RoutedEventArgs e)
		{
			overlaySettings.Owner = this;

			Overlay temp = ((MainWindow)this.Owner).overlay;

			overlaySettings.setOverlay(temp);

			overlaySettings.Show();
		}
		#endregion

		#region Defaults
		private void reset_defaults_Click(object sender, RoutedEventArgs e)
		{
			Properties.Settings.Default.MainBackground = "Black";
			Properties.Settings.Default.TextColor = "#FF0066CC";
			Properties.Settings.Default.ButtonBackground = "#FF2D2D30";
			Properties.Settings.Default.Meter_ListBackground = "#FF2D2D30";
			Properties.Settings.Default.MeterForeground = "#FF0066CC";

			Properties.Settings.Default.OverlayColor = "#FF0066CC";
			Properties.Settings.Default.timeVisibility = "Visible";
			Properties.Settings.Default.timeMargin = "10,10,0,0";
			Properties.Settings.Default.dateVisibility = "Visible";
			Properties.Settings.Default.dateMargin = "10,44,0,0";
			Properties.Settings.Default.cpuVisibility = "Visible";
			Properties.Settings.Default.cpuMargin = "0,10,10,0";
			Properties.Settings.Default.ramVisibility = "Visible";
			Properties.Settings.Default.ramMargin = "0,44,10,0";

			Properties.Settings.Default.MainTop = -1;
			Properties.Settings.Default.MainLeft = -1;
			Properties.Settings.Default.MainWidth = 960;
			Properties.Settings.Default.MainHeight = 869;
			Properties.Settings.Default.MainState = "Normal";
			Properties.Settings.Default.MainStartLocation = "CenterScreen";
		}
		#endregion

		#region Done
		private void done_btn_Click(object sender, RoutedEventArgs e)
		{
			((MainWindow)this.Owner).disownSettings();
			this.Visibility = Visibility.Hidden;
		}
		#endregion
	}
}
