import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

function StudentLogin() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    const response = await fetch(
      "http://localhost:5294/api/student/login",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          email,
          password
        })
      }
    );

    const message = await response.text();

    if (response.ok) {
      localStorage.setItem("studentEmail", email);
      alert("Login successful");
      navigate("/student/dashboard");
    } else {
      alert(message);
    }
  };

  return (
    <div className="dashboard-container">
      <div className="dashboard-card">
        <h2>Student Login</h2>

        <input
          type="email"
          placeholder="Enter email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <br /><br />

        <input
          type="password"
          placeholder="Enter password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <br /><br />

        <button onClick={handleLogin}>
          Login
        </button>
      </div>
    </div>
  );
}

export default StudentLogin;