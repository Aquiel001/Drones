namespace Drones.Shared
{
    /// <summary>
    /// Drone Status
    /// </summary>
    public enum DroneStatus
    {
        /// <summary>
        /// Idle
        /// </summary>
        IDLE=0,
        /// <summary>
        /// Loading
        /// </summary>
        LOADING=1,
        /// <summary>
        /// Loaded
        /// </summary>
        LOADED=2,
        /// <summary>
        /// Delivering
        /// </summary>
        DELIVERING=3,
        /// <summary>
        /// Delivered
        /// </summary>
        DELIVERED=4,
        /// <summary>
        /// Returning
        /// </summary>
        RETURNING=5
    }
}
