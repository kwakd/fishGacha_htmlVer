using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationScript : MonoBehaviour
{
    private Animator playerAnimator;

    public bool isPlayerGrounded;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetBool("isPlayerGrounded", isPlayerGrounded);
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == ("Ground")) 
        {
            isPlayerGrounded = true;
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isPlayerGrounded = false;
        }
    }

}
