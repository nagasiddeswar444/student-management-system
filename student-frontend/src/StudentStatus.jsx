import React, { useState } from "react";

function StudentStatus() {
  const [email, setEmail] = useState("");
  const [student, setStudent] = useState(null);
  const [error, setError] = useState("");

  const checkStatus = async () => {
    try {
      setError("");

      const response = await fetch(
        `http://localhost:5294/api/student/status/${email}`
      );

      if (!response.ok) {
        throw new Error("Student not found");
      }

      const data = await response.json();
      setStudent(data);
    } catch (err) {
      setStudent(null);
      setError("Student not found");
    }
  };

  return (
    <div className="dashboard-container">
      <div className="dashboard-card">
        <h2>Check Application Status</h2>

        <input
          type="email"
          placeholder="Enter your email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          style={{
            padding: "10px",
            width: "300px",
            marginBottom: "15px"
          }}
        />

        <br />

        <button className="approve-btn" onClick={checkStatus}>
          Check Status
        </button>

        {error && <p style={{ color: "red" }}>{error}</p>}

        {student && (
          <div style={{ marginTop: "20px" }}>
            <p><strong>Name:</strong> {student.name}</p>
            <p><strong>Email:</strong> {student.email}</p>
            <p><strong>Course:</strong> {student.course}</p>
            <p><strong>Status:</strong> {student.status}</p>
          </div>
        )}
      </div>
    </div>
  );
}

export default StudentStatus;