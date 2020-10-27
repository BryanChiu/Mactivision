using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBreaker : MonoBehaviour
{
    public GameObject player;
    public Sprite[] breakAnimation;
    string digKey;

    const int MAX_HITS = 9;
    int hits;
    bool touching;

    SpriteRenderer spriteRender;

    // Start is called before the first frame update
    void Start()
    {
        digKey = player.GetComponent<PlayerController>().digKey;

        hits = 0;
        touching = false;

        spriteRender = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.name == player.name) {
            touching = true;
        }
    }

    void Update()
    {
        if (touching && Input.GetKeyDown(digKey)) {
            if (hits==MAX_HITS) {
                gameObject.SetActive(false);
            } else {
                spriteRender.sprite = breakAnimation[++hits];
            }
        }
    }
}
