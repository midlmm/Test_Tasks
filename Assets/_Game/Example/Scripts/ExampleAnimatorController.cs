using UnityEngine;

public class ExampleAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void OnStarted()
    {
        _animator.SetTrigger("Started");
    }

    public void OnEnded()
    {
        _animator.SetTrigger("Ended");
    }
}
