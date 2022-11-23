using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptBtnShop : MonoBehaviour
{


    [SerializeField] Image cadenasBleu;
    [SerializeField] Image cadenasVert;
    [SerializeField] Image cadenasJaune;
    [SerializeField] Image cadenasViolet;

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("LaserBleu", 0);
        PlayerPrefs.SetInt("LaserVert", 0);
        PlayerPrefs.SetInt("LaserViolet", 0);
        PlayerPrefs.SetInt("LaserJaune", 0);
    }

    public void ResetCoins()
    {
        PlayerPrefs.SetInt("Coins", 0);
    }

    public void LaserBleu()
    {
        if (PlayerPrefs.GetInt("Coins") >= 5 && PlayerPrefs.GetInt("LaserBleu") == 0)
        {

           PlayerPrefs.SetInt("LaserBleu",1);
            Debug.Log("Buy");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 5);
        }
        else
        {
            Debug.Log("Not Buy");
        }


    }

    public void LaserVert()
    {
        if (PlayerPrefs.GetInt("Coins") >= 10 && PlayerPrefs.GetInt("LaserVert") == 0)
        {
            PlayerPrefs.SetInt("LaserVert", 1);
            Debug.Log("Buy");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 5);
    }
        else
        {
            Debug.Log("Not Buy");
        }

    }

    public void LaserJaune()
    {
        if (PlayerPrefs.GetInt("Coins") >= 15 && PlayerPrefs.GetInt("LaserJaune") == 0)
        {
            PlayerPrefs.SetInt("LaserJaune", 1);
            Debug.Log("Buy");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 5);
        }
        else
        {
            Debug.Log("Not Buy");
        }
    }

    public void LaserViolet()
    {
        if (PlayerPrefs.GetInt("Coins") >= 20 && PlayerPrefs.GetInt("LaserViolet") == 0)
        {
            PlayerPrefs.SetInt("LaserViolet", 1);
            Debug.Log("Buy");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 5);
        }
        else
        {
            Debug.Log("Not Buy");
        }
    }

    private void Update()
    {
        if ( PlayerPrefs.GetInt("LaserBleu") == 1)
        {
            cadenasBleu.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("LaserVert") == 1)
        {
            cadenasVert.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("LaserJaune") == 1)
        {
            cadenasJaune.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("LaserViolet") == 1)
        {
            cadenasViolet.gameObject.SetActive(false);
        }

    }


}
