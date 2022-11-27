using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10;

    [SerializeField] GameObject smokeVFX;

    // [SerializeField] GameObject fx2;

    GameManager gameManager;

    public AudioSource audioSource;
    public AudioClip sound;

    private bool onlyTarget = true;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.comboFruit = 0;
    }

    private void Update()
    {

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
           // gameManager.comboFruit = 0;
            Destroy(gameObject);
            gameManager.NombreTentative -= 1;
        }

        if (collision.gameObject.tag == "Target" && onlyTarget == true)
        {
            AudioSource.PlayClipAtPoint(sound, transform.position);

            Vector3 coImpact = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,7);

            var Smoke = Instantiate(smokeVFX, coImpact, Quaternion.identity) as GameObject;
            Destroy(Smoke, 1);
        }

        if (collision.gameObject.tag == "Fruit")
        {

            gameManager.comboFruit += 1;
            onlyTarget = false;
        }

        if (collision.gameObject.tag == "Raspberry")
        {
            gameManager.raspberryBool = true;
            onlyTarget = false;
        }

    }
}
