using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBreaker : MonoBehaviour
{
    public GameObject player;
    public Sprite[] breakAnimation;
    KeyCode digKey;

    public int hitsToBreak = 10;
    int hits;
    bool touching;

    SpriteRenderer spriteRender;

    // Start is called before the first frame update
    void Start()
    {
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
            if (hits<hitsToBreak-1) {
                spriteRender.sprite = breakAnimation[Mathf.FloorToInt((++hits/(float)hitsToBreak)*10)];
            } else {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetDigKey(KeyCode key) {
        digKey = key;
    }

    public void SetHitsToBreak(int hits) {
        hitsToBreak = hits;
    }
}
