import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Dashboard from "./components/Dashboard";
function App() {
  return (
    <Router>
      <Routes>
        <div className="min-h-screen bg-gray-50">
          {/* Public routes under here */}
          <Route path="/" element={<Navigate to="/login" />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />

          {/* Protected Routes under here */}
          <Route path="/dashboard" element={<Dashboard />} />
        </div>
      </Routes>
    </Router>
  );
}

export default App;
