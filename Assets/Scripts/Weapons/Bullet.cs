using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float LifeTime;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
        
        Destroy(gameObject, LifeTime);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemy))
        {
            Vector3 position = collision.contacts[0].point;
            GameObject bloodParticles = Instantiate(enemy.Blood.gameObject, position, Quaternion.identity);
            ParticleSystem bloodParticleSystem = bloodParticles.GetComponent<ParticleSystem>();
            bloodParticleSystem.Play();
            Destroy(bloodParticles, bloodParticleSystem.main.duration);
            Destroy(gameObject);
        }
    }
/*
    private void Update()
    {
        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemy))
        {
            Vector3 position = collision.contacts[0].point;
            GameObject bloodParticles = Instantiate(enemy.Blood.gameObject, position, Quaternion.identity);
            ParticleSystem bloodParticleSystem = bloodParticles.GetComponent<ParticleSystem>();
            bloodParticleSystem.Play();
            Destroy(bloodParticles, bloodParticleSystem.main.duration);
            enemy.Health -= 10;
            Destroy(gameObject);
        }
    }
    */
}