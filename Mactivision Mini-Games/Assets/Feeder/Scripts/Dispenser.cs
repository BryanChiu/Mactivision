using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This class is responsible for managing the games foods, and dispensing of foods.
public class Dispenser : MonoBehaviour
{
    public Animator pipe;               // this class will play the pipe dispensing animation
    public Transform monitor;           // the monitor that shows food updates
    public GameObject screenGreen;      // monitor flashes green when food is updated to preferred
    public GameObject screenRed;        // monitor flashes red when food is updated to unpreferred
    public GameObject[] allFoods;       // array of all possible foods
    public AudioClip dispense_sound;    // sound played when a food is dispensed from the pipe
    public AudioClip screen_sound;      // sound played when there is a food update
    AudioSource sound;

    System.Random randomSeed;   // seed of the current game
    float avgUpdateFreq;        // average number of foods dispensed between each food update
    float stdDevUpdateFreq;     // standard deviation of `avgUpdateFreq`
    int lastUpdate = 0;         // number of foods dispensed since last food update

    string[] gameFoods;                                 // foods being used in the current game
    public string[] goodFoods { protected set; get; }   // foods the monster will eat
    string[] badFoods;                                  // foods the monster will spit out
    int goodFoodCount = 0;                              // number of foods the monster likes

    GameObject screenFood;                                  // the food shown on the screen during a food update
    public string currentFood { protected set; get; }       // the current food the player has to decide on
    public DateTime choiceStartTime { protected set; get; } // the time the current food is dispensed and the player can make a choice

    // Initializes the dispenser with the seed.
    // Randomly chooses which foods will be used in the game.
    // `gameFoods` has the list of `tf` food items that will
    // be used in the game, and initially, `badFoods` = `gameFoods`,
    // as all foods begin "unpreferred". `goodFoods` is an array of empty
    // strings the same length as `gameFoods` and `badFoods`.
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

    // Decides whether to update the list of liked foods.
    // Returns whether there is an update or not
    public bool DispenseNext()
    {
        bool update = false;

        if (randomSeed.NextDouble()<CDF(++lastUpdate) || goodFoodCount==0) {
            UpdateFoods();
            update = true;
            lastUpdate = 0;
            StartCoroutine(WaitForFoodUpdate(1.75f)); // wait for a food update and then dispense next
        } else {
            StartCoroutine(WaitForFoodUpdate(0f)); // instantly dispense next food
        }

        return update;
    }

    // Returns whether the choice to `feed` was correct or not
    public bool MakeChoice(bool feed)
    {
        return (Array.IndexOf(goodFoods, currentFood)>=0 == feed);
    }

    // The actual function that randomly chooses a food and dispenses it.
    // Sets the `choiceStartTime` to the current time and activate the 
    // food GameObject and places it "in the pipe".
    // Physics does the rest to make it fall out of the pipe.
    void Dispense()
    {
        int randIdx;
        randIdx = randomSeed.Next(gameFoods.Length);
        currentFood = gameFoods[randIdx];
        choiceStartTime = DateTime.Now;

        // find the current food GameObject and place it in the pipe
        foreach (GameObject obj in allFoods) {
            if (obj.name==currentFood) {
                obj.SetActive(true);
                obj.transform.position = new Vector3(0f, 4f, 0f);
                break;
            }
        }

        // dispensing animation and sound
        pipe.Play("Base Layer.pipe_dispense");
        sound.clip = dispense_sound;
        sound.PlayDelayed(0f);
    }
 
    // Update the list of good and bad foods. Essentially swaps items between
    // the good and bad lists. If there are less than two foods in the good list, 
    // it will move a food from the bad list to the good list. 
    // On update, this function will activate the flashing green or red screen
    // depending on if a food was moved to the good or bad list, respectively.
    void UpdateFoods()
    {
        // choose a food to update and swap it to the other list
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

        // show the food to be updated on the monitor, and activate either
        // the green or red flashing screen animation and sound
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
    // a peak at `avgUpdateFreq` and an inverted standard deviation of `stdDevUpdateFreq`.
    // stdDev = 2.8: avg-2 -> 0%,  avg-1 -> 20%, avg -> 80%, avg+1 -> 100%
    // stdDev = 0.9: avg-2 -> 20%, avg-1 -> 40%, avg -> 60%, avg+1 -> 80%, avg+2 -> 90%
    // stdDev = 15:                avg-1 -> 0%,  avg -> 100%
    float CDF(int x) {
        return 1f/(1f+Mathf.Exp(-stdDevUpdateFreq*(x-avgUpdateFreq+0.5f)));
    }

    // Wait for the flashing screen animation and then dispense the next food.
    IEnumerator WaitForFoodUpdate(float wait)
    {
        yield return new WaitForSeconds(wait);
        screenGreen.SetActive(false);
        screenRed.SetActive(false);
        screenFood.SetActive(false);
        Dispense();
    }
}
