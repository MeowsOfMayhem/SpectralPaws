using UnityEngine;

public class DashParticleController : MonoBehaviour
{
    public GameObject dashParticle; // Reference to the particle system GameObject for dash effects
    public float dashDuration = 1f; // Duration of the dash particle effect
    public float dashCooldown = 2f; // Cooldown duration between dashes

    private bool isDashing = false;
    private bool canDash = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canDash)
        {
            Dash();
        }
    }

    void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            canDash = false;

            // Enable dash particle
            if (dashParticle != null)
            {
                dashParticle.SetActive(true);
                Invoke("StopDashParticles", dashDuration); // Deactivate particle after dash duration
            }

            Invoke("ResetDash", dashCooldown); // Cooldown before player can dash again
        }
    }

    void StopDashParticles()
    {
        isDashing = false;

        // Disable dash particle
        if (dashParticle != null)
        {
            dashParticle.SetActive(false);
        }
    }

    void ResetDash()
    {
        canDash = true;
    }
}
