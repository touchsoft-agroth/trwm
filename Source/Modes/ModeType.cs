using System;

namespace trwm.Source.Modes
{
    public enum ModeType
    {
        Normal,
        Drone,
        DroneEntityPlacement,
        Window,
        Camera
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
                _ => throw new NotImplementedException()
            };
        }
    }
}