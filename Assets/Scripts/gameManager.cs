using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public Transform spawnLocationsFish;
    public GameObject fishing2Minigame;
    public GameObject sellMenu;
    public GameObject playerFishAlert1;
    public GameObject fishObjectCommon;
    public GameObject fishObjectRare;
    public GameObject fishObjectEpic;
    public GameObject fishObjectLegendary;

    public GameObject tempGO;
    public GameObject trophyVictory;
    public GameObject crownVictory;
    public Slider reelingSlider;
    public Animator playerAnimator;
    public Rigidbody2D playerRb;
    public List<GameObject> fishList = new List<GameObject>();
    public List<AudioClip> musicList = new List<AudioClip>();
    public TextMeshProUGUI playerMoneyText;

    public TextMeshProUGUI mutedText;
    public AudioSource myAudioSource;
    

    public int fishRarityValue; // 0=default 1=common 2=uncommon 3=rare 4=epic 5=legendary
    public int playerMoneyCount = 0;
    public int currentList = 0;

    public bool isFishCaught1 = false; // first checkmark (auto fisher)
    public bool isFishCaught2 = false; // second checkmark (player mash W / reeling part)
    public bool isFishCaught3 = false; // third checkmark (player now playing 2nd minigame)

    public bool playerHasBeatenGame100 = false;

    private backgroundScript myBackgroundScript;
    private cameraScript myCameraScript;
    

    private int spawnLocationsFishInt;
    private int testSpawnLocationX; // -8 8
    private int testSpawnLocationY; // -5 5

    private int tempIntPlayerAnim;
    
    private float spawnTimer;
    private float fishGetAwayTimer = 5f;
    private float fishHP;
    private float fishHealValue;
    private float fishDifficulty;

    private bool pauseSound = true;

    public bool sellMenuTrue = false; 
    


    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(10,21);
        fishGetAwayTimer = Random.Range(2,6);
        myBackgroundScript = FindObjectOfType<backgroundScript>();
        myCameraScript = FindObjectOfType<cameraScript>();
        myAudioSource = GetComponent<AudioSource>();
        AudioListener.volume = 0f;
        if(fishList.Count > 0)
        {
            for(int i=0; i<fishList.Count; i++)
            {
                testSpawnLocationX = Mathf.FloorToInt(Random.Range(-8, 9));
                testSpawnLocationY = Mathf.FloorToInt(Random.Range(-5, 6));
                tempGO = Instantiate(fishList[i], spawnLocationsFish.position + new Vector3(testSpawnLocationX, testSpawnLocationY, 0), spawnLocationsFish.rotation);
            }
        }
        // spawnTimer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        //spawnLocationsFishInt = Mathf.FloorToInt(Random.Range(0, spawnLocationsFish.Length)); // 0 1 2 3 4 
        if(spawnTimer > 0 && !isFishCaught1 && !isFishCaught2 && !isFishCaught3) 
        {
            spawnTimer -= Time.deltaTime;
            //Debug.Log("spawnTimer: " + Mathf.FloorToInt(spawnTimer));
        }
        else if(fishGetAwayTimer > 0 && isFishCaught1 && !isFishCaught2 && !isFishCaught3)
        {
            fishGetAwayTimer -= Time.deltaTime;
            //Debug.Log("fishGetAwayTimer: " + Mathf.FloorToInt(fishGetAwayTimer));
        }
        else if(fishGetAwayTimer < 0 && isFishCaught1 && !isFishCaught2 && !isFishCaught3)
        {
            isFishCaught1 = false;
            spawnTimer = Random.Range(20,60);
            playerFishAlert1.SetActive(false);
            fishGetAwayTimer = Random.Range(3,5);
            Debug.Log("FISH GOT AWAY FROM THE REEL 1");
        }
        else if(spawnTimer < 0 && !isFishCaught1 && !isFishCaught2 && !isFishCaught3) 
        {
            //spawnTimer = Random.Range(20,60);
            JumpPlayer();
            spawnTimer = Random.Range(20,60);
            playerFishAlert1.SetActive(true);
            isFishCaught1 = true;
            Debug.Log("FISH CAUGHT! PLAYER NEEDS TO REEL");
        }

        if(Input.GetKeyDown(KeyCode.W) && !isFishCaught1 && !isFishCaught2 && !isFishCaught3 && myCameraScript.cameraSceneValue == 1)
        {
            playerAnimator.SetTrigger("PlayerIdle1");
            spawnTimer -= 0.01f;
        }
        
        if(Input.GetKeyDown(KeyCode.W) && isFishCaught1 && !isFishCaught2 && !isFishCaught3 && myCameraScript.cameraSceneValue == 1)
        {
            playerFishAlert1.SetActive(false);
            sellMenu.SetActive(false);
            sellMenuTrue = false;
            reelingSlider.gameObject.SetActive(true);
            isFishCaught2 = true;
            fishGetAwayTimer = Random.Range(2,6);
            fishHP = Mathf.FloorToInt(Random.Range(20, 51));
            reelingSlider.value = fishHP;
            fishHealValue = Random.Range(1.5f, 2f);
            fishDifficulty = Mathf.FloorToInt(Random.Range(1, 4));
            myAudioSource.clip = musicList[3];
            myAudioSource.volume = 0.5f;
            myAudioSource.Play();
            Debug.Log("player pressed W and reeled");
            Debug.Log("fishHP value rolled: " + fishHP);
            Debug.Log("fishHealValue value rolled: " + fishHealValue);
            Debug.Log("fishDifficulty value rolled: " + fishDifficulty);
        }

        if(isFishCaught1 && isFishCaught2 && !isFishCaught3)
        {
           fishHP -= Time.deltaTime*fishDifficulty;
           reelingSlider.value -= Time.deltaTime*fishDifficulty;
           playerAnimator.SetTrigger("PlayerIdle2");
           //Debug.Log("fishHP: " + fishHP);
        }
        if(Input.GetKeyDown(KeyCode.W) && isFishCaught1 && isFishCaught2 && !isFishCaught3)
        {
            //Debug.Log("user has healed fish W: " + Mathf.FloorToInt(fishHP));
            fishHP += fishHealValue;
            reelingSlider.value += fishHealValue;
            //Debug.Log("fishHP + 1: " + fishHP);
        }
        if(fishHP > 99.9f && isFishCaught1 && isFishCaught2 && !isFishCaught3)
        {
            isFishCaught3 = true;
            reelingSlider.gameObject.SetActive(false);
            myAudioSource.Stop();
            myAudioSource.clip = musicList[4];
            myAudioSource.volume = 0.3f;
            myAudioSource.Play();
            fishing2Minigame.SetActive(true);
            Debug.Log("FISH HAS BEEN REELED UP -> QUE 2ND MINIGAME");
        }
        else if(fishHP < 0.1f && isFishCaught1 && isFishCaught2 && !isFishCaught3 ) 
        {
            Debug.Log("FISH GOT AWAY FROM THE REEL 2");
            reelingSlider.gameObject.SetActive(false);
            myAudioSource.Stop();
            isFishCaught1 = false;
            isFishCaught2 = false;
        }

        // spawn random fish
        if(Input.GetKeyDown(KeyCode.Y) && !myBackgroundScript.sendFishListUp)
        {
            if(playerMoneyCount >= 50)
            {
                playerMoneyCount -= 50;
                //myAudioSource.PlayOneShot(musicList[0], 0.7f);
                myAudioSource.PlayOneShot(musicList[1], 0.5f);
                SpawnFish();
            }
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            if(playerMoneyCount >= 1000)
            {
                playerMoneyCount -= 1000;
                playerHasBeatenGame100 = true;
                trophyVictory.SetActive(true);
                crownVictory.SetActive(true);
            }
        }

        // SellMenu
        if(Input.GetKeyDown(KeyCode.P) && !isFishCaught2 && !isFishCaught3 && myCameraScript.cameraSceneValue == 2)
        {
            PauseMenuToggle();
        }

        // mute sound
        if(Input.GetKeyDown(KeyCode.M))
        {
            pauseSound = !pauseSound;
            if(pauseSound)
            {
                AudioListener.volume = 0f;
                mutedText.enabled = true;
            }
            else
            {
                AudioListener.volume = 1f;
                mutedText.enabled = false;
            }
            
        }

        //DEVKEY: SPAWN FISH
        if(Input.GetKeyDown(KeyCode.L) && !myBackgroundScript.sendFishListUp)
        {
            SpawnFish();
        }
        //DEVKEY: FORCE FISH
        if(Input.GetKeyDown(KeyCode.K) && !isFishCaught1 && !isFishCaught2 && !isFishCaught3)
        {
            spawnTimer = 0.5f;
        }
        
        
        if(!isFishCaught1 && !isFishCaught2 && !isFishCaught3)
        {
            tempIntPlayerAnim = Mathf.FloorToInt(Random.Range(0, 20001));
        }
        if(tempIntPlayerAnim >= 20000 && !isFishCaught1 && !isFishCaught2 && !isFishCaught3)
        {
            tempIntPlayerAnim = Mathf.FloorToInt(Random.Range(0, 2));
            switch(tempIntPlayerAnim)
            {
                case 0:
                    playerAnimator.SetTrigger("PlayerIdle1");
                    break;
                case 1:
                    playerAnimator.SetTrigger("PlayerIdle2");
                    break;
                default:
                    Debug.Log("anim default break");
                    break;
            }
            
        }

        playerMoneyText.text = ("$" + playerMoneyCount.ToString());

        //https://forum.unity.com/threads/accessing-variables-for-a-script-attached-to-a-just-instantiated-object.454185/
    }

    public void SpawnFish()
    {
        testSpawnLocationX = Mathf.FloorToInt(Random.Range(-8, 9));
        testSpawnLocationY = Mathf.FloorToInt(Random.Range(-5, 6));
        fishRarityValue = Random.Range(0, 101); // 0 ... 100
        Debug.Log("fishSpawned rarity value = " + fishRarityValue);
        if(fishRarityValue < 65)
        {
            myBackgroundScript.fishListRaritySpawn = 0;
            myBackgroundScript.sendFishListUp = true;
            //SpawnFishCommon();
        }
        else if(fishRarityValue >= 65 && fishRarityValue < 95)
        {
            myBackgroundScript.fishListRaritySpawn = 1;
            myBackgroundScript.sendFishListUp = true;
            //SpawnFishRare();
        }
        else if(fishRarityValue >= 95 && fishRarityValue < 100)
        {
            myBackgroundScript.fishListRaritySpawn = 2;
            myBackgroundScript.sendFishListUp = true;
            //SpawnFishEpic();
        }
        else if(fishRarityValue == 100)
        {
            myBackgroundScript.fishListRaritySpawn = 3;
            myBackgroundScript.sendFishListUp = true;
            //SpawnFishLegendary();
        }
    }

    public void SpawnFishCommon()
    {
        testSpawnLocationX = Mathf.FloorToInt(Random.Range(-8, 9));
        testSpawnLocationY = Mathf.FloorToInt(Random.Range(-5, 6));
        tempGO = Instantiate(fishObjectCommon, spawnLocationsFish.position + new Vector3(testSpawnLocationX, testSpawnLocationY, 0), spawnLocationsFish.rotation);
        fishList.Add(tempGO);
        //Debug.Log(tempGO.totalFishCost);
    }
    public void SpawnFishRare()
    {
        testSpawnLocationX = Mathf.FloorToInt(Random.Range(-8, 9));
        testSpawnLocationY = Mathf.FloorToInt(Random.Range(-5, 6));
        tempGO = Instantiate(fishObjectRare, spawnLocationsFish.position + new Vector3(testSpawnLocationX, testSpawnLocationY, 0), spawnLocationsFish.rotation);
        fishList.Add(tempGO);
    }
    public void SpawnFishEpic()
    {
        testSpawnLocationX = Mathf.FloorToInt(Random.Range(-8, 9));
        testSpawnLocationY = Mathf.FloorToInt(Random.Range(-5, 6));
        tempGO = Instantiate(fishObjectEpic, spawnLocationsFish.position + new Vector3(testSpawnLocationX, testSpawnLocationY, 0), spawnLocationsFish.rotation);
        fishList.Add(tempGO);
    }
    public void SpawnFishLegendary()
    {
        testSpawnLocationX = Mathf.FloorToInt(Random.Range(-8, 9));
        testSpawnLocationY = Mathf.FloorToInt(Random.Range(-5, 6));
        tempGO = Instantiate(fishObjectLegendary, spawnLocationsFish.position + new Vector3(testSpawnLocationX, testSpawnLocationY, 0), spawnLocationsFish.rotation);
        fishList.Add(tempGO);
    }

    public void JumpPlayer()
    {
        int tempInt = Mathf.FloorToInt(Random.Range(0, 2)); //0,1
        if(tempInt == 0)
        {
            playerAnimator.SetTrigger("Jump1");
        }
        else
        {
            playerAnimator.SetTrigger("Jump2");
        }
        playerRb.velocity = new Vector2(playerRb.velocity.x, 5);
    }

    public void PauseMenuToggle()
    {
        if(!sellMenuTrue)
        {
            sellMenu.SetActive(true);
            sellMenuTrue = true;
        }
        else
        {
            sellMenu.SetActive(false);
            sellMenuTrue = false;
        }
}
}

// TODO    
    // in the future full game or future updates
        // fishing rod rpg stats to boost player stats
        // better balancing for difficulty to fish gacha rate
        // do a "chaos garden" type auto racing wish player fishes
        // each fish will have random stats for the chaos garden
        // background -> day and night cycle
        // actual boss fight -> mash against kingFish

// CREDITS
    // assets
    // sound

    // potential songs (temp)
        // https://pixabay.com/music/bossa-nova-cocktail-music-110969/
        // SEARCH: https://pixabay.com/music/search/calm%20beach/