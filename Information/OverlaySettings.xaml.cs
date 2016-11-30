#region Using Includes
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
#endregion

namespace Information
{
	/// <summary>
	/// Interaction logic for OverlaySettings.xaml
	/// </summary>
	public partial class OverlaySettings : Window
	{
		#region Variables/Windows
		Overlay overlay;

		int selectedElement = 0;
		bool allowclose = false;
		#endregion

		#region Constructor
		public OverlaySettings()
		{
			InitializeComponent();

			DispatcherTimer update = new DispatcherTimer();
			update.Tick += update_Tick;
			update.Interval = TimeSpan.FromMilliseconds(100);
			update.Start();

			initialWindowSize();

			this.Loaded += Settings_Loaded;

			this.KeyDown += new KeyEventHandler(KeyDownHandler);
		}
		#endregion

		#region Initialize Window
		//
		// Resize window based on resolution
		//
		private void initialWindowSize()
		{
			double width = System.Windows.SystemParameters.PrimaryScreenWidth;
			double height = System.Windows.SystemParameters.PrimaryScreenHeight;
			double widthscale = width / 12;
			double heightscale = height / 6;

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

		public void setOverlay(Overlay tempWindow)
		{
			overlay = tempWindow;
		}

		private void Settings_Loaded(object sender, RoutedEventArgs e)
		{
			if (Properties.Settings.Default.timeVisibility == "Hidden")
				time_enabled.IsChecked = false;
			if (Properties.Settings.Default.dateVisibility == "Hidden")
				date_enabled.IsChecked = false;
			if (Properties.Settings.Default.cpuVisibility == "Hidden")
				cpu_enabled.IsChecked = false;
			if (Properties.Settings.Default.ramVisibility == "Hidden")
				memory_enabled.IsChecked = false;
		}
		#endregion

		#region Window Closing
		public void allowClose(bool allow)
		{
			allowclose = allow;
		}

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			if (allowclose == false)
			{
				base.OnClosing(e);
				e.Cancel = true;
				this.Visibility = Visibility.Hidden;

				Properties.Settings.Default.movingVisibility = "Hidden";
				selectedElement = 0;
			}
		}
		#endregion

		#region Update Visibilities
		void updateVisibility()
		{
			if (time_enabled.IsChecked == true)
				Properties.Settings.Default.timeVisibility = "Visible";
			else
				Properties.Settings.Default.timeVisibility = "Hidden";

			if (date_enabled.IsChecked == true)
				Properties.Settings.Default.dateVisibility = "Visible";
			else
				Properties.Settings.Default.dateVisibility = "Hidden";

			if (cpu_enabled.IsChecked == true)
				Properties.Settings.Default.cpuVisibility = "Visible";
			else
				Properties.Settings.Default.cpuVisibility = "Hidden";

			if (memory_enabled.IsChecked == true)
				Properties.Settings.Default.ramVisibility = "Visible";
			else
				Properties.Settings.Default.ramVisibility = "Hidden";
		}
		void update_Tick(object sender, EventArgs e)
		{
			updateVisibility();
		}
		#endregion

		#region Change Position Button Clicks
		private void time_position_Click(object sender, RoutedEventArgs e)
		{
			Properties.Settings.Default.movingVisibility = "Visible";
			selectedElement = 1;
		}

		private void date_position_Click(object sender, RoutedEventArgs e)
		{
			Properties.Settings.Default.movingVisibility = "Visible";
			selectedElement = 2;
		}

		private void cpu_position_Click(object sender, RoutedEventArgs e)
		{
			Properties.Settings.Default.movingVisibility = "Visible";
			selectedElement = 3;
		}

		private void memory_position_Click(object sender, RoutedEventArgs e)
		{
			Properties.Settings.Default.movingVisibility = "Visible";
			selectedElement = 4;
		}
		#endregion

		#region Movement Handler
		void KeyDownHandler(object sender, KeyEventArgs e)
		{
			double left = 0, top = 0, right = 0;
			switch (selectedElement)
			{
				case 1: // Time
					left = overlay.time.Margin.Left;
					top = overlay.time.Margin.Top;
					break;
				case 2: // Date
					left = overlay.date.Margin.Left;
					top = overlay.date.Margin.Top;
					break;
				case 3: // CPU
					right = overlay.cpu.Margin.Right;
					top = overlay.cpu.Margin.Top;
					break;
				case 4: // Ram
					right = overlay.memory.Margin.Right;
					top = overlay.memory.Margin.Top;
					break;
			}

			switch (selectedElement)
			{
				case 1:
					{
						switch (e.Key)
						{
							case Key.Left: left--; break;
							case Key.Right: left++; break;
							case Key.Up: top--; break;
							case Key.Down: top++; break;
							case Key.Escape:
								selectedElement = 0;
								Properties.Settings.Default.movingVisibility = "Hidden";
								break;
						}
						break;
					}
				case 2:
					{
						switch (e.Key)
						{
							case Key.Left: left--; break;
							case Key.Right: left++; break;
							case Key.Up: top--; break;
							case Key.Down: top++; break;
							case Key.Escape:
								selectedElement = 0;
								Properties.Settings.Default.movingVisibility = "Hidden";
								break;
						}
						break;
					}
				case 3:
				case 4:
					switch (e.Key)
					{
						case Key.Left: right++; break;
						case Key.Right: right--; break;
						case Key.Up: top--; break;
						case Key.Down: top++; break;
						case Key.Escape:
							selectedElement = 0;
							Properties.Settings.Default.movingVisibility = "Hidden";
							break;
					}
					break;
			}

			switch (selectedElement)
			{
				case 1: // Time
					Properties.Settings.Default.timeMargin = left.ToString() + "," + top.ToString() + ",0,0";
					break;
				case 2: // Date
					Properties.Settings.Default.dateMargin = left.ToString() + "," + top.ToString() + ",0,0";
					break;
				case 3: // CPU
					Properties.Settings.Default.cpuMargin = "0," + right.ToString() + "," + top.ToString() + ",0";
					break;
				case 4: // Ram
					Properties.Settings.Default.ramMargin = "0," + right.ToString() + "," + top.ToString() + ",0";
					break;
			}
			e.Handled = true;
		}
		#endregion

		#region Done Button
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		#endregion
	}
}
