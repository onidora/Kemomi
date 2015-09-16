﻿using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    //左手操作に使う変数
    public Vector2 leftDirection;
    Vector2 basePoint;
    bool leftAction = false;
    //右手操作に使う変数
    Vector2 startPos;
    Vector2 endPos;
    
    void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.position.x < Screen.width * 0.5)
            {
                leftCon(touch);
                Debug.Log("touch left");
            }

            if (touch.position.x >= Screen.width * 0.5)
            {
                rightCon(touch);
                Debug.Log("touch right");
            }

        }
    }

    public void leftCon(Touch touch)
    {
        if (leftAction == false)
        {
            basePoint = touch.position;
            leftAction = true;
        }

        if (leftAction)
        {
            leftDirection = (touch.position - basePoint).normalized;
            if (touch.phase == TouchPhase.Ended)
            {
                leftAction = false;
                leftDirection = Vector2.zero;
                Debug.Log("TouchPhase.Ended");
            }

        }
    }

    public void rightCon(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                startPos = touch.position;
                //					prevPos = touch.position;
                break;

            case TouchPhase.Moved:
                //					gameObject.GetComponent<Rigidbody2D>().velocity = touch.position - prevPos;
                //					prevPos = touch.position;
                break;

            case TouchPhase.Ended:
                endPos = touch.position;
                Vector2 direction = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);
                float radian = Mathf.Atan2(direction.y, direction.x);
                Debug.Log("magnitude : " + direction.magnitude);
                if (direction.magnitude > 8) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(4 * Mathf.Cos(radian), 4 * Mathf.Sin(radian));
                break;

        }
    }

}