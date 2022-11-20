using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    Text txtScore;

    [SerializeField]
    Text txtTentatives;

    [SerializeField]
    GameObject gameOver;

    [SerializeField]
    public int NombreTentative = 1;

    [SerializeField]
    GameObject Target;

    [SerializeField]
    GameObject fruit;

    [SerializeField]
    GameObject fruit2;

    [SerializeField]
    GameObject fruit3;

    [SerializeField]
    GameObject fruit4;

    [SerializeField]
    CameraShake CamShake;

    [SerializeField]
    int probaRasberry = 8;

    public Transform targetZone;

    [SerializeField]
    public int nombreDeFruits = 2;

    public int score = 0;

    public List<Fruit> fruits;

    bool canDestroyTarget = false;

    [SerializeField] int nbFruitMax = 3;

    [SerializeField] int nbFruitMin = 2;

    GameObject typeFruit;

    [SerializeField] Flash flash;

    public AudioSource audioSource;
    public AudioClip sound;

    //public float spawnRate = 0.5F;
    //private float nextSpawn = 0.0F;

    public enum State
    {
        Begining,
        InGame,
        GameOver,
    }

    private void Awake()
    {
        fruits = new List<Fruit>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Newtarget();
    }

    // Update is called once per frame
    void Update()
    {

        txtScore.text = score.ToString();

        txtTentatives.text = NombreTentative.ToString();

        switch (score)
        {
            case 0: nbFruitMax = 3;
                break;
            case 10: nbFruitMax = 4;
                txtScore.color = new Color(255f, 0f, 0f);
                break;
            case 20: nbFruitMax = 5;
                break;
        }

        if (fruits.Count == 0 && canDestroyTarget == true)
        {

            FindObjectOfType<Destroytarget>().Destruction();
            CamShake.gameObject.SetActive(true);
            //audioSource.PlayOneShot(sound);
            AudioSource.PlayClipAtPoint(sound, transform.position);
            canDestroyTarget = false;
            Invoke("Newtarget", 2f);
        }

        if (NombreTentative <= 0 && canDestroyTarget == true)
        {
            FindObjectOfType<PlayerController>().GameOver();
            gameOver.SetActive(true);
        }

    }

    void Newtarget()
    {

        int randomRasberry = Random.Range(0, probaRasberry);

        CamShake.gameObject.SetActive(false);

        int randomNbFruit = Random.Range(nbFruitMin, nbFruitMax);

        nombreDeFruits = randomNbFruit + 1;

        GameObject target = Instantiate(Target, targetZone);
        target.transform.position = targetZone.position;

        for (int i = 0; i <= randomNbFruit ; i++)
        {

        float randomPosX = Random.Range(-3.1f, 3.1f);
        float randomPosY = Random.Range(-3.1f, 3.1f);

        int randomTypeFruit = Random.Range(0, 3);

            switch (randomTypeFruit)
            {
                case 0: 
                    typeFruit = fruit;
                    break;
                case 1:
                    typeFruit = fruit2;
                    break;
                case 2:
                    typeFruit = fruit3;
                    break;
            }

          //  if (Time.time > nextSpawn)
           // {
          //  nextSpawn = Time.time + spawnRate;

            GameObject f = Instantiate(typeFruit, targetZone);
            f.transform.position = new Vector3(targetZone.position.x - randomPosX, targetZone.position.y - randomPosY, 7f);

            fruits.Add(f.GetComponent<Fruit>());


           // }

        }

        if (randomRasberry == 4)
        {
            float randomPosX = Random.Range(0f, 2f);
            float randomPosY = Random.Range(0f, 2f);

            GameObject f = Instantiate(fruit4, targetZone);
            f.transform.position = new Vector3(targetZone.position.x - randomPosX, targetZone.position.y - randomPosY, 7f);

            fruits.Add(f.GetComponent<Fruit>());
        }


        canDestroyTarget = true;

        NombreTentative = 10;

    }

    void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GameOver()
    {
        Invoke("Restart", 2f);
    }
}
