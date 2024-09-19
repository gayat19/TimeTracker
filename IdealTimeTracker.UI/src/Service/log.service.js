import { BASE_URL } from "./app.config";
import axios from "./axiosInterceptor";

export const getAllLogs = (payload) =>
  axios.post(`${BASE_URL}/UserLog`, payload);

export const getAllLogsExcel = (payload) =>
  axios.post(`${BASE_URL}/UserLog/Excel`, payload);

export const getCondolidatedLogs = (payload) =>
  axios.post(`${BASE_URL}/UserLog/consolidate`, payload);

export const getCondolidatedLogsExcel = (payload) =>
  axios.post(`${BASE_URL}/UserLog/excel/consolidate`, payload);

export const getDetailLogForUser = (payload) =>
  axios.post(`${BASE_URL}/UserLog/details`, payload);
