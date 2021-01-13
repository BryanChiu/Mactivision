using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    public Dispenser dispenser;
    Animator anim;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject food = other.gameObject;
        string[] goodFoods = dispenser.goodFoods;
        if (Array.IndexOf(goodFoods, food.name)<0) {
            anim.Play("Base Layer.monster_spit");
            sound.PlayDelayed(0.2f);
        } else {
            food.SetActive(false);
        }
        StartCoroutine(WaitForMonsterSpit(other.attachedRigidbody));
        
    }

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
