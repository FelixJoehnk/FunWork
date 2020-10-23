using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement_OLD : MonoBehaviour
{
    [SerializeField] private float _acceleration;
    [SerializeField] private float _accMultiplayer;

    private Rigidbody _rb;
    private Vector3 _oldDir;
    private float _speed;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rb.AddForce(transform.forward * _acceleration * Time.deltaTime);
            _acceleration *= _accMultiplayer;
            _oldDir = _rb.velocity;
            _speed = _oldDir.magnitude;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newDir = Vector3.Reflect(_oldDir, collision.GetContact(0).normal);

        InitCamRotation(_oldDir, newDir.normalized, collision.GetContact(0).normal, _speed);

        _rb.velocity = newDir;
    }

    private void InitCamRotation(Vector3 oldDir, Vector3 newDir, Vector3 normal, float speed)
    {
        float angle, rotationspeed, duration;

        if(Vector3.Angle(oldDir, normal) > 45.0f)
        {
            angle = Vector3.Angle(oldDir, newDir);
        }
        else
        {
            angle = 360 - Vector3.Angle(oldDir, newDir);
        }

        duration = 5 / speed;
        rotationspeed = angle / duration;

        StartCoroutine(AnimateCam(rotationspeed, duration));
    }

    private IEnumerator AnimateCam(float rotationspeed, float duration)
    {

        Vector3 startrotation = transform.eulerAngles;

        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            transform.rotation = Quaternion.AngleAxis(startrotation.y + rotationspeed * counter, Vector3.up);

            yield return null;
        } 

        startrotation.y += rotationspeed * duration;

        transform.eulerAngles = startrotation;

        _oldDir = _rb.velocity;
        _speed = _oldDir.magnitude;
    }
}
