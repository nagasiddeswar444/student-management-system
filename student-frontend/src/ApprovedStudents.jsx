import React, { useEffect, useState } from "react";
import "./styles/AdminDashboard.css";

function ApprovedStudents() {
  const [students, setStudents] = useState([]);

  useEffect(() => {
    Promise.all([
      fetch("http://localhost:5294/api/admin/approved").then((res) => res.json()),
      fetch("http://localhost:5294/api/admin/rejected").then((res) => res.json())
    ])
      .then(([approved, rejected]) => {
        setStudents([...approved, ...rejected]);
      })
      .catch((err) => console.log(err));
  }, []);

  return (
    <div className="dashboard-container">
      <div className="dashboard-card">
        <h2>Application History</h2>

        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Email</th>
              <th>Course</th>
              <th>Status</th>
              <th>Rejection Reason</th>
            </tr>
          </thead>

          <tbody>
            {students.map((student) => (
              <tr key={student.id}>
                <td>{student.name}</td>
                <td>{student.email}</td>
                <td>{student.course}</td>

                <td>
                  <span
                    className={`status ${student.status.toLowerCase()}`}
                  >
                    {student.status}
                  </span>
                </td>

                <td>
                  {student.status === "Rejected"
                    ? student.rejectionReason
                    : "-"}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default ApprovedStudents;