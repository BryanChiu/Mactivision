using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This class animates the monster and resets food GameObjects entering it
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
            other.attachedRigidbody.velocity = Vector2.zero;
            other.gameObject.transform.eulerAngles = Vector3.zero;
            other.gameObject.SetActive(false);
        } else {
            anim.Play("Base Layer.monster_spit");
            sound.PlayDelayed(0.2f);
            StartCoroutine(MonsterSpit(other.attachedRigidbody));
        }
    }

    // Wait for the eating animation to finish before ejecting the food.
    // At the end, the food gets deactivated and rotation reset.
    IEnumerator MonsterSpit(Rigidbody2D food)
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
