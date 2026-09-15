import { Link, NavLink, Outlet, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

const LINKS = [
  { to: "/admin", label: "Inicio", end: true },
  { to: "/admin/usuarios", label: "Usuarios" },
  { to: "/admin/roles", label: "Roles" },
  { to: "/admin/canchas", label: "Canchas" },
  { to: "/admin/reservas", label: "Reservas" },
];

export default function Layout() {
  const { logout, usuario } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/", { replace: true });
  };

  return (
    <div className="app-shell">
      <header className="app-header">
        <span className="brand">ReservaGol · Admin</span>
        <nav className="app-nav">
          {LINKS.map((link) => (
            <NavLink key={link.to} to={link.to} end={link.end} className={({ isActive }) => (isActive ? "active" : "")}>
              {link.label}
            </NavLink>
          ))}
        </nav>
        <div className="app-user">
          {usuario?.correo && <span className="muted">{usuario.correo}</span>}
          <Link to="/" className="btn btn-secondary">
            Ver sitio
          </Link>
          <button type="button" className="btn btn-secondary" onClick={handleLogout}>
            Salir
          </button>
        </div>
      </header>
      <main className="app-content">
        <Outlet />
      </main>
    </div>
  );
}
