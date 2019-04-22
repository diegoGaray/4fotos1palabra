using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

	//Script muy sencillo usado solo para probar el funcionamiento. en el GameObjectCoins hay que ponerle un objeto del tipo UI.Text y simplemente 
	//simulara monetizacion añadiendo puntos  cada vez que se llame a su funcion publica Recompensar(int) 
	//No tiene ningun uso en un juego final y debera ser sustituido por el scipt que controle la monetizacion real. 


	//objeto del tipo UI.Text
	public GameObject coins;
	//cantidad de monedas obtenidas (No se guarda tras cerrar la app)
	int coinNumber;

	void Start () 
	{
		coinNumber = 0;
	}

	//Actualiza las monedas en la interfaz
	void Update () 
	{
		coins.gameObject.GetComponent<Text> ().text = coinNumber.ToString();
	}

	//Funcion a la que puede llamar cualquier objeto para aumentar el numero de monedas en X cantidad
	public void Recompensar(int cantidad)
	{
		coinNumber += cantidad;
	}
}
