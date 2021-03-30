using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Handler init process.
public class InitializeAdSystem : MonoBehaviour, IInitializeAdSystem
{
	private string bannerUnitId;
	private AdView view;

	private void Awake()
	{
		SetupEvents();
	}

	public void SetupEvents()
	{
		MoPubManager.OnSdkInitializedEvent += OnSdkInitializedEvent;
	}

	public void InitializeAds(string UnitId)
	{
		bannerUnitId = UnitId;
		view = GetComponent<AdView>();
		MoPub.InitializeSdk(SdkConfiguration);
	}

	public void OnSdkInitializedEvent(string adUnitId)
	{
		view.EndInitAdSystem();
	}

	public MoPub.SdkConfiguration SdkConfiguration
	{
		get
		{
			var config = new MoPub.SdkConfiguration
			{
				AdUnitId = bannerUnitId,
				MediatedNetworks = GetComponents<MoPubNetworkConfig>().Where(nc => nc.isActiveAndEnabled).Select(nc => nc.NetworkOptions).ToArray()
			};
			return config;
		}
	}
}
