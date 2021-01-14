using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class animates the background to have a parallax effect
public class BackgroundParallax : MonoBehaviour
{
    Vector3 current;

    // Start is called before the first frame update
    void Start()
    {
        current = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float parentYDelta = gameObject.transform.parent.position.y;
        gameObject.transform.localPosition = new Vector3(current.x, current.y+(parentYDelta*-0.8f), current.z);
    }
}
