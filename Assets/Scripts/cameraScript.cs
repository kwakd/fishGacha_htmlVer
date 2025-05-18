using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class cameraScript : MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI fishAlertText;
    public GameObject gachaText;

    public int cameraSceneValue;
    private float cameraSpeedValue = 0.35f;

    private gameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = FindObjectOfType<gameManager>();
        cameraSceneValue = 1; //fishing
        instructionText.text = ("'M' = un/mute sound" + "\n" + "'Y' = roll fish gacha ($50)" + "\n" + "'U' = $1000" + "\n" + "'W' = action button" + "\n" + "'1' = fishing scene" + "\n" + "'2' = fish collection");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraSceneValue = 1;
            //transform.position = new Vector3(0, 0, -10);
            gachaText.SetActive(true);
            instructionText.text = ("'M' = un/mute sound" + "\n" + "'Y' = roll fish gacha ($50)" + "\n" + "'U' = $1000" + "\n" + "'W' = action button" + "\n" + "'1' = fishing scene" + "\n" + "'2' = fish collection");
            if(gameManagerScript.sellMenuTrue)
            {
                gameManagerScript.PauseMenuToggle();
            }
            Debug.Log("user has pressed 1");
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraSceneValue = 2;
            gachaText.SetActive(false);
            instructionText.text = ("'M' = mute sound" + "\n" + "'P' = fish collection menu" + "\n" + "'O' = sell fish" + "\n" + "'1' = fishing scene" + "\n" + "'2' = fish collection");
            //transform.position = new Vector3(0, -25, -10);
            Debug.Log("user has pressed 2");
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && gameManagerScript.playerHasBeatenGame100)
        {
            cameraSceneValue = 3;
            gachaText.SetActive(false);
            //transform.position = new Vector3(0, -25, -10);
            Debug.Log("user has pressed 3");
        }

        if(cameraSceneValue == 1)
        {
            if(transform.position.y < 0f)
            {
                transform.position += new Vector3(0, cameraSpeedValue, 0);
            }
        }
        if(cameraSceneValue == 2 && !gameManagerScript.isFishCaught2 && !gameManagerScript.isFishCaught3)
        {
            if(transform.position.y > -25f)
            {
                transform.position -= new Vector3(0, cameraSpeedValue, 0);
            }
            if(transform.position.y < -25f)
            {
                transform.position += new Vector3(0, cameraSpeedValue, 0);
            }
        }
        if(cameraSceneValue == 3 && !gameManagerScript.isFishCaught2 && !gameManagerScript.isFishCaught3 && gameManagerScript.playerHasBeatenGame100)
        {
            if(transform.position.y > -50f)
            {
                transform.position -= new Vector3(0, cameraSpeedValue, 0);
            }
        }
        
    }
}
