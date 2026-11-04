// using UnityEngine;

// public class CameraNavigationController : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }


// using System.Collections;
// using UnityEngine;

// public class CameraNavigationController : MonoBehaviour
// {
//     [Header("References")]
//     public Transform cameraTransform;
//     public FreeFlyCamera freeFlyCamera;
//     public PlanetSelectionManager selectionManager;

//     [Header("Fly To Settings")]
//     public float flyDistance = 15f;
//     public float flyHeight = 3f;
//     public float flyDuration = 2f;

//     [Header("Follow Settings")]
//     public float followDistance = 15f;
//     public float followHeight = 3f;
//     public float followSmoothness = 5f;

//     private Transform followTarget;
//     private Vector3 followOffset;
//     private Vector3 velocity;

//     private Coroutine flyCoroutine;
//     private bool isFollowing;

//     public void FlyToSelected()
//     {
//         if (!TryGetSelectedBody(out Transform target))
//             return;

//         StopCurrentMovement();

//         if (freeFlyCamera != null)
//             freeFlyCamera.enabled = false;

//         flyCoroutine = StartCoroutine(FlyToTarget(target));
//     }

//     public void FollowSelected()
//     {
//         if (!TryGetSelectedBody(out Transform target))
//             return;

//         StopCurrentMovement();

//         if (freeFlyCamera != null)
//             freeFlyCamera.enabled = false;

//         followTarget = target;

//         Vector3 direction = -cameraTransform.forward;
//         followOffset = direction * followDistance
//                      + Vector3.up * followHeight;

//         isFollowing = true;
//     }

//     public void StopFollowing()
//     {
//         StopCurrentMovement();

//         if (freeFlyCamera != null)
//             freeFlyCamera.enabled = true;
//     }

//     private void LateUpdate()
//     {
//         if (!isFollowing || followTarget == null)
//             return;

//         Vector3 desiredPosition =
//             followTarget.position + followOffset;

//         cameraTransform.position = Vector3.SmoothDamp(
//             cameraTransform.position,
//             desiredPosition,
//             ref velocity,
//             1f / followSmoothness
//         );

//         Vector3 direction =
//             followTarget.position - cameraTransform.position;

//         if (direction.sqrMagnitude > 0.001f)
//         {
//             Quaternion desiredRotation =
//                 Quaternion.LookRotation(direction);

//             cameraTransform.rotation = Quaternion.Slerp(
//                 cameraTransform.rotation,
//                 desiredRotation,
//                 Time.deltaTime * followSmoothness
//             );
//         }
//     }

//     private IEnumerator FlyToTarget(Transform target)
//     {
//         Vector3 startPosition = cameraTransform.position;
//         Quaternion startRotation = cameraTransform.rotation;

//         Vector3 direction = cameraTransform.forward;

//         Vector3 endPosition =
//             target.position
//             - direction * flyDistance
//             + Vector3.up * flyHeight;

//         Quaternion endRotation = Quaternion.LookRotation(
//             target.position - endPosition
//         );

//         float elapsed = 0f;

//         while (elapsed < flyDuration)
//         {
//             elapsed += Time.deltaTime;

//             float t = Mathf.Clamp01(elapsed / flyDuration);
//             float smoothT = Mathf.SmoothStep(0f, 1f, t);

//             cameraTransform.position = Vector3.Lerp(
//                 startPosition, endPosition, smoothT
//             );

//             cameraTransform.rotation = Quaternion.Slerp(
//                 startRotation, endRotation, smoothT
//             );

//             yield return null;
//         }

//         cameraTransform.position = endPosition;
//         cameraTransform.rotation = endRotation;

//         flyCoroutine = null;

//         if (freeFlyCamera != null)
//             freeFlyCamera.enabled = true;
//     }

//     private bool TryGetSelectedBody(out Transform target)
//     {
//         target = null;

//         if (selectionManager == null ||
//             selectionManager.selectedBody == null ||
//             cameraTransform == null)
//         {
//             Debug.LogWarning(
//                 "Camera navigation references or selected body are missing."
//             );
//             return false;
//         }

//         target = selectionManager.selectedBody;
//         return true;
//     }

//     private void StopCurrentMovement()
//     {
//         isFollowing = false;
//         followTarget = null;
//         velocity = Vector3.zero;

//         if (flyCoroutine != null)
//         {
//             StopCoroutine(flyCoroutine);
//             flyCoroutine = null;
//         }
//     }
// }










using System.Collections;
using UnityEngine;

public class CameraNavigationController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;
    public FreeFlyCamera freeFlyCamera;
    public PlanetSelectionManager selectionManager;

    [Header("Fly To Settings")]
    public float flyDistance = 15f;
    public float flyHeight = 3f;
    public float flyDuration = 2f;

    private Coroutine flyCoroutine;

    public void FlyToSelected()
    {
        if (!TryGetSelectedBody(out Transform target))
            return;

        StopCurrentMovement();

        freeFlyCamera.ClearFollowTarget();
        freeFlyCamera.enabled = false;

        flyCoroutine = StartCoroutine(FlyToTarget(target));
    }

    public void FollowSelected()
    {
        if (!TryGetSelectedBody(out Transform target))
            return;

        StopCurrentMovement();

        freeFlyCamera.enabled = true;
        freeFlyCamera.SetFollowTarget(target);
    }

    public void StopFollowing()
    {
        StopCurrentMovement();

        if (freeFlyCamera != null)
        {
            freeFlyCamera.ClearFollowTarget();
            freeFlyCamera.enabled = true;
        }
    }

    private IEnumerator FlyToTarget(Transform target)
    {
        Vector3 startPosition = cameraTransform.position;
        Quaternion startRotation = cameraTransform.rotation;

        Vector3 direction = cameraTransform.forward;

        Vector3 endPosition =
            target.position
            - direction * flyDistance
            + Vector3.up * flyHeight;

        Quaternion endRotation = Quaternion.LookRotation(
            target.position - endPosition
        );

        float elapsed = 0f;

        while (elapsed < flyDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / flyDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            cameraTransform.position = Vector3.Lerp(
                startPosition, endPosition, smoothT
            );

            cameraTransform.rotation = Quaternion.Slerp(
                startRotation, endRotation, smoothT
            );

            yield return null;
        }

        cameraTransform.position = endPosition;
        cameraTransform.rotation = endRotation;

        flyCoroutine = null;

        freeFlyCamera.enabled = true;
    }

    private bool TryGetSelectedBody(out Transform target)
    {
        target = null;

        if (selectionManager == null ||
            selectionManager.selectedBody == null ||
            cameraTransform == null ||
            freeFlyCamera == null)
        {
            Debug.LogWarning(
                "Camera navigation references or selected body are missing."
            );

            return false;
        }

        target = selectionManager.selectedBody;
        return true;
    }

    private void StopCurrentMovement()
    {
        if (flyCoroutine != null)
        {
            StopCoroutine(flyCoroutine);
            flyCoroutine = null;
        }
    }
}
