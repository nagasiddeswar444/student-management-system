import React, { useState } from "react";
import axios from "axios";
import "./styles/StudentForm.css";


const StudentForm = () => {
  const [formData, setFormData] = useState({
    name: "",
    fatherName: "",
    email: "",
    phone: "",
    collegeName: "",
    yearOfStudy: "",
    course: "",
    dateOfBirth: "",
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        "http://localhost:5294/api/student/register",
        formData
      );

      alert(response.data);

      setFormData({
        name: "",
        fatherName: "",
        email: "",
        phone: "",
        collegeName: "",
        yearOfStudy: "",
        course: "",
        dateOfBirth: "",
      });
    } catch (error: any) {
  console.error(error);

  if (error.response) {
    alert(JSON.stringify(error.response.data));
  } else {
    alert(error.message);
  }
}
  };

  return (
    <div style={{ width: "400px", margin: "50px auto" }}>
      <h2>Student Registration</h2>

      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="name"
          placeholder="Name"
          value={formData.name}
          onChange={handleChange}
          required
        />
        <br /><br />

        <input
          type="text"
          name="fatherName"
          placeholder="Father Name"
          value={formData.fatherName}
          onChange={handleChange}
          required
        />
        <br /><br />

        <input
          type="email"
          name="email"
          placeholder="Email"
          value={formData.email}
          onChange={handleChange}
          required
        />
        <br /><br />

        <input
          type="text"
          name="phone"
          placeholder="Phone"
          value={formData.phone}
          onChange={handleChange}
          required
        />
        <br /><br />

        <input
          type="text"
          name="collegeName"
          placeholder="College Name"
          value={formData.collegeName}
          onChange={handleChange}
          required
        />
        <br /><br />

        <input
          type="text"
          name="yearOfStudy"
          placeholder="Year of Study"
          value={formData.yearOfStudy}
          onChange={handleChange}
          required
        />
        <br /><br />

        <input
          type="text"
          name="course"
          placeholder="Course"
          value={formData.course}
          onChange={handleChange}
          required
        />
        <br /><br />

        <input
          type="date"
          name="dateOfBirth"
          value={formData.dateOfBirth}
          onChange={handleChange}
          required
        />
        <br /><br />

        <button type="submit">Register</button>
      </form>
    </div>
  );
};

export default StudentForm;