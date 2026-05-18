import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";

function AdminStudentDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [student, setStudent] = useState(null);

  useEffect(() => {
    fetch(`https://localhost:5294/api/student/${id}`)
      .then((res) => res.json())
      .then((data) => setStudent(data))
      .catch((err) => console.log(err));
  }, [id]);

  const approveStudent = async () => {
    const response = await fetch(
      `https://localhost:5294/api/admin/approve/${id}`,
      {
        method: "PUT"
      }
    );

    const message = await response.text();
    alert(message);

    if (response.ok) {
      navigate("/admin/dashboard");
    }
  };

  const rejectStudent = async () => {
    const reason = prompt("Enter rejection reason:");

    if (!reason) return;

    const response = await fetch(
      `http://localhost:5294/api/admin/reject/${id}`,
      {
        method: "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          decision: "Rejected",
          rejectionReason: reason
        })
      }
    );

    const message = await response.text();
    alert(message);

    if (response.ok) {
      navigate("/admin/dashboard");
    }
  };

  if (!student) {
    return <h2>Loading...</h2>;
  }

  return (
    <div className="dashboard-container">
      <div className="dashboard-card">
        <h2>Student Details</h2>

        <p><strong>Name:</strong> {student.name}</p>
        <p><strong>Father Name:</strong> {student.fatherName}</p>
        <p><strong>Email:</strong> {student.email}</p>
        <p><strong>Phone:</strong> {student.phone}</p>
        <p><strong>College Name:</strong> {student.collegeName}</p>
        <p><strong>Year of Study:</strong> {student.yearOfStudy}</p>
        <p><strong>Course:</strong> {student.course}</p>
        <p><strong>Status:</strong> {student.status}</p>

        <button className="approve-btn" onClick={approveStudent}>
          Approve
        </button>

        <button className="reject-btn" onClick={rejectStudent}>
          Reject
        </button>
      </div>
    </div>
  );
}

export default AdminStudentDetails;