using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseControl : MonoBehaviour
{
    private Vector2 mPos_Down;
    private Vector2 mPos_Up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mPos_Down = Input.mousePosition;
            Debug.Log(mPos_Down);
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            mPos_Up = Input.mousePosition;
        }

    }
}
