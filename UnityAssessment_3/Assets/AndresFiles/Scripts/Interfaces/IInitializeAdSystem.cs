using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Methods required for intialize ADs system
interface IInitializeAdSystem
{
	void SetupEvents();
	void InitializeAds(string unitId);
	void OnSdkInitializedEvent(string adUnitId);
}
