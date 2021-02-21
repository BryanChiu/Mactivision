using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    System.Random randomSeed;   // seed of the current game
    float changeFreq;           // how often the velocity changes

    float minVel;                               // minimum velocity the meter drops
    float maxVel;                               // maximum velocity the meter drops
    float upVel;                                // velocity the meter is raised by player
    public float velocity { private set; get; } // just x velocity because y doesn't change

    float minLvl;                               // the minimum value for the meter
    float maxLvl;                               // the maximum value for the meter
    public float level { private set; get; }    // the meter's level

    //// TEMP SPRITE STUFF ////
    public GameObject firework;
    public Transform meterTop;
    public Transform meterBottom;
    public float meterLevel;
    ///////////////////////////

    // Initializes the meter with the seed
    public void Init(string seed, float cf, float minV, float maxV, float upV, float minL, float maxL, float l)
    {
        randomSeed = new System.Random(seed.GetHashCode());
        changeFreq = cf;

        minVel = minV;
        maxVel = maxV;
        upVel = upV;
        UpdateVelocity(true);

        minLvl = minL;
        maxLvl = maxL;
        level = l;
    }

    // Update is called once per frame
    void Update()
    {
        Drop();
        UpdateVelocity(false);

        //// TEMP SPRITE STUFF ////
        meterLevel = Mathf.Lerp(meterBottom.position.y, meterTop.position.y, level/100f);
        Vector3 currPos = gameObject.transform.position;
        currPos.y = meterLevel;
        gameObject.transform.position = currPos;
        ///////////////////////////
    }

    // updates the velocity the meter drops. Can be forced to update
    void UpdateVelocity(bool force)
    {
        if (force || randomSeed.NextDouble() < Time.deltaTime/changeFreq) {
            velocity = (float)randomSeed.NextDouble()*(maxVel-minVel) + minVel;
        }
    }

    void Drop()
    {
        level = Mathf.MoveTowards(level, minLvl, velocity*Time.deltaTime);
    }

    public void Raise()
    {
        level = Mathf.MoveTowards(level, maxLvl, upVel*Time.deltaTime);
        if (Random.value<0.03f) {
            GameObject fw = Instantiate(firework, new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f), -3f), transform.rotation);
            Destroy(fw, 1);
        }
    }
}
