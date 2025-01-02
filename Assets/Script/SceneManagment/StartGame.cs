using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void GoToNewScene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(1);
        
    }
}
