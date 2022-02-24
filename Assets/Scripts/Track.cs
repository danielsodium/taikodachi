using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
public class Track : MonoBehaviour
{

    public GameObject hitter;
    public GameObject spawner;

    public TextAsset map;
    public GameObject hitNote;
    

    public float speed;

    public float startDelay;

    public Text score;
    private AudioSource track;
    private Music audioManager;

    private float timeSinceStart;
    private bool started;
    public float inputDelay;
    private List<int> hits = new List<int>();


    void Start() {
        audioManager = FindObjectOfType<Music>();
        ReadMap();
        CreateNotes();  
    }

    void FixedUpdate() {
        if (!started) {
            timeSinceStart += 20;
            if (timeSinceStart >= startDelay) {
                track = audioManager.Play("song");
                started = true;
            }
        } else if (!audioManager.isPlaying("song")) {
            SceneManager.LoadScene (sceneName:"Menu");
        }
    }

    
    

    void CreateNotes() {
        foreach(float hitTime in hits) {
            float dist = (speed/20)*(hitTime+inputDelay + startDelay);
            Vector3 pos = new Vector3(hitter.transform.position.x + dist, spawner.transform.position.y, spawner.transform.position.z);
            GameObject note = Instantiate(hitNote, pos, Quaternion.identity);
            Note script = note.GetComponent<Note>();
            script.hitter = hitter;
            script.speed = speed;
        }
    }
    void ReadMap() {
        
        string path = LoadMaps.hitsPath;
        string[] fileLines = File.ReadAllLines(path);
        bool times = false;
        foreach(string line in fileLines) {
            if (times && line != "") {
                string[] data = line.Split(',');
                hits.Add(int.Parse(data[2]));
            }
            else if (line.IndexOf("[HitObjects]") > -1) {
                times = true;
            }
            
        }

        /*
        string[] fileLines = map.text.Split('\n');
        bool times = false;
        foreach(string line in fileLines) {
            if (times && line != "") {
                string[] data = line.Split(',');
                hits.Add(int.Parse(data[2]));
            }
            else if (line.IndexOf("[HitObjects]") > -1) {
                times = true;
            }
            
        }*/
    }

}
