const API_BASE = "http://localhost:5291/api/Users";

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
