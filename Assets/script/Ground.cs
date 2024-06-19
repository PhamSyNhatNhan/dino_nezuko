using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private GameManager gm;
    public float rotationSpeed = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(0, rotationSpeed * Time.deltaTime * gm.GameSpeed, 0);
        
        transform.Rotate(rotation, Space.Self);
    }
}
