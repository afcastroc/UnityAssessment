using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedAd : MonoBehaviour, IContentAd
{
	#region Variables
	private string unitId;
	private AdView view;
	private int times = 0;
	#endregion

	#region Setup
	private void Awake()
	{
		view = GetComponent<AdView>();
		SetupEvents();
	}

	public void SetupEvents()
	{
		MoPubManager.OnRewardedVideoLoadedEvent += OnRewardedVideoLoadedEvent;
		MoPubManager.OnRewardedVideoFailedEvent += OnRewardedVideoFailedEvent;
		MoPubManager.OnRewardedVideoShownEvent += OnRewardedVideoShownEvent;
	}
	#endregion

	#region AdMethods
	public void LoadAd(string _unitId)
	{
		unitId = _unitId;
		string[] _rewardedAdUnits = { unitId };
		MoPub.LoadRewardedVideoPluginsForAdUnits(_rewardedAdUnits);
		MoPub.RequestRewardedVideo(
						adUnitId: unitId, keywords: "rewarded, video, mopub",
						latitude: 37.7833, longitude: 122.4167, customerId: "customer101");
	}

	public void ShowAd()
	{
		MoPub.ShowRewardedVideo(unitId, GetCustomData(unitId));
	}

	private static string GetCustomData(string customDataFieldValue)
	{
		return customDataFieldValue != "example" ? customDataFieldValue : null;
	}
	#endregion

	#region Events

	private void OnRewardedVideoFailedEvent(string arg1, string arg2)
	{
		Debug.Log("Failed Rewarded Load: " + arg1 + " - " + arg2);
	}

	private void OnRewardedVideoLoadedEvent(string obj)
	{
		Debug.Log("Rewarded Added: " + obj);
		view.RewardedLoaded();
	}

	private void OnRewardedVideoShownEvent(string obj)
	{
		times++;
		view.UpdateRewardedCounter(times);
	}

	#endregion
}
