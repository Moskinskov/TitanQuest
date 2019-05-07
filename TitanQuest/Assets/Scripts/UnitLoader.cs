using UnityEngine;
using UnityEngine.Networking;

public class UnitLoader : NetworkBehaviour
{
    [SerializeField] private GameObject unitPrefab;

    public override void OnStartServer()
    {
        var tempUnit = Instantiate(unitPrefab, transform.position, Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(tempUnit, gameObject);
    }
}
