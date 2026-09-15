import client from "./client";

const BASE = "/RolesControladores";

export const obtenerRoles = () =>
  client.get(`${BASE}/ObtenerRoles`).then((r) => r.data);

export const obtenerRolPorId = (id) =>
  client.get(`${BASE}/ObtenerRolesPorId/${id}`).then((r) => r.data);

export const crearRol = (rol) =>
  client.post(`${BASE}/CrearRol`, rol).then((r) => r.data);

export const actualizarRol = (rol) =>
  client.put(`${BASE}/ActualizarRol`, rol).then((r) => r.data);

export const eliminarRol = (id) =>
  client.delete(`${BASE}/EliminarRol/${id}`).then((r) => r.data);
