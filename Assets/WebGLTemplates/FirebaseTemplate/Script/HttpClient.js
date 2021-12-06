const sendHttpRequest = (method, url, data) => {
  return fetch(url, {
    method: method,
    body: JSON.stringify(data),
    headers: data ? { "Content-Type": "application/json" } : {},
  }).then((response) => {
    const contentType = response.headers.get("content-type");
    console.log(contentType);
    if (response.status >= 400) {
      // !response.ok
      const error = new Error("Something went wrong!");
      response.json().then((errResData) => {
        const error = new Error("Something went wrong!");
        error.data = errResData;
        throw error;
      });
    }
    return response.text();
  });
};

const getData = (url) => {
  sendHttpRequest("GET", url + "api/users").then((responseData) => {
    console.log(responseData);
  });
};

const sendData = (url, data) => {
  console.log(url);
  console.log(data);
  sendHttpRequest("POST", url, JSON.parse(data))
    .then((responseData) => {
      console.log(responseData);
    })
    .catch((err) => {
      console.error(err, err.data);
    });
  return responseData;
};
// https://reqres.in/api/users
