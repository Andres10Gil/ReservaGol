import { useEffect, useState } from "react";
import Modal from "./Modal";

function initialFormValues(fields, item) {
  const values = {};
  for (const field of fields) {
    let value = item ? item[field.name] : undefined;
    if (field.type === "time" && value) value = String(value).slice(0, 5);
    if (field.type === "date" && value) value = String(value).slice(0, 10);
    if (value === undefined || value === null) {
      if (!item && field.default) value = field.default();
      else value = field.type === "checkbox" ? false : "";
    }
    values[field.name] = value;
  }
  return values;
}

export default function CrudPage({
  title,
  idField,
  columns,
  fields,
  api,
  loadExtraOptions,
  transformBeforeSubmit,
}) {
  const [items, setItems] = useState([]);
  const [extraOptions, setExtraOptions] = useState({});
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [formError, setFormError] = useState("");
  const [editing, setEditing] = useState(null); // null = cerrado, {} = nuevo, item = editar
  const [formValues, setFormValues] = useState({});
  const [saving, setSaving] = useState(false);

  const cargarLista = async () => {
    setLoading(true);
    setError("");
    try {
      const data = await api.list();
      setItems(data);
    } catch (err) {
      if (err.response?.status === 404) {
        setItems([]);
      } else {
        setError("No se pudo cargar la información.");
      }
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    cargarLista();
    (async () => {
      if (!loadExtraOptions) return;
      const entries = await Promise.all(
        Object.entries(loadExtraOptions).map(async ([key, loader]) => [key, await loader()])
      );
      setExtraOptions(Object.fromEntries(entries));
    })();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const abrirCrear = () => {
    setFormError("");
    setFormValues(initialFormValues(fields, null));
    setEditing({});
  };

  const abrirEditar = (item) => {
    setFormError("");
    setFormValues(initialFormValues(fields, item));
    setEditing(item);
  };

  const cerrarModal = () => setEditing(null);

  const handleChange = (field, rawValue) => {
    setFormValues((prev) => ({ ...prev, [field]: rawValue }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSaving(true);
    setFormError("");
    try {
      let payload = { ...formValues };
      for (const field of fields) {
        if (field.type === "number") payload[field.name] = Number(payload[field.name] || 0);
        if (field.type === "checkbox") payload[field.name] = !!payload[field.name];
        if (field.type === "time" && payload[field.name]) payload[field.name] = `${payload[field.name]}:00`;
      }
      if (transformBeforeSubmit) payload = transformBeforeSubmit(payload, editing);

      const esNuevo = !editing[idField];
      if (esNuevo) {
        await api.create(payload);
      } else {
        payload[idField] = editing[idField];
        await api.update(payload);
      }
      cerrarModal();
      await cargarLista();
    } catch (err) {
      setFormError(err.response?.data || "No se pudo guardar. Verifica los datos.");
    } finally {
      setSaving(false);
    }
  };

  const handleEliminar = async (item) => {
    if (!window.confirm("¿Seguro que quieres eliminar este registro?")) return;
    try {
      await api.remove(item[idField]);
      await cargarLista();
    } catch {
      setError("No se pudo eliminar el registro.");
    }
  };

  return (
    <div className="page">
      <div className="page-header">
        <h2>{title}</h2>
        <button type="button" className="btn btn-primary" onClick={abrirCrear}>
          + Nuevo
        </button>
      </div>

      {error && <p className="alert alert-error">{error}</p>}

      {loading ? (
        <p>Cargando...</p>
      ) : items.length === 0 ? (
        <p className="muted">No hay registros todavía.</p>
      ) : (
        <div className="table-wrapper">
          <table>
            <thead>
              <tr>
                {columns.map((col) => (
                  <th key={col.key}>{col.label}</th>
                ))}
                <th aria-label="Acciones" />
              </tr>
            </thead>
            <tbody>
              {items.map((item) => (
                <tr key={item[idField]}>
                  {columns.map((col) => (
                    <td key={col.key}>{col.render ? col.render(item, extraOptions) : String(item[col.key] ?? "")}</td>
                  ))}
                  <td className="actions-cell">
                    <button type="button" className="btn btn-secondary" onClick={() => abrirEditar(item)}>
                      Editar
                    </button>
                    <button type="button" className="btn btn-danger" onClick={() => handleEliminar(item)}>
                      Eliminar
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {editing !== null && (
        <Modal title={editing[idField] ? `Editar ${title}` : `Nuevo ${title}`} onClose={cerrarModal}>
          <form onSubmit={handleSubmit} className="form">
            {fields.map((field) => (
              <div className="form-field" key={field.name}>
                <label htmlFor={field.name}>{field.label}</label>
                {field.type === "select" ? (
                  <select
                    id={field.name}
                    required={field.required}
                    value={formValues[field.name] ?? ""}
                    onChange={(e) => handleChange(field.name, e.target.value)}
                  >
                    <option value="">Selecciona...</option>
                    {(field.optionsSource ? extraOptions[field.optionsSource] : field.options)?.map((opt) => (
                      <option key={opt.value} value={opt.value}>
                        {opt.label}
                      </option>
                    ))}
                  </select>
                ) : field.type === "checkbox" ? (
                  <input
                    id={field.name}
                    type="checkbox"
                    checked={!!formValues[field.name]}
                    onChange={(e) => handleChange(field.name, e.target.checked)}
                  />
                ) : (
                  <input
                    id={field.name}
                    type={field.type === "password" ? "password" : field.type}
                    step={field.step}
                    placeholder={field.placeholder}
                    required={field.required}
                    value={formValues[field.name] ?? ""}
                    onChange={(e) => handleChange(field.name, e.target.value)}
                  />
                )}
                {field.hint && <small className="hint">{field.hint}</small>}
              </div>
            ))}

            {formError && <p className="alert alert-error">{String(formError)}</p>}

            <div className="form-actions">
              <button type="button" className="btn btn-secondary" onClick={cerrarModal}>
                Cancelar
              </button>
              <button type="submit" className="btn btn-primary" disabled={saving}>
                {saving ? "Guardando..." : "Guardar"}
              </button>
            </div>
          </form>
        </Modal>
      )}
    </div>
  );
}
