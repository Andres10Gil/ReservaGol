import CrudPage from "../components/CrudPage";
import { obtenerRoles, crearRol, actualizarRol, eliminarRol } from "../api/roles";

const columns = [
  { key: "nombre_rol", label: "Nombre" },
  { key: "descripcion", label: "Descripción" },
  { key: "nivel_acceso", label: "Nivel de acceso" },
  { key: "activo", label: "Activo", render: (item) => (item.activo ? "Sí" : "No") },
];

const fields = [
  { name: "nombre_rol", label: "Nombre", type: "text", required: true },
  { name: "descripcion", label: "Descripción", type: "text" },
  { name: "nivel_acceso", label: "Nivel de acceso", type: "number", required: true },
  { name: "activo", label: "Activo", type: "checkbox" },
];

export default function RolesPage() {
  return (
    <CrudPage
      title="Roles"
      idField="id_Roles"
      columns={columns}
      fields={fields}
      api={{ list: obtenerRoles, create: crearRol, update: actualizarRol, remove: eliminarRol }}
    />
  );
}
