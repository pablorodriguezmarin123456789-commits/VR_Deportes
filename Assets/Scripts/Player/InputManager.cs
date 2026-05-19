using Oculus.Interaction.Locomotion;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Singleton;
    [SerializeField] FirstPersonLocomotor locomotor;
    [SerializeField] Oculus.Interaction.Locomotion.CharacterController characterController;
    [SerializeField] GameObject slideInteractor;
    [SerializeField] GameObject tpInteractor;
    [SerializeField] GameObject tunnelingObj;
    [SerializeField] TurnerEventBroadcaster turnerEventBroadcaster;

    public bool teleport;
    public bool turner;
    public bool tunneling;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        SlideTeleportSwapper();
        TurnSmothSwap();
        Tunneling();

    }

    public void TurnSmothSwap()
    {
        if (turner)
            turnerEventBroadcaster.TurnMethod = TurnerEventBroadcaster.TurnMode.Snap;
        if (!turner)
            turnerEventBroadcaster.TurnMethod = TurnerEventBroadcaster.TurnMode.Smooth;
    }

    public void Tunneling()
    {
        if (!turner && tunneling)
        {
            tunnelingObj.SetActive(true);
        }
        if (turner || !turner && !tunneling)
        {
            tunnelingObj.SetActive(false);
        }
    }

    public void SlideTeleportSwapper()
    {
        slideInteractor.SetActive(!teleport);
        tpInteractor.SetActive(teleport);
        if (!teleport && characterController.TryGround(1))
            locomotor.EnableMovement();
        else 
            locomotor.DisableMovement();
    }
}
