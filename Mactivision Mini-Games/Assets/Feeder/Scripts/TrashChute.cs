using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashChute : MonoBehaviour
{
    public Dispenser dispenser;
    public SpriteRenderer recycleIcon;
    public Color correct = Color.green;
    public Color incorrect = Color.red;
    Color defaultCol = Color.white;
    AudioSource sound;

    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject food = other.gameObject;
        string[] goodFoods = dispenser.goodFoods;
        recycleIcon.color = Array.IndexOf(goodFoods, food.name)<0 ? correct : incorrect;
        other.attachedRigidbody.velocity = Vector2.zero;
        food.transform.eulerAngles = Vector3.zero;
        food.SetActive(false);
        sound.PlayDelayed(0f);
        StartCoroutine(WaitForIconIndicator());
    }

    IEnumerator WaitForIconIndicator()
    {
        yield return new WaitForSeconds(1.5f);
        recycleIcon.color = defaultCol;
    }
}
