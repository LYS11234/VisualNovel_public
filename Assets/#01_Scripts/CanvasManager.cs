using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CanvasManager : MonoBehaviour
{
    private Animator anim;
    private Vector2 mPos_Down;
    private Vector2 mPos_Up;

   




    private bool isDown;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0)
        {
            mPos_Down = Vector2.zero;
            mPos_Up = Vector2.zero;
        }
        else
        { 
            if (Input.GetMouseButtonDown(0))
            {
                mPos_Down = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                mPos_Up = Input.mousePosition;
                if (anim.GetBool("Appear"))
                {
                    if (mPos_Down.y >= (mPos_Up.y + 100))
                    {
                        anim.SetBool("Appear", false);
                        mPos_Up = Vector2.zero;
                        mPos_Down = Vector2.zero;
                    }
                }
                else if (!anim.GetBool("Appear"))
                {
                    anim.SetBool("Appear", true);
                    mPos_Up = Vector2.zero;
                    mPos_Down = Vector2.zero;
                }

            }
        }
        
    }
}
