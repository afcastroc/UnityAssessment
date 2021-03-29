using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInitializeAdSystem
{
	void SetupEvents();
	void InitializeAds(string unitId);
	void OnSdkInitializedEvent(string adUnitId);
}
