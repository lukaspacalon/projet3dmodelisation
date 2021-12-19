using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float mouseSensitivity = 100f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        if (!photonView.IsMine)
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(hAxis, 0, vAxis);
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity*2  * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        
        transform.Translate(direction /*Vector3.forward*/ * Time.deltaTime * moveSpeed);
        if ( direction != Vector3.zero )
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
}
