using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteLibrary))]

public class Player : MonoBehaviour
{
    public SpriteLibraryAsset Front;
    public SpriteLibraryAsset Back;
    public SpriteLibraryAsset Side;
    SpriteLibrary library;
    public SpriteResolver Hair;
    public SpriteResolver Dress;

    public float moveSpeed = 5f;
    Animator animator;
    Rigidbody2D rb;
    Vector2 movement;
    bool isFacingRight;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        library = GetComponent<SpriteLibrary>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);

            if (movement.x > 0)
            {
                library.spriteLibraryAsset = Side;
                transform.eulerAngles = Vector3.zero;

            }
            if (movement.x < 0)
            {
                library.spriteLibraryAsset = Side;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            if (movement.y > 0)
            {
                library.spriteLibraryAsset = Back;
            }
            if (movement.y < 0)
            {
                library.spriteLibraryAsset = Front;
            }
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void EquipHair(string name, string category)
    {
        Hair.SetCategoryAndLabel(category, name);
    }
    public void EquipDress(string name, string category)
    {
        Dress.SetCategoryAndLabel(category, name);
    }
}