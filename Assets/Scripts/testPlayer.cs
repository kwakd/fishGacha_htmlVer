using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class testPlayer : MonoBehaviour
{
    private bool rightArrowKeyPressed = false;
    private bool downArrowKeyPressed = false;
    private bool leftArrowKeyPressed = false;
    private bool upArrowKeyPressed = false;
    private bool fishRight = true;

    private float tempRandomIntForDirection = 0f;
    private float playerStrength;
    private float fishDifficulty;

    //public float currentHP;
    public float defaultHP;

    //public TextMeshProUGUI tempHPvalueText;
    public Slider fishingSlider;
    public Sprite[] arrowKeyPressArray;
    public RectTransform arrowDirectionSprite;
    public gameManager gameManagerScript;
    public GameObject selfWholeFishingGame;

    private Image spriteRenderer;

    void OnEnable()
    {
        spriteRenderer = GetComponent<Image>();
        defaultHP = Mathf.FloorToInt(Random.Range(20, 51));
        fishingSlider.value = defaultHP;
        playerStrength = Mathf.FloorToInt(Random.Range(6, 8));
        fishDifficulty = Random.Range(1f, 5f);
        RandomDirectionBeginning();
    }

    // Update is called once per frame
    void Update()
    {
        if(fishRight)
        {
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                rightArrowKeyPressed = true;
                spriteRenderer.sprite = arrowKeyPressArray[1];
                //Debug.Log("rightArrowKeyPressed = " + rightArrowKeyPressed);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow) && rightArrowKeyPressed)
            {
                downArrowKeyPressed = true;
                spriteRenderer.sprite = arrowKeyPressArray[2];
                //Debug.Log("downArrowKeyPressed = " + downArrowKeyPressed);
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow) && rightArrowKeyPressed && downArrowKeyPressed)
            {
                leftArrowKeyPressed = true;
                spriteRenderer.sprite = arrowKeyPressArray[3];
                //Debug.Log("leftArrowKeyPressed = " + leftArrowKeyPressed);
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow) && rightArrowKeyPressed && downArrowKeyPressed && leftArrowKeyPressed)
            {
                upArrowKeyPressed = true;
                spriteRenderer.sprite = arrowKeyPressArray[0];
                //Debug.Log("upArrowKeyPressed = " + upArrowKeyPressed);
            }
            else if(rightArrowKeyPressed && downArrowKeyPressed && leftArrowKeyPressed && upArrowKeyPressed)
            {
                fishingSlider.value += playerStrength;
                ResetArrowPressed();
                Debug.Log("RESET ARROW RIGHT SIDE");
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                leftArrowKeyPressed = true;
                spriteRenderer.sprite = arrowKeyPressArray[1];
                //Debug.Log("leftArrowKeyPressed = " + leftArrowKeyPressed);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow) && leftArrowKeyPressed)
            {
                downArrowKeyPressed = true;
                spriteRenderer.sprite = arrowKeyPressArray[0];
                //Debug.Log("downArrowKeyPressed = " + downArrowKeyPressed);
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow) && leftArrowKeyPressed && downArrowKeyPressed)
            {
                rightArrowKeyPressed = true;
                spriteRenderer.sprite = arrowKeyPressArray[3];
                //Debug.Log("rightArrowKeyPressed = " + rightArrowKeyPressed);
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow) && leftArrowKeyPressed && downArrowKeyPressed && rightArrowKeyPressed)
            {
                upArrowKeyPressed = true;
                spriteRenderer.sprite = arrowKeyPressArray[2];
                //Debug.Log("upArrowKeyPressed = " + upArrowKeyPressed);
            }
            else if(rightArrowKeyPressed && downArrowKeyPressed && leftArrowKeyPressed && upArrowKeyPressed)
            {
                fishingSlider.value += playerStrength;
                ResetArrowPressed();
                Debug.Log("RESET ARROW LEFT SIDE");
            }
        }

        if(fishingSlider.value > 99.99f)
        {
            Debug.Log("Success! Player has caught the fish!");
            gameManagerScript.isFishCaught1 = false;
            gameManagerScript.isFishCaught2 = false;
            gameManagerScript.isFishCaught3 = false;
            gameManagerScript.myAudioSource.Stop();
            gameManagerScript.myAudioSource.PlayOneShot(gameManagerScript.musicList[2], 0.7f);
            gameManagerScript.SpawnFish();
            gameManagerScript.JumpPlayer(); 
            selfWholeFishingGame.SetActive(false);
        }
        else if(fishingSlider.value < 0.01f)
        {
            Debug.Log("Failure! Player has lost the fish.");
            gameManagerScript.isFishCaught1 = false;
            gameManagerScript.isFishCaught2 = false;
            gameManagerScript.isFishCaught3 = false;
            selfWholeFishingGame.SetActive(false);
        }

        fishingSlider.value -= Time.deltaTime * fishDifficulty;
        //tempHPvalueText.text = fishingSlider.value.ToString("#.##");

        tempRandomIntForDirection = Random.Range(0.0f, 50.0f);
        //Debug.Log("randomInt = " + tempRandomIntForDirection);
        if(tempRandomIntForDirection > 49.98f)
        {
            fishRight = !fishRight;
            Debug.Log("fishRight is now: " + fishRight);
            if(fishRight)
            {
                gameManagerScript.myAudioSource.PlayOneShot(gameManagerScript.musicList[5], 0.7f);
                spriteRenderer.sprite = arrowKeyPressArray[0];
                arrowDirectionSprite.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                gameManagerScript.myAudioSource.PlayOneShot(gameManagerScript.musicList[5], 0.7f);
                spriteRenderer.sprite = arrowKeyPressArray[2];
                arrowDirectionSprite.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }        
    }

    private void ResetArrowPressed()
    {
        downArrowKeyPressed = false;
        leftArrowKeyPressed = false;
        upArrowKeyPressed = false;
        rightArrowKeyPressed = false;
    }

    private void RandomDirectionBeginning()
    {
        int tempRandomInt = Random.Range(0, 10);
        Debug.Log("tempRandomInt Beginning: " + tempRandomInt);
        if(tempRandomInt > 5)
        {
            fishRight = false;
            spriteRenderer.sprite = arrowKeyPressArray[2];
            arrowDirectionSprite.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    public void SetHealth(float playerHealth)
    {
        fishingSlider.value = playerHealth;
    }
}
