import { createContext, useContext, useMemo, useState } from "react";
import { login as loginRequest } from "../api/auth";

function decodeToken(token) {
  try {
    const payload = token.split(".")[1];
    const json = atob(payload.replace(/-/g, "+").replace(/_/g, "/"));
    const claims = JSON.parse(decodeURIComponent(escape(json)));

    const buscar = (sufijo) => {
      const clave = Object.keys(claims).find((k) => k.toLowerCase().endsWith(sufijo));
      return clave ? claims[clave] : undefined;
    };

    return {
      id: buscar("/nameidentifier"),
      nombre: buscar("/name"),
      correo: buscar("/emailaddress"),
    };
  } catch {
    return null;
  }
}

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [token, setToken] = useState(() => localStorage.getItem("token"));

  const usuario = useMemo(() => (token ? decodeToken(token) : null), [token]);

  const login = async (nombreUsuario, contrasena) => {
    const { token: nuevoToken } = await loginRequest(nombreUsuario, contrasena);
    localStorage.setItem("token", nuevoToken);
    setToken(nuevoToken);
  };

  const logout = () => {
    localStorage.removeItem("token");
    setToken(null);
  };

  const value = { token, usuario, isAuthenticated: !!token, login, logout };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth debe usarse dentro de AuthProvider");
  return ctx;
}
