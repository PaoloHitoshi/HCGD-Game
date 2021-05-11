using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerPuzzle : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefabTarget;
    private GameObject[] objects;

    //private void createInstance(int index){
    //	//TODO find another way to do this
    //	GameObject newPrefab = Instantiate(prefab) as GameObject;
    //       objects[index] = newPrefab;
    //}

    private IEnumerator loadImage(int index, string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            // Create a texture in DXT1 format
            Texture2D texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.DXT1, false);
            // assign the downloaded image to sprite
            www.LoadImageIntoTexture(texture);
            Rect rec = new Rect(0, 0, texture.width, texture.height);
            Sprite spriteToUse = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 1);

            //Where player need to drop image
            GameObject newPrefabTarget = Instantiate(prefabTarget) as GameObject;
            newPrefabTarget.transform.position = new Vector2(Random.Range(-440, 416), Random.Range(-253, -148));
            Debug.Log("XY");
            Debug.Log(newPrefabTarget.transform.position.x);
            Debug.Log(newPrefabTarget.transform.position.y);
            newPrefabTarget.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            newPrefabTarget.GetComponent<Image>().sprite = spriteToUse;
            newPrefabTarget.name = "target_obj" + index.ToString();

            //Initial image (draggable)
            //GameObject newPrefab = Instantiate(prefab) as GameObject;
            GameObject newPrefab = Instantiate(prefab) as GameObject;
            newPrefab.transform.position = new Vector2(Random.Range(-425, Random.Range(10, 30) + (index * (texture.width / 2))), 243);
            newPrefab.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            newPrefab.GetComponent<Image>().sprite = spriteToUse;
            newPrefab.name = "obj" + index.ToString();
            objects[index] = newPrefab;
        }
    }

    private IEnumerator loadSound(int index, string url)
    {
        WWW www = new WWW(url);
        yield return www;

        AudioClip wwwsound = www.GetAudioClip(true, true);
        objects[index].GetComponent<Collectable>().sound = wwwsound;
        Debug.Log("Sound added");
        www.Dispose();
        www = null;
    }

    void Start()
    {
        // Loads component resources
    }

}
