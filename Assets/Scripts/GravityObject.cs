using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class GravityObject : MonoBehaviour
{
    // Properties
    public Collider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Planet CurrentPlanet { get; private set; }

    // Private fields
    private Vector2 _currentNormal, _lastNormal = Vector2.zero;

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (CurrentPlanet != null)
        {
            AdjustObjectAngle();
            ApplyGravityPull();
        }
    }

    protected virtual void AdjustObjectAngle()
    {
        // Get normal
        Vector2 f = CurrentPlanet.Collider.ClosestPoint(transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector2)transform.position - f, LayerMask.NameToLayer("Planet"));
        Debug.DrawRay(hit.point, hit.normal, Color.red);
        Debug.DrawLine(transform.position, f);

        _currentNormal = hit.normal;
        Vector3 vectorToTarget = hit.normal.normalized;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;

        float rotationSpeed = Vector2.Dot(_currentNormal, _lastNormal);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), 250 * Time.deltaTime);
        _lastNormal = _currentNormal;
    }

    protected virtual void ApplyGravityPull() => Rigidbody.AddRelativeForce(Vector2.down * 10);

    // Bind & Release this object from gravity
    public void BindToPlanetGravity(Planet gravityAttractor) => CurrentPlanet = gravityAttractor;
    public void ReleaseFromPlanetGravity() => CurrentPlanet = null;
}