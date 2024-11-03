using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class VRControllerExample : MonoBehaviour
{
    // Prefab
    public GameObject hookOBJ;

    // Movement
    public float speed = 10.0f;

    // Camera
    private Transform cam;

    private CharacterController characterController;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        // Optionally lock the cursor if necessary
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = cam.TransformDirection(direction);
        direction.y = 0; // No vertical movement
        characterController.Move(direction * speed * Time.deltaTime);

        // Camera and rotation using Oculus controller inputs
        Vector2 primaryAxis = Vector2.zero;

        // Get the input from the right controller
        InputDevice rightController = GetDevice(XRNode.RightHand);
        if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out primaryAxis))
        {
            float rotationY = primaryAxis.x * speed; // Adjust this multiplier for sensitivity
            transform.Rotate(0, rotationY, 0);
        }

        // Hook action using the primary button on the right controller
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonPressed) && buttonPressed)
        {
            Hook();
        }
    }

    private InputDevice GetDevice(XRNode node)
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(node, devices);
        return devices.Count > 0 ? devices[0] : new InputDevice();
    }

    void Hook()
    {
        // Creates prefab in front of the camera using camera's rotation
        Vector3 hookPosition = cam.position + cam.forward * 2.0f; // Distance of 2 units
        Instantiate(hookOBJ, hookPosition, cam.rotation);
    }
}
