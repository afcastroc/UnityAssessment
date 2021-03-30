using UnityEngine;
using TMPro;

//This is the main Class that handle Ad process
public class AdView : MonoBehaviour
{
	#region variables

	[Header("Unit IDs Setup")]
	[SerializeField] private string bannerUnitId;
	[SerializeField] private string intersticialUnitID;
	[SerializeField] private string rewardedUnitID;

	[Header("LoadButtons")]
	[SerializeField] private Transform loadBannerBtn;
	[SerializeField] private Transform loadInterstitialBtn;
	[SerializeField] private Transform loadRewardedBtn;

	[Header("ShowButtons")]
	[SerializeField] private Transform showBannerBtn;
	[SerializeField] private Transform showInterstitialBtn;
	[SerializeField] private Transform showRewardedBtn;

	[Header("States & counters")]
	[SerializeField] private Transform interstitialState;
	[SerializeField] private Transform rewardedcounter;

	private InitializeAdSystem system;
	private BannerAd bannerAd;
	private InterstitialAd interstitialAd;
	private RewardedAd rewardedAd;

	#endregion

	#region InitSystem

	private void Start()
	{
		system = GetComponent<InitializeAdSystem>();
		bannerAd = GetComponent<BannerAd>();
		interstitialAd = GetComponent<InterstitialAd>();
		rewardedAd = GetComponent<RewardedAd>();
	}

	public void StartSystem()
	{
		system.InitializeAds(bannerUnitId);
	}

	public void EndInitAdSystem()
	{
		loadBannerBtn.gameObject.SetActive(true);
	}

	#endregion

	#region Banners

	public void LoadBanner()
	{
		bannerAd.LoadAd(bannerUnitId);
	}

	public void BannerLoaded()
	{
		showBannerBtn.gameObject.SetActive(true);
	}

	public void ShowBanner()
	{
		bannerAd.ShowAd();
		showBannerBtn.gameObject.SetActive(false);
		loadRewardedBtn.gameObject.SetActive(true);
	}

	#endregion

	#region Interstitials

	public void LoadInterstitial()
	{
		interstitialAd.LoadAd(intersticialUnitID);
	}

	public void InterstitialLoaded()
	{
		showInterstitialBtn.gameObject.SetActive(true);
	}

	public void ShowInterstitial()
	{
		interstitialAd.ShowAd();
	}

	public void ShowInterstitialState(string state)
	{
		interstitialState.GetComponent<TextMeshProUGUI>().text = "Interstitial was " + state;
		interstitialState.gameObject.SetActive(true);
	}

	#endregion

	#region RewardedAds

	public void LoadRewardedAd()
	{
		rewardedAd.LoadAd(rewardedUnitID);
	}

	public void RewardedLoaded()
	{
		showRewardedBtn.gameObject.SetActive(true);
	}

	public void ShowRewardedAd()
	{
		rewardedAd.ShowAd();
		showRewardedBtn.gameObject.SetActive(false);
	}

	public void UpdateRewardedCounter(int times)
	{
		rewardedcounter.GetComponent<TextMeshProUGUI>().text = "videos completed: " + times.ToString();
		rewardedcounter.gameObject.SetActive(true);
		loadInterstitialBtn.gameObject.SetActive(true);
	}

	#endregion
}
