using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLaserColor : MonoBehaviour
{

    [SerializeField] public int nombreSkin = 5;
    [SerializeField] public int skinIndex = 0;


    public void SwitchLaserG()
    {
        
        skinIndex -= 1;

        if (skinIndex < 0)
        {
            skinIndex = nombreSkin - 1;
        }

        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        FindObjectOfType<GameManager>().SwitchColorLaser();
        Debug.Log(PlayerPrefs.GetInt("SkinIndex"));
    }

    public void SwitchLaserD()
    {
        Debug.Log("LaserGD");
        skinIndex += 1;
        if (skinIndex >= nombreSkin)
        {
            skinIndex = 0;
        }

        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        FindObjectOfType<GameManager>().SwitchColorLaser();
    }

  

}
