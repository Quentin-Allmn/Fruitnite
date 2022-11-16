using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Vector3 moveDirection;
    Vector3 aimDirection;

    [SerializeField]
    DynamicJoystick joystick;

    [SerializeField]
    float moveSpeed = 0;

    [SerializeField]
    float acceleration = 0.1f;

    [SerializeField]
    float speedMax = 10;

    [SerializeField]
    float decceleration = 0.1f;

    Rigidbody rb;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject bulletSpawn;

    [SerializeField]
    GameObject laser;

    GameManager gameManager;

    bool canShoot = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

        //Health = 100f;

        aimDirection = bulletSpawn.transform.forward;

    }

    // Start is called before the first frame update
    void Start()
    {
        laser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (joystick.Direction.magnitude > 0)
        {
            // Vecteur de direction de déplacement
            moveDirection = new Vector3(joystick.Direction.x, joystick.Direction.y , 0).normalized;
            moveSpeed = Mathf.Lerp(moveSpeed, speedMax, acceleration) * joystick.Direction.magnitude;
            laser.SetActive(true);
            canShoot = true;
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, decceleration);

            if (canShoot == true)
            {
                Shoot();
                canShoot = false;
            }

           // Debug.Log(gameManager.NombreTentative);

        }



    }

    private void FixedUpdate()
    {
        // Vélocité = direction * vitesse * inclinaison du joystick
        rb.velocity = moveDirection * moveSpeed;

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Bullet>().ShootDirection(aimDirection);
        laser.SetActive(false);
        gameManager.NombreTentative -= 1;
    }

    public void GameOver()
    {
        enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameManager.GameOver();
    }

}
