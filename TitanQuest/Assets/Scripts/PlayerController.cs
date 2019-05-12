using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private LayerMask movementMask;

    private Camera cam;
    private Character character;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void SetCharacter(Character character, bool isLocalPlayer)
    {
        this.character = character;
        if (isLocalPlayer)
            cam.GetComponent<CameraController>().Target = character.transform;
    }

    [Command]
    public void CmdSetMovePoint(Vector3 point)
    {
        character.SetMovePoint(point);
    }


    private void Update()
    {
        if (isLocalPlayer)
        {
            if (character != null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, 100f, movementMask))
                    {
                        CmdSetMovePoint(hit.point);
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(character?.gameObject);
    }

}
