using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookScript : MonoBehaviour {

	public Text FriendsText;

	[Space(10)] 
	[Header("Posts en tablon")]
	public string promocionURL;
	public string textoTablon;
	public string textoTablon2;
	public string imagenUrl;

	[Space(10)] 
	[Header("Invitaciones personales")]
	public string appUrl;
	public string tituloInvitacion;
	public string textoInvitacion;



	//incializa facebook
	private void Awake()
	{
		if (!FB.IsInitialized)
		{
			FB.Init(() =>
				{
					if (FB.IsInitialized)
						FB.ActivateApp();
					else
						Debug.LogError("No se ha podido inicializar facebook");
				},
				isGameShown =>
				{
					if (!isGameShown)
						Time.timeScale = 0;
					else
						Time.timeScale = 1;
				});
		}
		else
			FB.ActivateApp();
	}


	//______________Acceder o cerral la cuenta de facebook_______________
	public void FacebookLogin()
	{
		var permissions = new List<string>() { "public_profile", "email", "user_friends" };
		FB.LogInWithReadPermissions(permissions);
	}

	public void FacebookLogout()
	{
		FB.LogOut();
	}

	//_____________________Compartir en facebook______________________
	public void FacebookShare()
	{
		FB.ShareLink(new System.Uri(promocionURL), textoTablon,
			textoTablon2,
			new System.Uri(imagenUrl));
	}

	//______________Invitaciones a probar una app en facebook_______________
	public void FacebookGameRequest()
	{
		FB.AppRequest(textoInvitacion, title: tituloInvitacion);
	}

	public void FacebookInvite()
	{
		FB.Mobile.AppInvite(new System.Uri(appUrl));
	}


	//______________________Amigos con esta aplicacion______________________
	public void GetFriendsPlayingThisGame()
	{
		string query = "/me/friends";
		FB.API(query, HttpMethod.GET, result =>
			{
				var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
				var friendsList = (List<object>)dictionary["data"];
				FriendsText.text = string.Empty;
				foreach (var dict in friendsList)
					FriendsText.text += ((Dictionary<string, object>)dict)["name"];
			});
	}

	//______________________Comprueba si esta logeado en facebook______________________
	public bool FB_Loged()
	{
		if (FB.IsLoggedIn) {
			return true;
		} else {
			return false;
		}
	}
}