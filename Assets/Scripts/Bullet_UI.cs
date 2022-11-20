using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet_UI : MonoBehaviour
{

    [SerializeField] List<GameObject> bullets;

    GameManager gameManager;

    private int nbBullet;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        nbBullet = gameManager.NombreTentative;
    }

    // Update is called once per frame
    void Update()
    {
        nbBullet = gameManager.NombreTentative;

        switch (nbBullet)
        {
            case 10:
                bullets[9].SetActive(true);
                bullets[8].SetActive(true);
                bullets[7].SetActive(true);
                bullets[6].SetActive(true);
                bullets[5].SetActive(true);
                bullets[4].SetActive(true);
                bullets[3].SetActive(true);
                bullets[2].SetActive(true);
                bullets[1].SetActive(true);
                bullets[0].SetActive(true);
                bullets[2].GetComponent<Image>().color = new Color(255, 255, 255);
                bullets[1].GetComponent<Image>().color = new Color(255, 255, 255);
                bullets[0].GetComponent<Image>().color = new Color(255, 255, 255);
                break;

            case 9:
                bullets[9].SetActive(false);
                break;
            case 8:
                bullets[8].SetActive(false);
                break;
            case 7:
                bullets[7].SetActive(false);
                break;
            case 6:
                bullets[6].SetActive(false);
                break;
            case 5:
                bullets[5].SetActive(false);
                break;
            case 4:
                bullets[4].SetActive(false);
                break;
            case 3:
                bullets[3].SetActive(false);
                bullets[2].GetComponent<Image>().color = new Color(255, 0, 0);
                bullets[1].GetComponent<Image>().color = new Color(255, 0, 0);
                bullets[0].GetComponent<Image>().color = new Color(255, 0, 0);
                break;
            case 2:
                bullets[2].SetActive(false);
                bullets[1].GetComponent<Image>().color = new Color(255, 0, 0);
                bullets[0].GetComponent<Image>().color = new Color(255, 0, 0);
                break;
            case 1:
                bullets[1].SetActive(false);
                bullets[0].GetComponent<Image>().color = new Color(255, 0, 0);
                break;
            case 0:
                bullets[0].SetActive(false);
                break;
        }

    }
}
