using System.Collections;
using UnityEngine.Networking;
using System.IO;
using UnityEngine;

public class LoadMaps : MonoBehaviour
{
    // Start is called before the first frame update
    private string mapsFolder;
    public string mapPath;
    public static AudioClip musicClip;
    public static string hitsPath;


    void Start() {
        mapsFolder = Path.Combine(Application.persistentDataPath, "maps");
        FindMaps();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void FindMaps() {
        
        string[] maps = Directory.GetDirectories(mapsFolder);
        foreach (string map in maps) {
            string[] files = Directory.GetFiles(map, "*.mp3");
            foreach (string file in files) {
                mapPath = "file:///" + file;
                StartCoroutine(GetAudioClip());
            }
            files = Directory.GetFiles(map, "*.osu");
            foreach (string file in files) {
                hitsPath = file;
            }
        }
    }
    IEnumerator GetAudioClip()
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(mapPath, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                musicClip = DownloadHandlerAudioClip.GetContent(www);
                Debug.Log("DONE");
            }
        }
    }
}
