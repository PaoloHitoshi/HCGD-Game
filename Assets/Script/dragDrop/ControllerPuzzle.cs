using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ControllerPuzzle : MonoBehaviour {

	public bool isCompleted;
	private static int pieces;
	private int totalPieces;
	public GameObject feedback;
	private GameResult performance;

	void Start () {
		pieces = 0;
		isCompleted = false;

        foreach (var component in Settings.puzzle.game.components)
        {
            foreach (var resource in component.resources)
            {
                if (resource.resourceType.name == "Imagem")
                {
                    totalPieces++;
                }
            }
        }

        performance = new GameResult();
		//set plataform id
		performance.game = 2;
		performance.hits = 0;
		performance.fails = 0;
	}
	
	public static void completePiece(){
		pieces++;
		
	}

	void Update() {
		if(pieces/2 >= totalPieces && !isCompleted){
			showFeedback();
		}
	}

	void showFeedback(){
		isCompleted= true;
        //GameObject feedbackMenu = PrefabUtility.InstantiatePrefab(feedback) as GameObject;
        //feedbackMenu.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);

        feedback.SetActive(true);

    }
	
	public void setFeelingRate(int stars){
		performance.feeling_rate = stars;
		JsonUtils jsonUtils = (new GameObject("jsonUtils")).AddComponent<JsonUtils>();
		jsonUtils.sendResponse(performance);
	}
}
