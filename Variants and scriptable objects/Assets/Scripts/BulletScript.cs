using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField] Rigidbody _rbBullet;

    private float _bulletForwardForce = 20f;
    private float _bulletUpForce = 8f;

    private void Start()
    {
        _rbBullet.AddForce(transform.forward * _bulletForwardForce, ForceMode.Impulse);
        _rbBullet.AddForce(transform.up * _bulletUpForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.ToString() == "ground") 
        {
            Destroy(this.gameObject);
        }
    }
}
