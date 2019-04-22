using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Clase Statica que vale de Back Up de los datos publicitarios en caso de borrar alguno por accidente. 
//Solo se utilizaran estos datos si alguna de las IDs quedan vacias en su script original. 
public static class GestorDeContenidoPublicitario {

	#region Admob
	//__________________ADMOB__________________
	public static string AdmobAppID = "ca-app-pub-8807477565969136~6584885268"; 

	public static  string AdmobAndroidBannerID = "ca-app-pub-3940256099942544/6300978111"; 
	public static  string AdmobAndroidInterstitialID = "ca-app-pub-3940256099942544/1033173712"; 
	public static  string AdmobAndroidVideoID = "ca-app-pub-3940256099942544/5224354917"; 

	public static  string AdmobIosBannerID = "ca-app-pub-3940256099942544/6300978111"; 
	public static  string AdmobIosInterstitialID = "ca-app-pub-3940256099942544/1033173712"; 
	public static  string AdmobIosVideoID = "ca-app-pub-3940256099942544/5224354917"; 
	#endregion


	#region AdColony
	//__________________ADCOLONY__________________
	public static string AdcolonnyAppID = "app965462477c644b02a5";
	public static string AdcolonnyIdDeZona = "vz88d6ea303ba44efea5";
	#endregion


	#region Chartboost
	//__________________CHARTBOOST__________________
	public static string chatboostAppId = "5beac3dad8e02e0adb45b66d"; 
	public static string chatboostAppSignature = "958a31d3b1dc4bb602902c9868ad551886cab13";
	#endregion  


	#region OneSignal
	//__________________OneSignal__________________
	public static string OneSignalAppId = "9fd580f6-a0ec-47c6-acbd-64cfcbec5abd"; 
	#endregion 

}
