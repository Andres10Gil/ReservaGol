import CrudPage from "../components/CrudPage";
import { obtenerReservas, crearReserva, actualizarReserva, eliminarReserva } from "../api/reservas";
import { obtenerUsuarios } from "../api/usuarios";
import { obtenerCanchas } from "../api/canchas";

const ESTADOS = [
  { value: "Pendiente", label: "Pendiente" },
  { value: "Confirmada", label: "Confirmada" },
  { value: "Cancelada", label: "Cancelada" },
  { value: "Completada", label: "Completada" },
];

const columns = [
  {
    key: "id_Usuario",
    label: "Usuario",
    render: (item, opts) => opts.usuarios?.find((u) => u.value === item.id_Usuario)?.label ?? item.id_Usuario,
  },
  {
    key: "id_Cancha",
    label: "Cancha",
    render: (item, opts) => opts.canchas?.find((c) => c.value === item.id_Cancha)?.label ?? item.id_Cancha,
  },
  { key: "fecha_reserva", label: "Fecha", render: (item) => String(item.fecha_reserva).slice(0, 10) },
  { key: "hora_inicio", label: "Hora inicio", render: (item) => String(item.hora_inicio).slice(0, 5) },
  { key: "hora_fin", label: "Hora fin", render: (item) => String(item.hora_fin).slice(0, 5) },
  { key: "estado", label: "Estado" },
];

const fields = [
  { name: "id_Usuario", label: "Usuario", type: "select", optionsSource: "usuarios", required: true },
  { name: "id_Cancha", label: "Cancha", type: "select", optionsSource: "canchas", required: true },
  { name: "fecha_reserva", label: "Fecha", type: "date", required: true },
  { name: "hora_inicio", label: "Hora inicio", type: "time", required: true },
  { name: "hora_fin", label: "Hora fin", type: "time", required: true },
  { name: "estado", label: "Estado", type: "select", options: ESTADOS, required: true },
];

export default function ReservasPage() {
  return (
    <CrudPage
      title="Reservas"
      idField="id_Reserva"
      columns={columns}
      fields={fields}
      api={{ list: obtenerReservas, create: crearReserva, update: actualizarReserva, remove: eliminarReserva }}
      loadExtraOptions={{
        usuarios: async () => {
          const usuarios = await obtenerUsuarios().catch(() => []);
          return usuarios.map((u) => ({ value: u.id_Usuario, label: `${u.nombre} (${u.correo})` }));
        },
        canchas: async () => {
          const canchas = await obtenerCanchas().catch(() => []);
          return canchas.map((c) => ({ value: c.id_Canchas, label: c.nombre }));
        },
      }}
    />
  );
}
