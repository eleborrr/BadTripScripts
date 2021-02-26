using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnusSpawn : MonoBehaviour
{
    public int indx;
    public bool free_spot;
    public GameObject empty;
    public List<Vector3> lst;
    [SerializeField] public GameObject[] SnusSpawnPoints;
    [SerializeField] public GameObject[] Snusi;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject spawnpoint in SnusSpawnPoints)
        {
            lst.Add(spawnpoint.transform.position);
        }
        indx = 0;
        foreach(GameObject snus in Snusi)
        {
            free_spot = true;
            indx = Random.Range(0, lst.Count);
            snus.transform.position = lst[indx];
            lst.Remove(lst[indx]);
            Debug.Log(lst.Count);
        }
    }
}
