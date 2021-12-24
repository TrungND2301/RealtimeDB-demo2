using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCircle : MonoBehaviour
{
    public RectTransform CircleImage;
    [Tooltip("Seconds to complete a 360 rotation")]
    public float RotateTimer = 0.5f;

    private const int PartsCount = 8;
    private float timer;

    void Start()
    {
        timer = RotateTimer / PartsCount;
        CircleImage.localEulerAngles = Vector3.zero;
    }

    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                float angle = CircleImage.localEulerAngles.z - 360f / PartsCount;
                if (angle < 0)
                    angle += 360;

                CircleImage.localEulerAngles = new Vector3(0f, 0f, angle);

                timer = RotateTimer / PartsCount;
            }
        }
    }
}
