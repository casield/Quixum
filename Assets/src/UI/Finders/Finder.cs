using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    private Sprite sprite;
    public GameObject findObject;

    public Vector3 padding = new Vector3();

    public bool isOutside { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Sprite>();

    }

    public bool IsTargetVisible()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(findObject.transform.position);
        bool isTargetVisible = screenPosition.z > 0 && screenPosition.x > 0 && screenPosition.x < Screen.width && screenPosition.y > 0 && screenPosition.y < Screen.height;
        return isTargetVisible;
    }


    private void FindObject()
    {
        if (findObject != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(findObject.transform.position+padding);
            Vector3 screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
            Vector2 screenBounds = new Vector3(Screen.width, Screen.height) /2.1f;


            if (IsTargetVisible())
            {
              
               
            }
            else
            {
                screenPosition -= screenCentre;

                // When the targets are behind the camera their projections on the screen (WorldToScreenPoint) are inverted,
                // so just invert them.
                if (screenPosition.z < 0)
                {
                    screenPosition *= -1;
                }

                // Angle between the x-axis (bottom of screen) and a vector starting at zero(bottom-left corner of screen) and terminating at screenPosition.
                float angle = Mathf.Atan2(screenPosition.y, screenPosition.x);
                // Slope of the line starting from zero and terminating at screenPosition.
                float slope = Mathf.Tan(angle);

                // Two point's line's form is (y2 - y1) = m (x2 - x1) + c, 
                // starting point (x1, y1) is screen botton-left (0, 0),
                // ending point (x2, y2) is one of the screenBounds,
                // m is the slope
                // c is y intercept which will be 0, as line is passing through origin.
                // Final equation will be y = mx.
                if (screenPosition.x > 0)
                {
                    // Keep the x screen position to the maximum x bounds and
                    // find the y screen position using y = mx.
                    screenPosition = new Vector3(screenBounds.x, screenBounds.x * slope, 0);
                }
                else
                {
                    screenPosition = new Vector3(-screenBounds.x, -screenBounds.x * slope, 0);
                }
                // Incase the y ScreenPosition exceeds the y screenBounds 
                if (screenPosition.y > screenBounds.y)
                {
                    // Keep the y screen position to the maximum y bounds and
                    // find the x screen position using x = y/m.
                    screenPosition = new Vector3(screenBounds.y / slope, screenBounds.y, 0);
                }
                else if (screenPosition.y < -screenBounds.y)
                {
                    screenPosition = new Vector3(-screenBounds.y / slope, -screenBounds.y, 0);
                }
                // Bring the ScreenPosition back to its original reference.
                screenPosition += screenCentre;
            }

            screenPosition = new Vector3(screenPosition.x,screenPosition.y,0);


            transform.position = screenPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindObject();
    }
}
