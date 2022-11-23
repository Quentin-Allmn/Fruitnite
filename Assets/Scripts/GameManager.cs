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

    //[SerializeField]
    //CameraShake CamShake;

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

    [SerializeField]
    Text txtCoins;

    [SerializeField] GameObject camShake;

    [SerializeField] FloatingJoystick joystick;

    public AudioSource audioSource;
    public AudioClip sound;

    [SerializeField] public int coins;

    Rasberry raspberry;

    public int comboFruit;

    public bool raspberryBool = false;

    private bool canCoins = true;

    private bool isBlackLaser = false;


    [SerializeField] GameObject laser;
    [SerializeField] Material laserRed;
    [SerializeField] Material laserBleu;
    [SerializeField] Material laserVert;
    [SerializeField] Material laserJaune;
    [SerializeField] Material laserViolet;
    [SerializeField] Material laserNoir;

    //public float spawnRate = 0.5F;
    //private float nextSpawn = 0.0F;

    SwitchLaserColor switchLaserColor;

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
        switchLaserColor = FindObjectOfType<SwitchLaserColor>();
        SwitchColorLaser();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Création de la cible
        Newtarget();

        // Affichage du meilleur score
        txtBestScore.text = "Best :" + PlayerPrefs.GetInt("Score", 0).ToString();

        // Game Over en invisible
        txtGameOver.gameObject.SetActive(false);
        gameOver.SetActive(false);
        txtCoins.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Changement d'etat
        if (joystick.Direction.magnitude > 0 && gameState == State.Begining)
        {
            StartGame();
        }

        // Mise à jour de l'affichage du score
        txtScore.text = score.ToString();

       


        // Changement du nombre de fruit Max et Min en fonction du score
        switch (score)
        {
            case 0: nbFruitMax = 3;
                break;
            case 10: nbFruitMax = 4;
                break;
            case 25: nbFruitMax = 5;
                break;
            case 50:
                nbFruitMax = 6;
                nbFruitMin = 4;
                break;
            case 125:
                nbFruitMax = 7;
                nbFruitMin = 5;
                break;
            case 200:
                nbFruitMin = 6;
                nbFruitMax = 8;
                break;
        }

        // Verifictaion et destruction de la cible
        if (fruits.Count == 0 && canDestroyTarget == true)
        {

            FindObjectOfType<Destroytarget>().Destruction();
            Instantiate(camShake, targetZone);
            //audioSource.PlayOneShot(sound);
            AudioSource.PlayClipAtPoint(sound, transform.position);
            canDestroyTarget = false;
            Invoke("Newtarget", 2f);
        }

        // Verication de la condition de défaite 
        if (NombreTentative <= 0 && canDestroyTarget == true)
        {
            FindObjectOfType<PlayerController>().GameOver();
            
        }

        // Affichage texte combo lors d'un combo
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

        // Affichage texte +10 lorsqu'une framboise est détruite
        if (raspberryBool == true)
        {
            Debug.Log("Collided");
            txtRaspberry.text = "+ 10 !";
            txtRaspberry.gameObject.SetActive(true);
            Invoke("MaskText2", 1.5f);
            raspberryBool = false;
            score = score + 10;
        }

        txtCoins.text = "Your Coins :" + PlayerPrefs.GetInt("Coins").ToString();

    }

    void StartGame()
    {
        gameState = State.InGame;

        BroadcastMessage("OnStartGame", SendMessageOptions.DontRequireReceiver);

        titre.SetActive(false);

        txtBestScore.gameObject.SetActive(false);

        if (isBlackLaser)
        {
            laser.GetComponent<MeshRenderer>().material = laserRed;
        }
    }

    void Newtarget()
    {
        // Random chance d'avoir une framboise
        int randomRasberry = Random.Range(0, probaRasberry);

        // Random nombre de fruit présent sur la cible
        int randomNbFruit = Random.Range(nbFruitMin, nbFruitMax);

        nombreDeFruits = randomNbFruit + 1;

        // Création de la cible
        GameObject target = Instantiate(Target, targetZone);
        target.transform.position = targetZone.position;

        // Ajout des Fruits sur la cible
        for (int i = 0; i <= randomNbFruit ; i++)
        {

        float randomPosX = Random.Range(-3.1f, 3.1f);
        float randomPosY = Random.Range(-3.1f, 3.1f);

            // Ramdom type de fruit (Pastèque, orange)
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

        // Verification condition d'apparition de la Framboise
        if (randomRasberry == 4)
        {
            float randomPosX = Random.Range(0f, 2f);
            float randomPosY = Random.Range(0f, 2f);

            // Ajout de la Framboise
            GameObject f = Instantiate(fruit4, targetZone);
            f.transform.position = new Vector3(targetZone.position.x - randomPosX, targetZone.position.y - randomPosY, 7f);

            fruits.Add(f.GetComponent<Fruit>());
        }


        canDestroyTarget = true;

        // Reset nombre de tentatives du joueur
        NombreTentative = 10;

    }

    void Restart()
    {
 
        SceneManager.LoadScene("SampleScene");
    }

    public void GameOver()
    {
        FunctionCoin();

        // Affichage écran Game Over
        gameOver.SetActive(true);
        txtCoins.gameObject.SetActive(true);
        // Affichage du score réalisé par le joueur
        txtGameOver.text = "Your score :" + score.ToString();
        txtGameOver.gameObject.SetActive(true);

        // Changement d'état
        gameState = State.GameOver;
        // Indication de l'état au autres classes
        BroadcastMessage("OnGameOver", SendMessageOptions.DontRequireReceiver);


        // On verifie si le meilleur score à été battu
        int bestScore = PlayerPrefs.GetInt("Score", 0);
        // On met à jour le meilleur score 
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("Score", score);
        }

        // appel de la fonction rejouer
        Invoke("Restart", 2f);



    }

    void MaskText()
    {
        // Masque le texte Combo
        txtCombo.gameObject.SetActive(false);
    }
    void MaskText2()
    {
        // Masque le texte Framboise
        txtRaspberry.gameObject.SetActive(false);
    }

    public void SwitchColorLaser()
    {
        switch (PlayerPrefs.GetInt("SkinIndex"))
        {
            case 0:
                laser.GetComponent<MeshRenderer>().material = laserRed;
                isBlackLaser = false;
                break;
            case 1:
                if (PlayerPrefs.GetInt("LaserBleu") == 1)
                {
                  laser.GetComponent<MeshRenderer>().material = laserBleu;
                    isBlackLaser = false;
                }
                else
                {
                    laser.GetComponent<MeshRenderer>().material = laserNoir;
                    isBlackLaser = true;
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("LaserVert") == 1)
                {
                    laser.GetComponent<MeshRenderer>().material = laserVert;
                    isBlackLaser = false;
                }
                else
                {
                    laser.GetComponent<MeshRenderer>().material = laserNoir;
                    isBlackLaser = true;
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("LaserJaune") == 1)
                {
                    laser.GetComponent<MeshRenderer>().material = laserJaune;
                    isBlackLaser = false;
                }
                else
                {
                    laser.GetComponent<MeshRenderer>().material = laserNoir;
                    isBlackLaser = true;
                }
                break;
            case 4:
                if (PlayerPrefs.GetInt("LaserViolet") == 1)
                {
                    laser.GetComponent<MeshRenderer>().material = laserViolet;
                    isBlackLaser = false;
                }
                else
                {
                    laser.GetComponent<MeshRenderer>().material = laserNoir;
                    isBlackLaser = true;
                };
                break;
        }
    }

    int FunctionCoin()
    {
        if (canCoins == true)
        {

            coins = PlayerPrefs.GetInt("Coins");
            Debug.Log(PlayerPrefs.GetInt("Coins"));
            Debug.Log(coins);
            coins += (score / 10);

            PlayerPrefs.SetInt("Coins", coins);

            canCoins = false;
        }
        return coins;
    }

}
