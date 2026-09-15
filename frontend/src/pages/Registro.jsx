import { useState } from "react";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import { registrarUsuario } from "../api/usuarios";
import { useAuth } from "../context/AuthContext";

export default function Registro() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const next = searchParams.get("next") || "/";

  const [form, setForm] = useState({ nombre: "", correo: "", telefono: "", contrasena: "" });
  const [error, setError] = useState("");
  const [cargando, setCargando] = useState(false);

  const handleChange = (campo) => (e) => setForm((prev) => ({ ...prev, [campo]: e.target.value }));

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setCargando(true);
    try {
      await registrarUsuario({
        Nombre: form.nombre,
        Correo: form.correo,
        Telefono: form.telefono,
        Contraseña: form.contrasena,
      });
      await login(form.correo, form.contrasena);
      navigate(next, { replace: true });
    } catch (err) {
      setError(err.response?.data || "No se pudo completar el registro.");
    } finally {
      setCargando(false);
    }
  };

  return (
    <div className="login-page">
      <form className="login-card" onSubmit={handleSubmit}>
        <h1>Crear cuenta</h1>
        <p className="muted">Regístrate para poder agendar tu cancha</p>

        <div className="form-field">
          <label htmlFor="nombre">Nombre</label>
          <input id="nombre" type="text" required value={form.nombre} onChange={handleChange("nombre")} autoFocus />
        </div>

        <div className="form-field">
          <label htmlFor="correo">Correo</label>
          <input id="correo" type="email" required value={form.correo} onChange={handleChange("correo")} />
        </div>

        <div className="form-field">
          <label htmlFor="telefono">Teléfono</label>
          <input
            id="telefono"
            type="tel"
            required
            pattern="[0-9]{7,15}"
            title="Solo números, entre 7 y 15 dígitos"
            value={form.telefono}
            onChange={handleChange("telefono")}
          />
        </div>

        <div className="form-field">
          <label htmlFor="contrasena">Contraseña</label>
          <input
            id="contrasena"
            type="password"
            required
            minLength={6}
            value={form.contrasena}
            onChange={handleChange("contrasena")}
          />
        </div>

        {error && <p className="alert alert-error">{String(error)}</p>}

        <button type="submit" className="btn btn-primary btn-block" disabled={cargando}>
          {cargando ? "Creando cuenta..." : "Crear cuenta"}
        </button>

        <p className="muted center">
          ¿Ya tienes cuenta? <Link to={`/login?next=${encodeURIComponent(next)}`}>Inicia sesión</Link>
        </p>
      </form>
    </div>
  );
}
