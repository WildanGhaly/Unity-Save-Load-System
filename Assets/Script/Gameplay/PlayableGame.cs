using UnityEngine;

public class PlayableGame : MonoBehaviour
{
    [SerializeField] private CharacterPlayable characterPlayable;

    private void OnEnable()
    {
        characterPlayable.enabled = true;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        characterPlayable.GetComponent<Transform>().localPosition = new Vector3(-6.25f, -0.2f, -0.038f);
        Continue();
    }

    public void Jump()
    {
        characterPlayable.Jump();
    }

    private void OnDisable()
    {
        characterPlayable.enabled = false;
    }
}
