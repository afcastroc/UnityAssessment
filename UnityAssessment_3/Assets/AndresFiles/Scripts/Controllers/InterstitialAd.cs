using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialAd : MonoBehaviour, IContentAd
{
	#region Variables
	private string unitId;
	private AdView view;

	enum state
	{
		shown,
		dismiss
	}
	#endregion

	#region Setup
	private void Awake()
	{
		view = GetComponent<AdView>();
		SetupEvents();
	}

	public void SetupEvents()
	{
		MoPubManager.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
		MoPubManager.OnInterstitialFailedEvent += OnInterstitialFailedEvent;
		MoPubManager.OnInterstitialDismissedEvent += OnInterstitialDismissedEvent;
		MoPubManager.OnInterstitialShownEvent += OnInterstitialShownEvent;
		MoPubManager.OnInterstitialExpiredEvent += OnInterstitialExpiredEvent;
	}
	#endregion

	#region AdMethods
	public void LoadAd(string _unitId)
	{
		unitId = _unitId;
		string[] _interstitialAdUnits = { _unitId };
		MoPub.LoadInterstitialPluginsForAdUnits(_interstitialAdUnits);
		MoPub.RequestInterstitialAd(_unitId);
	}

	public void ShowAd()
	{
		MoPub.ShowInterstitialAd(unitId);
	}
	#endregion

	#region Events

	private void OnInterstitialFailedEvent(string arg1, string arg2)
	{
		Debug.Log("Failed Instertitial Load: " + arg1 + " - " + arg2);
	}

	private void OnInterstitialLoadedEvent(string obj)
	{
		Debug.Log("Interstitial added " + obj);
		view.InterstitialLoaded();
	}

	private void OnInterstitialShownEvent(string obj)
	{
		Debug.Log("Interstitial was showned: " + obj);
		view.ShowInterstitialState(state.shown.ToString());
	}

	private void OnInterstitialDismissedEvent(string obj)
	{
		Debug.Log("Interstitial was dismissed: " + obj);
		view.ShowInterstitialState(state.dismiss.ToString());
	}

	private void OnInterstitialExpiredEvent(string obj)
	{
		Debug.Log("Interstitial was Expired: " + obj);
	}

	#endregion
}
