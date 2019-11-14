using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player : GravityObject
{
    private float _moveSpeed;
    private float _jumpForce;
    private float _moveInput;

    public Plant CurrentlySelectedPlant { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        BindToPlanetGravity(GameObject.Find("Planet").GetComponent<Planet>());
    }

    protected override void FixedUpdate()
    {
        AdjustObjectAngle();

        // Convert velocity to local space (as if gravity was always the same direction)
        Vector2 newVelocity = transform.InverseTransformDirection(Rigidbody.velocity);

        // Move the player
        if (Input.GetAxisRaw("Horizontal") != 0)
            newVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * 2, newVelocity.y);
        else
            newVelocity = new Vector2(0, newVelocity.y);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            newVelocity = new Vector2(newVelocity.x, 5);
        }

        ApplyGravityPull();

        // Convert calculated velocity to world space again
        Rigidbody.velocity = transform.TransformDirection(newVelocity);
    }

    public LayerMask m;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector3(mousePosition.x, mousePosition.y, CurrentPlanet.transform.position.z);
            Vector3 direction = mousePosition - CurrentPlanet.transform.position;
            Vector3 pos = CurrentPlanet.transform.position + direction.normalized * (CurrentPlanet.transform.localScale.x / 2);
            if (Vector2.Distance(mousePosition, pos) <= 0.5f)
            {
                CurrentPlanet.PlaceSeedBox(new DefaultPlantationBox(CurrentPlanet, Resources.Load<GameObject>("PlantationBox"), 0.2f), pos);

                // Auto plant
                CurrentPlanet.GetPlantationBoxAtPosition(mousePosition).PlantSeed(new PlantationData(2, Resources.LoadAll<GameObject>("PlantLol")));
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector3(mousePosition.x, mousePosition.y, CurrentPlanet.transform.position.z);
            CurrentPlanet.GetPlantationBoxAtPosition(mousePosition).Water();
        }
    }
}