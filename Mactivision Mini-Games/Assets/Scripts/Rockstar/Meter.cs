using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the crowd energy meter(s). Also launches fireworks
public class Meter : MonoBehaviour
{
    public Transform meterLevelR;               // The right meter's level
    public GameObject meterGoodRangeR;          // The right meter's green zone (good range)
    public Transform meterTopR;                 // The position of the top of right meter
    public Transform meterBottomR;              // The position of the bottom of right meter
    public Transform meterLevelL;               // The left meter's level
    public GameObject meterGoodRangeL;          // The left meter's green zone (good range)
    public Transform meterTopL;                 // The position of the top of left meter
    public Transform meterBottomL;              // The position of the bottom of left meter

    public GameObject firework;                 // firework prefab that gets launched
    public AudioSource firework_sound;          // firework sound effect

    System.Random randomSeed;                   // seed of the current game

    float changeFreq;                           // how often the velocity changes

    float minVel;                               // minimum velocity the meter drops
    float maxVel;                               // maximum velocity the meter drops
    float upVel;                                // velocity the meter is raised by player
    public float velocity { private set; get; } // just x velocity because y doesn't change

    float minLvl;                               // the minimum value for the meter
    float maxLvl;                               // the maximum value for the meter
    float goodRange;                            // the range considered good, centered at 50
    public float level { private set; get; }    // the meter's level

    Color badZone = new Color(1f, 0.5f, 0.5f, 1f);  // colour that the greenzones change to
    float colourLerp;                               // colour changer helper variable

    // Initializes the meter with the seed
    public void Init(string seed, float cf, float minV, float maxV, float upV, float minL, float maxL, float gr, float l)
    {
        randomSeed = new System.Random(seed.GetHashCode());
        changeFreq = cf;

        minVel = minV;
        maxVel = maxV;
        upVel = upV;
        UpdateVelocity(true);

        minLvl = minL;
        maxLvl = maxL;
        goodRange = gr;
        level = l;

        meterGoodRangeR.transform.localScale = new Vector3(1f, goodRange/100f, 1f);
        meterGoodRangeL.transform.localScale = new Vector3(1f, goodRange/100f, 1f);

        colourLerp = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Drop();
        UpdateVelocity(false);

        // display the meter level to match this internal representation
        meterLevelR.position = new Vector3(meterLevelR.position.x , Mathf.Lerp(meterBottomR.position.y, meterTopR.position.y, level/100f), meterLevelR.position.z);
        meterLevelL.position = new Vector3(meterLevelL.position.x , Mathf.Lerp(meterBottomL.position.y, meterTopL.position.y, level/100f), meterLevelL.position.z);

        // apply "colour filter" when level is outside of `goodRange`
        if (level <= (maxLvl-minLvl)/2f - goodRange/2f || level >= (maxLvl-minLvl)/2f + goodRange/2f) {         
            colourLerp = Mathf.Clamp(colourLerp + 2f * Time.deltaTime, 0f, 1f);
        } else {
            colourLerp = Mathf.Clamp(colourLerp - 2f * Time.deltaTime, 0f, 1f);
        }
        meterGoodRangeR.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, badZone, colourLerp);
        meterGoodRangeL.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, badZone, colourLerp);
    }

    // updates the velocity the meter drops. Can be forced to update
    void UpdateVelocity(bool force)
    {
        // only update if forced, or randomly based on `changeFreq`
        if (force || randomSeed.NextDouble() < Time.deltaTime/changeFreq) {
            velocity = (float)randomSeed.NextDouble()*(maxVel-minVel) + minVel;
        }
    }

    // drops the meter each frame
    void Drop()
    {
        level = Mathf.MoveTowards(level, minLvl, velocity*Time.deltaTime);
    }

    // raises the meter when the player presses the correct button, and randomly launches fireworks
    public void Raise()
    {
        level = Mathf.MoveTowards(level, maxLvl, upVel*Time.deltaTime);
        // randomly launches fireworks of random colour in random location that dies in 1 second
        if (randomSeed.NextDouble()<0.04f && GameObject.FindGameObjectsWithTag("Firework").Length<5) {
            GameObject fw = Instantiate(firework, new Vector3((float)randomSeed.NextDouble()*18f-9f, (float)randomSeed.NextDouble()*10f-5f, -3f), transform.rotation);
            fw.transform.localScale = Vector3.one * ((float)randomSeed.NextDouble() * 0.3f + 0.5f);
            fw.GetComponent<SpriteRenderer>().color = Color.HSVToRGB((float)randomSeed.NextDouble(), 1f, 1f);
            firework_sound.Play();
            Destroy(fw, 1);
        }
    }
}
