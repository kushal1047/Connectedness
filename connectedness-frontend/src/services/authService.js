const API_BASE = "http://localhost:5291/api/Users";

export async function login(email, password) {
  const res = await fetch(`${API_BASE}/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ Email: email, Password: password }),
  });
  if (!res.ok) {
    const error = await res.json();
    throw new Error(error.message || "Login failed!");
  }
  return res.json();
}

export async function register(fullName, gender, email, password) {
  const res = await fetch(`${API_BASE}/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      FullName: fullName,
      Email: email,
      Gender: gender,
      Password: password,
    }),
  });
  if (!res.ok) {
    const error = await res.json();
    throw new Error(error.message || "Registeration failed!");
  }
  return res.json();
}
