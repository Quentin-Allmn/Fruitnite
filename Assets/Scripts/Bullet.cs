using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10;

    // [SerializeField] GameObject fx2;

    GameManager gameManager;

    private int comboFruit;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        comboFruit = 0;
    }

    private void Update()
    {

        if (comboFruit == gameManager.nombreDeFruits)
        {
            Debug.Log("ComboFruit");
        }
        if (comboFruit >= 2)
        {
            Debug.Log("Combo x2");
        }
    }


    public void ShootDirection(Vector3 direction)
    {

        GetComponent<Rigidbody>().velocity = direction * speed;

        Destroy(gameObject, 2f);

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Target")
        {
            // var Impact = Instantiate(fx2, collision.contacts[0].point, Quaternion.identity) as GameObject;

            //Destroy(Impact, 2);
            //Debug.Log("Target collison");
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Fruit")
        {

            comboFruit += 1;

        }

    }
}
