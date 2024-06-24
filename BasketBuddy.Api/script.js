import http from "k6/http";
import { sleep } from "k6";

export default function () {
  // const host = "10.0.0.48";
  const host = "localhost";

  const token =
      "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhbm9uIjoiNDJmNDg4ZjktYTMxNC00YjQ5LTkxOTQtYjcwOTYxN2M4ZjA5IiwiZXhwIjo0ODU4ODkzNzUxLCJpc3MiOiJiYXNrZXRidWRkeS1hcGkiLCJhdWQiOiJhbm9uIn0.JuFh2n8MArNSiSHl9wh9kLoLVCqtkSKVf0CjNxndezg";

  http.get(`http://${host}:5031/api/v1/Share/create`, {
    headers: {
      "Content-Type": "application/json",
      Authorization: token,
    },
    body: {
      "data": {
        "shareId": "fe61e443-6cb3-430c-a509-78ea4fdae36e"
      }
    }
  });
  
  sleep(1);
}
