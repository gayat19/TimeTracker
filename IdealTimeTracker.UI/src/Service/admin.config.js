import { BASE_URL } from "./app.config";
import axios from "./axiosInterceptor";

export const getAllConfig = () =>
  axios.get(`${BASE_URL}/ApplicationConfig/Config`);

export const updateConfig = (data) =>
  axios.put(`${BASE_URL}/ApplicationConfig/Config`, data);
