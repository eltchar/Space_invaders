using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public SpriteRenderer border;

    // Use this for initialization
    void Start()
    {
        float currentRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = (border.bounds.size.x / border.bounds.size.y)*1.2f;

        if (currentRatio >= targetRatio)
        {
            Camera.main.orthographicSize = border.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / currentRatio;
            Camera.main.orthographicSize = border.bounds.size.y / 2 * differenceInSize;
        }
    }
}
