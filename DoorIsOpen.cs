using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIsOpen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public bool isOpen;
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
