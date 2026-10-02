using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer; // set this to whatever layer your walls/ground are on
    [SerializeField] private float rayDistance = 0.1f;
    [SerializeField] private Vector2 rayOffset = Vector2.zero; // fine-tune ray origin relative to this object

    private bool isWallCheck;
    private int facingDirection = 1; // 1 = right, -1 = left — sync this from PlayerMovement if it tracks facing

    private void FixedUpdate()
    {
        Vector2 origin = (Vector2)transform.position + rayOffset;
        Vector2 direction = Vector2.right * facingDirection;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance, wallLayer);
        isWallCheck = hit.collider != null;

        Debug.DrawRay(origin, direction * rayDistance, isWallCheck ? Color.red : Color.green);
    }

    public void SetFacingDirection(int dir)
    {
        facingDirection = dir;
    }

    public bool getWallCheck()
    {
        return isWallCheck;
    }
}