mergeInto(LibraryManager.library, {
  WebGLGameOver: function (shipsDestroyed, obstaclesDestroyed) {
    window.dispatchReactUnityEvent("WebGLGameOver", shipsDestroyed, obstaclesDestroyed);
  },
});