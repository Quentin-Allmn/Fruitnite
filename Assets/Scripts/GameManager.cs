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


    [SerializeField] Text txtRaspberry;

    [SerializeField]  Text txtCombo;

    [SerializeField] Text txtGameOver;

    [SerializeField]
    GameObject gameOver;

    [SerializeField]
    GameObject titre;

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

    [SerializeField]
    Text txtBestScore;

    [SerializeField] DynamicJoystick joystick;

    public AudioSource audioSource;
    public AudioClip sound;

    Rasberry raspberry;

    public int comboFruit;

    public bool raspberryBool = false;

    //public float spawnRate = 0.5F;
    //private float nextSpawn = 0.0F;

    public enum State
    {
        Begining,
        InGame,
        GameOver,
    }

    public State gameState = State.Begining;
    private void Awake()
    {
        fruits = new List<Fruit>();
        raspberry = FindObjectOfType<Rasberry>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Newtarget();

        txtBestScore.text = "Best :" + PlayerPrefs.GetInt("Score", 0).ToString();

        txtGameOver.gameObject.SetActive(false);
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (joystick.Direction.magnitude > 0 && gameState == State.Begining)
        {
            StartGame();
        }

        txtScore.text = score.ToString();

        txtTentatives.text = NombreTentative.ToString();

        switch (score)
        {
            case 0: nbFruitMax = 3;
                break;
            case 10: nbFruitMax = 4;
                //txtScore.color = new Color(255f, 0f, 0f);
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
            
        }

        switch (comboFruit)
        {
            case 2:
                txtCombo.text = "Combo +" + comboFruit.ToString() + "!";
                txtCombo.gameObject.SetActive(true);
                comboFruit = 0;
                Invoke("MaskText", 1.5f);
                score = score + 2;
                break;
            case 3:
                txtCombo.text = "Combo +" + comboFruit.ToString() + "!";
                txtCombo.gameObject.SetActive(true);
                comboFruit = 0;
                Invoke("MaskText", 1.5f);
                score = score + 3;
                break;
            case 4:
                txtCombo.text = "Combo +" + comboFruit.ToString() + "!";
                txtCombo.gameObject.SetActive(true);
                comboFruit = 0;
                Invoke("MaskText", 1.5f);
                score = score + 4;
                break;
            default :
                comboFruit = 0;
                break;
        }

        if (raspberryBool == true)
        {
            Debug.Log("Collided");
            txtRaspberry.text = "+ 10 !";
            txtRaspberry.gameObject.SetActive(true);
            Invoke("MaskText2", 1.5f);
            raspberryBool = false;
            score = score + 10;
        }

    }

    void StartGame()
    {
        gameState = State.InGame;

        BroadcastMessage("OnStartGame", SendMessageOptions.DontRequireReceiver);

        titre.SetActive(false);

        txtBestScore.gameObject.SetActive(false);
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

        gameOver.SetActive(true);

        txtGameOver.text = "Your score :" + score.ToString();
        txtGameOver.gameObject.SetActive(true);

        gameState = State.GameOver;

        BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);

        Invoke("Restart", 2f);

        int bestScore = PlayerPrefs.GetInt("Score", 0);

        if (score > bestScore)
        {
            PlayerPrefs.SetInt("Score", score);
        }
    }

    void MaskText()
    {
        txtCombo.gameObject.SetActive(false);
    }
    void MaskText2()
    {
        txtRaspberry.gameObject.SetActive(false);
    }
}
