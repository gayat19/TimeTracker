import { Navigate, Route, Routes } from "react-router-dom";
import Navbar from "../../components/Navbar/Navbar";
import UserLog from "../../components/UserLog/UserLog";
import "./HomePage.css";
import User from "../../components/User/User";
import UserActivity from "../../components/UserActiviy/UserActivity";
import ManagerProtectedRoutes from "../../components/Routes/ManagerProtectedRoutes";
import AdminProtectedRoutes from "../../components/Routes/AdminProtectedRoutes";
import UserConsolidate from "../../components/UserConsolidate/UserConsolidate";
import Config from "../../components/Config/Config";
import UserDetailLog from "../../components/UserDetailLog/UserDetailLog";
import UserLogin from "../../components/UserLogin/UserLogin";
import ProtectedRoutes from "../../components/Routes/ProtectedRoute";

function HomePage() {
  const role = sessionStorage.getItem("role");
  return (
    <div className="homepage-container">
      <Navbar role={role} />
      <div className="homepage-content">
        <Routes>
          <Route
            path=""
            element={
              role === "admin" ? (
                <Navigate to="user" />
              ) : role === "manager" ? (
                <Navigate to="log" />
              ) : (
                <Navigate to={"/userlogin"} />
              )
            }
          ></Route>
          <Route
            path="userlogin"
            element={
              <ProtectedRoutes>
                <UserLogin />
              </ProtectedRoutes>
            }
          ></Route>

          <Route
            path="log"
            element={
              <ManagerProtectedRoutes>
                <UserLog />
              </ManagerProtectedRoutes>
            }
          ></Route>

          <Route
            path="detail"
            element={
              <ManagerProtectedRoutes>
                <UserDetailLog />
              </ManagerProtectedRoutes>
            }
          ></Route>

          <Route
            path="config"
            element={
              <AdminProtectedRoutes>
                <Config />
              </AdminProtectedRoutes>
            }
          ></Route>

          <Route
            path="consolidate"
            element={
              <ManagerProtectedRoutes>
                <UserConsolidate />
              </ManagerProtectedRoutes>
            }
          ></Route>
          <Route
            path="user"
            element={
              <AdminProtectedRoutes>
                <User />
              </AdminProtectedRoutes>
            }
          ></Route>
          <Route
            path="activity"
            element={
              <AdminProtectedRoutes>
                <UserActivity />
              </AdminProtectedRoutes>
            }
          ></Route>
        </Routes>
      </div>
    </div>
  );
}

export default HomePage;
