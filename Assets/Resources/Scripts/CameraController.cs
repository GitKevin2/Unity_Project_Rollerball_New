using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float lookSensitivity = 90f;
    public Transform northBoundary, southBoundary, eastBoundary, westBoundary;
    private float boundaryNorth = float.PositiveInfinity;
    private float boundarySouth = float.NegativeInfinity; 
    private float boundaryEast = float.PositiveInfinity;
    private float boundaryWest = float.NegativeInfinity;

    private Vector3 offset;
    private Vector3 baseOffset;

    // Awake is called when first loaded and before every Start
    void Awake()
    {
        GameManager.CurrentCamera ??= this;
    }

    private void OnDestroy()
    {
        if (GameManager.CurrentCamera == this) GameManager.CurrentCamera = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        baseOffset = offset;
        if (northBoundary != null) boundaryNorth = northBoundary.position.z;
        if (southBoundary != null) boundarySouth = southBoundary.position.z;
        if (eastBoundary != null) boundaryEast = eastBoundary.position.x;
        if (westBoundary != null) boundaryWest = westBoundary.position.x;
    }

    // LateUpdate is called once per frame after every Update()
    void LateUpdate()
    {
        if (player.activeInHierarchy)
        {
            Move(player.transform);
            Watch(player.transform);
            DetectWall();
        }
    }

    void Move(Transform tf)
    {
        Vector3 pos = tf.position;
        pos.x = Mathf.Clamp(pos.x, boundaryWest, boundaryEast);
        pos.z = Mathf.Clamp(pos.z, boundarySouth, boundaryNorth);
        transform.position = pos + offset;
    }

    void Watch(Transform tf)
    {
        if (GameManager.Paused) return;
        transform.LookAt(tf.position);
    }

    public void Orbit(Vector2 lookVect)
    {
        float lookSpeedX = lookVect.x * lookSensitivity * Time.deltaTime;
        offset = Quaternion.AngleAxis(lookSpeedX, Vector3.up) * offset;
        baseOffset = Quaternion.AngleAxis(lookSpeedX, Vector3.up) * baseOffset;
    }


    public void DetectWall()
    {
        if  (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            if (hit.collider.CompareTag(Tags.PLAYER)) 
            {
                offset = Vector3.Lerp(offset, baseOffset, 2f * Time.deltaTime);
            }
            else
            {
                offset = Vector3.Lerp(offset, hit.transform.forward, 2f * Time.deltaTime);
            }
        }
    }

}
