import client from "./client";

const BASE = "/ReservaControladores";

export const obtenerReservas = () =>
  client.get(`${BASE}/ObtenerReservas`).then((r) => r.data);

export const obtenerReservaPorId = (id) =>
  client.get(`${BASE}/ObtenerReservaPorId/${id}`).then((r) => r.data);

export const obtenerReservasPorCancha = (idCancha) =>
  client.get(`${BASE}/ObtenerReservasPorCancha/${idCancha}`).then((r) => r.data);

export const crearReserva = (reserva) =>
  client.post(`${BASE}/CrearReserva`, reserva).then((r) => r.data);

export const actualizarReserva = (reserva) =>
  client.put(`${BASE}/ActualizarReserva`, reserva).then((r) => r.data);

export const eliminarReserva = (id) =>
  client.delete(`${BASE}/EliminarReserva/${id}`).then((r) => r.data);
