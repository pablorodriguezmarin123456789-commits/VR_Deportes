using Oculus.Interaction.Locomotion;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Singleton;
    public bool Learning;
    public int errorscount;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
