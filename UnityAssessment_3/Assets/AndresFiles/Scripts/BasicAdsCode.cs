using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoPubInternal;

public class BasicAdsCode : MonoBehaviour
{
	public string AdUnitId;

	private void Start()
	{
		MoPub.InitializeSdk(SdkConfiguration);
		//MoPub.ReportApplicationOpen(itunesAppId);
		//MoPub.EnableLocationSupport(LocationAware);
	}

	public MoPub.SdkConfiguration SdkConfiguration
	{
		get
		{
			var config = new MoPub.SdkConfiguration
			{
				AdUnitId = AdUnitId,
				//AllowLegitimateInterest = AllowLegitimateInterest,
				//LogLevel = LogLevel,
				//MediatedNetworks = GetComponents<MoPubNetworkConfig>().Where(nc => nc.isActiveAndEnabled).Select(nc => nc.NetworkOptions).ToArray()
			};
			SendMessage("OnSdkConfiguration", config, SendMessageOptions.DontRequireReceiver);
			return config;
		}
	}
}
