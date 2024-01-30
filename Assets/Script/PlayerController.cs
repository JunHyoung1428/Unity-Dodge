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
    void FixedUpdate() //���������� Update ��� 1�ʿ� 50�� ������ ���� �� �� �ִ� FixedUpdate()�� ���°� ����
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        newVector = new Vector3 (xSpeed,0f, zSpeed);
    }

    private void FixedUpdate()
    {
        // �Է��� Update����, ���������� FixedUpdate���� ó���ϴ°��� ����
        // Update�� �����ӿ� ������ ������, FixedUpdate�� �ʴ� 50ȸ ������ ������ �� �� ����
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
        //�ڽ��� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);

        GameManager gameManager  = FindObjectOfType<GameManager>();     
        gameManager.EndGame();
    }
}
