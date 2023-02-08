using UnityEngine;

public class PlayerDieController: MonoBehaviour,IDieController
{
    [SerializeField] private GameObject DyingCanvasObject;
    [SerializeField] private PlayerController PlayerController;

    public void Die()
    {
        if (!DyingCanvasObject || !PlayerController) return;

        DyingCanvasObject.SetActive(true);
        PlayerController.IsDead = true;
    }
}