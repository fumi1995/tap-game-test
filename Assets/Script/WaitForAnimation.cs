using UnityEngine;

public class WaitForAnimation : CustomYieldInstruction
{
    public readonly Animator _animator;
    public readonly string _stateName;
    public readonly int _layerNum;

    public WaitForAnimation(Animator animator, string stateName, int layerNum = 0)
    {
        _animator = animator;
        _stateName = stateName;
        _layerNum = layerNum;
    }

    public override bool keepWaiting
    {
        get
        {
            var state = _animator.GetCurrentAnimatorStateInfo(_layerNum);
            return state.IsName(_stateName) && state.normalizedTime < 1;
        }
    }
}