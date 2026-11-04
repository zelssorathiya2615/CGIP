// using UnityEngine;

// public class FreeFlyCamera
// {
    
// }



// using UnityEngine;

// public class FreeFlyCamera : MonoBehaviour
// {
//     [Header("Movement")]
//     public float moveSpeed = 20f;
//     public float fastMultiplier = 5f;
//     public float lookSensitivity = 2f;

//     [Header("Look Limits")]
//     public float minPitch = -85f;
//     public float maxPitch = 85f;

//     private float yaw;
//     private float pitch;

//     private void Start()
//     {
//         Vector3 angles = transform.eulerAngles;
//         yaw = angles.y;
//         pitch = angles.x;

//         if (pitch > 180f)
//             pitch -= 360f;
//     }

//     private void Update()
//     {
//         // Hold the right mouse button to look around.
//         if (Input.GetMouseButton(1))
//         {
//             yaw += Input.GetAxis("Mouse X") * lookSensitivity;
//             pitch -= Input.GetAxis("Mouse Y") * lookSensitivity;
//             pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

//             transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
//         }

//         // WASD movement, Q/E vertical movement.
//         float speed = moveSpeed;

//         if (Input.GetKey(KeyCode.LeftShift))
//             speed *= fastMultiplier;

//         Vector3 movement =
//             transform.forward * Input.GetAxis("Vertical") +
//             transform.right * Input.GetAxis("Horizontal");

//         if (Input.GetKey(KeyCode.E))
//             movement += Vector3.up;

//         if (Input.GetKey(KeyCode.Q))
//             movement += Vector3.down;

//         transform.position +=
//             movement * speed * Time.deltaTime;
//     }
// }





// using UnityEngine;
// using UnityEngine.InputSystem;

// public class FreeFlyCamera : MonoBehaviour
// {
//     [Header("Movement")]
//     public float moveSpeed = 20f;
//     public float fastMultiplier = 5f;

//     [Header("Look")]
//     public float lookSensitivity = 0.15f;
//     public float minPitch = -85f;
//     public float maxPitch = 85f;

//     private float yaw;
//     private float pitch;

//     private void Start()
//     {
//         Vector3 angles = transform.eulerAngles;
//         yaw = angles.y;
//         pitch = angles.x;

//         if (pitch > 180f)
//             pitch -= 360f;
//     }

//     private void Update()
//     {
//         if (Keyboard.current == null || Mouse.current == null)
//             return;

//         // Hold the right mouse button to look around.
//         if (Mouse.current.rightButton.isPressed)
//         {
//             Vector2 mouseDelta = Mouse.current.delta.ReadValue();

//             yaw += mouseDelta.x * lookSensitivity;
//             pitch -= mouseDelta.y * lookSensitivity;
//             pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

//             transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
//         }

//         // WASD movement.
//         Vector3 movement = Vector3.zero;

//         if (Keyboard.current.wKey.isPressed)
//             movement += transform.forward;

//         if (Keyboard.current.sKey.isPressed)
//             movement -= transform.forward;

//         if (Keyboard.current.dKey.isPressed)
//             movement += transform.right;

//         if (Keyboard.current.aKey.isPressed)
//             movement -= transform.right;

//         if (Keyboard.current.eKey.isPressed)
//             movement += Vector3.up;

//         if (Keyboard.current.qKey.isPressed)
//             movement -= Vector3.up;

//         movement = Vector3.ClampMagnitude(movement, 1f);

//         float speed = moveSpeed;

//         if (Keyboard.current.leftShiftKey.isPressed ||
//             Keyboard.current.rightShiftKey.isPressed)
//         {
//             speed *= fastMultiplier;
//         }

//         transform.position += movement * speed * Time.deltaTime;
//     }
// }












using UnityEngine;
using UnityEngine.InputSystem;

