namespace trwm.Source.Game
{
    public class GameGateway
    {
        public DroneController Drone { get; }
        public WindowManager Windows { get; }

        public GameGateway(DroneController droneController, WindowManager windowManager)
        {
            Drone = droneController;
            Windows = windowManager;
        }
    }
}