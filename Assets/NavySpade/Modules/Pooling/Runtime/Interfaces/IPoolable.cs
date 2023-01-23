namespace NavySpade.Modules.Pooling.Runtime.Interfaces
{
    /// <summary>
    /// Classes that implement <see cref="IPoolable"/> will receive calls from the ObjectPool.
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// Invoked when the object is instantiated.
        /// </summary>
        void OnPoolCreate();

        /// <summary>
        /// Invoked when the object is grabbed from the object pool.
        /// </summary>
        void OnPoolGet();

        /// <summary>
        /// Invoked when the object is released back to the object pool.
        /// </summary>
        void OnPoolRelease();

        /// <summary>
        /// Invoked when the object is reused.
        /// </summary>
        void OnPoolReuse();
    }
}
