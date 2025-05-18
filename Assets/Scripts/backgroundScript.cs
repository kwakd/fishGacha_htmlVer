using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScript : MonoBehaviour
{ 
    public GameObject birdObject;
    public GameObject alienshipObject;
    public GameObject oceanObject;
    public List<GameObject> fishRarityList = new List<GameObject>();
    public List<AudioClip> backgroundMusicList = new List<AudioClip>();
    public AudioSource backgroundAudioSource;

    public int fishListRaritySpawn = 0; // 0common 1rare 2epic 3legendary

    public bool sendFishListUp = false;

    private gameManager gameManagerScript;

    private float xMaxOcean = -2f;
    private float xMinOcean = -7f;

    private float xMaxBird = 11f;
    private float xMinBird = -11f;

    private float xMaxAlienship = 11.5f;
    private float xMinAlienship = -11.5f;

    private float yMaxFishList = -2f;
    private float yMinFishList = -6.5f;

    private int randomRangeBackground = 0;
    private int randomRangeBackground2 = 0;

    private bool goRightOcean;
    public bool randomBackgroundBool1 = false;
    private bool goRightBird = true;
    public bool randomBackgroundBool2 = false;
    private bool goLeftAlien = true;
    
    // Start is called before the first frame update
    void Start()
    {
        backgroundAudioSource = GetComponent<AudioSource>();
        oceanObject.transform.position = new Vector3(-2f, 6.2f,0);
        goRightOcean = true;
        birdObject.transform.position = new Vector3(-10.9f, 2.5f,0);
        alienshipObject.transform.position = new Vector3(11.4f, 2.5f,0);
        gameManagerScript = FindObjectOfType<gameManager>();
        fishRarityList[0].transform.position = new Vector3(0, yMinFishList, 0);
        fishRarityList[1].transform.position = new Vector3(0, yMinFishList, 0);
        fishRarityList[2].transform.position = new Vector3(0, yMinFishList, 0);
        fishRarityList[3].transform.position = new Vector3(0, yMinFishList, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(goRightOcean)
        {
            oceanObject.transform.position -= new Vector3(0.001f, 0,0);
            if(oceanObject.transform.position.x < xMinOcean)
            {
                goRightOcean = false;
            }
        }
        else
        {
            oceanObject.transform.position += new Vector3(0.001f, 0,0);
            if(oceanObject.transform.position.x > xMaxOcean)
            {
                goRightOcean = true;
            }
        }

        randomRangeBackground = Mathf.FloorToInt(Random.Range(0, 20001));
        if(randomRangeBackground >= 20000 && !randomBackgroundBool1 && !randomBackgroundBool2)
        {
            randomBackgroundBool1 = true;
            backgroundAudioSource.Stop();
            backgroundAudioSource.clip = backgroundMusicList[0];
            backgroundAudioSource.volume = 0.5f;
            backgroundAudioSource.Play();
        }
        if(randomBackgroundBool1 && !randomBackgroundBool2)
        {   
            if(goRightBird)
            {
                birdObject.transform.position += new Vector3(0.02f, 0,0);
            }
            else
            {
                birdObject.transform.position -= new Vector3(0.02f, 0,0);
            }

            if(birdObject.transform.position.x > xMaxBird && goRightBird)
            {
                goRightBird = false;
                randomBackgroundBool1 = false;
                birdObject.transform.localScale = new Vector3(-1, 1, 1);
                backgroundAudioSource.Stop();
            }
            else if(birdObject.transform.position.x < xMinBird && !goRightBird)
            {
                goRightBird = true;
                randomBackgroundBool1 = false;
                birdObject.transform.localScale = new Vector3(1, 1, 1);
                backgroundAudioSource.Stop();
            }
        }

        randomRangeBackground2 = Mathf.FloorToInt(Random.Range(0, 20001));
        if(randomRangeBackground2 >= 20000 && !randomBackgroundBool1 && !randomBackgroundBool2)
        {
            randomBackgroundBool2 = true;
            backgroundAudioSource.PlayOneShot(backgroundMusicList[1], 0.5f);
        }
        if(randomBackgroundBool2 && !randomBackgroundBool1)
        {
            Debug.Log("hello1");
            if(goLeftAlien)
            {
                alienshipObject.transform.position -= new Vector3(0.05f, 0,0);
            }
            else
            {
                alienshipObject.transform.position += new Vector3(0.05f, 0, 0);
            }

            if(alienshipObject.transform.position.x < xMinAlienship && goLeftAlien)
            {
                goLeftAlien = false;
                randomBackgroundBool2 = false;
            }
            else if(alienshipObject.transform.position.x > xMaxAlienship && !goLeftAlien)
            {
                goLeftAlien = true;
                randomBackgroundBool2 = false;
            }

        }

        if(sendFishListUp)
        {
            switch(fishListRaritySpawn)
            {
                case 0:
                    fishRarityList[0].transform.position += new Vector3(0, 0.025f, 0);
                    if(fishRarityList[0].transform.position.y > yMaxFishList)
                    {
                        sendFishListUp = false;
                        fishRarityList[0].transform.position = new Vector3(0, yMinFishList, 0);
                        gameManagerScript.SpawnFishCommon();
                    }
                    break;
                case 1:
                    fishRarityList[1].transform.position += new Vector3(0, 0.025f, 0);
                    if(fishRarityList[1].transform.position.y > yMaxFishList)
                    {
                        sendFishListUp = false;
                        fishRarityList[1].transform.position = new Vector3(0, yMinFishList, 0);
                        gameManagerScript.SpawnFishRare();
                    }
                    break;
                case 2:
                    fishRarityList[2].transform.position += new Vector3(0, 0.025f, 0);
                    if(fishRarityList[2].transform.position.y > yMaxFishList)
                    {
                        sendFishListUp = false;
                        fishRarityList[2].transform.position = new Vector3(0, yMinFishList, 0);
                        gameManagerScript.SpawnFishEpic();
                    }
                    break;
                case 3:
                    fishRarityList[3].transform.position += new Vector3(0, 0.025f, 0);
                    if(fishRarityList[3].transform.position.y > yMaxFishList)
                    {
                        sendFishListUp = false;
                        fishRarityList[3].transform.position = new Vector3(0, yMinFishList, 0);
                        gameManagerScript.SpawnFishLegendary();
                    }
                    break;
                default:
                    Debug.Log("BACKGROUNDSCRIPT FAIL");
                    break;
            }
        }

        



    }

}
