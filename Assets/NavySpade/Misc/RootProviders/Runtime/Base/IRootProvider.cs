using System;
using JetBrains.Annotations;

namespace Misc.RootProviders.Runtime.Base
{
    public interface IRootProvider
    {
        /// <summary>
        /// Root activity state.
        /// </summary>
        [PublicAPI]
        bool IsActive { get; }

        /// <summary>
        /// Causes root activation (possibly with animation).
        /// </summary>
        /// <param name="onComplete">
        /// Callback to complete the show animation.
        /// Called immediately if there is no animation.
        /// </param>
        [PublicAPI]
        void Show(Action onComplete);

        /// <summary>
        /// Causes hiding of the root (possibly with animation).
        /// </summary>
        /// <param name="onComplete">
        /// Callback to complete the hide animation.
        /// Called immediately if there is no animation.
        /// </param>
        [PublicAPI]
        void Hide(Action onComplete);

        /// <summary>
        /// Instantly hides the root.
        /// </summary>
        [PublicAPI]
        void HideInstantly();
    }
}