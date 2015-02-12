using UnityEngine;
using System.Collections;

public class Movement {

    public void Move(Vector3 input, float speed, Transform whatToTransform)
    {
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
