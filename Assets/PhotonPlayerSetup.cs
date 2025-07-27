using System.Collections;
using System.Collections.Generic;
using _Scripts._PlayerScripts;
using UnityEngine;

public class PhotonPlayerSetup : MonoBehaviour
{
    public PlayerCamControl playerCamControl;
    public PlayerMoveControl playerMovement;
    public PlayerWallrunControl playerWallrun;
    public GameObject playerCamera;
    public GameObject PlayerUI;

    public void IsLocalPlayer()
    {
        playerCamera.SetActive(true);
        PlayerUI.SetActive(true);
        playerCamControl.enabled = true;
        playerMovement.enabled = true;
        playerWallrun.enabled = true;
        
        
    }
}