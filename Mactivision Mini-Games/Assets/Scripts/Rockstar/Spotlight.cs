using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the spotlight
public class Spotlight : MonoBehaviour
{
    float velocity;         // just x velocity because y doesn't change
    float minPos = -3.9f;   // the minimum value for position (left)
    float maxPos = 3.9f;    // the maximum value for position (right)

    // Initializes the spotlight
    public void Init(float v)
    {
        velocity = v;
    }

    // Move the spotlight left and right
    // Parameter `right` is true to move right, false to move left
    public void Move(bool right)
    {
        if (right && gameObject.transform.position.x <= maxPos) 
            gameObject.transform.Translate(Vector3.right*velocity*Time.deltaTime);

        else if (!right && gameObject.transform.position.x >= minPos) 
            gameObject.transform.Translate(Vector3.left*velocity*Time.deltaTime);
    }

    // Returns the spotlight's position
    public Vector2 GetPosition()
    {
        Vector2 pos = gameObject.transform.position;
        return pos;
    }
}
