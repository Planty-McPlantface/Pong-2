using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Default.Enable();
    }
    public float GetMoveDir(){  
        return playerInputActions.Default.Move.ReadValue<float>();
    }
    
}