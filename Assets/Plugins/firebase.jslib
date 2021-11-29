mergeInto(LibraryManager.library, {
  Hello: function () {
    window.alert("Hello, world!");
  },

  GetJSON: function (path, objectName, callback, fallback) {
    var parsedPath = Pointer_stringify(path);
    var parsedObjectName = Pointer_stringify(objectName);
    var parsedCallback = Pointer_stringify(callback);
    var parsedFallback = Pointer_stringify(fallback);
    // if (analytics) {
    //   window.alert("Hello, world!");
    // } else {
    //   window.alert("Hello, hell!");
    // }
    try {
      databaseGame
        .ref(parsedPath)
        .once("value")
        .then(function (snapshot) {
          window.alert("try " + JSON.stringify(snapshot.val()));
          unityGame.SendMessage(
            parsedObjectName,
            parsedCallback,
            JSON.stringify(snapshot.val())
          );
        });
    } catch (error) {
      window.alert("catch " + error.message);
      unityGame.SendMessage(
        parsedObjectName,
        parsedFallback,
        "There was an error: " + error.message
      );
    }
  },
});
