using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public GameObject _bulletHolePrefab;
    public float _force = 100f;
    public float _offset = 0.01f;
    
    // Start is called before the first frame update
   

    // Update is called once per frame
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

                    Vector3 point = hit.transform.InverseTransformPoint(hit.point);
                    float margin = 0.45f;

                    Vector3 clamp = new Vector3(
                        Mathf.Clamp(point.x, -margin, margin),
                        Mathf.Clamp(point.y, -margin, margin),
                        Mathf.Clamp(point.z, -margin, margin)
                        );
                    Vector3 localNormal = hit.transform.InverseTransformDirection(hit.normal);
                    if (localNormal.x != 0)
                    {
                        clamp.x = point.x;
                    }
                    if (localNormal.y != 0)
                    {
                        clamp.y = point.y;
                    }
                    if (localNormal.z != 0)
                    {
                        clamp.z = point.z;
                    }

                    

                    Vector3 spawnPosition = hit.transform.TransformPoint(clamp) + (hit.normal * _offset);

                    Quaternion spawnRotation = Quaternion.LookRotation(-hit.normal);

                    GameObject hole = Instantiate(_bulletHolePrefab, spawnPosition, spawnRotation);
                    hole.transform.SetParent(hit.transform);
                    hole.transform.localScale = Vector3.one;
                    hole.transform.localScale = Vector3.one * 0.1f;
                }
            }
        }
    }
}
