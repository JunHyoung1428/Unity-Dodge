using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public GameObject destroyTank;
    public ParticleSystem explosion;

    private Vector3 newVector;
    public float speed = 8.0f;
    public float rotateSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() //물리연산은 Update 대신 1초에 50번 연산을 보장 할 수 있는 FixedUpdate()에 쓰는게 좋음
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        newVector = new Vector3 (xSpeed,0f, zSpeed);
    }

    private void FixedUpdate()
    {
        // 입력은 Update에서, 물리연산은 FixedUpdate에서 처리하는것이 좋음
        // Update는 프레임에 영향을 받지만, FixedUpdate는 초당 50회 연산을 보장해 줄 수 있음
        playerRigidbody.velocity = newVector;
        if (newVector != Vector3.zero)
        {
            playerRigidbody.transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newVector), Time.deltaTime * rotateSpeed);
        }
    }

    public void Die()
    {
        
        if (explosion != null)
        {
           explosion.transform.position = playerRigidbody.transform.position;
           destroyTank.transform.position = playerRigidbody.transform.position;
           explosion.Play();
           destroyTank.SetActive(true);
        }
        //자신의 오브젝트 비활성화
        gameObject.SetActive(false);

        GameManager gameManager  = FindObjectOfType<GameManager>();     
        gameManager.EndGame();
    }
}
