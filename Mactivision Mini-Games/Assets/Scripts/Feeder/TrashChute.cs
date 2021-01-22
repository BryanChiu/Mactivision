using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This class animates the trashchute and resets food GameObjects entering it
public class TrashChute : MonoBehaviour
{
    public Dispenser dispenser;         // will tell this class the correctness of the decision
    public SpriteRenderer recycleIcon;  // icon changes colour based on correctness
    public Color correct = Color.green;
    public Color incorrect = Color.red;
    Color defaultCol = Color.white;
    AudioSource sound;

    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    // When the trashchute detects a food GameObject, it will determine
    // whether the food was correctly discarded. The `recycleIcon` will
    // change to green or red based on the correct or incorrect decision.
    // Food GameObject gets deactivated and rotation reset.
    void OnTriggerEnter2D(Collider2D other)
    {
        recycleIcon.color = dispenser.MakeChoice(false) ? correct : incorrect;
        other.attachedRigidbody.velocity = Vector2.zero;
        other.gameObject.transform.eulerAngles = Vector3.zero;
        other.gameObject.SetActive(false);
        sound.PlayDelayed(0f);

        StartCoroutine(WaitForIconIndicator());
    }

    // Wait a bit before returning the icon back to gray
    IEnumerator WaitForIconIndicator()
    {
        yield return new WaitForSeconds(1.5f);
        recycleIcon.color = defaultCol;
    }
}
