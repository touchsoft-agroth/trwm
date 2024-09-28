using System;

namespace trwm.Source.Modes
{
    public enum ModeType
    {
        Normal,
        Drone,
        DroneEntityPlacement,
        Window,
        Camera,
        Farm,
        Maze
    }

    public static class ModeTypeExtensions
    {
        public static string ToDisplayName(this ModeType modeType)
        {
            return modeType switch
            {
                ModeType.Normal => "Normal",
                ModeType.Drone => "Drone",
                ModeType.DroneEntityPlacement => "Entity",
                ModeType.Window => "Window",
                ModeType.Camera => "Camera",
                ModeType.Farm => "Farm",
                ModeType.Maze => "Maze",
                _ => modeType.ToString()
            };
        }
    }
}