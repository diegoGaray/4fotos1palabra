using UnityEngine;
using System.Collections;

public class FixAds : MonoBehaviour {

	public IEnumerator WaitToEnableVideo ()
	{
		yield return new WaitForSeconds(0.2f);
		GameObject.Find ("GAME").transform.Find("WatchVideoButton").GetComponent<BoxCollider2D>().enabled = true;
	}	
}
