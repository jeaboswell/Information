#region Using Includes
using System;
using System.Diagnostics;
using System.Windows.Threading;
#endregion

namespace Information
{
	/// <summary>
	/// Interaction logic for Overlay.xaml
	/// </summary>
	public partial class Overlay : IDisposable
	{
		#region Variables
		PerformanceCounter ramUsage = new PerformanceCounter("Memory", "% Committed Bytes In Use");

		bool disposed = false;

		public static string BaseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
		#endregion

		#region Constructor
		public Overlay()
		{
			InitializeComponent();
			//
			// Timer 1
			//
			DispatcherTimer seconds = new System.Windows.Threading.DispatcherTimer();
			seconds.Tick += timer_Tick;
			seconds.Interval = TimeSpan.FromMilliseconds(100);
			seconds.Start();
			time.Content = DateTime.Now.ToLongTimeString();
			date.Content = DateTime.Now.ToShortDateString();

			DispatcherTimer system = new DispatcherTimer();
			system.Tick += system_Tick;
			system.Interval = TimeSpan.FromMilliseconds(500);
			system.Start();

			this.Closed += Overlay_Closed;
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
				ramUsage.Dispose();
			}

			disposed = true;
		}

		protected void Overlay_Closed(object sender, EventArgs args)
		{
			Dispose();
		}
		#endregion

		#region Update Timers
		void timer_Tick(object sender, EventArgs e)
		{
			time.Content = DateTime.Now.ToLongTimeString();
			date.Content = DateTime.Now.ToShortDateString();
		}

		void system_Tick(object sender, EventArgs e)
		{
			memory.Content = Convert.ToInt32(ramUsage.NextValue()) + "%";
		}
		#endregion

		private void overlay_Deactivated(object sender, EventArgs e)
		{
			this.Activate();
			this.Topmost = true;
		}
	}
}
