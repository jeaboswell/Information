#region Using Includes
using System.Runtime.InteropServices;
#endregion

namespace Information
{
	class NativeMethods
	{
		#region Lock Computer
		[DllImport("user32")]
		public static extern void LockWorkStation();
		#endregion
	}
}
