import React, { useState } from "react";
import { useParams, useNavigate } from "react-router-dom";

function CreatePassword() {
  const { token } = useParams();
  const navigate = useNavigate();
  const [password, setPassword] = useState("");

  const handleSubmit = async () => {
    try {
      const response = await fetch(
        "http://localhost:5294/api/student/create-password",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            token,
            password,
          }),
        }
      );

      const message = await response.text();

      if (response.ok) {
        alert("Password created successfully!");
        navigate("/");
      } else {
        alert(message || "Invalid token or request failed.");
      }
    } catch (error) {
      alert("Something went wrong.");
      console.log(error);
    }
  };

  return (
    <div className="dashboard-container">
      <div className="dashboard-card">
        <h2>Create Password</h2>

        <input
          type="password"
          placeholder="Enter password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <br /><br />

        <button onClick={handleSubmit}>
          Create Password
        </button>
      </div>
    </div>
  );
}

export default CreatePassword;