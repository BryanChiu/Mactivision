using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for randomly moving the rockstar
public class Rockstar : MonoBehaviour
{
    public Animator rockstar;   // rockstar gameobject, used to animate red guy

    System.Random randomSeed;   // seed of the current game
    float changeFreq;           // how often the destination changes
    float velocity;             // just x velocity because y doesn't change
    float destination;          // rockstar's next destination position

    float minPos = -3.9f;       // the minimum value for position (left)
    float maxPos = 3.9f;        // the maximum value for position (right)

    Vector3 startingPos;
    public float currVelocity { private set; get; }

    // Initializes the rockstar with the seed
    public void Init(string seed, float cf, float v)
    {
        randomSeed = new System.Random(seed.GetHashCode());
        changeFreq = cf;
        velocity = v;
        destination = 0f;
        startingPos = gameObject.transform.position;
        currVelocity = 0f;
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
        float newVelocity = Mathf.Approximately(gameObject.transform.position.x, destination) ? 0f : 
                    (gameObject.transform.position.x < destination ? velocity : -velocity);

        // flip the sprite so it's facing the way it's moving
        rockstar.SetFloat("Velocity", Mathf.Abs(newVelocity));
        if (currVelocity != newVelocity && newVelocity != 0f) transform.localScale = newVelocity>0 ? Vector3.one*1.5f : Vector3.Reflect(Vector3.one, Vector3.right)*1.5f;
        currVelocity = newVelocity;
    }

    // Updates the destination the rockstar moves towards. Can be forced to update
    void UpdateDestination(bool force)
    {
        // only update if forced, or randomly based on `changeFreq`
        if (force || randomSeed.NextDouble() < Time.deltaTime/changeFreq) {
            destination = (float)randomSeed.NextDouble()*(maxPos-minPos) + minPos;
        }
    }

    // Returns the position of the rockstar
    public Vector2 GetPosition()
    {
        Vector2 pos = gameObject.transform.position;
        return pos;
    }
}
