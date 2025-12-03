using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public void UpdateFacing(SpriteRenderer spriteRenderer, Vector2 inputDir)
    {
        if (inputDir.x > 0)
            spriteRenderer.flipX = false;
        else if (inputDir.x < 0)
            spriteRenderer.flipX = true;
    }


    public void UpdateAnimation(Animator animator, Vector2 inputDir, bool isDead)
    {
        if (animator == null) return;

        float speed = inputDir.magnitude;

        animator.SetFloat("Speed", speed);    
        animator.SetFloat("MoveX", inputDir.x);
        animator.SetFloat("MoveY", inputDir.y);
        animator.SetBool("IsDead", isDead);   
    }
}
