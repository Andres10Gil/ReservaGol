import client from "./client";

const BASE = "/UsuariosControlador";

export const obtenerUsuarios = () =>
  client.get(`${BASE}/ObtenerUsuarios`).then((r) => r.data);

export const obtenerUsuarioPorId = (id) =>
  client.get(`${BASE}/ObtenerUsuarioPorId/${id}`).then((r) => r.data);

export const crearUsuario = (usuario) =>
  client.post(`${BASE}/CrearUsuario`, usuario).then((r) => r.data);

export const registrarUsuario = (usuario) =>
  client.post(`${BASE}/Registro`, usuario).then((r) => r.data);

export const actualizarUsuario = (usuario) =>
  client.put(`${BASE}/ActualizarUsuario`, usuario).then((r) => r.data);

export const eliminarUsuario = (id) =>
  client.delete(`${BASE}/EliminarUsuario/${id}`).then((r) => r.data);
