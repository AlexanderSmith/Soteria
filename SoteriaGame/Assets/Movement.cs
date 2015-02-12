using UnityEngine;
using System.Collections;

public class Movement {

    public void Move(float inputX, float inputY, float inputZ, float speed, Transform whatToTransform)
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        whatToTransform.Translate(input * speed);
        MakeTransformLookAtMoveDirection(whatToTransform, input);
    }

    private void MakeTransformLookAtMoveDirection(Transform whatToTransform, Vector3 inputVec)
    {
        // Rotation
        if (inputVec != Vector3.zero)
            whatToTransform.GetChild(0).transform.rotation = Quaternion.Slerp(whatToTransform.GetChild(0).transform.rotation,
                Quaternion.LookRotation(inputVec),
                Time.deltaTime * 20.0f);
    }
}
