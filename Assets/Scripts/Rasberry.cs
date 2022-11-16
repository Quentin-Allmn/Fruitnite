using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rasberry : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] Text raspberryScore;

    private Vector3 textPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        textPosition = new Vector3(-14f, 1522, 2966);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            gameManager.score += 10;

            var TxtRsbrScore = Instantiate(raspberryScore, textPosition, Quaternion.identity) ;
            Destroy(TxtRsbrScore, 2);
        }
    }
}
