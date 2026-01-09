using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public GameObject _ballPrefab;
    public float _shootForce = 15f;
    public float _sphereRadius = 0.1f;
    public float _maxDistance = 50f;
    public float _autoDestroy = 3f;
    public  Camera _camera;
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleShoot();
        }
    }

    void HandleShoot()
    {
        Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.SphereCast(_ray, _sphereRadius, out _hit , _maxDistance))
        {
            if (_hit.collider.CompareTag("GreyWall"))
            {
                Debug.Log("Muro Grigio");
                return;
            }
        }
        ShootBall(_ray.direction);
    }

    void ShootBall(Vector3 direction)
    {
        GameObject _ball = Instantiate(_ballPrefab, _camera.transform.position, Quaternion.identity);
        Rigidbody _rb = _ball.GetComponent<Rigidbody>();
        if (_rb != null)
        {
            _rb.AddForce(direction *  _shootForce , ForceMode.Impulse);
        }
        Destroy(_ball , _autoDestroy);
    }
}
