import { BASE_URL } from "./app.config";
import axios from "./axiosInterceptor";

export const getAllActivity = () => axios.get(`${BASE_URL}/UserActivity`);

export const addActivity = (data) =>
  axios.post(`${BASE_URL}/UserActivity`, data);
export const editActivity = (data) =>
  axios.put(`${BASE_URL}/UserActivity`, data);

export const deleteActivity = (data) =>
  axios.delete(`${BASE_URL}/UserActivity?id=${data}`);
