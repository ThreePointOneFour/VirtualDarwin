using UnityEngine;
using KitchenSink;

public class CameraController_script : MonoBehaviour {

    public float zoomSpeed = 1f;
    public float panSpeed = 1f;
    public Vector2 panLimit;
    public Vector2 zoomLimit;

    public Camera Camera;

    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const string ZoomAxis = "Mouse ScrollWheel";


	// Update is called once per frame
	void FixedUpdate () {

        Move();
        Zoom();

    }

    public void Move() {
        Vector3 pos = transform.position;

        float xInput = Input.GetAxis(HorizontalAxis);
        float yInput = Input.GetAxis(VerticalAxis);

        pos.x += xInput * panSpeed;
        pos.y += yInput * panSpeed;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
    }

    public void Zoom() {
        float zoom = Camera.orthographicSize;
        float zoomInput = Input.GetAxis(ZoomAxis);

        zoom -= zoomInput * 10f *zoomSpeed;

        zoom = Mathf.Clamp(zoom, zoomLimit.x, zoomLimit.y);

        Camera.orthographicSize = zoom;
    }
}
