#region Using Includes
using System.Windows;
using System.Windows.Media;
#endregion

namespace Information
{
	/// <summary>
	/// Interaction logic for ColorSelector.xaml
	/// </summary>
	public partial class ColorSelector : Window
	{
		#region Variable Declarations
		private const int GWL_STYLE = -16;
		private const int WS_SYSMENU = 0x80000;

		Brush selected = new SolidColorBrush();

		private int settingsSelection; // Represents which elemnt color is being changed
		#endregion

		#region Constructor
		public ColorSelector(int setBtn)
		{
			InitializeComponent();

			settingsSelection = setBtn;

			initialWindowSize();

			this.Loaded += new RoutedEventHandler(Window_Loaded);
		}
		#endregion

		#region Window Initialization
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
				selector.Width += widthscale;
			}
			else if (width < 1920)
			{
				selector.Width -= widthscale;
			}
			else if (width == 1920)
				return;

			if (height > 1080)
			{
				selector.Height += heightscale;
			}
			else if (height < 1080)
			{
				selector.Height -= heightscale;
			}
		}
		//
		// Set current fill of rectangle with current color of element
		//
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			switch (settingsSelection)
			{
				case 1:
					current.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(Properties.Settings.Default.TextColor);
					break;
				case 2:
					current.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(Properties.Settings.Default.ButtonBackground);
					break;
				case 3:
					current.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(Properties.Settings.Default.MeterForeground);
					break;
				case 4:
					current.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(Properties.Settings.Default.MainBackground);
					break;
				case 5:
					current.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(Properties.Settings.Default.Meter_ListBackground);
					break;
				case 6:
					current.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(Properties.Settings.Default.OverlayColor);
					break;
			}
		}
		#endregion

		#region Color Buttons
		//
		// Row A
		//
		private void A1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = A1.Background;
		}

		private void A2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = A2.Background;
		}

		private void A3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = A3.Background;
		}

		private void A4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = A4.Background;
		}

		private void A5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = A5.Background;
		}

		private void A6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = A6.Background;
		}

		private void A7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = A7.Background;
		}
		//
		// Row B
		//
		private void B1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = B1.Background;
		}

		private void B2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = B2.Background;
		}

		private void B3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = B3.Background;
		}

		private void B4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = B4.Background;
		}

		private void B5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = B5.Background;
		}

		private void B6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = B6.Background;
		}

		private void B7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = B7.Background;
		}

		private void B8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = B8.Background;
		}
		//
		// Row C
		//
		private void C1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = C1.Background;
		}

		private void C2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = C2.Background;
		}

		private void C3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = C3.Background;
		}

		private void C4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = C4.Background;
		}

		private void C5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = C5.Background;
		}

		private void C6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = C6.Background;
		}

		private void C7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = C7.Background;
		}

		private void C8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = C8.Background;
		}

		private void C9_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = C9.Background;
		}
		//
		// Row D
		//
		private void D1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = D1.Background;
		}

		private void D2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = D2.Background;
		}

		private void D3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = D3.Background;
		}

		private void D4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = D4.Background;
		}

		private void D5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = D5.Background;
		}

		private void D6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = D6.Background;
		}

		private void D7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = D7.Background;
		}

		private void D8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = D8.Background;
		}

		private void D9_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = D9.Background;
		}

		private void D10_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = D10.Background;
		}
		//
		// Row E
		//
		private void E1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E1.Background;
		}

		private void E2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E2.Background;
		}

		private void E3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E3.Background;
		}

		private void E4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E4.Background;
		}

		private void E5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E5.Background;
		}

		private void E6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E6.Background;
		}

		private void E7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E7.Background;
		}

		private void E8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E8.Background;
		}

		private void E9_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E9.Background;
		}

		private void E10_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E10.Background;
		}

		private void E11_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = E11.Background;
		}
		//
		// Row F
		//
		private void F1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F1.Background;
		}

		private void F2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F2.Background;
		}

		private void F3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F3.Background;
		}

		private void F4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F4.Background;
		}

		private void F5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F5.Background;
		}

		private void F6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F6.Background;
		}

		private void F7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F7.Background;
		}

		private void F8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F8.Background;
		}

		private void F9_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F9.Background;
		}

		private void F10_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F10.Background;
		}

		private void F11_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F11.Background;
		}

		private void F12_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = F12.Background;
		}
		//
		// Row G
		//
		private void G1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G1.Background;
		}

		private void G2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G2.Background;
		}

		private void G3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G3.Background;
		}

		private void G4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G4.Background;
		}

		private void G5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G5.Background;
		}

		private void G6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G6.Background;
		}

		private void G7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G7.Background;
		}

		private void G8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G8.Background;
		}

		private void G9_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G9.Background;
		}

		private void G10_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G10.Background;
		}

		private void G11_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G11.Background;
		}

		private void G12_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G12.Background;
		}

		private void G13_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = G13.Background;
		}
		//
		// Row H
		//
		private void H1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H1.Background;
		}

		private void H2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H2.Background;
		}

		private void H3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H3.Background;
		}

		private void H4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H4.Background;
		}

		private void H5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H5.Background;
		}

		private void H6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H6.Background;
		}

		private void H7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H7.Background;
		}

		private void H8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H8.Background;
		}

		private void H9_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H9.Background;
		}

		private void H10_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H10.Background;
		}

		private void H11_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H11.Background;
		}

		private void H12_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = H12.Background;
		}
		//
		// Row I
		private void I1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I1.Background;
		}

		private void I2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I2.Background;
		}

		private void I3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I3.Background;
		}

		private void I4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I4.Background;
		}

		private void I5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I5.Background;
		}

		private void I6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I6.Background;
		}

		private void I7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I7.Background;
		}

		private void I8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I8.Background;
		}

		private void I9_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I9.Background;
		}

		private void I10_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I10.Background;
		}

		private void I11_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = I11.Background;
		}
		//
		// Row J
		//
		private void J1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = J1.Background;
		}

		private void J2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = J2.Background;
		}

		private void J3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = J3.Background;
		}

		private void J4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = J4.Background;
		}

		private void J5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = J5.Background;
		}

		private void J6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = J6.Background;
		}

		private void J7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = J7.Background;
		}

		private void J8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = J8.Background;
		}

		private void J9_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = J9.Background;
		}

		private void J10_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = J10.Background;
		}
		//
		// Row K
		//
		private void K1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = K1.Background;
		}

		private void K2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = K2.Background;
		}

		private void K3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = K3.Background;
		}

		private void K4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = K4.Background;
		}

		private void K5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = K5.Background;
		}

		private void K6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = K6.Background;
		}

		private void K7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = K7.Background;
		}

		private void K8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = K8.Background;
		}

		private void K9_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = K9.Background;
		}
		//
		// Row L
		//
		private void L1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = L1.Background;
		}

		private void L2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = L2.Background;
		}

		private void L3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = L3.Background;
		}

		private void L4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = L4.Background;
		}

		private void L5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = L5.Background;
		}

		private void L6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = L6.Background;
		}

		private void L7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = L7.Background;
		}

		private void L8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = L8.Background;
		}
		//
		// Row M
		//
		private void M1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = M1.Background;
		}

		private void M2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = M2.Background;
		}

		private void M3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = M3.Background;
		}

		private void M4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = M4.Background;
		}

		private void M5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = M5.Background;
		}

		private void M6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = M6.Background;
		}

		private void M7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = M7.Background;
		}
		//
		// Row N
		//
		private void N1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = N1.Background;
		}

		private void N2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = N2.Background;
		}

		private void N3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = N3.Background;
		}

		private void N4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = N4.Background;
		}

		private void N5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = N5.Background;
		}

		private void N6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = N6.Background;
		}

		private void N7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = N7.Background;
		}

		private void N8_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = N8.Background;
		}
		//
		// Row O
		//
		private void O1_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = O1.Background;
		}

		private void O2_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = O2.Background;
		}

		private void O3_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = O3.Background;
		}

		private void O4_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = O4.Background;
		}

		private void O5_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = O5.Background;
		}

		private void O6_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = O6.Background;
		}

		private void O7_Click(object sender, RoutedEventArgs e)
		{
			current.Fill = selected = O7.Background;
		}
		#endregion

		#region Apply/Cancel Buttons
		//
		// When apply is clicked, implement color change
		//
		public void apply_Click(object sender, RoutedEventArgs e)
		{
			switch (settingsSelection)
			{
				case 1:
					Properties.Settings.Default.TextColor = current.Fill.ToString();
					break;
				case 2:
					Properties.Settings.Default.ButtonBackground = current.Fill.ToString();
					break;
				case 3:
					Properties.Settings.Default.MeterForeground = current.Fill.ToString();
					break;
				case 4:
					Properties.Settings.Default.MainBackground = current.Fill.ToString();
					break;
				case 5:
					Properties.Settings.Default.Meter_ListBackground = current.Fill.ToString();
					break;
				case 6:
					Properties.Settings.Default.OverlayColor = current.Fill.ToString();
					break;
			}
			this.Close();
		}
		//
		// When cancel is clicked, close color selector without saving changes
		//
		private void cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		#endregion
	}
}
