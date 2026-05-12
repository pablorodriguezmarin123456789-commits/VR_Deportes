using Oculus.Interaction.Locomotion;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Singleton;
    [SerializeField] FirstPersonLocomotor locomotor;
    [SerializeField] GameObject slideInteractor;
    [SerializeField] GameObject tpInteractor;
    public bool teleport;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if(Singleton == null)
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
        slideInteractor.SetActive(!teleport);
        tpInteractor.SetActive(teleport);
        if(!teleport && locomotor.IsGrounded)
            locomotor.EnableMovement();
    }
}
