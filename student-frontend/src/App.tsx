import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import StudentForm from "./StudentForm";
import AdminLogin from "./AdminLogin";
import AdminDashboard from "./AdminDashboard";
import ProtectedRoute from "./ProtectedRoute";
import ApprovedStudents from "./ApprovedStudents";
import StudentStatus from "./StudentStatus";
import CreatePassword from "./CreatePassword";
import StudentLogin from "./StudentLogin";
import StudentDashboard from "./StudentDashboard";
import AdminStudentDetails from "./AdminStudentDetails";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
  path="/admin/approved"
  element={
    <ProtectedRoute>
      <ApprovedStudents />
    </ProtectedRoute>
  }
/>

        <Route path="/" element={<StudentForm />} />
        <Route
  path="/admin/student/:id"
  element={
    <ProtectedRoute>
      <AdminStudentDetails />
    </ProtectedRoute>
  }
/>
        <Route path="/student/dashboard" element={<StudentDashboard />} />
        <Route path="/student/login" element={<StudentLogin />} />
        <Route path="/admin/login" element={<AdminLogin />} />
        <Route path="/create-password/:token" element={<CreatePassword />} />
        <Route
  path="/admin/dashboard"
  element={
    <ProtectedRoute>
      <AdminDashboard />
    </ProtectedRoute>
  }
/>
<Route path="/status" element={<StudentStatus />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;