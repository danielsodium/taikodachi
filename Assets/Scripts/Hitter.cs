using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hitter : MonoBehaviour
{
    private Music audioManager;
    public List<Collider2D> notes = new List<Collider2D>();

    public GameObject destroyer;
    public TextMeshProUGUI score;
    public TextMeshProUGUI version;
    public TextMeshProUGUI songTitle;
    public float goodRange;
    public float range;
    public int combo;
    public Transform dancers;
    public Transform flutes;
    public GameObject hitSprite;

    public ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<Music>();
        songTitle.text = LoadMaps.currentSongData["Title"];
        version.text = LoadMaps.currentSongData["Version"];
    }

    // Update is called once per frame
    void Update() {
        //if (Input.inputString != "") Debug.Log(Input.inputString);
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) {
            hitNote();
        }

        if (hitSprite.transform.localScale.x < 1) {
            hitSprite.transform.localScale = Vector3.Lerp(hitSprite.transform.localScale, new Vector3(1, 1, hitSprite.transform.localScale.z), 0.05f);
        }

    }

    private void hitNote() {

        hitSprite.transform.localScale = new Vector3(0.8f, 0.8f, hitSprite.transform.localScale.z);

        if (notes.Count == 0) return;
        float dist = Mathf.Abs(notes[0].transform.position.x - gameObject.transform.position.x);
        GameObject toDestroy = notes[0].gameObject;
        Note noteToDestroy = notes[0].gameObject.GetComponent<Note>();
        noteToDestroy.hit = true;
        noteToDestroy.destroyer = destroyer;
        notes.RemoveAt(0);
        audioManager.Play("hitclap");
            

        // Doing this so it doesn't trigger onTriggerExit first
        //Destroy(toDestroy);

        if (dist < range) {
            if (dist < goodRange) particles.Emit(5);
            combo += 1;
            if (score.color == Color.red) score.color = Color.white;
            if (combo == 10) {
                foreach(Transform obj in dancers) {
                    obj.GetComponent<Dancer>().active = true;
                }
            } else if (combo == 50) {
                foreach(Transform obj in flutes) {
                    obj.GetComponent<Dancer>().active = true;
                }
            } else if (combo == 100) {
                score.color = Color.yellow;
            }
        }
        else {
            breakCombo();
        }

        
        score.gameObject.transform.localScale = new Vector3(score.gameObject.transform.localScale.x, 20);
        score.text = combo.ToString();

        // No idea why I don't have to remove the note, but if I do it returns an error as count is zero
        //Debug.Log(notes.Count);
        //notes.RemoveAt(0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        notes.Add(other);
        //Debug.Log(currentNote);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (notes.Contains(other)) {
            notes.Remove(other);
            breakCombo();
        }
    }

    private void breakCombo() {
            combo = 0;
            foreach(Transform obj in dancers) {
                obj.GetComponent<Dancer>().active = false;
            }
            foreach(Transform obj in flutes) {
                obj.GetComponent<Dancer>().active = false;
            }
            score.text = "0";
            score.color = Color.red;
    }
}
