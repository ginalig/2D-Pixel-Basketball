using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowFinger : MonoBehaviour
{
    private InputMaster controls;

    private void Awake()
    {
        controls = new InputMaster();
    }

    private void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint((controls.Player.MousePosition.ReadValue<Vector2>()));
        pos.z = 0;
        transform.position = pos;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
