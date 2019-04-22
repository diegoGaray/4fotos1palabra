using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdColony;
using GoogleMobileAds;

public class AdColonyManager : MonoBehaviour {

	public string AppID;
	public string IdDeZona;

	AdColony.InterstitialAd publicidadAdcolony = null;

	bool rewardCooldown = false;

	void Start () 
	{
		RellenarIDsSiVacias ();
		ConfigurarPublicidad ();
		RegisterForAdsCallbacks ();
		SolicitarAdcolonyVideoAd ();
	}

	//Se encarga de controlar si se ven los anuncios publicitarios
	void Update ()
	{
		if (publicidadAdcolony.Expired) 
		{
			recompensarTrasVideo ();
		}
	}

	//Crea los parametros basicos de la publicidad
	void ConfigurarPublicidad()
	{
		AdColony.AppOptions opcionesApp = new AdColony.AppOptions ();
		opcionesApp.UserId = AppID;
		opcionesApp.AdOrientation = AdColony.AdOrientationType.AdColonyOrientationPortrait;

		AdColony.Ads.Configure (AppID, opcionesApp, IdDeZona);
	}

	//Registra la recepcion de eventos
	void RegisterForAdsCallbacks() 
	{
		AdColony.Ads.OnRequestInterstitial += (AdColony.InterstitialAd ad) => {
			publicidadAdcolony = ad;
		};

		AdColony.Ads.OnExpiring += (AdColony.InterstitialAd ad) => {
			AdColony.Ads.RequestInterstitialAd(IdDeZona, null);
		};

		AdColony.Ads.OnRewardGranted += (string zoneId, bool success, string name, int amount) => {
			recompensarTrasVideo();
		};
	}

	//Funcion a la que se llama para cargar en buffer un video de AdColony. 
	public void SolicitarAdcolonyVideoAd()
	{
		AdColony.Ads.RequestInterstitialAd(IdDeZona, null);
	}

	//Funcion a la que se llama para mostrar un video de AdColony. 
	public void MostrarAdcolonyVideoAd() 
	{
		if (publicidadAdcolony != null) 
		{
			AdColony.Ads.ShowAd (publicidadAdcolony);
		}
		else 
		{
			SolicitarAdcolonyVideoAd ();
		}
	}

	//Funcion a la que se llama para recompensar al usuario tras ver el video
	void recompensarTrasVideo()
	{
		SumarRecompensas ();

		//Volver a cargar en buffer otro anuncio
		SolicitarAdcolonyVideoAd();
	}

	void SumarRecompensas()
	{
		if(!rewardCooldown) //Comprueba que no haya recibido otra recompensa en los ultimos dos segundos. 
		{
			rewardCooldown = true;

			//aqui lo que se quiera recompensar. (la linea de abajo es solo un ejemplo)
			gameObject.GetComponent<Coins> ().Recompensar (5);

			StartCoroutine(corrutinaDeEspera()); //marca un limite de recompensas
		}
	}

	//Marca un limite de una recompensa por cada dos segundo
	IEnumerator corrutinaDeEspera()
	{
		yield return  new WaitForSeconds(2);     //Espera para poder optar a otra recompensa
		rewardCooldown = false;
	}


	//_________________________________Añadiendo IDs si quedan vacias_______________________________________________________________
	//Solo si las IDs de alguna de las publicidades esta vacia, se usaran las escritas en GestorDeContenidoPublicitario.cs como copia de
	//respaldo para rellenar aquella ID que pueda haber quedado sin completar. Si se desea desactivar esta funcion, quitar la llamada a esta
	//funcion dentro del start de este scipt. 
	public void RellenarIDsSiVacias()
	{
		if (AppID == "") {AppID = GestorDeContenidoPublicitario.AdcolonnyAppID;}
		if (IdDeZona == "") {AppID = GestorDeContenidoPublicitario.AdcolonnyIdDeZona;}
	}
		
}
