using UnityEngine;
using System.Collections;

public class AdsInterstitialControl : MonoBehaviour
{

	string[] testDeviceIds = new string[] {""};
	public static int levelsPlayed = 1;
//	static bool clicInPlay = false;
	public string idAdInterstitialAndroid;
    static int staticShowInterstitialEach;
    [SerializeField] int showInterstitialEach = 2;
	#if UNITY_IOS
	string idAdInterstitialIOS = "";
	#endif
	void Awake ()
    {

        staticShowInterstitialEach = showInterstitialEach;

        DontDestroyOnLoad(this);
		
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}

	}

	void Start ()
    {
	
		//EasyGoogleMobileAds.GetInterstitialManager().SetTestDevices(true, testDeviceIds);
		
		#if UNITY_ANDROID
		//EasyGoogleMobileAds.GetInterstitialManager().PrepareInterstitial(idAdInterstitialAndroid);
		#elif UNITY_IOS
	
		EasyGoogleMobileAds.GetInterstitialManager().PrepareInterstitial(idAdInterstitialIOS);
		#endif


	}

	public static void ShowInterstitialAd ()
    {

		if (levelsPlayed == staticShowInterstitialEach)
		{
			//EasyGoogleMobileAds.GetInterstitialManager().ShowInterstitial();
			levelsPlayed = 0;
		}

	}

}
