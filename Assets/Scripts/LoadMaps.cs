using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using TMPro;


public class LoadMaps : MonoBehaviour
{
    // Start is called before the first frame update
    private string mapsFolder;
    public string mapPath;

    public GameObject mapButton;

    public GameObject canvas;
    public static AudioClip musicClip;

    public static Dictionary<string, string> currentSongData;

    public List<Dictionary<string, string>> maps = new List<Dictionary<string, string>>();
    void Start() {
        mapsFolder = Path.Combine(Application.persistentDataPath, "maps");
        FindMaps();
    }

    // Update is called once per frame
    void Update() {
        
    }

    

    public void FindMaps() {
        if (!Directory.Exists(mapsFolder)) {
		    Directory.CreateDirectory(mapsFolder);
        }
        string[] songs = Directory.GetDirectories(mapsFolder);
        foreach (string song in songs) {
            string[] levels = Directory.GetFiles(song, "*.osu");
            foreach (string level in levels) {
                // Go through each file and find information
                string[] fileLines = File.ReadAllLines(level);
                readOSU(level, fileLines);
            }

        }
        makeButtons();
        /*
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
        }*/
    }
    
    void readOSU(string level, string[] fileLines) {
        Dictionary<string, string> songData = new Dictionary<string, string>();
        songData.Add("fullPath", level);
        foreach (string line in fileLines) {
            if (line.Contains(":")) {
                string[] keyVal = line.Split(':');
                songData.Add(keyVal[0].Trim(), keyVal[1].Trim());
            } else if (line.Contains("[TimingPoints]") || line.Contains("[HitObjects]")) {
                break;
            }
        }
        maps.Add(songData);
    }
    
    void makeButtons() {
        float index = 0;
        foreach (Dictionary<string, string> song in maps) {
            Vector3 pos = new Vector3(canvas.transform.position.x, canvas.transform.position.y, canvas.transform.position.z);
            GameObject button = Instantiate(mapButton, pos, Quaternion.identity, canvas.transform);
            TextMeshProUGUI songTitle = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI songArtist = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            songTitle.text = song["Title"] + " - " + song["Version"];
            songArtist.text = song["Artist"];
            button.GetComponent<SongButton>().data = song;
            index++;
        }
    }

    
}
