using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for randomly moving the rockstar
public class Rockstar : MonoBehaviour
{
    System.Random randomSeed;   // seed of the current game
    float changeFreq;           // how often the destination changes
    float velocity;             // just x velocity because y doesn't change
    float destination;          // rockstar's next destination position

    float minPos = -5.5f;       // the minimum value for position (left)
    float maxPos = 5.5f;        // the maximum value for position (right)

    Vector3 startingPos;

    // Initializes the rockstar with the seed
    public void Init(string seed, float cf, float v)
    {
        randomSeed = new System.Random(seed.GetHashCode());
        changeFreq = cf;
        velocity = v;
        destination = 0f;
        startingPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateDestination(false);
    }

    // Moves the position of the rockstar
    void Move()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                                                            new Vector3(destination, startingPos.y, startingPos.z),
                                                            velocity*Time.deltaTime);
    }

    // updates the destination the rockstar moves towards. Can be forced to update
    void UpdateDestination(bool force)
    {
        if (force || randomSeed.NextDouble() < Time.deltaTime/changeFreq) {
            destination = (float)randomSeed.NextDouble()*(maxPos-minPos) + minPos;
        }
    }

    public Vector2 GetPosition()
    {
        Vector2 pos = gameObject.transform.position;
        return pos;
    }
}
