using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAnimation : MonoBehaviour
{
    private GameObject characterModel;
    private Animator animator;
    private ChessController chessController;

    private Vector3 lastFramePosition;
    // Start is called before the first frame update
    void Start()
    {
        characterModel = this.transform.Find("character").gameObject;
        animator = characterModel.GetComponent<Animator>();
        chessController = this.transform.GetComponent<ChessController>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = (this.transform.position - lastFramePosition).magnitude / Time.deltaTime;
        animator.SetFloat("movementSpeed", movementSpeed);
        lastFramePosition = this.transform.position;
    }

    public void DoAttack(bool b)
    {
        animator.SetBool("isAttacking", b);
    }

    public void OnAttackAnimationFinished()
    {
        animator.SetBool("isAttacking", false);
        chessController.OnAttackAnimationFinished();
    }
    public void IsAnimated(bool b)
    {
        animator.enabled = b;
    }

}
