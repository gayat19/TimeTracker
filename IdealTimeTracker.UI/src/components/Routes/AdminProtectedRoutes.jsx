import { Navigate } from "react-router-dom";

function AdminProtectedRoutes({ children }) {
  const role = sessionStorage.getItem("role");
  if (role != null && role === "admin") return children;
  return <Navigate to="/" />;
}

export default AdminProtectedRoutes;
