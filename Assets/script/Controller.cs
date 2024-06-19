    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class Controller : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator amt;
        private GameManager gm;
        
        [SerializeField] private float jumpForce;
        
        [Header("Ground check")]
        [SerializeField] private bool isGrounded;
        public Transform GroundCheck;
        public float GroundCheckRadius;
        public LayerMask WhatisGround;
        
        private bool canInput = true;
        private bool isSlash = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            amt = GetComponent<Animator>();
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }


        void Update()
        {
            checkInput();
            amt.SetBool("isGameStart", gm.IsGameStart);
        }

        void FixedUpdate()
        {
            checkGround();
        }

        private void checkGround()
        {
            isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatisGround);
        }

        private void checkInput()
        {
            if (canInput && isGrounded && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.Space)))
            {
                resetSlash();
                canInput = false;
                StartCoroutine(resetInput());
                jump();
            }
            else if (canInput && isGrounded && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.LeftShift)))
            {
                isSlash = true;
                canInput = false;
                slash();
            }
            
            if (isSlash && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.LeftShift))
            {
                if (!canInput)
                {
                    resetSlash();
                }
                canInput = true;
            }
        }

        IEnumerator resetInput()
        {
            yield return new WaitForSeconds(0.15f);
            
            canInput = true;
        }

        private void jump()
        {
            rb.velocity = new Vector2(0, jumpForce);
        }

        private void slash()
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        
        private void resetSlash()
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            gm.gameOver();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
        }
        
    }
