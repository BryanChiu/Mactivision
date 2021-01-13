using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    public Dispenser dispenser; // will tell this class the correctness of the decision
    Animator anim;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
    }

    // When the monster eats the food GameObject, it will determine
    // whether the food was correctly feed. If incorrectly feed,
    // play the monster spiting animation and sound.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (dispenser.MakeChoice(true)) {
            other.gameObject.SetActive(false);
        } else {
            anim.Play("Base Layer.monster_spit");
            sound.PlayDelayed(0.2f);
        }

        StartCoroutine(WaitForMonsterSpit(other.attachedRigidbody));
    }

    // Wait for the eating animation to finish before ejecting the food.
    // Regardless of the choice, the food gets ejected as if the monster
    // spit it out; if the choice was correct, the food GameObject will be
    // invisible. At the end, the food gets deactivated and rotation reset.
    IEnumerator WaitForMonsterSpit(Rigidbody2D food)
    {
        yield return new WaitForSeconds(0.37f);
        food.velocity = new Vector2(6f, 8f);
        food.position = new Vector3(5.1f, -4.3f, 0f);

        yield return new WaitForSeconds(0.8f);
        food.velocity = Vector2.zero;
        food.gameObject.transform.eulerAngles = Vector3.zero;
        food.gameObject.SetActive(false);
    }
}
