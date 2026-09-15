import { Link } from "react-router-dom";

const ACCESOS = [
  { to: "/admin/usuarios", label: "Usuarios", desc: "Administrar cuentas de usuario y sus roles." },
  { to: "/admin/roles", label: "Roles", desc: "Definir roles y niveles de acceso." },
  { to: "/admin/canchas", label: "Canchas", desc: "Gestionar canchas disponibles y precios." },
  { to: "/admin/reservas", label: "Reservas", desc: "Ver y administrar las reservas realizadas." },
];

export default function Dashboard() {
  return (
    <div className="page">
      <h2>Panel principal</h2>
      <div className="dashboard-grid">
        {ACCESOS.map((item) => (
          <Link to={item.to} key={item.to} className="dashboard-card">
            <h3>{item.label}</h3>
            <p className="muted">{item.desc}</p>
          </Link>
        ))}
      </div>
    </div>
  );
}
