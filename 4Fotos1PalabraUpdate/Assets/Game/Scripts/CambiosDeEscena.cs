using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiosDeEscena : MonoBehaviour {

	public void CargarEscena (string nombre, bool aditive)
	{
		if (aditive) {
			SceneManager.LoadScene (nombre, LoadSceneMode.Additive);
		} else {
			SceneManager.LoadScene (nombre, LoadSceneMode.Single);
		}
	}

	public void CargarMenu ()
	{
		SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
	}
}
