using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ComandosBasicos : MonoBehaviour {

	public void CarregaCena(string nome){
		SceneManager.LoadScene (nome, LoadSceneMode.Single);
	}

	public void Quit(){
		  Application.Quit();
	}
}
