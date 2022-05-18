using UnityEngine;

public class JumpableController : MonoBehaviour {
    Rigidbody mRigidBody;
    [SerializeField] float multiplier = 200, torqueMultiplier;
    [SerializeField, Range(0,1)] float upVectorAmount = .4f;

    bool isOnGround = false, jumpable = false;

    private void Start() {
        mRigidBody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other) {
        jumpable = true;
    }

    void OnCollisionStay(Collision other) {
        isOnGround = true;
    }

    void OnCollisionExit(Collision other) {
        isOnGround = false;
    }

    public void Jump(float val) {   //val is ranged from -1 to 1
        if(!isOnGround) return;

        if(!jumpable) return;

        mRigidBody.AddForce(
            ((
                Vector3.Lerp(transform.forward, SignOf(val) * transform.right, Mathf.Abs(val))
            ) + (transform.up * upVectorAmount)) * multiplier
        );
        mRigidBody.AddTorque(Vector3.up * val * torqueMultiplier);
        
        jumpable = false;
    }

    private int SignOf(float val) {
        if(val < 0) return -1;
        else return 1;
    }
}
