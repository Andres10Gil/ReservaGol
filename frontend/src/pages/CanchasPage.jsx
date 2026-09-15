import CrudPage from "../components/CrudPage";
import { obtenerCanchas, crearCancha, actualizarCancha, eliminarCancha } from "../api/canchas";

const columns = [
  { key: "nombre", label: "Nombre" },
  { key: "ubicacion", label: "Ubicación" },
  { key: "dimenciones", label: "Dimensiones" },
  { key: "precio_Hora", label: "Precio/hora", render: (item) => `$${item.precio_Hora}` },
];

const fields = [
  { name: "nombre", label: "Nombre", type: "text", required: true },
  { name: "ubicacion", label: "Ubicación", type: "text", required: true },
  { name: "dimenciones", label: "Dimensiones", type: "text" },
  { name: "precio_Hora", label: "Precio por hora", type: "number", step: "0.01", required: true },
  {
    name: "imagenUrl",
    label: "URL de la imagen",
    type: "text",
    placeholder: "https://...",
    hint: "Se muestra en la galería pública. Déjalo vacío para usar una imagen por defecto.",
  },
];

export default function CanchasPage() {
  return (
    <CrudPage
      title="Canchas"
      idField="id_Canchas"
      columns={columns}
      fields={fields}
      api={{ list: obtenerCanchas, create: crearCancha, update: actualizarCancha, remove: eliminarCancha }}
    />
  );
}
