import { BASE_URL } from "./app.config";
import axios from "./axiosInterceptor";

export const login = (data) => axios.post(`${BASE_URL}/User/login`, data);

export const getAllUser = (pageNumber) =>
  axios.get(`${BASE_URL}/User?pageNumber=${pageNumber}`);

export const addUser = (data) => axios.post(`${BASE_URL}/User`, data);
export const editUser = (data) => axios.put(`${BASE_URL}/User/Report`, data);

export const updatePassword = (data) =>
  axios.put(`${BASE_URL}/User/Password`, data);

export const deleteUser = (data) =>
  axios.delete(`${BASE_URL}/User/id?id=${data}`);

export const getReportingDropdown = () =>
  axios.get(`${BASE_URL}/User/IdsByRole?role=manager`);

export const getAllUserIDs = (manangerId) => {
  axios.get(`${BASE_URL}/User/IdsByRole?id=${manangerId}`);
};

export const getAllUserEmployeeId = (id) =>
  axios.get(`${BASE_URL}/User/EmployeeIds?ManagerId=${id}`);
