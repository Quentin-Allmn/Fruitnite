using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] GameObject destructionFx;

    GameManager gameManager;

    [SerializeField] Flash flash;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            var Impact = Instantiate(destructionFx, collision.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(Impact, 2);

            Destroy(collision.gameObject);
            Destroy(gameObject);

            gameManager.score += 1;
            gameManager.fruits.Remove(this);

            flash.gameObject.SetActive(true);



        }
    }



}
