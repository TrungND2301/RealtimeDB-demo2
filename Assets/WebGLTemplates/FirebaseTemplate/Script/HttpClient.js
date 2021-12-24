async function sendHttpRequest(method, url, token, data) {
  let myHeaders = new Headers();

  if (data) myHeaders.append("Content-Type", "application/json");
  if (token) myHeaders.append("x-auth-token", token);

  const response = await fetch(url, {
    method: method,
    body: data,
    headers: myHeaders,
  });

  const contentType = response.headers.get("content-type");
  console.log(contentType);

  switch (contentType) {
    case "application/json":
      return response.json();
    case "text/html; charset=UTF-8":
      return response.text();
    default:
      return response.text();
  }
}

const getData = (url, token, objectName, callback, fallback) => {
  sendHttpRequest("GET", url, token).then((responseData) => {
    unityInstance.SendMessage(objectName, callback, responseData);
  });
};

const sendData = (url, token, data, objectName, callback, fallback) => {
  sendHttpRequest("POST", url, token, data).then((responseData) => {
    unityInstance.SendMessage(objectName, callback, responseData);
  });
};
