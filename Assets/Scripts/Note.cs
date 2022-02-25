using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject hitter;
    public float speed;
    public GameObject destroyer;
    public bool hit = false;
    private Music audioManager;

    private float progress = 0;
    void Start()
    {
        audioManager = FindObjectOfType<Music>();
    }
    void FixedUpdate()
    {
        if (hit && progress <= 100) {
            Vector2 newPos = Vector2.Lerp(hitter.transform.position, destroyer.transform.position, progress/100);
            gameObject.transform.position = new Vector3(newPos.x, newPos.y+getArc(progress/100),gameObject.transform.position.z);
            progress += 10;
        }
        else if (!hit) {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - speed, gameObject.transform.position.y, gameObject.transform.position.z);
        } else {
            Destroy(gameObject);
        }
    }

    private float getArc(float _progress) {
        return (-5*Mathf.Pow(_progress, 2) + 5*_progress);
    }

}
