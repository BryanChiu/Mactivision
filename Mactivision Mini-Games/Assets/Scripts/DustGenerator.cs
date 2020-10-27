using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustGenerator : MonoBehaviour
{
    public int lifespan = 120;
    int age;
    SpriteRenderer spriterender;
    // Start is called before the first frame update
    void Start()
    {
        age = 0;
        spriterender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriterender.color = new Color(1f, 1f, 1f, (float)(lifespan-(age++/2))/lifespan);
    }
}
