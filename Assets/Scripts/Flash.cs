using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 1;
    float alpha = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        alpha -= Time.deltaTime * fadeSpeed;
        GetComponent<Material>().color = new Color(1, 1, 1, alpha);
    }
}
