using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Android;

public class AdmobManager : MonoBehaviour {

	//Estas variables se rellenan desde el editor. Ponenen todas las IDs de publicidad y aplicacion. Y se puede seleccionar si se quiere que funcione en Android 
	//O IOs. Se podria controlar automaticamente, pero si no se tienen las librerias de IOs da error al compilar. De esta forma se puede elgir desde el editor con 
	//la misma facilidad. Si se quedan vacias ("") el programa recurrira a las que hay por defecto en el GestorDeContenidoPublicitario.cs
	public enum posibleOS { Android, Ios};
	public posibleOS OS;

	public string appID; 
	[Space(10)] 
	[Header("Android")]

	#region Android
	public string AndroidBannerID; 
	public string AndroidInterstitialID; 
	public string AndroidVideoID; 
	#endregion


	[Space(10)] 
	[Header("Ios")]
	#region IOS
	public string IosBannerID; 
	public string IosInterstitialID; 
	public string IosVideoID; 
	#endregion

	//Variables internas que gestion la ID de la publicidad.
	string ID_Banner;
	string ID_Interstitial;
	string ID_Video;

	//Estos son los diferentes tipos de publicidad mostrados. Se pueden añadir mas banners por ejemplo para mostrar varios simultaneos. 
	private BannerView BannerPublicidad;
	private RewardBasedVideoAd VideoPublicidad;
	private InterstitialAd InterstitialPublicidad;

	//Inicializa los valores de la publicidad segun si se esta ejecutando en android o IOs
	void Awake () 
	{
		RellenarIDsSiVacias ();

		MobileAds.Initialize (appID);

		if (OS == posibleOS.Android) 
		{
			ID_Banner = AndroidBannerID;
			ID_Interstitial = AndroidInterstitialID;
			ID_Video = AndroidVideoID;
		}
		else if(OS == posibleOS.Ios)
		{
			ID_Banner = IosBannerID;
			ID_Interstitial = IosInterstitialID;
			ID_Video = IosVideoID;
		}
	}

	//genera los receptores de eventos para el cierre de la publicidad
	//tambien inicia cargando en buffer tanto un Interstitial como un video. 
	void Start()
	{
		VideoPublicidad = RewardBasedVideoAd.Instance;
		VideoPublicidad.OnAdRewarded += HandleOnAdRewarded;

		MostrarBanner (BannerPublicidad,AdPosition.Bottom);
		CargarInterstitial ();	
		CargarVideoAnuncio();
	}

	//_____________________________________Intersitial ADs___________________________________________________________
	//Muestra anuncios a pantalla completa. Al igual que el video, debe de cargarse en buffer primero para mejorar
	//la experiencia de usuario, ya que si no se dispone de buena conexion a internet podria tardar más de lo esperado
	//Al cerrarse un add Interstitial se genera otro automaticamente. 

	public void MostrarInterstitial()
	{
		if (InterstitialPublicidad.IsLoaded ()) 
		{
			InterstitialPublicidad.Show ();
		}
	}

	void CargarInterstitial()
	{
		InterstitialPublicidad = new InterstitialAd (ID_Interstitial);
		AdRequest iRequest = new AdRequest.Builder().Build();
		InterstitialPublicidad.LoadAd (iRequest);
	}


	void InterstitialPublicidad1_OnAdClosed (object sender, System.EventArgs e)
	{
		CargarInterstitial();
	}
	//___________________________________BANNER ADs_____________________________________________________________
	//Muestra un banner al ser llamada la funcion. Se le debe pasar el banner y la posicion en pantalla en la
	//que se quiere que aparezca el banner. De esta forma con solo esta funcion se pueden cargar tantos 
	//banners simultaneos como se quiera. La segunda funcion se asegura de eliminar el banner si se cierra la app

	public void MostrarBanner(BannerView miBanner, AdPosition posicionDelBanner)
	{
		miBanner = new BannerView (ID_Banner, AdSize.Banner, posicionDelBanner);
		AdRequest BannerRequest = new AdRequest.Builder ().Build ();
		miBanner.LoadAd (BannerRequest);
	}


	private void OnDisable ()
	{
		BannerPublicidad.Destroy ();
	}


	//_________________________________VIDEO ADs_______________________________________________________________
	//Todo lo relacionado con los rewardedVideos. Necesita dos funciones, ambas son publicas para poder
	//acceder a ellas desde otros scipts, aun que no deberia ser necesario ya que todo se autogestiona en este 
	//script. Una carga el video en buffer (puede tardar varios segundos en hacerse, depende enteramente de 
	//la conexion a internet). Y la otra funcion lanza el anuncio si esta cargado. En caso de que no lo este
	//llama de nuevo a cargar el video. 

	public void CargarVideoAnuncio()
	{
		VideoPublicidad.LoadAd (new AdRequest.Builder ().Build (), ID_Video);
	}

	public void MostrarVideoAnuncio()
	{
		if (VideoPublicidad.IsLoaded ()) 
		{
			VideoPublicidad.Show ();
		}
		else 
		{
			Debug.Log ("El anuncio no esta listo aun");
			CargarVideoAnuncio ();
		}
	}

	//_________________________________Recompensas_______________________________________________________________
	//Aqui es donde se debe llamar a la funcion que recompense al jugador por haber visto un rewardedVideo. 
	//Es buena idea tambien empezar a cargar el siguiente video en buffer por si el jugador quiere ver oto. 
	//Pero no se puede cargar uno mientras ya haya uno cargado o justo acabe de terminar, por ello uso una
	//Corrutina que no carga el nuevo video hasta despues de las recompensas. 

	public void HandleOnAdRewarded(object sender, Reward args)
	{
		gameObject.GetComponent<Coins> ().Recompensar (5);
		StartCoroutine(corrutinaDeEspera());
	}

	IEnumerator corrutinaDeEspera()
	{
		yield return  new WaitForSeconds(1);     //Espera para cargar un nuevo anuncio
		CargarVideoAnuncio ();
	}



	//_________________________________Añadiendo IDs si quedan vacias_______________________________________________________________
	//Solo si las IDs de alguna de las publicidades esta vacia, se usaran las escritas en GestorDeContenidoPublicitario.cs como copia de
	//respaldo para rellenar aquella ID que pueda haber quedado sin completar. Si se desea desactivar esta funcion, quitar la llamada a esta
	//funcion dentro del awake de este scipt. 

	public void RellenarIDsSiVacias()
	{
		if (appID == "") { appID = GestorDeContenidoPublicitario.AdmobAppID;}
		if (OS == posibleOS.Android) 
		{
			if (ID_Banner == "") { appID = GestorDeContenidoPublicitario.AdmobAndroidBannerID;}
			if (ID_Interstitial == "") { appID = GestorDeContenidoPublicitario.AdmobAndroidInterstitialID;}
			if (ID_Video == "") { appID = GestorDeContenidoPublicitario.AdmobAndroidVideoID;}
		}
		else if(OS == posibleOS.Ios)
		{
			if (ID_Banner == "") { appID = GestorDeContenidoPublicitario.AdmobIosBannerID;}
			if (ID_Interstitial == "") { appID = GestorDeContenidoPublicitario.AdmobIosInterstitialID;}
			if (ID_Video == "") { appID = GestorDeContenidoPublicitario.AdmobIosVideoID;}
		}
	}
}
