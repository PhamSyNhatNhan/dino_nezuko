using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gm;
    private Rigidbody2D rb;

    [SerializeField] private float speed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(- speed * Time.deltaTime * gm.GameSpeed, 0.0f);
        
        if(transform.position.x <= -15.0f) Destroy(gameObject);
    }
}
