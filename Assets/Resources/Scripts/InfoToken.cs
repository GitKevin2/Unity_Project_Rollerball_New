using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoToken : MonoBehaviour
{
    public Info infoName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Vector3.up * 200f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Tags.PLAYER)) 
        {
            GameManager.MainHUD.ShowInfo(infoName);
        }
    }
}
