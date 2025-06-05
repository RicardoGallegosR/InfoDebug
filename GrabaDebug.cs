using System;
using System.IO;

namespace InfoDebug {
    public class GrabaDebug : IDisposable {
        private bool disposed;
        internal static string NombreArchivo;

        public GrabaDebug() {
            disposed = false;
        }

        public void AbreDebug(string prefijoArchivo) {
            if (!string.IsNullOrEmpty(NombreArchivo))
                return;

            string logDir = @"C:\LogProcesos";
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            NombreArchivo = Path.Combine(logDir, $"{prefijoArchivo}{timestamp}.txt");
        }

        public void ProcesaDebug(string libreria, string metodo, string info) {
            try {
                if (string.IsNullOrEmpty(NombreArchivo))
                    return;

                string line = $"{DateTime.Now:HH:mm:ss:fff} | {libreria} | {metodo} | {info}{Environment.NewLine}";
                File.AppendAllText(NombreArchivo, line);
            } catch (IOException) {
                // Manejo básico, puedes agregar logs o relanzar si lo deseas
            } catch (Exception) {
                // Lo mismo aquí
            }
        }

        public void CierraDebug() {
            NombreArchivo = string.Empty;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                disposed = true;
            }
        }

        ~GrabaDebug() {
            Dispose(false);
        }
    }
}
