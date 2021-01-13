using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dispenser : MonoBehaviour
{
    public Animator pipe;
    public Transform monitor;
    public GameObject screenGreen;
    public GameObject screenRed;
    public GameObject[] allFoods;
    public AudioClip dispense_sound;
    public AudioClip screen_sound;
    AudioSource sound;

    System.Random randomSeed;
    float avgUpdateFreq;
    float stdDevUpdateFreq;
    int lastUpdate = 0;

    string[] gameFoods;
    public string[] goodFoods { protected set; get; }
    string[] badFoods;
    int goodFoodCount = 0;

    GameObject screenFood;
    public string currentFood { protected set; get; }
    public DateTime choiceStartTime { protected set; get; }

    // initializes the dispenser with the seed, as well as the foods to
    // be used and the frequency the "good foods" are updated
    public void Init(string seed, int tf, float uf, float sd)
    {
        screenGreen.SetActive(false);
        screenRed.SetActive(false);
        foreach (GameObject obj in allFoods) obj.SetActive(false);

        sound = gameObject.GetComponent<AudioSource>();

        randomSeed = new System.Random(seed.GetHashCode());
        avgUpdateFreq = uf;
        stdDevUpdateFreq = sd;
        
        gameFoods = new string[tf];
        goodFoods = new string[tf];
        badFoods = new string[tf];
        
        // randomly select the foods that will be used in the game 
        int i = 0;
        while (i<tf) {
            string food = allFoods[randomSeed.Next(allFoods.Length)].name;
            if (Array.IndexOf(gameFoods, food)<0) {
                gameFoods[i] = food;
                goodFoods[i] = "";
                badFoods[i++] = food;
            }
        }
    }

    public bool DispenseNext()
    {
        bool update = false;

        if (randomSeed.NextDouble()<CDF(++lastUpdate) || goodFoodCount==0) {
            UpdateFoods();
            update = true;
            lastUpdate = 0;
            StartCoroutine(WaitForFoodUpdate(1.75f));
        } else {
            StartCoroutine(WaitForFoodUpdate(0f));
        }

        return update;
    }

    void Dispense()
    {
        int randIdx;
        randIdx = randomSeed.Next(gameFoods.Length);
        currentFood = gameFoods[randIdx];
        choiceStartTime = DateTime.Now;

        foreach (GameObject obj in allFoods) {
            if (obj.name==currentFood) {
                obj.SetActive(true);
                obj.transform.position = new Vector3(0f, 4f, 0f);
                break;
            }
        }

        pipe.Play("Base Layer.pipe_dispense");
        sound.clip = dispense_sound;
        sound.PlayDelayed(0f);
    }
 
    void UpdateFoods()
    {
        int randIdx = randomSeed.Next(gameFoods.Length);
        string food = badFoods[randIdx];
        if (goodFoodCount<2) {
            do {
                randIdx = randomSeed.Next(gameFoods.Length);
                food = badFoods[randIdx];
            } while (food=="");
        }
        badFoods[randIdx] = goodFoods[randIdx];
        goodFoods[randIdx] = food;

        if (food=="") {
            food = badFoods[randIdx];
            screenRed.SetActive(true);
            goodFoodCount--;
        } else {
            screenGreen.SetActive(true);
            goodFoodCount++;
        }
        food = food + " (screen)";
        screenFood = monitor.Find(food).gameObject;
        screenFood.SetActive(true);
        sound.PlayOneShot(screen_sound);
    }

    // Rough approximation of the cumulative distribution function based off the
    // probability density function as defined by `avgUpdateFreq` and `stdDevUpdateFreq`.
    // Returns the percentage of values less than x on a bell curve with
    // a peak at `avgUpdateFreq` and an inverted standard deviation of `stdDevUpdateFreq`
    float CDF(int x) {
        return 1f/(1f+Mathf.Exp(-stdDevUpdateFreq*(x-avgUpdateFreq+0.5f)));
    }

    IEnumerator WaitForFoodUpdate(float wait)
    {
        yield return new WaitForSeconds(wait);
        screenGreen.SetActive(false);
        screenRed.SetActive(false);
        screenFood.SetActive(false);
        Dispense();
    }
}
