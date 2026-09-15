import client from "./client";

export const login = (nombreUsuario, contrasena) =>
  client
    .post("/AutControlador/Login", {
      NombreUsuario: nombreUsuario,
      Contraseña: contrasena,
    })
    .then((r) => r.data);
