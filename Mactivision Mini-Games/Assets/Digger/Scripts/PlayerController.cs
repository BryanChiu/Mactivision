using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject dust1;
    public GameObject dust2;
    public GameObject jackhammer;
    public Vector3 hammerRest = new Vector3(0f, -0.191f, 0f);
    public Vector3 hammerJump = new Vector3(0f, -0.126f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DigUp() {
        jackhammer.transform.localPosition = hammerJump;
    }

    public void DigDown() {
        jackhammer.transform.localPosition = hammerRest;
        Vector3 randomOffset = new Vector3(Random.Range(-0.27f, 0.27f), -0.29f+Random.Range(-0.15f, 0.15f), 0f);
        GameObject dust = Random.value>0.5 ? Instantiate(dust1, transform.position+randomOffset, transform.rotation) : 
                                             Instantiate(dust2, transform.position+randomOffset, transform.rotation);
        Destroy(dust, 2);
    }
}
