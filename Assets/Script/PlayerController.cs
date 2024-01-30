using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public ParticleSystem explosion;
    public float speed = 8.0f;
    public float rotateSpeed = 10.0f;

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

        Vector3 newVector = new Vector3 (xSpeed,0f, zSpeed);

        playerRigidbody.velocity = newVector; //가속도를 줌
        if(newVector != null)
        {
            playerRigidbody.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newVector), Time.deltaTime * rotateSpeed);
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
