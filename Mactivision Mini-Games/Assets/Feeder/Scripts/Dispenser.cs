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
    int changeFreq;
    string[] gameFoods;
    string[] goodFoods;
    string[] badFoods;
    int goodFoodCount = 0;
    string currentFood;
    GameObject screenFood;
    DateTime choiceStartTime;

    // initializes the dispenser with the seed, as well as the foods to
    // be used and the frequency the "good foods" are updated
    public void Init(string seed, int tf, int cf)
    {
        screenGreen.SetActive(false);
        screenRed.SetActive(false);
        foreach (GameObject obj in allFoods) obj.SetActive(false);

        sound = gameObject.GetComponent<AudioSource>();

        randomSeed = new System.Random(seed.GetHashCode());
        changeFreq = cf;
        
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

        if (randomSeed.NextDouble()<1f/changeFreq || goodFoodCount==0) {
            UpdateFoods();
            update = true;
            StartCoroutine(WaitForFoodUpdate(1.75f));
        } else {
            StartCoroutine(WaitForFoodUpdate(0f));
        }

        return update;
    }

    public string[] MakeChoice(bool choice)
    {
        return goodFoods;
    }

    public string GetCurrent() {return currentFood;}

    public DateTime GetChoiceStartTime() {return choiceStartTime;}

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

    void Dispense()
    {
        int randIdx;
        string food;
        randIdx = randomSeed.Next(gameFoods.Length);
        food = gameFoods[randIdx];

        foreach (GameObject obj in allFoods) {
            if (obj.name==food) {
                obj.SetActive(true);
                obj.transform.position = new Vector3(0f, 4f, 0f);
                break;
            }
        }

        pipe.Play("Base Layer.pipe_dispense");
        sound.clip = dispense_sound;
        sound.PlayDelayed(0f);
    }

    IEnumerator WaitForFoodUpdate(float wait)
    {
        Debug.Log(Time.frameCount.ToString() + ": WaitForFoodUpdate Start");
        yield return new WaitForSeconds(wait);
        screenGreen.SetActive(false);
        screenRed.SetActive(false);
        screenFood.SetActive(false);
        Dispense();
        Debug.Log(Time.frameCount.ToString() + ": WaitForFoodUpdate End");
    }
}
