import axios from "axios";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function requestInterceptor(config) {
  const token = sessionStorage.getItem("token");

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
}

function responseInterceptor(response) {
  return response;
}

function errorInterceptor(error) {
  // if (error?.request) {
  // toast.error("No response from server.");
  // }

  if (error.code === "ERR_NETWORK")
    toast.error(error.message + ": Server not responding try again later");
  return Promise.reject(error);
}

axios.interceptors.request.use(requestInterceptor);
axios.interceptors.response.use(responseInterceptor, errorInterceptor);

export default axios;
