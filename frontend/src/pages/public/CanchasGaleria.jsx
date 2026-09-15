import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { obtenerCanchas } from "../../api/canchas";

const IMAGEN_DEFECTO =
  "data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='400' height='240'%3E%3Crect width='400' height='240' fill='%23dcfce7'/%3E%3Ctext x='50%25' y='50%25' text-anchor='middle' fill='%2316a34a' font-family='sans-serif' font-size='20'%3ECancha%3C/text%3E%3C/svg%3E";

export default function CanchasGaleria() {
  const [canchas, setCanchas] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    (async () => {
      try {
        const data = await obtenerCanchas();
        setCanchas(data);
      } catch (err) {
        if (err.response?.status !== 404) setError("No se pudieron cargar las canchas.");
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  return (
    <div className="page">
      <div className="hero">
        <h1>Reserva tu cancha</h1>
        <p className="muted">Consulta disponibilidad en tiempo real y agenda en segundos.</p>
      </div>

      {error && <p className="alert alert-error">{error}</p>}
      {loading ? (
        <p>Cargando canchas...</p>
      ) : canchas.length === 0 ? (
        <p className="muted">Todavía no hay canchas publicadas.</p>
      ) : (
        <div className="gallery-grid">
          {canchas.map((cancha) => (
            <Link to={`/canchas/${cancha.id_Canchas}`} key={cancha.id_Canchas} className="gallery-card">
              <img src={cancha.imagenUrl || IMAGEN_DEFECTO} alt={cancha.nombre} />
              <div className="gallery-card-body">
                <h3>{cancha.nombre}</h3>
                <p className="muted">📍 {cancha.ubicacion}</p>
                <p className="muted">📐 {cancha.dimenciones}</p>
                <p className="price">${cancha.precio_Hora} / hora</p>
              </div>
            </Link>
          ))}
        </div>
      )}
    </div>
  );
}
