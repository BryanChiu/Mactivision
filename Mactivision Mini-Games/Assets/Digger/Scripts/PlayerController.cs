using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class doesn't actually control the player, it just animates stuff around the player
public class PlayerController : MonoBehaviour
{
    public GameObject dust1;                                    // dust sprite
    public GameObject dust2;                                    // different looking dust sprite
    public GameObject jackhammer;                               // jackhammer sprite
    public Vector3 hammerRest = new Vector3(0f, -0.191f, -1f);  // jackhammer position when the dig key is not pressed
    public Vector3 hammerJump = new Vector3(0f, -0.126f, -1f);  // jackhammer position when the dig key is pressed 

    // Called when the dig key is pressed
    public void DigUp() {
        jackhammer.transform.localPosition = hammerJump;
    }

    // Called when the dig key is released
    public void DigDown() {
        jackhammer.transform.localPosition = hammerRest;
        // Create a "dust particle" somewhere randomly around the player
        Vector3 randomOffset = new Vector3(Random.Range(-0.27f, 0.27f), -0.29f+Random.Range(-0.15f, 0.15f), 0f);
        GameObject dust = Random.value>0.5 ? Instantiate(dust1, transform.position+randomOffset, transform.rotation) : 
                                             Instantiate(dust2, transform.position+randomOffset, transform.rotation);
        Destroy(dust, 2); // Destroys the dust object after two seconds
    }
}
