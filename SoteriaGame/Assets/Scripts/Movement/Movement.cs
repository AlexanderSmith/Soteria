using UnityEngine;
using System.Collections;

public class Movement {

    public void Move(Vector3 input, float speed, Transform whatToTransform)
    {
        whatToTransform.rigidbody.velocity = new Vector3(0, 0, 0);
        whatToTransform.rigidbody.MovePosition((input * speed) + whatToTransform.position);
        whatToTransform.position = new Vector3(whatToTransform.rigidbody.position.x, 0.5f, whatToTransform.rigidbody.position.z);
        
        MakeTransformLookAtMoveDirection(whatToTransform, input);
    }

    private void MakeTransformLookAtMoveDirection(Transform whatToTransform, Vector3 inputVec)
    {
        // Rotation
        if (inputVec != Vector3.zero)
            whatToTransform.rotation = Quaternion.Slerp(whatToTransform.rotation,
                Quaternion.LookRotation(inputVec),
                Time.deltaTime * 20.0f);//*/
    }
}
