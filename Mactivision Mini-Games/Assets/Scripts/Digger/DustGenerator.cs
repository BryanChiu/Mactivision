using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class animates a dust particle
public class DustGenerator : MonoBehaviour
{
    public float lifespan = 2f;
    float age;
    SpriteRenderer spriterender;

    // Start is called before the first frame update
    void Start()
    {
        age = 0f;
        spriterender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // slowly fade the dust to transparent
        age += Time.deltaTime;
        spriterender.color = new Color(1f, 1f, 1f, (lifespan-age)/lifespan);
    }
}
