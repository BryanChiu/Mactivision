using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimator : MonoBehaviour
{
    public bool opened;
    public GameObject player;
    public GameObject coin;
    public Vector3 destination = new Vector3(0f, 0.83f, -1.5f);
    public float coinspeed = 1.0f;
    Animator animator;
    AudioSource sound;


    // Start is called before the first frame update
    void Start()
    {
        opened = false;
        animator = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (opened) {
            coin.transform.localPosition = Vector3.MoveTowards(coin.transform.localPosition, destination, coinspeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name == player.name) {
            opened = true;
            animator.SetBool("Open", true);
            sound.PlayDelayed(0.75f);
        }
    }
}
