using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;

    #region Movement
    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    #endregion

    #region Gravity
    [SerializeField] float drag = 8f;
    private float gravity = -9.81f;
    private Vector3 velocity;
    #endregion

    // Update is called once per frame
    void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity.normalized * drag * Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3 (horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            controller.Move(moveDir * speed * Time.deltaTime);
        }
    }
}
