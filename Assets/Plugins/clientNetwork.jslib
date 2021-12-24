mergeInto(LibraryManager.library, {
  Login: function (url, data, objectName, callback, fallback) {
    var parsedURL = Pointer_stringify(url);
    var parsedData = Pointer_stringify(data);
    var parsedObjectName = Pointer_stringify(objectName);
    var parsedCallback = Pointer_stringify(callback);
    var parsedFallback = Pointer_stringify(fallback);

    sendData(
      parsedURL, "",
      parsedData,
      parsedObjectName,
      parsedCallback,
      parsedFallback
    );
  },

  SendData: function (url, token, data, objectName, callback, fallback) {
    var parsedURL = Pointer_stringify(url);
    var parsedToken = Pointer_stringify(token);
    var parsedData = Pointer_stringify(data);
    var parsedObjectName = Pointer_stringify(objectName);
    var parsedCallback = Pointer_stringify(callback);
    var parsedFallback = Pointer_stringify(fallback);

    sendData(
      parsedURL,
      parsedToken,
      parsedData,
      parsedObjectName,
      parsedCallback,
      parsedFallback
    );
  },

  GetData: function (url, token, objectName, callback, fallback) {
    var parsedURL = Pointer_stringify(url);
    var parsedToken = Pointer_stringify(token);
    var parsedObjectName = Pointer_stringify(objectName);
    var parsedCallback = Pointer_stringify(callback);
    var parsedFallback = Pointer_stringify(fallback);

    getData(
      parsedURL,
      parsedToken,
      parsedObjectName,
      parsedCallback,
      parsedFallback
    );
  },
});
