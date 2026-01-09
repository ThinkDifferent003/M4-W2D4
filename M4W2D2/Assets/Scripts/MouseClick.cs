using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public GameObject _bulletHolePrefab;
    public float _force = 100f;
    public float _offset = 0.01f;
    
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray , out hit))
            {
                Rigidbody _rb = hit.collider.GetComponent<Rigidbody>();
                if (_rb != null)
                {
                    Vector3 forceDirection = _ray.direction * _force;
                    _rb.AddForceAtPosition(forceDirection, hit.point);
                    Debug.Log("Forza Applicata");
                }
                Vector3 spawnPosition = hit.point + (hit.normal * _offset);

                Quaternion spawnRotation = Quaternion.LookRotation(-hit.normal);

                GameObject hole = Instantiate (_bulletHolePrefab, spawnPosition, spawnRotation);
                hole.transform.SetParent(hit.transform);
            }
        }
    }
}
