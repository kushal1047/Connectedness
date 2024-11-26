import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Dashboard from "./components/Dashboard";
import GroupDetails from "./pages/GroupDetails";
import TestPage from "./pages/testPage";
import CreateQuestions from "./pages/CreateQuestions";
function App() {
  return (
    <Router>
      <Routes>
        {/* Public routes under here */}
        <Route path="/" element={<Navigate to="/login" />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        {/* Protected Routes under here */}
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/group/:groupId" element={<GroupDetails />} />
        <Route path="/create-questions" element={<CreateQuestions />} />
        <Route path="/test" element={<TestPage />} />
      </Routes>
    </Router>
  );
}

export default App;
