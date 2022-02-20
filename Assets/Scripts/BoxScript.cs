using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BoxScript : MonoBehaviourPun
{
    public GameObject boxPrefab;
    public int maxBoxes = 5;

    private void Start()
    {
        StartCoroutine(SpawnBoxesCoroutine());
    }

    IEnumerator SpawnBoxesCoroutine()
    {
        while (true)
        {
            SpawnBox();
            yield return new WaitForSeconds(5);
        }
    }

    void SpawnBox()
    {
        if (!PhotonNetwork.IsMasterClient || boxPrefab == null)
            return;

        //find a more efficient way
        int amountOfBoxes = GameObject.FindGameObjectsWithTag("Ammo").Length;
        if(amountOfBoxes < maxBoxes)
        {
            Vector3 pos = new Vector3();
            pos.x = Random.Range(-4.5f, 4.5f);
            pos.y = 1;
            pos.z = Random.Range(-4f, 4f);
            PhotonNetwork.Instantiate(boxPrefab.name, pos, Quaternion.identity);
        }
    }
}
