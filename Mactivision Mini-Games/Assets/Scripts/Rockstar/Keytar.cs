using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the keytar
public class Keytar : MonoBehaviour
{
    public Transform rockstar;
    Vector3 posOffset;
    float rot;

    // Start is called before the first frame update
    void Start()
    {
        posOffset = rockstar.position - transform.position;
        rot = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        // the keytar follows the rockstar (matches position)
        transform.position = rockstar.position - posOffset;
        // the keytar tilts (rotates) in the direction the rockstar is moving
        float rVel = Mathf.Clamp(rockstar.gameObject.GetComponent<Rockstar>().currVelocity, -1f, 1f);
        transform.eulerAngles = new Vector3(0f, 0f, rot - rVel*10);
    }
}
