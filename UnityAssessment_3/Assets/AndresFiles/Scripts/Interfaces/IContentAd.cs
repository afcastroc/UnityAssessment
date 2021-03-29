using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IContentAd
{
	void SetupEvents();
	void LoadAd(string unitId);
	void ShowAd();
}
