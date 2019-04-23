using UnityEngine;
using System.Collections;

public class AdsIntersticialControl : MonoBehaviour {

	public string[] testDeviceIDs;
	public static int levelsPlayed = 0;
	static bool clicInPlay = false;
	public string idAd;

	void Awake () {

		DontDestroyOnLoad(this);
		
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}

	}

	void Start () {
	
		EasyGoogleMobileAds.GetInterstitialManager().SetTestDevices(true, testDeviceIDs);
		
		#if UNITY_ANDROID
		EasyGoogleMobileAds.GetInterstitialManager().PrepareInterstitial(idAd);
		#elif UNITY_IOS
		EasyGoogleMobileAds.GetInterstitialManager().PrepareInterstitial(idAd);
		#endif


	}

	public static void ShowInterstitialAd () {

		if (levelsPlayed == 2)
		{
			EasyGoogleMobileAds.GetInterstitialManager().ShowInterstitial();
			levelsPlayed = 0;
		}

	}

	void Update () 
	{
		if (Input.GetMouseButtonUp (0))
		{
			
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
			
			if (hit.collider != null) 
			{
				
				GameObject objectHit = hit.collider.gameObject;

				if (objectHit.name == "CircleLevel" || objectHit.name == "buttonPlay")
				{
					if (!clicInPlay)
					{
						clicInPlay = true;
						EasyGoogleMobileAds.GetInterstitialManager().ShowInterstitial();

					}
					
				}
			}
		}
	}
	

}
