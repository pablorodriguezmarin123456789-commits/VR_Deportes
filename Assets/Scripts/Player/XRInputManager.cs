using UnityEngine;

public class XRInputManager : MonoBehaviour {
    void Update() {
        OVRInput.Update();
        // Handle input logic here
    }
    void FixedUpdate() {
        OVRInput.FixedUpdate();
        // Handle physics-based input here
    }
}
