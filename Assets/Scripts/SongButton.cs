using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class SongButton : MonoBehaviour
{
    public Dictionary<string, string> data;
    
	void Start () {
		Button btn = gameObject.transform.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
        LoadMaps.currentSongData = data;
        StartCoroutine(GetAudioClip());
		Debug.Log ("You have clicked the button!");
	}
    IEnumerator GetAudioClip()
    {
        string clip = Path.Combine(Directory.GetParent(data["fullPath"]).FullName, data["AudioFilename"]);
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(clip, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                LoadMaps.musicClip = DownloadHandlerAudioClip.GetContent(www);
                SceneManager.LoadScene (sceneName:"Taiko");
            }
        }
    }
}
