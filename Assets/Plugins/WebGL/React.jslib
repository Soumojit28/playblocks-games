mergeInto(LibraryManager.library, {
  WebGLGameOver: function (shipsDestroyed, obstaclesDestroyed) {
    window.dispatchReactUnityEvent("WebGLGameOver", shipsDestroyed, obstaclesDestroyed);
  },
  
  WebGLPowerUpActive: function (duration) {
    window.dispatchReactUnityEvent("WebGLPowerUpActive", duration);
  },
});