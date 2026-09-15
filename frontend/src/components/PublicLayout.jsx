import { Link, Outlet, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export default function PublicLayout() {
  const { isAuthenticated, usuario, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/", { replace: true });
  };

  return (
    <div className="app-shell">
      <header className="app-header">
        <Link to="/" className="brand">
          ⚽ ReservaGol
        </Link>
        <nav className="app-nav">
          <Link to="/">Canchas</Link>
        </nav>
        <div className="app-user">
          {isAuthenticated ? (
            <>
              <span className="muted">{usuario?.nombre}</span>
              <Link to="/admin" className="btn btn-secondary">
                Panel admin
              </Link>
              <button type="button" className="btn btn-secondary" onClick={handleLogout}>
                Salir
              </button>
            </>
          ) : (
            <>
              <Link to="/login" className="btn btn-secondary">
                Ingresar
              </Link>
              <Link to="/registro" className="btn btn-primary">
                Crear cuenta
              </Link>
            </>
          )}
        </div>
      </header>
      <main className="app-content">
        <Outlet />
      </main>
    </div>
  );
}
