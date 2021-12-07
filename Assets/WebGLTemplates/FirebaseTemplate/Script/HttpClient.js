const sendHttpRequest = (method, url, data) => {
  // fetch(url)
  //   .then((response) => response.json())
  //   .then((data) => console.log(data));

  return fetch(url, {
    method: method,
    body: data,
    headers: data ? { "Content-Type": "application/json" } : {},
  }).then((response) => {
    const contentType = response.headers.get("content-type");
    console.log(contentType);
    // if (response.status >= 400) {
    //   // !response.ok
    //   const error = new Error("Something went wrong!");
    //   response.json().then((errResData) => {
    //     const error = new Error("Something went wrong!");
    //     error.data = errResData;
    //     throw error;
    //   });
    // }
    return response.text();
  });
};

async function postData(url = "", data = {}) {
  // Default options are marked with *
  const response = await fetch(url, {
    method: "POST", // *GET, POST, PUT, DELETE, etc.
    mode: "cors", // no-cors, *cors, same-origin
    cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
    credentials: "same-origin", // include, *same-origin, omit
    headers: {
      "Content-Type": "application/json",
      // 'Content-Type': 'application/x-www-form-urlencoded',
    },
    redirect: "follow", // manual, *follow, error
    referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
    body: JSON.stringify(data), // body data type must match "Content-Type" header
  });
  return response.json(); // parses JSON response into native JavaScript objects
}

const getData = (url) => {
  sendHttpRequest("GET", url + "api/users").then((responseData) => {
    console.log(responseData);
  });
};

const sendData = async (url, data, objectName, callback, fallback) => {
  await sendHttpRequest("POST", url, data)
    .then((responseData) => {
      console.log("httpclient");
      console.log(responseData);
      unityInstance.SendMessage(objectName, callback, responseData);
      return responseData;
    })
    .catch((err) => {
      console.error(err, err.data);
    });
};
// https://reqres.in/api/users
