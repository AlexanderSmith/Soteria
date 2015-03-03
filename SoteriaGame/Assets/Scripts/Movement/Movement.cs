using UnityEngine;
using System.Collections;

public class Movement {

    public void Move(Vector3 input, float speed, Transform whatToTransform)
    {
        Vector3 relativeInput = input;//Camera.main.transform.TransformDirection(input);
        Vector3 prevPos = whatToTransform.position;
        Debug.Log(relativeInput);
        
        whatToTransform.rigidbody.MovePosition((relativeInput * speed) + whatToTransform.position);//.rigidbody.MovePosition(relativeInput * speed);
        
        whatToTransform.Rotate(whatToTransform.up, Vector3.Angle(prevPos, whatToTransform.position));
        //MakeTransformLookAtMoveDirection(whatToTransform, input);
    }

    private void MakeTransformLookAtMoveDirection(Transform whatToTransform, Vector3 inputVec)
    {
        // Rotation
        if (inputVec != Vector3.zero)
            whatToTransform.GetChild(0).transform.rotation = Quaternion.Slerp(whatToTransform.GetChild(0).transform.rotation,
                Quaternion.LookRotation(inputVec),
                Time.deltaTime * 20.0f);//*/
    }
}
