import { createContext, useEffect, useState } from "react";
import jwtDecode from "jwt-decode";

const UserContext = createContext();

export default function UserContextProvider({ children }) {
  const [user, setUser] = useState(null);
  const updateUser = () => {
    const token = localStorage.getItem("token");
    if (token) {
      try {
        const decoded = jwtDecode(token);
        setUser(decoded);
      } catch (error) {
        console.error("Couldn't decode token", error);
        setUser(null);
      }
    } else {
      setUser(null);
    }
  };

  useEffect(() => {
    updateUser();
    window.addEventListener("storage", updateUser);
    window.addEventListener("update-user", updateUser);
    return () => {
      window.removeEventListener("storage");
      window.removeEventListener("update-user");
    };
  }, []);
  return <UserContext.Provider value={user}>{children}</UserContext.Provider>;
}
