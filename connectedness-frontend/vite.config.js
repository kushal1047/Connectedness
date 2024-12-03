import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    host: "0.0.0.0", // Allow external access (not just localhost)
    port: 5173,
    allowedHosts: [
      "3733-2405-6e00-642-ef25-727e-6d4d-3271-7f94.ngrok-free.app", // Replace with your actual ngrok URL
    ],
  },
});
