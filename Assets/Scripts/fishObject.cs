using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishObject : MonoBehaviour
{

    private gameManager gameManagerScript;

    //private float fishMoveSpeed = 1.0f;

    public Vector2 decisionTime = new Vector2(1, 4);
    private float decisionTimeCount = 0;
    private bool isFishFacingRight;

    private int currentMoveDirection;
    private int fishMoveDirection;

    private Rigidbody2D thisFishRigidbody;
    private SpriteRenderer thisFishSpriteRenderer;

    public Sprite[] fishSprites;
    //private Transform thisFishTransform;

    public int tempFishColor;
    public int totalFishCost;
    public int fishCostMin;
    public int fishCostMax;

    public string fishName;

    // Start is called before the first frame update
    void Start()
    {
        thisFishRigidbody = GetComponent<Rigidbody2D>();
        thisFishSpriteRenderer = GetComponent<SpriteRenderer>();
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        tempFishColor = Mathf.FloorToInt(Random.Range(0, fishSprites.Length));
        thisFishSpriteRenderer.sprite = fishSprites[tempFishColor];
        fishName = thisFishSpriteRenderer.sprite.name;
        //Debug.Log("FISHNAME: " + fishName);

        totalFishCost = Random.Range(fishCostMin, fishCostMax);

        isFishFacingRight = Random.value < 0.5f;
        if(!isFishFacingRight)
        {
            FlipFishSprite();
        }
        gameManagerScript = FindObjectOfType<gameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if (decisionTimeCount > 0) 
        {
            decisionTimeCount -= Time.deltaTime;
            //Debug.Log(decisionTimeCount);
        }
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }
    }

    void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        currentMoveDirection = Mathf.FloorToInt(Random.Range(0, 3)); //0, 1, 2
        fishMoveDirection = Mathf.FloorToInt(Random.Range(0, 8)); //0, 1, 2, 3, 4 5 6 7
        switch(fishMoveDirection)
        {
            case 0:
                //Debug.Log("fish will go RIGHT");
                if(!isFishFacingRight)
                {
                    FlipFishSprite();
                    isFishFacingRight = true;
                }
                thisFishRigidbody.velocity = new Vector2(transform.localScale.x * currentMoveDirection, 0);
                break;
            case 1:
                //Debug.Log("fish will go LEFT");
                if(isFishFacingRight)
                {
                    FlipFishSprite();
                    isFishFacingRight = false;
                }
                thisFishRigidbody.velocity *= -1;
                thisFishRigidbody.velocity = new Vector2(transform.localScale.x * currentMoveDirection, 0);
                break;
            case 2:
                //Debug.Log("fish will go UP");
                thisFishRigidbody.velocity = new Vector2(0, transform.localScale.y * currentMoveDirection);
                break;
            case 3:
                //Debug.Log("fish will go DOWN");
                thisFishRigidbody.velocity = new Vector2(0, transform.localScale.y * -currentMoveDirection);
                break;
            case 4: case 5: case 6: case 7:
                //Debug.Log("fish will do NOTHING");
                thisFishRigidbody.velocity = new Vector2(0, 0);
                break;
            default:
                Debug.Log("fish DEFAULT SHOULD NOT HIT THIS");
                break;
        }

    }

    public void DeleteFish()
    {
        gameManagerScript.fishList.Remove(this.gameObject);
        Object.Destroy(this.gameObject);
    }

    void FlipFishSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    void OnDisable()
    {
        gameManagerScript.playerMoneyCount += totalFishCost;
        //Debug.Log("playerMoney: " + gameManagerScript.playerMoneyCount);
    }

}
