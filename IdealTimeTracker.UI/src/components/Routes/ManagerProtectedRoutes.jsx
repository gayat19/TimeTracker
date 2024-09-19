import { Navigate } from "react-router-dom";

function ManagerProtectedRoutes({ children }) {
  const role = sessionStorage.getItem("role");
  if (role != null && role === "manager") return children;
  return <Navigate to="/" />;
}

export default ManagerProtectedRoutes;
