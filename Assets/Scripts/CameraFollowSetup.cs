using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraFollowSetup : MonoBehaviour
{
    public GameObject cameraTarget;

    private void Start()
    {
        StartCoroutine(FindLocalPlayer());
    }

    private IEnumerator FindLocalPlayer()
    {
        while (true)
        {
            foreach (KeyValuePair<uint, NetworkIdentity> kvp in NetworkServer.spawned)
            {
                if (kvp.Value.isLocalPlayer)
                {
                    // Get the PlayerCameraRoot for the local player
                    cameraTarget = kvp.Value.transform.Find("PlayerCameraRoot").gameObject;

                    // Set the Cinemachine target
                    GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = cameraTarget.transform;

                    // Break the loop
                    yield break;
                }
            }
            // Wait a bit before trying again
            yield return new WaitForSeconds(0.1f);
        }
    }
}
