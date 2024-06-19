using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public float ExplosionForce => _explosionForce;
    public float ExplosionRadius => _explosionRadius;

    private float _upWardsModifier = 3;

    public void Explode() 
    {
        foreach (Rigidbody explobadleObject in GetExplodableObject()) 
        {
            explobadleObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upWardsModifier);
        }
    }

    private List<Rigidbody> GetExplodableObject() 
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> affected = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                affected.Add(hit.attachedRigidbody);

        return affected;
    }

    public void SetStart(float explosionForce, float explosionRadius)
    {
        _explosionForce = explosionForce;
        _explosionRadius = explosionRadius;
    }
}
