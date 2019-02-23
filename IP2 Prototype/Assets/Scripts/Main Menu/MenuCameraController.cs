using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    public GameObject settingsCameraPoint;
    public GameObject quitCameraPoint;

    public void MoveToSettings()
    {
        StartCoroutine(LerpToPosition(this.transform.position, settingsCameraPoint.transform.position, 1));
        StartCoroutine(LerpToRotation(this.transform.rotation, settingsCameraPoint.transform.rotation, 1));
    }

    public void MoveToQuit()
    {
        StartCoroutine(LerpToPosition(this.transform.position, quitCameraPoint.transform.position, 1));
        StartCoroutine(LerpToRotation(this.transform.rotation, quitCameraPoint.transform.rotation, 1));
    }


    /*
    ==================================================
    Lerping Methods
    ==================================================
    */
    private IEnumerator LerpToPosition(Vector3 currentPosition, Vector3 targetPosition, float maxTime)
    {
        float t = 0;

        while (t < 1)
        {
            t += (Time.deltaTime / maxTime);
            
            float sStart = (t * t);
            float sEnd = (1 - (1 - t) * (1 - t));
            float i = Mathf.Lerp(sStart, sEnd, t);

            this.transform.position = Vector3.Lerp(currentPosition, targetPosition, i);

            if (t >= 1)
            {
                this.transform.position = targetPosition;
            }

            yield return null;
        }
    }

    private IEnumerator LerpToRotation(Quaternion currentRotation, Quaternion targetRotation, float maxTime)
    {
        float t = 0;

        while (t < 1)
        {
            t += (Time.deltaTime / maxTime);

            float sStart = (t * t);
            float sEnd = (1 - (1 - t) * (1 - t));
            float i = Mathf.Lerp(sStart, sEnd, t);

            this.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, i);

            if (t >= 1)
            {
                this.transform.rotation = targetRotation;
            }

            yield return null;
        }
    }
}
