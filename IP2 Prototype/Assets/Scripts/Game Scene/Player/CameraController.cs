using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Variables")]
    //Default Movement Variables
    public float cameraRotationSpeed = 180;
    public Transform defaultPosition;
    private bool isInTransition = false;
    public Vector2 maxOrbitRange;
    public Vector2 minOrbitRange;
    private Vector2 currentOrbitRange;

    //Zooming In Variables
    [Header("Zooming In Varables")]
    public LerpType zoomType;
    public enum LerpType {LINEAR, SMOOTHSTART, SMOOTHEND, SMOOTHSTEP};
    public float zoomTime = 2;
    private ObjectController selectedObject;
    private Transform targetPosition;

    //Other Control Variables
    private bool canInput = false;

    void Start()
    {
        currentOrbitRange = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInTransition)
        {
            if (selectedObject != null)
            {
                ObjectControls();
            }
            else
            {
                EnvironmentControls();
            }
        }

        OrbitCamera();
    }


    /*
    ============================================================
    Controlling Player Input Capabilities
    ============================================================
    */
    public void SetPlayerInputState(bool newInputState)
    {
        canInput = newInputState;
    }


    /*
    ============================================================
    Handles When The Player Is Looking Round The Environment
    ============================================================
    */
    private void EnvironmentControls()
    {
        if (canInput)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Selectable Object")
                {
                    //Checking if the cursor is hovering over an interactable object
                    hit.transform.gameObject.GetComponent<ObjectController>().SetHighlightVisibility(true);

                    //Input for selecting appliances
                    if (Input.GetMouseButtonDown(0))
                    {
                        //Getting What Object The Player Has Clicked On
                        Debug.Log("Selected Object: " + hit.transform.name);
                        selectedObject = hit.transform.gameObject.GetComponent<ObjectController>();
                        targetPosition = selectedObject.SelectedObject();

                        StartCoroutine(MoveCamera(targetPosition.position, targetPosition.rotation, zoomTime));
                    }
                }
            }
        }
    }

    /*
    ============================================================
    Handles When The Player Has Selected An Object
    ============================================================
    */
    private void ObjectControls()
    {
        if (canInput)
        {
            //Input for moving back to the center of the room
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Moving Back To Room Center");
                selectedObject.ResetObject();
                selectedObject = null;
                targetPosition = null;

                StartCoroutine(MoveCamera(defaultPosition.position, defaultPosition.rotation, zoomTime));
            }
        }
    }


    /*
    ============================================================
    Handles When The Player Has Selected An Object
    ============================================================
    */
    private void OrbitCamera()
    {
        //Rotating The Camera Based On Mouse Position
        {
            Vector3 newRotation = Vector3.zero;
            Vector2 mousePos = Input.mousePosition;

            mousePos.x = (mousePos.x / Screen.width) * 2 - 1;
            mousePos.y = (mousePos.y / Screen.height) * 2 - 1;

            mousePos.x = Mathf.Clamp(mousePos.x, -1.0f, 1.0f);
            mousePos.y = Mathf.Clamp(mousePos.y, -1.0f, 1.0f);

            //Debug.Log(mousePos);

            newRotation.x += (mousePos.y * -currentOrbitRange.x);
            newRotation.y += (mousePos.x * currentOrbitRange.y);

            Camera.main.transform.eulerAngles = newRotation + this.transform.eulerAngles;
        }
    }

    /*
    ============================================================
    Handles When The Camera Is Moving To & From An Object
    ============================================================
    */
    public IEnumerator MoveCamera(Vector3 targetPosition, Quaternion targetRotation, float moveTime)
    {
        isInTransition = true;

        StartCoroutine(LerpToPosition(this.transform.position, targetPosition, moveTime));
        StartCoroutine(LerpToRotation(this.transform.rotation, targetRotation, moveTime));

        if (selectedObject == null)
        {
            StartCoroutine(RestoreOrbitRange(moveTime));
        }
        else
        {
            StartCoroutine(LimitOrbitRange(moveTime));
        }

        yield return new WaitForSeconds(moveTime);
        isInTransition = false;
    }
    
    private IEnumerator LerpToPosition(Vector3 currentPosition, Vector3 targetPosition, float maxTime)
    {
        float t = 0;

        while (t < 1)
        {
            t += (Time.deltaTime / maxTime);

            float i = CalculateI(t);

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

            float i = CalculateI(t);

            this.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, i);

            if (t >= 1)
            {
                this.transform.rotation = targetRotation;
            }

            yield return null;
        }
    }

    private IEnumerator LimitOrbitRange(float maxTime)
    {
        float t = 0;

        while (t < 1)
        {
            t += (Time.deltaTime / maxTime);

            float i = CalculateI(t);

            currentOrbitRange = Vector2.Lerp(currentOrbitRange, minOrbitRange, i);

            if (t >= 1)
            {
                currentOrbitRange = minOrbitRange;
            }

            yield return null;
        }
    }

    private IEnumerator RestoreOrbitRange(float maxTime)
    {
        float t = 0;

        while (t < 1)
        {
            t += (Time.deltaTime / maxTime);

            float i = CalculateI(t);

            currentOrbitRange = Vector2.Lerp(currentOrbitRange, maxOrbitRange, i);

            if (t >= 1)
            {
                currentOrbitRange = maxOrbitRange;
            }

            yield return null;
        }
    }

    private float CalculateI(float t)
    {
        float i = t;

        switch (zoomType)
        {
            case (LerpType.LINEAR):
                {
                    i = t;
                }
                break;

            case (LerpType.SMOOTHSTART):
                {
                    i = (i * i);
                }
                break;

            case (LerpType.SMOOTHEND):
                {
                    i = (1 - (1 - i) * (1 - i));
                }
                break;

            case (LerpType.SMOOTHSTEP):
                {
                    float sStart = (i * i);
                    float sEnd = (1 - (1 - i) * (1 - i));

                    i = Mathf.Lerp(sStart, sEnd, t);
                }
                break;
        }

        return i;
    }
}
