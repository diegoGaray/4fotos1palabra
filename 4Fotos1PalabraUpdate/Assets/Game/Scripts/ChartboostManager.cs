using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;

public class ChartboostManager : MonoBehaviour {
	
	public string AppId; 
	public string AppSignature;

	void Start()
	{
		RellenarIDsSiVacias ();
		CargarInterstitial ();
	}

	void Update()
	{
		//MostrarInterstitial (); 
	}

	//Carga el anuncio en cache cuando se llama a esta funcion.
	public void CargarInterstitial ()
	{
		Chartboost.cacheInterstitial (CBLocation.Default);
	}

	//Muestra el anuncio cuando se llama a esta funcion
	public void MostrarInterstitial () 
	{
		
		if (Chartboost.hasInterstitial (CBLocation.Default)) 
		{
			Chartboost.showInterstitial(CBLocation.Default);
			//Chartboost.showInterstitial(CBLocation.HomeScreen);
		} 
		else 
		{
			CargarInterstitial ();
		}
	}


	//_________________________________Añadiendo IDs si quedan vacias_______________________________________________________________
	//Solo si las IDs de alguna de las publicidades esta vacia, se usaran las escritas en GestorDeContenidoPublicitario.cs como copia de
	//respaldo para rellenar aquella ID que pueda haber quedado sin completar. Si se desea desactivar esta funcion, quitar la llamada a esta
	//funcion dentro del start de este scipt. 
	public void RellenarIDsSiVacias()
	{
		if (AppId == "") {AppId = GestorDeContenidoPublicitario.chatboostAppId;}
		if (AppSignature == "") {AppSignature = GestorDeContenidoPublicitario.chatboostAppSignature;}
	}
	

}
