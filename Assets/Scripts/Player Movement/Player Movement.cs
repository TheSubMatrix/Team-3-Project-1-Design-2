using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Speed")]
    [SerializeField] float speed = 12f;

    private CharacterController characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 characterMove = transform.right * x + transform.forward * z;

        characterController.Move(characterMove * speed * Time.deltaTime);
    }
}
