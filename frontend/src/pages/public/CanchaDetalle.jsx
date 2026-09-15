import { useEffect, useMemo, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { obtenerCanchaPorId } from "../../api/canchas";
import { obtenerReservasPorCancha, crearReserva } from "../../api/reservas";
import { useAuth } from "../../context/AuthContext";
import Calendar from "../../components/Calendar";

const IMAGEN_DEFECTO =
  "data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='800' height='320'%3E%3Crect width='800' height='320' fill='%23dcfce7'/%3E%3Ctext x='50%25' y='50%25' text-anchor='middle' fill='%2316a34a' font-family='sans-serif' font-size='28'%3ECancha%3C/text%3E%3C/svg%3E";

const HORA_APERTURA = 6;
const HORA_CIERRE = 23;

function pad(n) {
  return String(n).padStart(2, "0");
}

function generarSlots() {
  const slots = [];
  for (let h = HORA_APERTURA; h < HORA_CIERRE; h++) {
    slots.push({ inicio: `${pad(h)}:00`, fin: `${pad(h + 1)}:00` });
  }
  return slots;
}

function hoyISO() {
  const d = new Date();
  return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}`;
}

export default function CanchaDetalle() {
  const { id } = useParams();
  const { isAuthenticated, usuario } = useAuth();
  const navigate = useNavigate();

  const [cancha, setCancha] = useState(null);
  const [reservas, setReservas] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [fechaSeleccionada, setFechaSeleccionada] = useState(hoyISO());
  const [slotSeleccionado, setSlotSeleccionado] = useState(null);
  const [agendando, setAgendando] = useState(false);
  const [mensaje, setMensaje] = useState("");

  const cargarReservas = async () => {
    const data = await obtenerReservasPorCancha(id).catch(() => []);
    setReservas(data.filter((r) => r.estado !== "Cancelada"));
  };

  useEffect(() => {
    (async () => {
      try {
        const [datosCancha] = await Promise.all([obtenerCanchaPorId(id), cargarReservas()]);
        setCancha(datosCancha);
      } catch {
        setError("No se pudo cargar la información de la cancha.");
      } finally {
        setLoading(false);
      }
    })();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);

  const fechasConReserva = useMemo(
    () => new Set(reservas.map((r) => String(r.fecha_reserva).slice(0, 10))),
    [reservas]
  );

  const reservasDelDia = useMemo(
    () => reservas.filter((r) => String(r.fecha_reserva).slice(0, 10) === fechaSeleccionada),
    [reservas, fechaSeleccionada]
  );

  const slots = useMemo(() => {
    return generarSlots().map((slot) => {
      const ocupado = reservasDelDia.some((r) => {
        const inicio = String(r.hora_inicio).slice(0, 5);
        const fin = String(r.hora_fin).slice(0, 5);
        return slot.inicio < fin && slot.fin > inicio;
      });
      return { ...slot, ocupado };
    });
  }, [reservasDelDia]);

  const handleSeleccionarFecha = (fecha) => {
    setFechaSeleccionada(fecha);
    setSlotSeleccionado(null);
    setMensaje("");
  };

  const handleAgendar = async () => {
    if (!slotSeleccionado) return;
    setAgendando(true);
    setMensaje("");
    try {
      await crearReserva({
        id_Usuario: usuario.id,
        id_Cancha: id,
        fecha_reserva: fechaSeleccionada,
        hora_inicio: `${slotSeleccionado.inicio}:00`,
        hora_fin: `${slotSeleccionado.fin}:00`,
        estado: "Pendiente",
      });
      setMensaje("¡Reserva agendada correctamente!");
      setSlotSeleccionado(null);
      await cargarReservas();
    } catch {
      setMensaje("No se pudo agendar la reserva. Intenta con otro horario.");
    } finally {
      setAgendando(false);
    }
  };

  if (loading) return <p className="page">Cargando...</p>;
  if (error || !cancha) return <p className="page alert alert-error">{error || "Cancha no encontrada."}</p>;

  return (
    <div className="page">
      <Link to="/" className="back-link">
        ← Volver a canchas
      </Link>

      <div className="detail-hero">
        <img src={cancha.imagenUrl || IMAGEN_DEFECTO} alt={cancha.nombre} />
        <div>
          <h1>{cancha.nombre}</h1>
          <p>📍 {cancha.ubicacion}</p>
          <p>📐 Medidas: {cancha.dimenciones}</p>
          <p className="price">${cancha.precio_Hora} / hora</p>
        </div>
      </div>

      <div className="detail-grid">
        <div>
          <h2>Elige una fecha</h2>
          <Calendar selectedDate={fechaSeleccionada} onSelect={handleSeleccionarFecha} markedDates={fechasConReserva} />
        </div>

        <div>
          <h2>Horarios para {fechaSeleccionada}</h2>
          <div className="slots-grid">
            {slots.map((slot) => (
              <button
                type="button"
                key={slot.inicio}
                disabled={slot.ocupado}
                className={`slot-btn${slotSeleccionado?.inicio === slot.inicio ? " selected" : ""}${slot.ocupado ? " ocupado" : ""}`}
                onClick={() => setSlotSeleccionado(slot)}
              >
                {slot.inicio} - {slot.fin}
                {slot.ocupado && <span className="slot-label">Reservado</span>}
              </button>
            ))}
          </div>

          {mensaje && <p className={`alert ${mensaje.startsWith("¡") ? "alert-success" : "alert-error"}`}>{mensaje}</p>}

          {isAuthenticated ? (
            <button type="button" className="btn btn-primary btn-block" disabled={!slotSeleccionado || agendando} onClick={handleAgendar}>
              {agendando ? "Agendando..." : slotSeleccionado ? `Agendar ${slotSeleccionado.inicio} - ${slotSeleccionado.fin}` : "Elige un horario"}
            </button>
          ) : (
            <div className="auth-prompt">
              <p className="muted">Necesitas una cuenta para agendar.</p>
              <div className="form-actions">
                <button type="button" className="btn btn-secondary" onClick={() => navigate(`/login?next=/canchas/${id}`)}>
                  Ingresar
                </button>
                <button type="button" className="btn btn-primary" onClick={() => navigate(`/registro?next=/canchas/${id}`)}>
                  Crear cuenta
                </button>
              </div>
            </div>
          )}
        </div>
      </div>

      <div className="page">
        <h2>Reservas ya realizadas en {cancha.ubicacion}</h2>
        {reservasDelDia.length === 0 ? (
          <p className="muted">No hay reservas para el {fechaSeleccionada}.</p>
        ) : (
          <ul className="reservas-list">
            {reservasDelDia.map((r) => (
              <li key={r.id_Reserva}>
                {String(r.hora_inicio).slice(0, 5)} - {String(r.hora_fin).slice(0, 5)} · {cancha.nombre} ({cancha.ubicacion})
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}
