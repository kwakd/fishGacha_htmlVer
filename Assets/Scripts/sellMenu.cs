using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class sellMenu : MonoBehaviour
{
    private gameManager gameManagerScript;
    public GameObject sellTemplateCopy;

    public int userNum = 1;
    public int numPage = 0;

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        
    }

    // OnEnable is called when the object becomes enabled and active
    void OnEnable()
    {
        GameObject g;
        userNum = 1;
        numPage = 0;

        gameManagerScript = FindObjectOfType<gameManager>();
        
        if(gameManagerScript.fishList.Count < 10)
        {
            for(int i=1; i<=gameManagerScript.fishList.Count; i++)
            {
                g = Instantiate(sellTemplateCopy, transform);

                g.transform.GetChild(0).GetComponent<Text>().text = gameManagerScript.fishList[i-1].GetComponent<SpriteRenderer>().sprite.name;

                g.transform.GetChild(1).GetComponent<Text>().text = gameManagerScript.fishList[i-1].GetComponent<fishObject>().totalFishCost.ToString();

                g.transform.GetChild(2).GetComponent<Image>().sprite = gameManagerScript.fishList[i-1].GetComponent<SpriteRenderer>().sprite;

                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            for(int i=1; i<10; i++)
            {
                g = Instantiate(sellTemplateCopy, transform);

                g.transform.GetChild(0).GetComponent<Text>().text = gameManagerScript.fishList[i-1].GetComponent<SpriteRenderer>().sprite.name;

                g.transform.GetChild(1).GetComponent<Text>().text = gameManagerScript.fishList[i-1].GetComponent<fishObject>().totalFishCost.ToString();

                g.transform.GetChild(2).GetComponent<Image>().sprite = gameManagerScript.fishList[i-1].GetComponent<SpriteRenderer>().sprite;

                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //currently selected fish
        if(gameManagerScript.fishList.Count > 0)
        {
            transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,165,165,255);

            if(Input.GetKeyDown(KeyCode.DownArrow) && userNum < transform.childCount-1 && ((userNum + numPage*9) < gameManagerScript.fishList.Count))
            {
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum++;
                Debug.Log("userNum = " + userNum);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow) && userNum >= transform.childCount-1 && ((userNum + numPage*9) < gameManagerScript.fishList.Count))
            {
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum = 1;
                numPage++;
                for(int i=1; i <= transform.childCount; i++)
                {
                    if((i + numPage*9) <= gameManagerScript.fishList.Count)
                    {
                        transform.GetChild(i).GetChild(0).GetComponent<Text>().text = gameManagerScript.fishList[i-1+(9*numPage)].GetComponent<SpriteRenderer>().sprite.name;

                        transform.GetChild(i).GetChild(1).GetComponent<Text>().text = gameManagerScript.fishList[i-1+(9*numPage)].GetComponent<fishObject>().totalFishCost.ToString();

                        transform.GetChild(i).GetChild(2).GetComponent<Image>().sprite = gameManagerScript.fishList[i-1+(9*numPage)].GetComponent<SpriteRenderer>().sprite;                        
                    }
                    else
                    {
                        transform.GetChild(i).GetChild(0).GetComponent<Text>().text = "";

                        transform.GetChild(i).GetChild(1).GetComponent<Text>().text = "";

                        transform.GetChild(i).GetChild(2).GetComponent<Image>().sprite = null;
                    }
                    
                }
            }

            if(Input.GetKeyDown(KeyCode.UpArrow) && userNum > 1)
            {
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum--;
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow) && userNum <= 1 && numPage > 0)
            {
                transform.GetChild(userNum).GetComponent<Image>().color = new Color32(255,255,255,255);
                userNum = 9;
                numPage--;
                for(int i=1; i <= transform.childCount; i++)
                {
                    if((i + numPage*9) <= gameManagerScript.fishList.Count)
                    {
                        transform.GetChild(i).GetChild(0).GetComponent<Text>().text = gameManagerScript.fishList[i-1+(9*numPage)].GetComponent<SpriteRenderer>().sprite.name;

                        transform.GetChild(i).GetChild(1).GetComponent<Text>().text = gameManagerScript.fishList[i-1+(9*numPage)].GetComponent<fishObject>().totalFishCost.ToString();

                        transform.GetChild(i).GetChild(2).GetComponent<Image>().sprite = gameManagerScript.fishList[i-1+(9*numPage)].GetComponent<SpriteRenderer>().sprite;                        
                    }
                    else
                    {
                        transform.GetChild(i).GetChild(0).GetComponent<Text>().text = "";

                        transform.GetChild(i).GetChild(1).GetComponent<Text>().text = "";

                        transform.GetChild(i).GetChild(2).GetComponent<Image>().sprite = null;
                    }
                    
                }
            }

            if(Input.GetKeyDown(KeyCode.O))
            {
                Destroy(transform.GetChild(userNum).gameObject);
                gameManagerScript.fishList[userNum-1+(9*numPage)].GetComponent<fishObject>().DeleteFish();
                gameManagerScript.myAudioSource.PlayOneShot(gameManagerScript.musicList[0], 0.7f);
                gameManagerScript.PauseMenuToggle();
            }
        }
        else
        {
            Debug.Log("LIST IS EMPTY");
        }
        
    }

    void OnDisable()
    {
        for(int i=1; i<transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
    }
}
