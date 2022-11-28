using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Vector3 moveDirection;
    Vector3 aimDirection;

    [SerializeField]
    FloatingJoystick joystick;

    [SerializeField]
    float moveSpeed = 0;

    //[SerializeField]
    //float acceleration = 0.1f;

    [SerializeField]
    float speedMax = 10;

    //[SerializeField]
    //float decceleration = 0.1f;

    Rigidbody rb;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject bulletSpawn;

    [SerializeField]
    GameObject laser;

    public AudioSource audioSource;
    public AudioClip sound;


    GameManager gameManager;

    bool canShoot = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

        aimDirection = bulletSpawn.transform.forward;

    }

    // Start is called before the first frame update
    void Start()
    {
        laser.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    Debug.Log("Touch");

        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    // Save the info
        //    RaycastHit hit;
        //    ray.direction = new Vector3(0, 1, 0);
            
            
        //    // You successfully hi
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        // Find the direction to move in
        //        Vector3 pos3d = hit.point - transform.position;

        //        // Make it so that its only in x and y axis
        //        pos3d.y = 0; // No vertical movement

        //        GameObject.Find("Cube (1)").transform.position = pos3d;

        //        Debug.Log(pos3d);
        //    }
            
        //}

        if (joystick.Direction.magnitude > 0)
        {
            // Vecteur de direction de déplacement
            moveDirection = new Vector3(joystick.Direction.x, joystick.Direction.y , 0).normalized;
            moveSpeed = speedMax * joystick.Direction.magnitude;
            laser.SetActive(true);
            canShoot = true;
        }
        else
        {
            //moveSpeed = Mathf.Lerp(moveSpeed, 0, decceleration);
            moveSpeed = 0;

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
        audioSource.PlayOneShot(sound);
    }

    public void GameOver()
    {
        enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameManager.GameOver();
    }

}
