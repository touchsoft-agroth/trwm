using UnityEngine;

namespace trwm.Source
{
    public class GameUtils
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }
}