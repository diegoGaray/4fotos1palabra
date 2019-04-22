using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversorCredencialesAcceso : MonoBehaviour {

	string emailDeUsuario;

	public string emailAConvertir;
	public string emailConvertido;
	public bool esCorrecto = false;
	public bool esInCorrecto = false;

	void Start () {
		ConvertirCadena (emailAConvertir);
	}


	public void ConvertirCadena (string email) 
	{
		emailDeUsuario = email.ToLower ();
		if (emailDeUsuario.Contains ("@")) 
		{
			esCorrecto = true;
			emailConvertido = emailDeUsuario;
		} 
		else 
		{
			esInCorrecto = true;
		}
	}
}
