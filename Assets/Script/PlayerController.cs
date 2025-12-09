using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float detectDistance = 1.2f;
    public LayerMask tileLayer;

    public Tile currentTile;   
    Tile lastTile;             

    private Vector2 moveInput;
    private Vector2 lookDirection = Vector2.down;
    private Vector2 lastDirection = Vector2.down;   //SIMPAN ARAH TERAKHIR

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // INPUT GERAK (WASD)
        moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) moveInput.y = 1;
        if (Keyboard.current.sKey.isPressed) moveInput.y = -1;
        if (Keyboard.current.aKey.isPressed) moveInput.x = -1;
        if (Keyboard.current.dKey.isPressed) moveInput.x = 1;

        moveInput = moveInput.normalized;

        // GERAKKAN PLAYER
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0);
        transform.position += movement * speed * Time.deltaTime;

        // SIMPAN ARAH TERAKHIR
        if (moveInput != Vector2.zero)
        {
            lookDirection = moveInput;
            lastDirection = moveInput;
        }

        // UPDATE ANIMASI (GERAK + IDLE ARAH)
        Vector2 animDir = moveInput != Vector2.zero ? moveInput : lastDirection;

        animator.SetFloat("moveX", animDir.x);
        animator.SetFloat("moveY", animDir.y);
        animator.SetBool("isMoving", moveInput != Vector2.zero);

        // FLIP KIRI / KANAN SAAT IDLE & JALAN
        if (animDir.x < 0)
            spriteRenderer.flipX = false;   // kiri
        else if (animDir.x > 0)
            spriteRenderer.flipX = true;    // kanan

        // DETEKSI TILE
        DetectTile();

        // ✅ INTERAKSI (TOMBOL E)
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (currentTile != null)
                currentTile.Interact();
        }
    }

    void DetectTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            lookDirection,
            detectDistance,
            tileLayer
        );

        Debug.DrawRay(transform.position, lookDirection * detectDistance, Color.red);

        if (hit.collider != null)
        {
            currentTile = hit.collider.GetComponent<Tile>();

            if (currentTile != lastTile)
            {
                if (lastTile != null)
                    lastTile.OnDeselect();

                if (currentTile != null)
                    currentTile.OnSelect();

                lastTile = currentTile;
            }
        }
        else
        {
            if (lastTile != null)
            {
                lastTile.OnDeselect();
                lastTile = null;
            }

            currentTile = null;
        }
    }
}
