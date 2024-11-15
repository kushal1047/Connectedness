import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import "./App.css";

function App() {
  return (
    <Router>
      <Routes>
        {/* Public routes under here */}
        <Route path="/" element={<Navigate to="/login" />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />

        {/* Protected Routes under here */}
        {/* <Route path="./dashboard" element={<Dashboard />}/> */}
      </Routes>
    </Router>
  );
}

export default App;
