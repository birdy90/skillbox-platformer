using UnityEngine;

public class InputController_NoMovement : MonoBehaviour
{
    public PlayerController_NoMovement PlayerController;

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis(Constants.HorizontalAxis);
        
        if (Input.GetButton(Constants.JumpButton)) {
            PlayerController.IsGrounded = false;
        }
        else
        {
            PlayerController.IsGrounded = true;
        }
        
        PlayerController.Move(horizontalInput);

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerController.Attack();
        }
    }
}
