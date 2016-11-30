using System;
using CoreAudioApi;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Information
{
	[BrowsableAttribute(true), ComVisible(true), DescriptionAttribute("Simplified Class for using CoreAudioAPI.dll.")]
	public sealed class SimplifiedAudioAPI
	{

		internal MMDevice deviceCapture;
		internal MMDevice deviceRender;
		internal MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
		public SimplifiedAudioAPI()
		{
			try
			{
				deviceCapture = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eMultimedia);
			}
			catch (COMException)
			{
				deviceCapture = null;
			}
			deviceRender = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
		}
		[BrowsableAttribute(true), DescriptionAttribute("Audio Channels.")]
		public enum AudioChannels : int
		{
			GET_IN_LeftPeak = 1,
			GET_IN_RightPeak = 2,
			GET_IN_MasterPeak = 3,
			GET_OUT_LeftPeak = 4,
			GET_OUT_RightPeak = 5,
			GET_OUT_MasterPeak = 6
		}
		[BrowsableAttribute(true), DescriptionAttribute("Get Output Value of selected Audio Channel.")]
		public float GetPeakValue(AudioChannels AudioChannel)
		{
			float value = 0;
			switch (AudioChannel)
			{
				case AudioChannels.GET_IN_LeftPeak:
					if (deviceCapture == null)
						value = 0;
					else
						value = deviceCapture.AudioMeterInformation.PeakValues[0] * 100;
					break;
				case AudioChannels.GET_IN_RightPeak:
					if (deviceCapture == null)
						value = 0;
					else
						try
						{
							value = deviceCapture.AudioMeterInformation.PeakValues[1] * 100;
						}
						catch (IndexOutOfRangeException)
						{
							value = deviceCapture.AudioMeterInformation.PeakValues[0] * 100;
						}
					break;
				case AudioChannels.GET_IN_MasterPeak:
					if (deviceCapture == null)
						value = 0;
					else
						value = deviceCapture.AudioMeterInformation.MasterPeakValue * 100;
					break;
				case AudioChannels.GET_OUT_LeftPeak:
					value = deviceRender.AudioMeterInformation.PeakValues[0] * 100;
					break;
				case AudioChannels.GET_OUT_RightPeak:
					value = deviceRender.AudioMeterInformation.PeakValues[1] * 100;
					break;
				case AudioChannels.GET_OUT_MasterPeak:
					value = deviceRender.AudioMeterInformation.MasterPeakValue * 100;
					break;
				default:
					break;
			}
			return value;
		}
		public float IN_MasterScalar
		{
			get
			{
				if (deviceCapture == null)
					return 0;
				else
					return deviceCapture.AudioEndpointVolume.MasterVolumeLevelScalar;
			}
			set
			{
				if (deviceCapture == null)
				{
					float temp = value;
				}
				else
					deviceCapture.AudioEndpointVolume.MasterVolumeLevelScalar = value;
			}
		}
		public float OUT_MasterScalar
		{
			get { return deviceRender.AudioEndpointVolume.MasterVolumeLevelScalar; }
			set { deviceRender.AudioEndpointVolume.MasterVolumeLevelScalar = value; }
		}
		public bool IN_MUTE
		{
			get
			{
				if (deviceCapture == null)
					return false;
				else
					return deviceCapture.AudioEndpointVolume.Mute;
			}
			set
			{
				if (deviceCapture == null)
				{
					bool temp = value;
				}
				else
					deviceCapture.AudioEndpointVolume.Mute = value;
			}
		}
		public bool OUT_MUTE
		{
			get { return deviceRender.AudioEndpointVolume.Mute; }
			set { deviceRender.AudioEndpointVolume.Mute = value; }
		}
	}
}
