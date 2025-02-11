using UnityEngine;

public class CameraTilt : MonoBehaviour
{
        public Transform cameraTransform;
        public float tiltAngle = 45f; 
        public float resetSpeed = 5f;
    
        private bool isMovingHorizontal = false;
        private bool isMovingVertical = false;
        private bool isResetting = false;
    
        void FixedUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
    
            if (verticalInput < 0)
            {
                RotateCamera(Vector3.left);
                isMovingHorizontal = true;
                isResetting = false; 
            }
            else if (verticalInput > 0)
            {
                RotateCamera(Vector3.right);
                isMovingHorizontal = true;
                isResetting = false; 
            }
            else
            {
                isMovingHorizontal = false;
            }
    
            if (horizontalInput > 0)
            {
                RotateCamera(Vector3.back);
                isMovingVertical = true;
                isResetting = false;
            }
            else if (horizontalInput < 0)
            {
                RotateCamera(Vector3.forward); 
                isMovingVertical = true;
                isResetting = false;
            }
            else
            {
                isMovingVertical = false;
            }
    
            if (!isMovingHorizontal && !isMovingVertical && !isResetting)
            {
                isResetting = true;
            }
    
            if (isResetting)
            {
                ResetCameraRotation();
            }
        }
    
        void RotateCamera(Vector3 rotationAxis)
        {
            Quaternion targetRotation = Quaternion.Euler(rotationAxis * tiltAngle);
            cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, targetRotation, Time.deltaTime * 5f);
        }
    
        void ResetCameraRotation()
        {
            Quaternion targetRotation = Quaternion.identity; // Quaternion.identity represents no rotation
            cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, targetRotation, Time.deltaTime * resetSpeed);
        }
}
