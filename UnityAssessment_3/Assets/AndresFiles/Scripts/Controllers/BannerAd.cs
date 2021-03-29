using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerAd : MonoBehaviour, IContentAd
{
	private string unitId;
	private AdView view;

	private void Awake()
	{
		view = GetComponent<AdView>();
		SetupEvents();
	}

	public void SetupEvents()
	{
		MoPubManager.OnAdLoadedEvent += OnAdLoadedEvent;
		MoPubManager.OnAdFailedEvent += OnAdFailedEvent;
		MoPubManager.OnAdCollapsedEvent += OnAdCollapsedEvent;
	}

	public void LoadAd(string _unitId)
	{
		unitId = _unitId;
		string[] _bannerAdUnits = { unitId };
		MoPub.LoadBannerPluginsForAdUnits(_bannerAdUnits);
		MoPub.RequestBanner(unitId, MoPub.AdPosition.BottomCenter, MoPub.MaxAdSize.Width336Height280);
	}

	public void ShowAd()
	{
		MoPub.ShowBanner(unitId, true);
	}

	#region Events

	private void OnAdFailedEvent(string arg1, string arg2)
	{
		Debug.LogError("Failed loading ads: " + arg1 + " - " + arg2);
	}

	private void OnAdLoadedEvent(string arg1, float arg2)
	{
		Debug.Log("Success loading Ad banner");
		MoPub.ShowBanner(unitId, false);
		view.BannerLoaded();
	}

	private void OnAdCollapsedEvent(string obj)
	{
		Debug.Log("Success Collapsed Ad banner");
	}

	#endregion
}
