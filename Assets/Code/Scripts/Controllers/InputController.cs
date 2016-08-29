using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    public static InputController obj;

    public const float ZOOM_SPEED = 4;
    public const float CAMERA_MIN_VIEW = 2;
    public const float CAMERA_MAX_VIEW = 30;

    Tile MouseOverTile = null;
    Vector3 MousePositionOnDragStart = Vector3.zero;
    Vector3 CameraPositionOnDragStart = Vector3.zero;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        Vector3 mousePosition = Input.mousePosition;

        if (!screenRect.Contains(mousePosition))
        {
            //Mouse is off-screen, do not do any processing.
            if (MouseOverTile != null)
            {
                MouseOverTile.OnHoverOut();
                MouseOverTile = null;
            }
            return;
        }

        Vector3 mousePos = camera.ScreenToWorldPoint(mousePosition);
        mousePos.z = 0;
        Tuple<uint, uint> worldPos = WorldController.obj.WorldCoordsToGameCoords(mousePos);
        if (worldPos != null)
        {
            //Hovering over tiles
            Tile tile = WorldController.obj.world[worldPos.First, worldPos.Second];

            if (tile != MouseOverTile)
            {
                //Hovered mouse onto another tile.
                if (MouseOverTile != null)
                {
                    MouseOverTile.OnHoverOut();
                }

                MouseOverTile = tile;

                if (MouseOverTile != null)
                {
                    MouseOverTile.OnHoverIn();
                }
            }

            if (tile != null)
            {
                tile.OnHoverOver();
            }
        }
        else
        {
            //Hovered out of world map
            if (MouseOverTile != null)
            {
                MouseOverTile.OnHoverOut();
            }

            MouseOverTile = null;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Left click
            if (MouseOverTile != null)
            {
                MouseOverTile.OnPrimaryInteraction();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            //Right click
            if (MouseOverTile != null)
            {
                MouseOverTile.OnSecondaryInteraction();
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            //Middle Click (start dragging)
            MousePositionOnDragStart = mousePosition;
            CameraPositionOnDragStart = camera.transform.position;
        }

        if (Input.GetMouseButton(2))
        {
            //Middle mouse button down
            //Screen dragging
            //The constant axis in the game is the vertical one, so the viewport coordinates for width need to be adjusted based on the screen's aspect ration
            //to retain pixel perfect dragging.
            float screenAspectRatio = Screen.width / (float)Screen.height;
            Vector3 cameraMovement = 2 * camera.orthographicSize * camera.ScreenToViewportPoint(MousePositionOnDragStart - mousePosition);
            cameraMovement.x *= screenAspectRatio;
            cameraMovement.z = 0;
            camera.transform.position = CameraPositionOnDragStart + cameraMovement;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            float amount = camera.orthographicSize * (Input.mouseScrollDelta.y * Time.deltaTime * ZOOM_SPEED);
            float targetOrthographicSize = camera.orthographicSize - amount;
            targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, CAMERA_MIN_VIEW, CAMERA_MAX_VIEW);
            amount = camera.orthographicSize - targetOrthographicSize;

            // Calculate how much we will have to move towards the target position
            float multiplier = (1.0f / camera.orthographicSize * amount);

            // Move camera if zooming in and not already fully zoomed in.
            if (Input.mouseScrollDelta.y > 0 && amount > 0)
            {
                Vector3 cameraMovementVector = (mousePos - camera.transform.position) * multiplier;
                cameraMovementVector.z = 0;
                camera.transform.Translate(cameraMovementVector);
            }

            // Zoom camera
            camera.orthographicSize = targetOrthographicSize;

            // Limit zoom
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, CAMERA_MIN_VIEW, CAMERA_MAX_VIEW);

        }
    }
}
