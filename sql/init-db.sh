#!/bin/bash
echo "⏳ Esperando que SQL Server esté listo..."
sleep 25

YA_EXISTE=$(/opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P "$SA_PASSWORD" -h -1 -W -No -C \
  -Q "SET NOCOUNT ON; IF DB_ID('$DB_NAME') IS NOT NULL AND OBJECT_ID('$DB_NAME.dbo.Usuarios','U') IS NOT NULL PRINT 'EXISTS'" \
  2>/dev/null | tr -d '[:space:]')

if [ "$YA_EXISTE" = "EXISTS" ]; then
  echo "ℹ️  La base de datos ya estaba inicializada, se omite la importación."
else
  /opt/mssql-tools18/bin/sqlcmd \
    -S localhost \
    -U sa \
    -P "$SA_PASSWORD" \
    -i /docker-entrypoint-initdb.d/01_init.sql \
    -No -C

  echo "✅ Base de datos importada correctamente"
fi