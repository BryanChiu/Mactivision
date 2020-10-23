using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log("Line from David");    
        // This should be a conflict
    }

    // Update is called once per frame
    void Update()
    {
        // master test
        anim.SetBool("isRunning", true);
        // Bryan was here
    }
}
