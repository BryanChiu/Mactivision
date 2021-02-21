using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    float velocity;                             // just x velocity because y doesn't change
    float minPos = -5.5f;                       // the minimum value for position (left)
    float maxPos = 5.5f;                        // the maximum value for position (right)

    // Initializes the spotlight
    public void Init(float v)
    {
        velocity = v;
    }

    public void Move(bool right)
    {
        if (right && gameObject.transform.position.x <= maxPos) 
            gameObject.transform.Translate(Vector3.right*velocity*Time.deltaTime);

        else if (gameObject.transform.position.x >= minPos) 
            gameObject.transform.Translate(Vector3.left*velocity*Time.deltaTime);
    }

    public Vector2 GetPosition()
    {
        Vector2 pos = gameObject.transform.position;
        return pos;
    }
}
