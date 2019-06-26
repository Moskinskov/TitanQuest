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
                Character character = CreateCharacter();
                playerController.SetCharacter(character, true);
                InventoryUI.instance.SetInventory(character._inventory);
            }
            else CmdCreatePlayer();
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
                Character character = unit.GetComponent<Character>();
                playerController.SetCharacter(character, true);
                character.SetInventory(GetComponent<Inventory>());
                InventoryUI.instance.SetInventory(character._inventory);
            }
        }

        public Character CreateCharacter()
        {
            GameObject unit = Instantiate(unitPrefab);
            NetworkServer.Spawn(unit);
            unitIdentity = unit.GetComponent<NetworkIdentity>();
            unit.GetComponent<Character>().SetInventory(GetComponent<Inventory>());
            return unit.GetComponent<Character>();
        }

        public override bool OnCheckObserver(NetworkConnection conn)
        {
            return false;
        }
    }
}
