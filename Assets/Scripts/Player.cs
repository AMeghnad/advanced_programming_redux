using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform cam;

    public GameObject weapon;

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private int score;

    public CharacterController controller;

    private void Start()
    {
        GameData data = GameSave.GetData();
        if (GameSave.Exists())
        {
            // Get the data
            data = GameSave.GetData();
            transform.position = data.playerPos;
            score = data.score;
            transform.rotation = data.playerRot;
        }

    }

    private void OnDestroy()
    {
        // Save position when player is destroyed
        GameData data = GameSave.GetData();
        data.playerPos = transform.position;
        GameSave.Instance.Save();
    }

    public void Move(float inputH, float inputV, bool isJumping)
    {
        if (controller.isGrounded)
        {
            // Rotate the player in the direction of camera
            Vector3 euler = cam.transform.eulerAngles;
            transform.rotation = Quaternion.AngleAxis(euler.y, Vector3.up);

            moveDirection = new Vector3(inputH, 0, inputV);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (isJumping)
            {
                moveDirection.y = jumpSpeed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            weapon.SetActive(true);
        }
    }
}