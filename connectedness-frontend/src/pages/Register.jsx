import { useState } from "react";
import { register } from "../services/authService";
import api from "../services/axios";
import logo from "../assets/connectedness-full-logo.png";

export default function Register() {
  const [fullName, setFullName] = useState("");
  const [gender, setGender] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      await api.post("/users/register", {
        FullName: fullName,
        Gender: gender,
        Email: email,
        Password: password,
      });
      setSuccess("Registration Successful! Please click login below.");
      setError("");
    } catch (error) {
      setError(
        error.response?.data?.message ||
          "Registration failed! Please try again."
      );
      setSuccess("");
    }
  };
  return (
    <div className="flex flex-col items-center justify-center min-h-screen">
      <img src={logo} alt="Logo" className="mb-6 w-48 h-auto" />
      <div className="w-full max-w-md p-8 rounded-2xl shadow-lg bg-white">
        <h2 className="text-3xl font-bold mb-6 text-center">
          Create an Account
        </h2>
        <form onSubmit={handleSubmit} className="flex flex-col gap-5">
          <div>
            <label
              htmlFor="fullName"
              className="block text-sm mb-1 font-medium"
            >
              Full Name
            </label>
            <input
              id="fullName"
              type="text"
              placeholder="Full Name"
              value={fullName}
              onChange={(e) => setFullName(e.target.value)}
              required
              className="w-full py-2 px-4 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#FF2D55]"
            />
          </div>

          <div>
            <label htmlFor="gender" className="block mb-1 text-sm font-medium">
              Gender
            </label>
            <input
              id="gender"
              type="text"
              placeholder="Gender"
              value={gender}
              onChange={(e) => setGender(e.target.value)}
              required
              className="w-full py-2 px-4 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#FF2D55]"
            />
          </div>

          <div>
            <label htmlFor="email" className="block mb-1 font-medium text-sm">
              Email
            </label>
            <input
              id="email"
              type="email"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              className="w-full py-2 px-4 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#FF2D55]"
            />
          </div>

          <div>
            <label
              htmlFor="password"
              className="block mb-1 font-medium text-sm"
            >
              Password
            </label>
            <input
              id="password"
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#FF2D55]"
            />
          </div>
          {success && <p className="text-green-600">{success}</p>}
          {error && <p className="text-red-600">{error}</p>}
          <button type="submit">Register</button>
        </form>
        <p className="mt-4 text-sm text-center">
          Already have an account?{" "}
          <a href="/login" className="hover:underline">
            Let's login
          </a>
        </p>
      </div>
    </div>
  );
}
