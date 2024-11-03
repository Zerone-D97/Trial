using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Throwable : MonoBehaviour
{
    private List<Vector3> trackingPos = new List<Vector3>();
    public float velocity = 1000f;
    private bool pickedUp = false;
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the throwable object.");
        }

        // Add event listeners for grab and release
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        pickedUp = true;
        trackingPos.Clear(); // Clear previous tracking positions
    }

    private void Update()
    {
        if (pickedUp)
        {
            rb.useGravity = false;

            // Use the interactor's transform to position the object
            Transform interactorTransform = grabInteractable.selectingInteractor?.transform;
            if (interactorTransform != null)
            {
                transform.position = interactorTransform.position;
                transform.rotation = interactorTransform.rotation;

                if (trackingPos.Count > 15)
                {
                    trackingPos.RemoveAt(0);
                }
                trackingPos.Add(transform.position);
            }
        }
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        pickedUp = false;

        if (trackingPos.Count > 1)
        {
            Vector3 direction = (trackingPos[trackingPos.Count - 1] - trackingPos[0]).normalized; // Normalize
            rb.AddForce(direction * velocity);
        }

        rb.useGravity = true;
        rb.isKinematic = false;
        trackingPos.Clear(); // Clear tracking positions after release
    }
}
