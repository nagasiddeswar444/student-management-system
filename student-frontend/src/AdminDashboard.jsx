import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./styles/AdminDashboard.css";

function AdminDashboard() {
  const [students, setStudents] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetch("http://localhost:5294/api/admin/pending")
      .then((res) => res.json())
      .then((data) => setStudents(data))
      .catch((err) => console.log(err));
  }, []);

  const approveStudent = async (id) => {
    try {
      const response = await fetch(
        `http://localhost:5294/api/admin/approve/${id}`,
        {
          method: "PUT",
        }
      );

      const message = await response.text();
      alert(message);

      if (response.ok) {
        setStudents((prev) =>
          prev.filter((student) => student.id !== id)
        );
      }
    } catch (error) {
      console.log(error);
    }
  };

  const rejectStudent = async (id) => {
    try {
      const reason = prompt("Enter rejection reason:");

      if (!reason) return;

      const response = await fetch(
        `http://localhost:5294/api/admin/reject/${id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            decision: "Rejected",
            rejectionReason: reason
          }),
        }
      );

      const message = await response.text();
      alert(message);

      if (response.ok) {
        setStudents((prev) =>
          prev.filter((student) => student.id !== id)
        );
      }
    } catch (error) {
      console.log(error);
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("adminLoggedIn");
    navigate("/admin/login");
  };

  return (
    <div className="dashboard-container">
      <div className="dashboard-card">
        <button className="logout-btn" onClick={handleLogout}>
          Logout
        </button>

        <button
          className="logout-btn"
          style={{ marginRight: "10px" }}
          onClick={() => navigate("/admin/approved")}
        >
          Application History
        </button>

        <h2>Admin Dashboard</h2>

        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Email</th>
              <th>Status</th>
              <th>Action</th>
            </tr>
          </thead>

          <tbody>
            {students.map((student) => (
              <tr key={student.id}>
                <td
                  style={{
                    cursor: "pointer",
                    color: "blue",
                    textDecoration: "underline"
                  }}
                  onClick={() => navigate(`/admin/student/${student.id}`)}
                >
                  {student.name}
                </td>

                <td>{student.email}</td>

                <td>
                  <span className={`status ${student.status.toLowerCase()}`}>
                    {student.status}
                  </span>
                </td>

                <td>
                  <button
                    className="approve-btn"
                    onClick={() => approveStudent(student.id)}
                  >
                    Approve
                  </button>

                  <button
                    className="reject-btn"
                    onClick={() => rejectStudent(student.id)}
                  >
                    Reject
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default AdminDashboard;