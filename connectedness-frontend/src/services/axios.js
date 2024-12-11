import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5291/api/",
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      try {
        const refreshToken = localStorage.getItem("refreshToken");
        const res = await api.post("/auth/refresh-token", refreshToken);
        localStorage.setItem("token", res.data.token);
        localStorage.setItem("refreshToken", res.data.refreshToken);
        originalRequest.headers.Authorization = `Bearer ${res.data.token}`;
        return api(originalRequest);
      } catch (err) {
        console.error("Token refresh failed", err);
        localStorage.clear();
        window.location.href("/login");
        return Promise.reject(err);
      }
    }
    return Promise.reject(error);
  }
);
export default api;
