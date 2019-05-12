using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class PlayerLoader : NetworkBehaviour
    {
        [SerializeField] private GameObject unitPrefab;
        [SerializeField] private PlayerController playerController;

        [SyncVar(hook = "HookUnitIdentity")] private NetworkIdentity unitIdentity;

        public override void OnStartAuthority()
        {
            if (isServer)
            {
                GameObject unit = Instantiate(unitPrefab);
                NetworkServer.Spawn(unit);
                unitIdentity = unit.GetComponent<NetworkIdentity>();
                playerController.SetCharacter(unit.GetComponent<Character>(), true);
            }
            else
            {
                CmdCreatePlayer();
            }
        }
        [Command]
        private void CmdCreatePlayer()
        {
            GameObject unit = Instantiate(unitPrefab);
            NetworkServer.Spawn(unit);
            unitIdentity = unit.GetComponent<NetworkIdentity>();
            playerController.SetCharacter(unit.GetComponent<Character>(), false);
        }
        /// <summary>
        /// Функция - обработчик синхронизируемой переменной
        /// </summary>
        /// <param name="unit"></param>
        [ClientCallback]
        private void HookUnitIdentity(NetworkIdentity unit)
        {
            if (isLocalPlayer)
            {
                unitIdentity = unit;
                playerController.SetCharacter(unit.GetComponent<Character>(), true);
            }
        }
    }
}
