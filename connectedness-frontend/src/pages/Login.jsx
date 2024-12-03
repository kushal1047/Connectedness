import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/axios";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const res = await api.post("/users/login", {
        Email: email,
        Password: password,
      });
      localStorage.setItem("token", res.data.token);
      localStorage.setItem("userId", res.data.userId);
      navigate("/dashboard");
    } catch (error) {
      setError(
        error.response?.data.message || "Login Failed! Please try again."
      );
    }
  };
  return (
    <div className="flex items-center justify-center bg-[#FFE0E6] min-h-screen">
      <div className="w-full max-w-md p-8 rounded-2xl shadow-lg bg-white">
        <h2 className="text-3xl font-bold mb-6 text-center text-[#004FA4]">
          Login and Connect
        </h2>
        <form onSubmit={handleSubmit} className="flex flex-col gap-5">
          <div>
            <label htmlFor="email" className="block mb-1 text-sm font-medium">
              Email
            </label>
            <input
              id="email"
              type="email"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
            />
          </div>
          <div>
            <label
              htmlFor="password"
              className="block text-sm mb-1 font-medium"
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
              className="w-full py-2 px-4 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
            />
          </div>
          {error && <div className="text-red-600">{error}</div>}
          <button type="submit">Sign in</button>
        </form>
        <p className="text-sm mt-4 text-center">
          Don't have an account?{" "}
          <a href="/register" className="hover:underline">
            Register Here
          </a>
        </p>
      </div>
    </div>
  );
}
