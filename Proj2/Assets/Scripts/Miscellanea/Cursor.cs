using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class Cursor : MonoBehaviour
{

    [SerializeField] private float cursorRadius;
    private bool movedMouseSinceJoistickMoved;
    private Vector3 lastMousePosition;

    private void Start()
    {
        if (cursorRadius <= 0)
            Debug.LogWarning("Cursor with an areaRadius not valid", gameObject);

        if (!Application.isEditor)
            UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = GameManager.Instance.playerManager.getThrowPoint().position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;

        Gizmos.DrawWireSphere(transform.position, cursorRadius);

        if (Application.isPlaying)
            Gizmos.DrawSphere(GetCursorPositionOnWorld(), 0.2f);
    }

    public Vector3 GetCursorPositionOnWorld()
    {
        Vector2 inputRead = new Vector2(Input.GetAxis("Mouse_J X"), Input.GetAxis("Mouse_J Y"));
        if (inputRead == Vector2.zero)
            inputRead = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 cursorPosition = new Vector3(transform.position.x + inputRead.x * cursorRadius, transform.position.y + inputRead .y * cursorRadius, transform.position.z);

        if (cursorPosition == transform.position)
        {
            if (!movedMouseSinceJoistickMoved)
                movedMouseSinceJoistickMoved = lastMousePosition != Input.mousePosition;

            if (movedMouseSinceJoistickMoved)
            {
                lastMousePosition = Input.mousePosition;
                cursorPosition = GameManager.Instance.camera.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                cursorPosition.z = 0;
            }
        }
        else
        {
            movedMouseSinceJoistickMoved = false;
        }

        return Utils.GetClosestPointToCircle(transform.position, cursorRadius, cursorPosition);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(GetCursorPositionOnWorld(), 0.2f);
    }
}
