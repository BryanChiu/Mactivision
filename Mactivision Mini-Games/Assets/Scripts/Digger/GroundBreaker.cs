using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is applied to each of the ten blocks that the player has to break
public class GroundBreaker : MonoBehaviour
{
    public GameObject player;
    public Sprite[] breakAnimation;
    KeyCode digKey;

    public int hitsToBreak;         // number of hits it takes to break the block (set by `SetHitsToBreak`)
    int hits;                       // number of hits it has received
    bool touching;                  // whether the player is touching the block

    SpriteRenderer spriteRender;

    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
        touching = false;

        spriteRender = GetComponent<SpriteRenderer>();
    }

    // if the player is touching the block
    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.name == player.name) {
            touching = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the dig key is pressed, progress the breaking animation.
        // If the number of hits needed to fully break is reached, deactivate the object.
        // This causes the player to fall to the next block.
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
