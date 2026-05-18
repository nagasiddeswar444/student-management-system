import React, { useEffect, useState } from "react";

function StudentDashboard() {
  const [student, setStudent] = useState(null);

  useEffect(() => {
    const email = localStorage.getItem("studentEmail");

    fetch(`http://localhost:5294/api/student/details/${email}`)
      .then((res) => res.json())
      .then((data) => setStudent(data))
      .catch((err) => console.log(err));
  }, []);

  if (!student) {
    return <h2>Loading...</h2>;
  }

  return (
    <div className="dashboard-container">
      <div className="dashboard-card">
        <h2>Student Dashboard</h2>

        <p><strong>Name:</strong> {student.name}</p>
        <p><strong>Father Name:</strong> {student.fatherName}</p>
        <p><strong>Email:</strong> {student.email}</p>
        <p><strong>Phone:</strong> {student.phone}</p>
        <p><strong>College:</strong> {student.collegeName}</p>
        <p><strong>Year:</strong> {student.yearOfStudy}</p>
        <p><strong>Course:</strong> {student.course}</p>
        <p><strong>Status:</strong> {student.status}</p>
      </div>
    </div>
  );
}

export default StudentDashboard;