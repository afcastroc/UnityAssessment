using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoPubInternal;
using System.Linq;
using System;

public class BasicAdsCode : MonoBehaviour
{
	public string bannerUnitId;
	public string intersticialUnitID;
	public string fullscreenUnitID;
	public string rewardedUnitID;

	private bool initializedSystem = false;
	private bool bannerLoaded = false;
	private bool interstitialLoaded = false;
	private bool rewardedLoaded = false;

#if UNITY_ANDROID || UNITY_EDITOR
	private readonly string[] _bannerAdUnits = { "f73fe7d673f44bdaaf89d89982ea3bd7" };
	private readonly string[] _interstitialAdUnits = { "bcd6fcd21a10411b89e117764786bbba" };
	private readonly string[] _rewardedAdUnits = { "7b650db066b14c89a893d399bc900a55" };
#endif

	private void Awake()
	{
		SetupEvents();
	}

	private void SetupEvents()
	{
		MoPubManager.OnSdkInitializedEvent += OnSdkInitializedEvent;
		MoPubManager.OnAdLoadedEvent += OnAdLoadedEvent;
		MoPubManager.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
		MoPubManager.OnRewardedVideoLoadedEvent += OnRewardedVideoLoadedEvent;
	}

	public void InitializeAds()
	{
		MoPub.InitializeSdk(SdkConfiguration);
	}

	public void LoadAdBanner()
	{
		if (initializedSystem)
		{
			MoPub.LoadBannerPluginsForAdUnits(_bannerAdUnits);
		}
	}

	public void LoadAdInterstitial()
	{
		if (initializedSystem)
		{
			MoPub.LoadInterstitialPluginsForAdUnits(_interstitialAdUnits);
		}
	}

	public void LoadRewarded()
	{
		if (initializedSystem)
		{
			MoPub.LoadRewardedVideoPluginsForAdUnits(_rewardedAdUnits);
			MoPub.RequestRewardedVideo(
							adUnitId: rewardedUnitID, keywords: "rewarded, video, mopub",
							latitude: 37.7833, longitude: 122.4167, customerId: "customer101");
		}
	}

	public void ShowAdBanner()
	{
		MoPub.RequestBanner(bannerUnitId, MoPub.AdPosition.BottomCenter, MoPub.MaxAdSize.Width336Height280);
		if (bannerLoaded) MoPub.ShowBanner(bannerUnitId, true);
	}

	public void ShowAdInterstitial()
	{
		MoPub.RequestInterstitialAd(intersticialUnitID);
		if(interstitialLoaded) MoPub.ShowInterstitialAd(intersticialUnitID);
	}

	public void ShowRewarded()
	{
		if(rewardedLoaded) MoPub.ShowRewardedVideo(rewardedUnitID, GetCustomData(rewardedUnitID));
	}

	private void OnSdkInitializedEvent(string adUnitId)
	{
		initializedSystem = true;
	}

	public MoPub.SdkConfiguration SdkConfiguration
	{
		get
		{
			var config = new MoPub.SdkConfiguration
			{
				AdUnitId = bannerUnitId,
				//AllowLegitimateInterest = AllowLegitimateInterest,
				//LogLevel = LogLevel,
				MediatedNetworks = GetComponents<MoPubNetworkConfig>().Where(nc => nc.isActiveAndEnabled).Select(nc => nc.NetworkOptions).ToArray()
			};
			//SendMessage("OnSdkConfiguration", config, SendMessageOptions.DontRequireReceiver);
			return config;
		}
	}

	private void OnAdLoadedEvent(string adUnitId, float height)
	{
		Debug.Log("AdLoadEvent(" + adUnitId + "," + height + ")");
		bannerLoaded = true;
	}

	private void OnInterstitialLoadedEvent(string adUnitId)
	{
		Debug.Log("Intersticial Added");
		interstitialLoaded = true;
	}

	private void OnRewardedVideoLoadedEvent(string obj)
	{
		Debug.Log("Rewarded Added: " + obj);
		rewardedLoaded = true;
	}

	private static string GetCustomData(string customDataFieldValue)
	{
		return customDataFieldValue != "example" ? customDataFieldValue : null;
	}
}
