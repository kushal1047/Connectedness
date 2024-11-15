const API_BASE = "https://localhost:5191/api/users";

export async function login(email, password) {
  const res = await fetch(`${API_BASE}/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ Email: email, Password: password }),
  });
  if (!res.ok) throw new Error("Login failed!");
  return res.json();
}

export async function register(fullName, email, gender, password) {
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
  if (!res.ok) throw new Error("Registeration failed!");
  return res.json();
}
