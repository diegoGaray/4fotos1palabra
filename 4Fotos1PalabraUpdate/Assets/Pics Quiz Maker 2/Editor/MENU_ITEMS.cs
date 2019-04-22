﻿/// <summary>
///
///----------- ESPAÑOL -----------
/// 
/// Este script controla los botones del menu "Tools/Tools of my saved data".
/// 
///----------- ENGLISH -----------
/// 
/// This script controls the menu buttons "Tools/Tools of my saved data".
/// 
/// </summary>

using UnityEngine;
using UnityEditor;
using CodeStage.AntiCheat.ObscuredTypes;

public class MENU_ITEMS
{

	[MenuItem("Tools/Tools of my saved data/Add me 500 coins")]
	private static void Add5000coins()
	{
		if (ObscuredPrefs.HasKey ("coinsPlayer")){

			ObscuredPrefs.SetInt ("coinsPlayer", ObscuredPrefs.GetInt ("coinsPlayer") + 500);

		} else {

			ObscuredPrefs.SetInt ("coinsPlayer", 500);

		}

		Debug.Log ("5000 coins added. (JUST FOR YOUR PC)");

	}

	[MenuItem("Tools/Tools of my saved data/Clear my coins")]
	private static void ClearMyCoins()
	{
		if (ObscuredPrefs.HasKey ("coinsPlayer")){

			ObscuredPrefs.DeleteKey("coinsPlayer");

		}

		Debug.Log ("Your coins were deleted. (You will start with the coins that you put in the configurations) (JUST FOR YOUR PC)");

	}

	[MenuItem("Tools/Tools of my saved data/Clear my completed levels")]
	private static void ClearMyLevelsCompleted()
	{
		if (ObscuredPrefs.HasKey ("levelWord")){
			ObscuredPrefs.DeleteKey("levelWord");
			ObscuredPrefs.DeleteKey("levelPlayer");
		}
		Debug.Log ("Your levels completed were deleted. (JUST FOR YOUR PC)");
	}

	[MenuItem("Tools/Tools of my saved data/Put the game as the first opening")]
	private static void ClearAllData()
	{
		ObscuredPrefs.DeleteAll();
		Debug.Log ("All data deleted. (JUST FOR YOUR PC)");
	}


}