mergeInto(LibraryManager.library, {
  Login: function (url, data, objectName, callback, fallback) {
    var parsedURL = Pointer_stringify(url);
    var parsedData = Pointer_stringify(data);
    var parsedObjectName = Pointer_stringify(objectName);
    var parsedCallback = Pointer_stringify(callback);
    var parsedFallback = Pointer_stringify(fallback);

    try {
      const responseData = sendData(parsedURL, parsedData);
      console.log(responseData);
      // unityGame.SendMessage(parsedObjectName, parsedCallback, responseData);
    } catch (error) {
      unityGame.SendMessage(parsedObjectName, parsedFallback, error);
    }
  },
});
