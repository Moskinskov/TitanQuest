using UnityEngine;
using UnityEngine.Networking;

public class NetPlayerSetup : NetworkBehaviour
{
    [SerializeField] private MonoBehaviour[] disableScripts;

    private void Awake()
    {
        if (!hasAuthority)
        {
            for (int i = 0; i < disableScripts.Length; i++)
            {
                disableScripts[i].enabled = false;
            }
        }
    }

    public override void OnStartAuthority()
    {
        for (int i = 0; i < disableScripts.Length; i++)
        {
            disableScripts[i].enabled = true;
        }
    }

}
