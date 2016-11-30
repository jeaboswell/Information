#region Using Includes
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
#endregion

namespace Information
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : IDisposable
	{
		#region Variables/Windows
		SimplifiedAudioAPI AudioAPI;

		bool disposed = false;

		private bool IN_Muted;
		private bool OUT_Muted;
		private float INvScalarValue;
		private float OUTvScalarValue;
		private delegate void InVolumeScalarChangedEventHandler(float value);
		private delegate void OutVolumeScalarChangedEventHandler(float value);

		PerformanceCounter cpuUsage = new PerformanceCounter("Processor", "% Processor Time", "_Total");
		PerformanceCounter ramUsage = new PerformanceCounter("Memory", "% Committed Bytes In Use");
		Process[] procs;

		SettingsWindow settings = new SettingsWindow();

		public Overlay overlay = new Overlay();
		#endregion

		#region Constructor
		public MainWindow()
		{
			//
			// Included coreaudioapi.dll
			//
			AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
			{
				string resourceName = new AssemblyName(args.Name).Name + ".dll";
				string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

				using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
				{
					Byte[] assemblyData = new Byte[stream.Length];
					stream.Read(assemblyData, 0, assemblyData.Length);
					return Assembly.Load(assemblyData);
				}
			};

			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
			//
			// Initialize
			//
			InitializeComponent();

			if (Properties.Settings.Default.MainStartLocation == "Manual")
			{
				this.Top = Properties.Settings.Default.MainTop;
				this.Left = Properties.Settings.Default.MainLeft;
			}
			else
				this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

			initialWindowSize();
			//
			// Timer 1
			//
			DispatcherTimer seconds = new System.Windows.Threading.DispatcherTimer();
			seconds.Tick += timer_Tick;
			seconds.Interval = TimeSpan.FromMilliseconds(100);
			seconds.Start();
			time.Content = DateTime.Now.ToLongTimeString();
			date.Content = DateTime.Now.ToShortDateString();
			//
			// Timer 2
			//
			DispatcherTimer timer2 = new System.Windows.Threading.DispatcherTimer();
			timer2.Tick += timer2_Tick;
			timer2.Interval = TimeSpan.FromMilliseconds(500);
			timer2.Start();
			//
			// Handle loading window
			//
			this.Loaded += new RoutedEventHandler(Window_Loaded);
			//
			// Handle closing windows
			//
			this.Closed += new EventHandler(MainWindow_Closed);
			//
			// Handle session ending
			//
			SystemEvents.SessionEnding += Session_Ending;
		}
		#endregion

		#region Initialize Window
		static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			return EmbeddedAssembly.Get(args.Name);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SimplifiedAudioAPI temp = new SimplifiedAudioAPI();
			AudioAPI = temp;
		}

		private void initialWindowSize()
		{
			double width = System.Windows.SystemParameters.PrimaryScreenWidth;
			double height = System.Windows.SystemParameters.PrimaryScreenHeight;
			double widthscale = width / 8;
			double heightscale = height / 5.2;

			if (width > 1920)
			{
				main.Width += widthscale;
			}
			else if (width < 1920)
			{
				main.Width -= widthscale;
			}
			else if (width == 1920)
				return;

			if (height > 1080)
			{
				main.Height += heightscale;
			}
			else if (height < 1080)
			{
				main.Height -= heightscale;
			}
		}
		#endregion

		#region Closing Window
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;

			if (disposing)
			{
				cpuUsage.Dispose();
				ramUsage.Dispose();
				overlay.Dispose();
			}

			disposed = true;
		}

		private void main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Properties.Settings.Default.movingVisibility = "Hidden";
			Properties.Settings.Default.MainTop = this.Top;
			Properties.Settings.Default.MainLeft = this.Left;
			Properties.Settings.Default.Save();
		}

		protected void MainWindow_Closed(object sender, EventArgs args)
		{
			Dispose();
			settings.overideclose(true);
			settings.Close();

			overlay.Close();

			System.Windows.Application.Current.Shutdown();
		}

		private void Session_Ending(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region Timers
		void timer_Tick(object sender, EventArgs e)
		{
			time.Content = DateTime.Now.ToLongTimeString();
			date.Content = DateTime.Now.ToShortDateString();

			update_source();
			update_audio();

			update_keys();
		}

		void timer2_Tick(object sender, EventArgs e)
		{
			update_system();
		}
		#endregion

		#region Audio Handling
		void update_source()
		{
			SimplifiedAudioAPI temp = new SimplifiedAudioAPI();
			if (temp.deviceRender.ID != AudioAPI.deviceRender.ID || temp.deviceCapture.ID != AudioAPI.deviceCapture.ID)
			{
				AudioAPI = temp;
				Console.WriteLine("Information: Updated CoreAudioAPI");
			}
		}

		void update_audio()
		{
			in_level.Content = AudioAPI.GetPeakValue(SimplifiedAudioAPI.AudioChannels.GET_IN_MasterPeak);

			vpb_in_master.Value = AudioAPI.GetPeakValue(SimplifiedAudioAPI.AudioChannels.GET_IN_MasterPeak);

			vpb_in_left.Value = AudioAPI.GetPeakValue(SimplifiedAudioAPI.AudioChannels.GET_IN_LeftPeak);
			vpb_in_right.Value = AudioAPI.GetPeakValue(SimplifiedAudioAPI.AudioChannels.GET_IN_RightPeak);

			if (Convert.ToInt32(in_level.Content) != trackbar_in.Value)
				in_level.Content = trackbar_in.Value;

			vpb_out_master.Value = AudioAPI.GetPeakValue(SimplifiedAudioAPI.AudioChannels.GET_OUT_MasterPeak);

			vpb_out_left.Value = AudioAPI.GetPeakValue(SimplifiedAudioAPI.AudioChannels.GET_OUT_LeftPeak);
			vpb_out_right.Value = AudioAPI.GetPeakValue(SimplifiedAudioAPI.AudioChannels.GET_OUT_RightPeak);

			if (AudioAPI.IN_MUTE == true && mute_in.IsChecked == false)
			{
				IN_Muted = true;
				mute_in.IsChecked = true;
			}
			else if (AudioAPI.IN_MUTE == false && mute_in.IsChecked == true)
			{
				IN_Muted = false;
				mute_in.IsChecked = false;
			}
			else if (AudioAPI.IN_MUTE == true && IN_Muted == false)
			{
				mute_in.IsChecked = true;
				IN_Muted = false;
			}
			else if (AudioAPI.IN_MUTE == false && IN_Muted == true)
			{
				mute_in.IsChecked = false;
				IN_Muted = true;
			}

			if (AudioAPI.OUT_MUTE == true && mute_out.IsChecked == false)
			{
				OUT_Muted = true;
				mute_out.IsChecked = true;
			}
			else if (AudioAPI.OUT_MUTE == false && mute_out.IsChecked == true)
			{
				OUT_Muted = false;
				mute_out.IsChecked = false;
			}
			else if (AudioAPI.OUT_MUTE == true && OUT_Muted == false)
			{
				mute_out.IsChecked = true;
				OUT_Muted = false;
			}
			else if (AudioAPI.OUT_MUTE == false && OUT_Muted == true)
			{
				mute_out.IsChecked = false;
				OUT_Muted = true;
			}

			if (INvScalarValue != AudioAPI.IN_MasterScalar)
			{
				INvScalarValue = AudioAPI.IN_MasterScalar;
				ExtInVolumeChanged(INvScalarValue);
			}
			if (OUTvScalarValue != AudioAPI.OUT_MasterScalar)
			{
				OUTvScalarValue = AudioAPI.OUT_MasterScalar;
				ExtOutVolumeChanged(OUTvScalarValue);
			}
		}

		private void ExtInVolumeChanged(float value)
		{
			trackbar_in.Value = Convert.ToInt32(value * 100);
			in_level.Content = Convert.ToInt32(value * 100);
		}

		private void ExtOutVolumeChanged(float value)
		{
			trackbar_out.Value = Convert.ToInt32(value * 100);
			out_level.Content = Convert.ToInt32(value * 100);
		}

		private void mute_out_Clicked(object sender, RoutedEventArgs e)
		{
			if (OUT_Muted == true)
			{
				AudioAPI.OUT_MUTE = false;
				OUT_Muted = false;
			}
			else if (OUT_Muted == false)
			{
				AudioAPI.OUT_MUTE = true;
				OUT_Muted = true;
			}
		}

		private void mute_in_Click(object sender, RoutedEventArgs e)
		{
			if (IN_Muted == true)
			{
				AudioAPI.IN_MUTE = false;
				IN_Muted = false;
			}
			else if (IN_Muted == false)
			{
				AudioAPI.IN_MUTE = true;
				IN_Muted = true;
			}
		}

		private void trackbar_out_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			AudioAPI.OUT_MasterScalar = ((float)trackbar_out.Value / 100f);
		}

		private void trackbar_in_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			AudioAPI.IN_MasterScalar = ((float)trackbar_in.Value / 100f);
		}
		#endregion

		#region System Handling
		void update_system()
		{
			VPB_CPU.Value = cpuUsage.NextValue();
			cpuUtilization.Content = Convert.ToInt32(VPB_CPU.Value) + "%";
			Properties.Settings.Default.cpuString = Convert.ToInt32(VPB_CPU.Value) + "%";

			VPB_RAM.Value = ramUsage.NextValue();
			ramUtilization.Content = Convert.ToInt32(ramUsage.NextValue()) + "%";

			procs = Process.GetProcesses();

			if (Convert.ToInt32(processcount.Content) != procs.Length)
			{
				processlist.Items.Clear();
				for (int i = 0; i < procs.Length; i++)
				{
					processlist.Items.Add(procs[i].ProcessName);
				}
				processcount.Content = procs.Length.ToString();
			}
		}

		void update_keys()
		{
			if (Keyboard.GetKeyStates(Key.NumLock) == KeyStates.Toggled)
				numlock.Content = "On";
			else
				numlock.Content = "Off";

			if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled)
				capslock.Content = "On";
			else
				capslock.Content = "Off";

			if (Keyboard.GetKeyStates(Key.Scroll) == KeyStates.Toggled)
				scrolllock.Content = "On";
			else
				scrolllock.Content = "Off";
		}

		private void searchprocs_Text_Changed(object sender, TextChangedEventArgs e)
		{
			string find = searchprocs.Text;
			string upper = find.ToUpper();
			int length = processlist.Items.Count;
			Process search = new Process();
			List<Process> prclist = new List<Process>();


			for (int i = 0; i < length; i++)
			{
				if (procs[i].ProcessName.ToUpper().Contains(upper))
				{
					processlist.SelectedIndex = i;
					processlist.ScrollIntoView(processlist.SelectedItem);
					break;
				}
			}
		}

		private void searchprocs_click(object sender, System.Windows.Input.MouseEventArgs e)
		{
			searchprocs.Clear();
			processlist.SelectedIndex = -1;
		}

		private void resetsearch_btn_Click(object sender, RoutedEventArgs e)
		{
			searchprocs.Text = "Search";
			processlist.SelectedIndex = -1;
			processlist.ScrollIntoView(processlist.Items[0]);
		}

		private void killProcess_Click(object sender, RoutedEventArgs e)
		{
			procs[processlist.SelectedIndex].Kill();
			searchprocs.Text = "Search";
			processlist.SelectedIndex = -1;
			processlist.ScrollIntoView(processlist.Items[0]);
		}

		private void lock_btn_Click(object sender, RoutedEventArgs e)
		{
			NativeMethods.LockWorkStation();
		}
		#endregion

		#region Location Change
		private void main_LocationChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.MainStartLocation = "Manual";
		}
		#endregion

		#region Settings Window
		private void settings_btn_Click(object sender, RoutedEventArgs e)
		{
			settings.Owner = this;

			if (!settings.IsVisible)
				settings.Show();
			else
				settings.Activate();
		}

		public void disownSettings()
		{
			settings.Owner = null;
			this.RemoveVisualChild(settings);
		}
		#endregion

		#region Overlay
		private void toggleOverlay_Checked(object sender, RoutedEventArgs e)
		{
			overlay.Show();
		}

		private void toggleOverlay_Unchecked(object sender, RoutedEventArgs e)
		{
			overlay.Hide();
		}
		#endregion

		#region Fullscreen
		private void main_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (main.WindowStyle != WindowStyle.None)
			{
				main.WindowStyle = WindowStyle.None;
				main.WindowState = WindowState.Maximized;
			}
			else if (main.WindowStyle == WindowStyle.None)
			{
				main.WindowStyle = WindowStyle.SingleBorderWindow;
				main.WindowState = WindowState.Normal;
				main.Left = Properties.Settings.Default.MainLeft;
				main.Top = Properties.Settings.Default.MainTop;
				main.Height = Properties.Settings.Default.MainHeight;
				main.Width = Properties.Settings.Default.MainWidth;
			}
		}
		#endregion
	}
}
