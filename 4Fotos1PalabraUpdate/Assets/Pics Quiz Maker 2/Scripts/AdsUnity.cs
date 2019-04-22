using UnityEngine;
using UnityEngine.Advertisements;
using CodeStage.AntiCheat.ObscuredTypes;

public class AdsUnity : MonoBehaviour
{

	public string unityGameID;
	GameObject cameraObj;
	public string vungleAndroidAppID;
	public string adColonyAppID;
	public string adColonyZoneID;
	public int coinsEarnedByVideo;

	void Awake () 
	{

		cameraObj = GameObject.Find("Main Camera");

		Advertisement.Initialize (unityGameID, false);

		//Vungle.init(vungleAndroidAppID, "");

		//Vungle.onAdViewedEvent += onAdViewedEvent;
		Initialize();

	}


	public void Initialize()
	{


		// Assign any AdColony Delegates before calling Configure
		//AdColony.OnVideoFinished = this.OnVideoFinished;
		
		// If you wish to use a the customID feature, you should call  that now.
		// Then, configure AdColony:
		//AdColony.Configure
		/*(
				"version:1.0,store:google", // Arbitrary app version and Android app store declaration.
				adColonyAppID,   // ADC App ID from adcolony.com
				adColonyZoneID // A zone ID from adcolony.com

		);*/

	}

	
	private void OnVideoFinished(bool ad_was_shown)
	{

		ObscuredPrefs.SetInt ("coinsPlayer", (ObscuredPrefs.GetInt ("coinsPlayer") + coinsEarnedByVideo));
		Game_Controller.addCoinsLog = true;
		
		if (ObscuredPrefs.GetInt("sounds") == 1) 
		{
			cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().buyHelp);
		}
		
		GameObject.Find ("MENU").transform.Find ("CoinsInGame-Menu").transform.Find("TotalCoinsPlayerMenu").GetComponent<TextMesh>().text =  ObscuredPrefs.GetInt ("coinsPlayer").ToString();
		GameObject.Find ("GAME").transform.Find ("CoinsInGame").transform.Find("TotalCoinsPlayer").GetComponent<TextMesh>().text =  ObscuredPrefs.GetInt ("coinsPlayer").ToString();


	}


	

	void Update () {
	
		if (Input.GetMouseButtonUp (0))
		{
			
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
			
			if (hit.collider != null) 
			{
				
				GameObject objectHit = hit.collider.gameObject;
				
				if(objectHit.name.Contains("WatchVideoButton")) 
				{
					Debug.Log ("muestra ad");
					/*if (AdColony.IsVideoAvailable(adColonyZoneID)){
						
						//AdColony.ShowVideoAd(adColonyZoneID); 
					}
					else */if(Advertisement.IsReady()) 
					{
						Advertisement.Show (null, new ShowOptions 
						{
							resultCallback = result	=> 
							{
							
								ObscuredPrefs.SetInt ("coinsPlayer", (ObscuredPrefs.GetInt ("coinsPlayer") + coinsEarnedByVideo));
								Game_Controller.addCoinsLog = true;

									if (ObscuredPrefs.GetInt("sounds") == 1) 
									{
										cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().buyHelp);
									}

								GameObject.Find ("MENU").transform.Find ("CoinsInGame-Menu").transform.Find("TotalCoinsPlayerMenu").GetComponent<TextMesh>().text =  ObscuredPrefs.GetInt ("coinsPlayer").ToString();
								GameObject.Find ("GAME").transform.Find ("CoinsInGame").transform.Find("TotalCoinsPlayer").GetComponent<TextMesh>().text =  ObscuredPrefs.GetInt ("coinsPlayer").ToString();
								
							}
						});

					} /*else if (Vungle.isAdvertAvailable()){

						Vungle.playAd();

					} */

				}

			}
		}


		/*if (Advertisement.IsReady() || Vungle.isAdvertAvailable() || AdColony.IsVideoAvailable(adColonyZoneID)) 
		{

			GameObject.Find("MENU").transform.Find("WatchVideoButton").gameObject.SetActive(true);
			GameObject.Find("GAME").transform.Find("WatchVideoButton").gameObject.SetActive(true);
			if(GameObject.Find("WatchVideoButton2") != null){
				GameObject.Find("WatchVideoButton2").gameObject.SetActive(true);
			}

		} 
		else 
		{
			
			GameObject.Find("MENU").transform.Find("WatchVideoButton").gameObject.SetActive(false);
			GameObject.Find("GAME").transform.Find("WatchVideoButton").gameObject.SetActive(false);
			if(GameObject.Find("WatchVideoButton2") != null){
				GameObject.Find("WatchVideoButton2").gameObject.SetActive(false);
			}

		}*/

	}

	void onAdViewedEvent(double watched, double length)
	{

		ObscuredPrefs.SetInt ("coinsPlayer", (ObscuredPrefs.GetInt ("coinsPlayer") + coinsEarnedByVideo));
		Game_Controller.addCoinsLog = true;
		
		if (ObscuredPrefs.GetInt("sounds") == 1) 
		{
			cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().buyHelp);
		}
		
		GameObject.Find ("MENU").transform.Find ("CoinsInGame-Menu").transform.Find("TotalCoinsPlayerMenu").GetComponent<TextMesh>().text =  ObscuredPrefs.GetInt ("coinsPlayer").ToString();
		GameObject.Find ("GAME").transform.Find ("CoinsInGame").transform.Find("TotalCoinsPlayer").GetComponent<TextMesh>().text =  ObscuredPrefs.GetInt ("coinsPlayer").ToString();


	}
}
