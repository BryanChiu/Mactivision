using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class animates the background to have a parallax effect
public class BackgroundParallax : MonoBehaviour
{
    Vector3 startPos;
    Vector3 parentStartPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.localPosition;
        parentStartPos = gameObject.transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        float parentYDelta = parentStartPos.y - gameObject.transform.parent.position.y;
        gameObject.transform.localPosition = new Vector3(startPos.x, startPos.y+(parentYDelta*0.9f), startPos.z);
    }
}
