import React from "react";
import { Navigate } from "react-router-dom";

function ProtectedRoute({ children }) {
  const isAdminLoggedIn = localStorage.getItem("adminLoggedIn");

  return isAdminLoggedIn ? children : <Navigate to="/admin/login" />;
}

export default ProtectedRoute;