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
    public float range;
    public int combo;
    public Transform dancers;
    public Transform flutes;

    public ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<Music>();
    }

    // Update is called once per frame
    void Update() {
        //if (Input.inputString != "") Debug.Log(Input.inputString);
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) {
            hitNote();
        }
    }

    private void hitNote() {
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
            particles.Emit(5);
        }

        combo += 1;
        if (combo == 10) {
            foreach(Transform obj in dancers) {
                obj.GetComponent<Dancer>().active = true;
            }
        } else if (combo == 50) {
            foreach(Transform obj in flutes) {
                obj.GetComponent<Dancer>().active = true;
            }
        }
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
            combo = 0;
            foreach(Transform obj in dancers) {
                obj.GetComponent<Dancer>().active = false;
            }
            foreach(Transform obj in flutes) {
                obj.GetComponent<Dancer>().active = false;
            }
            score.text = combo.ToString();
            notes.Remove(other);
        }
    }
}
