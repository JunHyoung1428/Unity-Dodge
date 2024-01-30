using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public ParticleSystem explosion;
    public float speed = 8.0f;
    public float jumpForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3 (xSpeed,0f, zSpeed);

        playerRigidbody.velocity = newVelocity; //가속도를 줌
       
        if (Input.GetKey(KeyCode.Space)==true)
        {
            playerRigidbody.velocity =Vector3.up*jumpForce;
        }
    }

    public void Die()
    {
        
        if (explosion != null)
        {
           explosion.transform.position = playerRigidbody.transform.position;
           explosion.Play();
           Debug.Log("play explosion");
        }
        //자신의 오브젝트 비활성화
        gameObject.SetActive(false);

        GameManager gameManager  = FindObjectOfType<GameManager>();     
        gameManager.EndGame();
    }
}
