mergeInto(LibraryManager.library, {
  WebGLGameOver: function (userName, score) {
    window.dispatchReactUnityEvent("WebGLGameOver", UTF8ToString(userName), score);
  },
});