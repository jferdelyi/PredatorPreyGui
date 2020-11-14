namespace PredatorPreyGui.Sources.GameObject {
    /// <summary>
    /// If your object have update behavior
    /// </summary>
    interface IUpdatable {

        /// <summary>
        /// Update object
        /// </summary>
        /// <param name="deltaTime"></param>
        public abstract void Update(double deltaTime);
    }
}
