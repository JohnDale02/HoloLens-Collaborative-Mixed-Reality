using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PhotonRoomWordPuzzle : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        public static PhotonRoomWordPuzzle Room;

        [SerializeField] private GameObject photonUserPrefab = default;
        [SerializeField] private List<GameObject> interactablePrefabs = new List<GameObject>(); // Store multiple prefabs
        [SerializeField] private List<Transform> interactableLocations = new List<Transform>(); // Store corresponding locations

        private Player[] photonPlayers;
        private int playersInRoom;
        private int myNumberInRoom;

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom++;
        }

        private void Awake()  // I CHANGED WITH A
        {
            if (Room == null)
            {
                Room = this;
            }
            else
            {
                if (Room != this)
                {
                    Destroy(Room.gameObject);
                    Room = this;
                }
            } 

        }

        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        private void Start()
        {
            // Allow prefabs not in a Resources folder
            if (PhotonNetwork.PrefabPool is DefaultPool pool)
            {
                if (photonUserPrefab != null) pool.ResourceCache.Add(photonUserPrefab.name, photonUserPrefab);
                interactablePrefabs.ForEach(prefab => {
                    if (prefab != null) pool.ResourceCache.Add(prefab.name, prefab);
                });
            }
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom = photonPlayers.Length;
            myNumberInRoom = playersInRoom;
            PhotonNetwork.NickName = myNumberInRoom.ToString();

            StartGame();
        }

        private void StartGame()
        {
            CreatePlayer();

            if (!PhotonNetwork.IsMasterClient) return;

            if (TableAnchor.Instance != null) CreateInteractableObjects();
        }

        private void CreatePlayer()
        {
            if (photonUserPrefab == null)
            {
                Debug.LogError("Photon User Prefab is null.");
                return;
            }

            PhotonNetwork.Instantiate(photonUserPrefab.name, Vector3.zero, Quaternion.identity);
        }

        private void CreateInteractableObjects()
        {
            // Check if the lists are properly assigned and have the same number of elements
            if (interactablePrefabs.Count != interactableLocations.Count)
            {
                Debug.LogError("Interactables and locations lists do not match in size.");
                return;
            }

            // Loop through all prefabs and instantiate them at their locations
            for (int i = 0; i < interactablePrefabs.Count; i++)
            {
                if (interactablePrefabs[i] != null && interactableLocations[i] != null)
                {
                    if (interactablePrefabs[i] == null || interactableLocations[i] == null)
                    {
                        Debug.LogError($"Interactable or location is null at index: {i}");
                        continue; // Skip this iteration
                    }

                    var position = interactableLocations[i].position;
                    var positionOnTopOfSurface = new Vector3(position.x, position.y + interactableLocations[i].localScale.y / 2,
                position.z);

                GameObject instantiatedObj = PhotonNetwork.Instantiate(
                    interactablePrefabs[i].name, 
                    positionOnTopOfSurface, 
                    interactableLocations[i].rotation
                );

                if (instantiatedObj == null)
                {
                    Debug.LogError($"Failed to instantiate object at index: {i}");
                    continue; // Skip this iteration
                }

                if (CollectData.Instance == null || instantiatedObj == null)
                {
                    Debug.LogError("CollectData instance or instantiated object is null.");
                    continue; // Skip this iteration
                }

                CollectData.Instance.RegisterPoster(instantiatedObj); // Register the object with CollectData

                }

                else
                {
                    Debug.LogError($"Interactable or location is null at index: {i}");
                }
            }
        }
    }
}
