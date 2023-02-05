using UnityEngine;

public class InputController : MonoBehaviour
{
    public PlayerController PlayerController;

    /// <summary>
    /// Get inputs and call actions
    /// </summary>
    void Update()
    {
        float horizontalInput = Input.GetAxis(Constants.HorizontalAxis);
        
        if (Input.GetButton(Constants.JumpButton)) {
            PlayerController.Jump(1f);
        }
        
        PlayerController.Move(horizontalInput);

        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerController.Attack();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerController.Fire();
        }
    }
}
