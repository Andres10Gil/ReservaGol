import client from "./client";

const BASE = "/CanchaControlador";

export const obtenerCanchas = () =>
  client.get(`${BASE}/ObtenerCancha`).then((r) => r.data);

export const obtenerCanchaPorId = (id) =>
  client.get(`${BASE}/ObtenerCanchaPorId/${id}`).then((r) => r.data);

export const crearCancha = (cancha) =>
  client.post(`${BASE}/CrearCancha`, cancha).then((r) => r.data);

export const actualizarCancha = (cancha) =>
  client.put(`${BASE}/ActualizarCancha`, cancha).then((r) => r.data);

export const eliminarCancha = (id) =>
  client.delete(`${BASE}/EliminarCancha/${id}`).then((r) => r.data);

export const obtenerEstadisticasCancha = (id) =>
  client.get(`${BASE}/Estadisticas/${id}`).then((r) => r.data);
