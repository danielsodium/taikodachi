using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextBounce : MonoBehaviour
{
    // Start is called before the first frame update
    private float initY;
    void Start()
    {
        initY = gameObject.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localScale.y != initY) {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, Mathf.Lerp(gameObject.transform.localScale.y, initY, 0.1f));
        }
    }
}
