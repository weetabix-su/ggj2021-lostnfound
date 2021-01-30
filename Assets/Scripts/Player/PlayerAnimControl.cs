using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControl : MonoBehaviour
{
    [SerializeField] Animator CharacterAnimator;
    Transform CharacterTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (CharacterAnimator != null) CharacterTransform = CharacterAnimator.gameObject.transform;
    }

    public void MoveVector(float vector)
    {
        if (CharacterAnimator == null) return;
        if (CharacterTransform == null) return;
        if (vector < 0 && CharacterTransform.localScale.z != -1) CharacterTransform.localScale += Vector3.back * 2;
        else if (vector > 0 && CharacterTransform.localScale.z != 1) CharacterTransform.localScale += Vector3.forward * 2;
        CharacterAnimator.SetFloat("move", Mathf.Abs(vector));
    }

    public void Jump()
    {
        if (CharacterAnimator == null) return;
        CharacterAnimator.SetTrigger("jump");
    }
    public void Turn()
    {
        if (CharacterAnimator == null) return;
        CharacterAnimator.SetTrigger("turn");
    }
}
