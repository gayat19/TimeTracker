import { Navigate } from "react-router-dom";

function ProtectedRoutes({ children }) {
  const token = sessionStorage.getItem("token");
  if (token != null) return children;
  return <Navigate to="/login" />;
}

export default ProtectedRoutes;
