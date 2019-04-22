using UnityEngine;

public class AdsControl : MonoBehaviour {


	GameObject bannerTopMenu;
	GameObject bannerBottomGame;

	void Awake () 
	{
		bannerTopMenu = GameObject.Find("Main Camera").transform.Find("ADS-BANNER-TOP").gameObject;
		bannerBottomGame = GameObject.Find("Main Camera").transform.Find("ADS-BANNER-BOTTOM").gameObject;

		if (GameObject.Find("Main Camera").transform.position.x == -32)
		{
			bannerBottomGame.SetActive (false);
			bannerTopMenu.SetActive (true);
		} 
		else
		{
			bannerBottomGame.SetActive (true);
			bannerTopMenu.SetActive (false);
		}
	}

}
