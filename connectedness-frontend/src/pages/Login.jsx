import { useState } from "react";
import { login } from "../services/authService";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const { token } = await login(email, password);
      localStorage.setItem("token", token);
      alert("Login successful.");
      navigate("/dashboard");
    } catch (error) {
      alert(error.message);
    }
  };
  return (
    <div className="flex items-center justify-center bg-gradient-to-br from-blue-50 to-blue-100 min-h-screen">
      <div className="w-full max-w-md p-8 rounded-2xl shadow-lg bg-white">
        <h2 className="text-3xl font-bold mb-6 text-center text-green-600">
          Login and Connect
        </h2>
        <form onSubmit={handleSubmit} className="flex flex-col gap-5">
          <div>
            <label
              htmlFor="email"
              className="block mb-1 text-sm font-medium text-gray-700"
            >
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
              className="block text-sm mb-1 font-medium text-gray-700"
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
          <button
            type="submit"
            className="bg-green-600 hover:bg-green-700 text-white rounded-lg py-2 px-4 font-semibold transition duration-200"
          >
            Sign in
          </button>
        </form>
        <p className="text-sm mt-4 text-gray-700 text-center">
          Don't have an account?{" "}
          <a href="/register" className="text-green-600 hover:underline">
            Register Here
          </a>
        </p>
      </div>
    </div>
  );
}
