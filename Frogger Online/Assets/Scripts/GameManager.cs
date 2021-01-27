﻿using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace Com.Cotxe11.FroggerOnline
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        #region Public Variables

        [Tooltip("The prefab to use for representing the player")]
        public GameObject player1Prefab;
        public GameObject player2Prefab;

        public Transform player1Spawn;
        public Transform player2Spawn;

        #endregion

        private int lastLevelLoaded = 0;

        #region MonoBehaviour CallBacks

        private void Start()
        {
        }

        #endregion



        #region Photon Callbacks


        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }


        #endregion


        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion

        #region Photon Callbacks


        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

            if (lastLevelLoaded == 2)
            {
                PhotonNetwork.Destroy(Frog.LocalPlayerInstance);
            }


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        #endregion

        #region Private Methods

        private void OnLevelWasLoaded(int level)
        {
            lastLevelLoaded = level;

            if (level != 2) return;


            if (Frog.LocalPlayerInstance == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                if (PhotonNetwork.IsMasterClient)
                    PhotonNetwork.Instantiate(this.player1Prefab.name, player1Spawn.position, Quaternion.identity, 0);
                else
                    PhotonNetwork.Instantiate(this.player2Prefab.name, player2Spawn.position, Quaternion.identity, 0);
            }
            else
            {
                Frog.LocalPlayerInstance.transform.position = (PhotonNetwork.IsMasterClient) ? player1Spawn.position : player2Spawn.position;
            }

            GameObject.Find(PhotonNetwork.IsMasterClient ? "Player1DisplayName" : "Player2DisplayName").GetComponent<PlayerNameSync>().LinkMyName();

        }

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        }


        #endregion
    }
}