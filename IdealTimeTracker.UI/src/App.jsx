import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import HomePage from "./Pages/HomePage/HomePage";
import Login from "./Pages/Login/Login";
import ProtectedRoutes from "./components/Routes/ProtectedRoute";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useState } from "react";
function App() {
  return (
    <>
      <ToastContainer />

      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<Login />}></Route>
          <Route
            path="/*"
            element={
              <ProtectedRoutes>
                <HomePage />
              </ProtectedRoutes>
            }
          ></Route>
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
