using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Networking;

public class Spawner : MonoBehaviour {

	public GameObject prefab;
	public GameObject feedback;
	private GameObject[] collectables;

	private float timer;
	private float respawnTime;

	private void createInstance(int index, int score){
		//TODO find another way to do this
		GameObject newPrefab = Instantiate(prefab) as GameObject;
		newPrefab.GetComponent<Collectable>().score = score;
		collectables[index] = newPrefab;
	}

	private IEnumerator loadImage(int index, string url)
	{
		using (UnityWebRequest textureWebRequest = UnityWebRequestTexture.GetTexture(url))
		{
			yield return textureWebRequest.SendWebRequest();

			if (textureWebRequest.isNetworkError || textureWebRequest.isHttpError)
			{
				Debug.Log(textureWebRequest.error);
			}
			else
			{
                // Get downloaded asset bundle
                Texture2D texture = DownloadHandlerTexture.GetContent(textureWebRequest);
				//www.LoadImageIntoTexture(texture);
				Rect rec = new Rect(0, 0, texture.width, texture.height);
				Sprite spriteToUse = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 10);
				collectables[index].GetComponent<SpriteRenderer>().sprite = spriteToUse;
				Debug.Log("Resource added"+ collectables[index].GetComponent<Collectable>().score);
			}
		}
		
		// Create a texture in DXT1 format
		//Texture2D texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.DXT1, false);
		// assign the downloaded image to sprite
	}

	private IEnumerator loadSound(int index, string url){
		WWW www = new WWW(url);
		yield return www;

		AudioClip wwwsound =  www.GetAudioClip(true,true);
		collectables[index].GetComponent<Collectable>().sound = wwwsound;
		Debug.Log("Sound added");
		www.Dispose ();
		www = null;
	}

	private void loadFeedback(string text){
		feedback.GetComponentInChildren<Text>().text = text;
	}

	 void Awake(){
		Component[] componets = Settings.plataform.components;
		collectables = new GameObject[componets.Length];
		int index = 0;
		foreach(Component component in componets){
			Debug.Log("Loading component: " + component.name);
			foreach(ComponentField field in component.fields){
				createInstance(index, component.tag == "coletavel" ? 10 : 0);
                switch (field.type){
					case "Texto": // Text
						loadFeedback("Muito Bem!");
						break;
					case "Imagem": // Image
						StartCoroutine(loadImage(index, field.resource.content));
						break;
					case "Audio": // Sound
                        StartCoroutine(loadSound(index, field.resource.content));
						break;
					default:
						Debug.Log("Resource type not defined:" + field.type);
						break;
				}
			}
			index++;
		}
	 }
	 
	 void Start() {
		 respawnTime = 5 ;
         timer = Time.time + 3;
 	}

	void FixedUpdate () {	
		if(ControllerPlataform.isCompleted) return;
		if(timer < Time.time){
			 //avoid null pointer
			 if( collectables != null && collectables.Length > 0){
				 int rand = Random.Range(0, collectables.Length);
				 //TODO fix the map size
			 	 Instantiate (collectables[rand],
				 new Vector3(Random.Range(-100, 100), Random.Range(-100, 100),0),
				 new Quaternion(0, 0 , 0, 0));
			 }
			 timer = Time.time + respawnTime;
		}
	}
}