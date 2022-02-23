using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancer : MonoBehaviour
{
    public bool active = false;

    public GameObject hiddenPoint;
    
    private float progress;
    private Vector3 initPos;

    void Start() {
        initPos = gameObject.transform.position;
        gameObject.transform.position = hiddenPoint.transform.position;
    }
    void Update()
    {
        if (active && progress < 100) {
            progress += 1;
            gameObject.transform.position = Vector2.Lerp(hiddenPoint.transform.position, initPos, progress/100);
        }
        else if (!active && progress > 0) {
            progress -= 1;
            gameObject.transform.position = Vector2.Lerp(hiddenPoint.transform.position, initPos, progress/100);
        }
    }
}
