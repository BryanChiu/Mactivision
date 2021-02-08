using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is applied to each of the ten blocks that the player has to break
public class GroundBreaker : MonoBehaviour
{
    public PlayerController player;
    public Sprite[] breakAnimation;

    public int hitsToBreak {get; set;}         // number of hits it takes to break the block (set by `SetHitsToBreak`)
    int hits;                       // number of hits it has received

    SpriteRenderer spriteRender;

    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Called when a trigger collider collides with this object
    void OnTriggerStay2D(Collider2D c)
    {
        // if the player is touching this block, and the dig key has been pressed to activate dig
        if (c.gameObject==player.gameObject && player.dig) {
            // advance the breaking animation until the block is broken, in which case
            // we remove the object, allowing the player to fall to the next block
            if (hits<hitsToBreak-1) {
                spriteRender.sprite = breakAnimation[Mathf.FloorToInt((++hits/(float)hitsToBreak)*10)];
            } else {
                gameObject.SetActive(false);
            }
            player.dig = false;
        }
    }
}
