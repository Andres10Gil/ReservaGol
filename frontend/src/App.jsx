import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import ProtectedRoute from "./components/ProtectedRoute";
import Layout from "./components/Layout";
import PublicLayout from "./components/PublicLayout";
import Login from "./pages/Login";
import Registro from "./pages/Registro";
import Dashboard from "./pages/Dashboard";
import UsuariosPage from "./pages/UsuariosPage";
import RolesPage from "./pages/RolesPage";
import CanchasPage from "./pages/CanchasPage";
import ReservasPage from "./pages/ReservasPage";
import CanchasGaleria from "./pages/public/CanchasGaleria";
import CanchaDetalle from "./pages/public/CanchaDetalle";

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/registro" element={<Registro />} />

          <Route element={<PublicLayout />}>
            <Route path="/" element={<CanchasGaleria />} />
            <Route path="/canchas/:id" element={<CanchaDetalle />} />
          </Route>

          <Route path="/admin" element={<ProtectedRoute />}>
            <Route element={<Layout />}>
              <Route index element={<Dashboard />} />
              <Route path="usuarios" element={<UsuariosPage />} />
              <Route path="roles" element={<RolesPage />} />
              <Route path="canchas" element={<CanchasPage />} />
              <Route path="reservas" element={<ReservasPage />} />
            </Route>
          </Route>

          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  );
}
