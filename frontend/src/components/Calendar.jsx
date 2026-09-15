import { useState } from "react";

const DIAS = ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb"];
const MESES = [
  "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
  "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre",
];

const pad = (n) => String(n).padStart(2, "0");
const toKey = (year, month, day) => `${year}-${pad(month + 1)}-${pad(day)}`;

function startOfDay(date) {
  const d = new Date(date);
  d.setHours(0, 0, 0, 0);
  return d;
}

export default function Calendar({ selectedDate, onSelect, markedDates }) {
  const hoy = startOfDay(new Date());
  const inicial = selectedDate ? new Date(`${selectedDate}T00:00:00`) : hoy;
  const [vista, setVista] = useState({ year: inicial.getFullYear(), month: inicial.getMonth() });

  const primerDiaSemana = new Date(vista.year, vista.month, 1).getDay();
  const diasEnMes = new Date(vista.year, vista.month + 1, 0).getDate();

  const celdas = [];
  for (let i = 0; i < primerDiaSemana; i++) celdas.push(null);
  for (let dia = 1; dia <= diasEnMes; dia++) celdas.push(dia);

  const cambiarMes = (delta) => {
    setVista(({ year, month }) => {
      const nuevo = new Date(year, month + delta, 1);
      return { year: nuevo.getFullYear(), month: nuevo.getMonth() };
    });
  };

  return (
    <div className="calendar">
      <div className="calendar-header">
        <button type="button" className="btn btn-secondary" onClick={() => cambiarMes(-1)}>
          ‹
        </button>
        <strong>
          {MESES[vista.month]} {vista.year}
        </strong>
        <button type="button" className="btn btn-secondary" onClick={() => cambiarMes(1)}>
          ›
        </button>
      </div>
      <div className="calendar-grid calendar-weekdays">
        {DIAS.map((d) => (
          <div key={d} className="calendar-weekday">
            {d}
          </div>
        ))}
      </div>
      <div className="calendar-grid">
        {celdas.map((dia, idx) => {
          if (dia === null) return <div key={`vacio-${idx}`} className="calendar-cell calendar-cell-empty" />;

          const key = toKey(vista.year, vista.month, dia);
          const fecha = startOfDay(new Date(vista.year, vista.month, dia));
          const esPasado = fecha < hoy;
          const esSeleccionado = key === selectedDate;
          const tieneReservas = markedDates?.has(key);

          return (
            <button
              type="button"
              key={key}
              disabled={esPasado}
              onClick={() => onSelect(key)}
              className={`calendar-cell${esSeleccionado ? " selected" : ""}${esPasado ? " disabled" : ""}`}
            >
              {dia}
              {tieneReservas && <span className="calendar-dot" />}
            </button>
          );
        })}
      </div>
    </div>
  );
}