public class FreeFlyCamera : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 20f;
    public float fastMultiplier = 5f;

    [Header("Look")]
    public float lookSensitivity = 0.15f;
    public float minPitch = -85f;
    public float maxPitch = 85f;

    [Header("Zoom")]
    public float zoomSensitivity = 2f;
    public float minZoomDistance = 3f;
    public float maxZoomDistance = 100f;

    [Header("Follow Mode")]
    public float followDistance = 20f;
    public float panSpeed = 15f;

    private float yaw;
    private float pitch;

    private Transform followTarget;
    private Vector3 panOffset;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;

        yaw = angles.y;
        pitch = angles.x;

        if (pitch > 180f)
            pitch -= 360f;
    }

    private void Update()
    {
        if (Keyboard.current == null || Mouse.current == null)
            return;

        if (followTarget != null)
        {
            HandleFollowInput();
        }
        else
        {
            HandleFreeFlyInput();
        }
    }

    private void HandleFreeFlyInput()
    {
        // Right-click mouse rotation.
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            yaw += mouseDelta.x * lookSensitivity;
            pitch -= mouseDelta.y * lookSensitivity;

            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        // WASD and Q/E movement.
        Vector3 movement = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
            movement += transform.forward;

        if (Keyboard.current.sKey.isPressed)
            movement -= transform.forward;

        if (Keyboard.current.dKey.isPressed)
            movement += transform.right;

        if (Keyboard.current.aKey.isPressed)
            movement -= transform.right;

        if (Keyboard.current.eKey.isPressed)
            movement += Vector3.up;

        if (Keyboard.current.qKey.isPressed)
            movement -= Vector3.up;

        movement = Vector3.ClampMagnitude(movement, 1f);

        float speed = GetMovementSpeed();

        transform.position += movement * speed * Time.deltaTime;

        // Mouse-wheel zoom in free mode.
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (Mathf.Abs(scroll) > 0.01f)
        {
            transform.position +=
                transform.forward * scroll * zoomSensitivity * Time.deltaTime;
        }
    }

    private void HandleFollowInput()
    {
        // Right-click to orbit around the selected planet.
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            yaw += mouseDelta.x * lookSensitivity;
            pitch -= mouseDelta.y * lookSensitivity;

            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        // Mouse wheel to zoom.
        float scroll = Mouse.current.scroll.ReadValue().y;

        followDistance -= scroll * zoomSensitivity * Time.deltaTime;

        followDistance = Mathf.Clamp(
            followDistance,
            minZoomDistance,
            maxZoomDistance
        );

        // // WASD and Q/E to pan around the planet.
        // Vector3 pan = Vector3.zero;

        // if (Keyboard.current.wKey.isPressed)
        //     pan += transform.up;

        // if (Keyboard.current.sKey.isPressed)
        //     pan -= transform.up;

        // if (Keyboard.current.dKey.isPressed)
        //     pan += transform.right;

        // if (Keyboard.current.aKey.isPressed)
        //     pan -= transform.right;

        // if (Keyboard.current.eKey.isPressed)
        //     pan += Vector3.up;

        // if (Keyboard.current.qKey.isPressed)
        //     pan -= Vector3.up;

        // pan = Vector3.ClampMagnitude(pan, 1f);

        // panOffset += pan * GetMovementSpeed() * panSpeed / 20f
        //              * Time.deltaTime;


        // WASD movement relative to camera direction.
        Vector3 pan = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
            pan += transform.forward;

        if (Keyboard.current.sKey.isPressed)
            pan -= transform.forward;

        if (Keyboard.current.dKey.isPressed)
            pan += transform.right;

        if (Keyboard.current.aKey.isPressed)
            pan -= transform.right;

        // Q = Up, E = Down.
        if (Keyboard.current.qKey.isPressed)
            pan += Vector3.up;

        if (Keyboard.current.eKey.isPressed)
            pan -= Vector3.up;

        pan = Vector3.ClampMagnitude(pan, 1f);

        panOffset += pan * GetMovementSpeed() * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (followTarget == null)
            return;

        Quaternion orbitRotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 focusPoint = followTarget.position + panOffset;

        Vector3 orbitDirection = orbitRotation * Vector3.back;

        transform.position =
            focusPoint + orbitDirection * followDistance;

        transform.rotation = Quaternion.LookRotation(
            focusPoint - transform.position
        );
    }

    private float GetMovementSpeed()
    {
        float speed = moveSpeed;

        if (Keyboard.current.leftShiftKey.isPressed ||
            Keyboard.current.rightShiftKey.isPressed)
        {
            speed *= fastMultiplier;
        }

        return speed;
    }

    public void SetFollowTarget(Transform target)
    {
        if (target == null)
            return;

        followTarget = target;
        panOffset = Vector3.zero;

        Vector3 angles = transform.eulerAngles;

        yaw = angles.y;
        pitch = angles.x;

        if (pitch > 180f)
            pitch -= 360f;

        followDistance = Mathf.Clamp(
            Vector3.Distance(transform.position, target.position),
            minZoomDistance,
            maxZoomDistance
        );
    }

    public void ClearFollowTarget()
    {
        followTarget = null;
        panOffset = Vector3.zero;
    }
}
