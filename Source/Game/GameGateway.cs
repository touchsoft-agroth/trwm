using trwm.Source.Game.Maze;

namespace trwm.Source.Game
{
    public class GameGateway
    {
        public DroneController Drone { get; }
        public WindowManager Windows { get; }
        public WorkspaceController Workspace { get; }
        public FarmController Farm { get; }
        public MazeManager Mazes { get; }

        public GameGateway(DroneController droneController, WindowManager windowManager,
            WorkspaceController workspaceController, FarmController farmController, MazeManager mazeManager)
        {
            Drone = droneController;
            Windows = windowManager;
            Workspace = workspaceController;
            Farm = farmController;
            Mazes = mazeManager;
        }
    }
}