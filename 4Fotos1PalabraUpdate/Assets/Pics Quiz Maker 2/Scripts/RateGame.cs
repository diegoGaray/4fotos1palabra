using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
public class RateGame : MonoBehaviour
{

	public void RateNow ()
    {

        ObscuredPrefs.SetInt("ratedGame", 1);
        //Application.OpenURL("market://details?id=" + Application.bundleIdentifier);
        closeRateWindow();
        ObscuredPrefs.SetInt("coinsPlayer", ObscuredPrefs.GetInt("coinsPlayer") + GameObject.Find("Game_Controller").GetComponent<Game_Controller>().howManyCoinsEarnByRate);
        GameObject.Find("Game_Controller").GetComponent<Game_Controller>().RefreshBoardCoins();
    }

    public void closeRateWindow()
    {
        Destroy(GameObject.Find("RATE_GAME_WINDOW(Clone)"));
    }

}
