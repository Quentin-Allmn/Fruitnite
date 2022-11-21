using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Target : MonoBehaviour
{

    [SerializeField]
    float rotationDuration = 2f;

    Vector3 rot = new Vector3(0, 360, 0);

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

        transform.DORotate(rot, rotationDuration, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.score > 100)
        {
            rotationDuration = 1.75f;
        }
    }
}
