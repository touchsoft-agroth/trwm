using UnityEngine;

namespace trwm.Source
{
    public class CameraMover
    {
        private Camera Camera
        {
            get
            {
                if (_camera == null)
                {
                    _camera = Camera.main;
                }

                return _camera;
            }
        }
        
        private Camera _camera;

        private Vector3? _storedPos;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                FocusWindow(null);
            }
        }
        
        public void FocusWindow(CodeWindow codeWindow)
        {
            if (_storedPos == null)
            {
                _storedPos = Camera.transform.position;
            }
            
            Camera.transform.position = new Vector3(10, 4, 5);
        }
    }
}