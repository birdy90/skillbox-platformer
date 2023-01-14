using UnityEngine;

public class InputController : MonoBehaviour
{
    public PlayerController PlayerController;

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis(Constants.HorizontalAxis);
        
        if (Input.GetButton(Constants.JumpButton)) {
            PlayerController.Jump(1f);
        }
        
        PlayerController.Move(horizontalInput);

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerController.Attack();
        }
    }
}
