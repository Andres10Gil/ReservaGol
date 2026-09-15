import CrudPage from "../components/CrudPage";
import { obtenerUsuarios, crearUsuario, actualizarUsuario, eliminarUsuario } from "../api/usuarios";
import { obtenerRoles } from "../api/roles";

const columns = [
  { key: "nombre", label: "Nombre" },
  { key: "correo", label: "Correo" },
  { key: "telefono", label: "Teléfono" },
  {
    key: "id_Roles",
    label: "Rol",
    render: (item, opts) => opts.roles?.find((r) => r.value === item.id_Roles)?.label ?? item.id_Roles,
  },
];

const fields = [
  { name: "nombre", label: "Nombre", type: "text", required: true },
  { name: "correo", label: "Correo", type: "text", required: true },
  {
    name: "telefono",
    label: "Teléfono",
    type: "tel",
    required: true,
    placeholder: "3001234567",
  },
  { name: "id_Roles", label: "Rol", type: "select", optionsSource: "roles", required: true },
  {
    name: "contraseña",
    label: "Contraseña",
    type: "password",
    required: true,
    hint: "Al editar, debes volver a escribir la contraseña (nueva o la actual).",
  },
  {
    name: "fecha_registro",
    label: "Fecha de registro",
    type: "date",
    required: true,
    default: () => new Date().toISOString().slice(0, 10),
  },
];

export default function UsuariosPage() {
  return (
    <CrudPage
      title="Usuarios"
      idField="id_Usuario"
      columns={columns}
      fields={fields}
      api={{ list: obtenerUsuarios, create: crearUsuario, update: actualizarUsuario, remove: eliminarUsuario }}
      loadExtraOptions={{
        roles: async () => {
          const roles = await obtenerRoles().catch(() => []);
          return roles.map((r) => ({ value: r.id_Roles, label: r.nombre_rol }));
        },
      }}
    />
  );
}
