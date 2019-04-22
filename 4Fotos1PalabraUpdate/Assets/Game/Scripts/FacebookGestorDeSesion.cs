using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookGestorDeSesion : MonoBehaviour {

	public bool isloged;
	public GameObject panelDeBienvenida;
	public GameObject fbLogoutButton;

	void Start () 
	{
		isloged = false;
	}
	

	void Update () 
	{
		
		if (isloged) 
		{
			panelDeBienvenida.gameObject.SetActive (true);
			fbLogoutButton.gameObject.SetActive (true);
		} 
		else 
		{
			isloged = gameObject.GetComponent<FacebookScript> ().FB_Loged ();
			fbLogoutButton.gameObject.SetActive (false);
		}
	}

	public void SaliendoDeFacebook()
	{
		isloged = false;
		panelDeBienvenida.gameObject.SetActive (false);
		fbLogoutButton.gameObject.SetActive (false);
	}

	public void CompartirPublicacion()
	{
		isloged = gameObject.GetComponent<FacebookScript> ().FB_Loged ();
		if (isloged) 
		{
			gameObject.GetComponent<FacebookScript> ().FacebookShare ();
		} 
		else 
		{
			gameObject.GetComponent<FacebookScript> ().FacebookLogin ();
		}
	}
}
